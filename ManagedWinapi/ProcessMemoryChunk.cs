/*
 * ManagedWinapi - A collection of .NET components that wrap PInvoke calls to 
 * access native API by managed code. http://mwinapi.sourceforge.net/
 * Copyright (C) 2006 Michael Schierl
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; see the file COPYING. if not, visit
 * http://www.gnu.org/licenses/lgpl.html or write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ManagedWinapi
{
    /// <summary>
    /// A chunk in another processes memory. Mostly used to allocate buffers
    /// in another process for sending messages to its windows.
    /// </summary>
    public class ProcessMemoryChunk : IDisposable
    {
        private readonly Process process;
        private readonly IntPtr location;
        private readonly IntPtr hProcess;
        private readonly int size;
        private readonly bool free;

        #region Constructor

        
        public ProcessMemoryChunk()
        {

        }

        /// <summary>
        /// Create a new memory chunk that points to existing memory.
        /// Mostly used to read that memory.
        /// </summary>
        public ProcessMemoryChunk(Process process, IntPtr location, int size)
        {
            this.process = process;
            this.hProcess = OpenProcess(ProcessAccessFlags.VMOperation | ProcessAccessFlags.VMRead | ProcessAccessFlags.VMWrite, false, process.Id);
            ApiHelper.FailIfZero(hProcess);
            this.location = location;
            this.size = size;
            this.free = false;
        }

        private ProcessMemoryChunk(Process process, IntPtr hProcess, IntPtr location, int size, bool free)
        {
            this.process = process;
            this.hProcess = hProcess;
            this.location = location;
            this.size = size;
            this.free = free;
        }

        /// <summary>
        /// Free the memory in the other process, if it has been allocated before.
        /// </summary>
        public void Dispose()
        {
            if (free)
            {
                if (!VirtualFreeEx(hProcess, location, UIntPtr.Zero, MEM_RELEASE))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            CloseHandle(hProcess);
        }

        #endregion

        #region Fields


        /// <summary>
        /// The process this chunk refers to.
        /// </summary>
        public Process Process { get { return process; } }

        /// <summary>
        /// The location in memory (of the other process) this chunk refers to.
        /// </summary>
        public IntPtr Location { get { return location; } }

        /// <summary>
        /// The size of the chunk.
        /// </summary>
        public int Size { get { return size; } }

        #endregion
        
        #region Process

        /// <summary>
        /// Open Process and Return Process Handel
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static IntPtr OpenProcessById(int processId)
        {
            return OpenProcess(ProcessAccessFlags.VMOperation | ProcessAccessFlags.VMRead | ProcessAccessFlags.VMWrite, false, processId);
        }


        #endregion

        #region Memory Allocated

        /// <summary>
        /// Allocate a chunk in another process.
        /// </summary>
        public static ProcessMemoryChunk Alloc(Process process, int size)
        {
            IntPtr hProcess = OpenProcess(ProcessAccessFlags.VMOperation | ProcessAccessFlags.VMRead | ProcessAccessFlags.VMWrite, false, process.Id);
            IntPtr remotePointer = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)size,
                MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            ApiHelper.FailIfZero(remotePointer);
            return new ProcessMemoryChunk(process, hProcess, remotePointer, size, true);
        }

        /// <summary>
        /// Allocate a chunk in another process and unmarshal a struct
        /// there.
        /// </summary>
        public static ProcessMemoryChunk AllocStruct(Process process, object structure)
        {
            int size = Marshal.SizeOf(structure);
            ProcessMemoryChunk result = Alloc(process, size);
            result.WriteStructure(0, structure);
            return result;
        }



        #endregion

        #region Memory Read Data

        public T ReadDataFromProcessMemory<T>(int processId, int baseAddress) where T : class
        {
            //Open Process and Get Process Handel
            var processHandel = OpenProcessById(processId);

            //Read Data From Address of Process Memory
            var bufferSize = Marshal.SizeOf(typeof(T));
            var buffer = new byte[bufferSize];
            int readedBytes = 0;

            ReadProcessMemory(processHandel.ToInt32(), baseAddress, buffer, bufferSize, ref readedBytes);

            var readedData=Encoding.UTF8.GetString(buffer);

            object obj = readedData;

            return (T)obj;
        }

        public bool ReadDataFromProcessMemory(int processHandel,int baseAddress, byte[] buffer, int size, ref int numberOfBytesRead)
        {
            return ReadProcessMemory(processHandel, baseAddress, buffer, size,ref numberOfBytesRead);
        }

        /// <summary>
        /// Read this chunk.
        /// </summary>
        /// <returns></returns>
        public byte[] Read() { return Read(0, size); }

        /// <summary>
        /// Read a part of this chunk.
        /// </summary>
        public byte[] Read(int offset, int length)
        {
            if (offset + length > size) throw new ArgumentException("Exceeding chunk size");
            byte[] result = new byte[length];
            ReadProcessMemory(hProcess, new IntPtr(location.ToInt64() + offset), result, new UIntPtr((uint)length), IntPtr.Zero);
            return result;
        }

        /// <summary>
        /// Read this chunk to a pointer in this process.
        /// </summary>
        public void ReadToPtr(IntPtr ptr)
        {
            ReadToPtr(0, size, ptr);
        }

        /// <summary>
        /// Read a part of this chunk to a pointer in this process.
        /// </summary>
        public void ReadToPtr(int offset, int length, IntPtr ptr)
        {
            if (offset + length > size) throw new ArgumentException("Exceeding chunk size");
            ReadProcessMemory(hProcess, new IntPtr(location.ToInt64() + offset), ptr, new UIntPtr((uint)length), IntPtr.Zero);
        }

        /// <summary>
        /// Read a part of this chunk to a structure.
        /// </summary>
        public object ReadToStructure(int offset, Type structureType)
        {
            int size = Marshal.SizeOf(structureType);
            IntPtr localPtr = Marshal.AllocHGlobal(size);
            try
            {
                ReadToPtr(offset, size, localPtr);
                return Marshal.PtrToStructure(localPtr, structureType);
            }
            finally
            {
                Marshal.FreeHGlobal(localPtr);
            }
        }


        #endregion

        #region Memory Write Data

        /// <summary>
        /// Write a structure into this chunk.
        /// </summary>
        public void WriteStructure(int offset, object structure)
        {
            int size = Marshal.SizeOf(structure);
            IntPtr localPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, localPtr, false);
                Write(offset, localPtr, size);
            }
            finally
            {
                Marshal.FreeHGlobal(localPtr);
            }
        }

        /// <summary>
        /// Write into this chunk.
        /// </summary>
        public void Write(int offset, IntPtr ptr, int length)
        {
            if (offset < 0) throw new ArgumentException("Offset may not be negative", "offset");
            if (offset + length > size) throw new ArgumentException("Exceeding chunk size");
            WriteProcessMemory(hProcess, new IntPtr(location.ToInt64() + offset), ptr, new UIntPtr((uint)length), IntPtr.Zero);
        }

        public bool WriteDataIntoProcessMemory(int processHandel, int baseAddress, byte[] buffer, int size, ref int numberOfBytesWritten)
        {
            return WriteProcessMemory(processHandel, baseAddress, buffer, size, ref numberOfBytesWritten);
        }

        /// <summary>
        /// Write a byte array into this chunk.
        /// </summary>
        public void Write(int offset, byte[] ptr)
        {
            if (offset < 0) throw new ArgumentException("Offset may not be negative", "offset");
            if (offset + ptr.Length > size) throw new ArgumentException("Exceeding chunk size");
            WriteProcessMemory(hProcess, new IntPtr(location.ToInt64() + offset), ptr, new UIntPtr((uint)ptr.Length), IntPtr.Zero);
        }




        #endregion


        #region Windows API

        #region Process

        internal enum ProcessAccessFlags : int
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int CloseHandle(IntPtr hObject);

        #endregion

        #region Memory Allocated

        private static readonly uint MEM_COMMIT = 0x1000, MEM_RESERVE = 0x2000, MEM_RELEASE = 0x8000, PAGE_READWRITE = 0x04;

        /// <summary>
        /// Allocated Memory Size inside Process Memory , mean make it from Free to Reserved and ready for Read/Write to it 
        /// </summary>
        /// <param name="hProcess">Process Handel</param>
        /// <param name="lpAddress">Address of Memory to Allocated (Reserved)</param>
        /// <param name="dwSize">Size of Data</param>
        /// <param name="flAllocationType">Allocation Type</param>
        /// <param name="flProtect">is chunk is Protected or not</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        /// <summary>
        /// Free Reserved/Commited Chunk of Memory inside Memory Process
        /// </summary>
        /// <param name="hProcess">Process Handel</param>
        /// <param name="lpAddress">Address of Memory to Free</param>
        /// <param name="dwSize">Size of Data to Free</param>
        /// <param name="dwFreeType"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);


        #endregion

        #region Memory Data

        /// <summary>
        /// Read Data From Process Memory and Return Pointer of Bytes Array
        /// </summary>
        /// <param name="hProcess">Process Handel</param>
        /// <param name="lpBaseAddress">Memory Address To Read It</param>
        /// <param name="lpBuffer">Pointer of Bytes Array For Data To be Readed from Memory Address</param>
        /// <param name="nSize">This of Data To Read</param>
        /// <param name="lpNumberOfBytesRead"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        //IntPtr processHandel, IntPtr baseAddress, byte[] buffer, ref int numberOfBytesRead

        /// <summary>
        /// Write Data From Bytes Array To Process Memory
        /// </summary>
        /// <param name="hProcess">Process Handel</param>
        /// <param name="lpBaseAddress">Memory Address To Write In</param>
        /// <param name="lpBuffer">Bytes Array For Data To be Written inside Memory Address</param>
        /// <param name="nSize">This of Data To Written</param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);

        /// <summary>
        /// Write Data from Pointer To Process Memory
        /// </summary>
        /// <param name="hProcess">Process Handel</param>
        /// <param name="lpBaseAddress">Memory Address To Write In</param>
        /// <param name="lpBuffer">Pointer of Data To be Written inside Memory Address</param>
        /// <param name="nSize">This of Data To Written</param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress,byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        #endregion

        #endregion

    }

    
}
