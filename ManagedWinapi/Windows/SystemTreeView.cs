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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ManagedWinapi.Windows
{
    /// <summary>
    /// Any tree view, including those from other applications.
    /// </summary>
    public class SystemTreeView
    {
        /// <summary>
        /// Get a SystemTreeView reference from a SystemWindow (which is a tree view)
        /// </summary>
        public static SystemTreeView FromSystemWindow(SystemWindow sw)
        {
            if (sw.SendGetMessage(TVM_GETCOUNT) == 0 && sw.ClassName != "SysTreeView32") return null;
            return new SystemTreeView(sw);
        }

        public static SystemTreeView FromSystemWindow(SystemWindow sw,bool virtualStringTree)
        {
            if (sw.SendGetMessage(TVM_GETCOUNT) == 0 && sw.ClassName != "TVirtualStringTree") return null;
            return new SystemTreeView(sw);
        }

        internal readonly SystemWindow sw;

        private SystemTreeView(SystemWindow sw)
        {
            this.sw = sw;
        }

        /// <summary>
        /// The number of items (icons) in this tree view.
        /// </summary>
        public int Count
        {
            get
            {
                return sw.SendGetMessage(TVM_GETCOUNT);
            }
        }

        /// <summary>
        /// The root items of this tree view.
        /// </summary>
        public SystemTreeViewItem[] Roots
        {
            get
            {
                return FindSubItems(sw, IntPtr.Zero);
            }
        }

        internal static SystemTreeViewItem[] FindSubItems(SystemWindow sw, IntPtr hParent)
        {
            List<SystemTreeViewItem> result = new List<SystemTreeViewItem>();
            IntPtr hChild;
            HandleRef hr = new HandleRef(sw, sw.HWnd);
            if (hParent == IntPtr.Zero)
            {
                hChild = SystemWindow.SendMessage(hr, TVM_GETNEXTITEM, new IntPtr(TVGN_ROOT), IntPtr.Zero);
            }
            else
            {
                hChild = SystemWindow.SendMessage(hr, TVM_GETNEXTITEM, new IntPtr(TVGN_CHILD), hParent);
            }
            while (hChild != IntPtr.Zero)
            {
                result.Add(new SystemTreeViewItem(sw, hChild));
                hChild = SystemWindow.SendMessage(hr, TVM_GETNEXTITEM, new IntPtr(TVGN_NEXT), hChild);
                
                var count = SystemWindow.SendMessage(sw.HWnd, TVM_GETCOUNT, IntPtr.Zero, IntPtr.Zero);

                
            }
            return result.ToArray();
        }


        #region PInvoke Declarations

        private static readonly uint TVM_GETCOUNT = 0x1100 + 5;
        private static readonly uint TVM_GETNEXTITEM = 0x1100 + 10;
        private static readonly uint TVGN_ROOT = 0;
        private static readonly uint TVGN_NEXT = 1;
        private static readonly uint TVGN_CHILD = 4;

        

        #endregion
    }

    /// <summary>
    /// An item of a tree view.
    /// </summary>
    public class SystemTreeViewItem
    {
        readonly IntPtr handle;
        readonly SystemWindow sw;

        internal SystemTreeViewItem(SystemWindow sw, IntPtr handle)
        {
            this.sw = sw;
            this.handle = handle;
        }

        /// <summary>
        /// The title of that item.
        /// </summary>
        public string Title
        {
            get
            {
                ProcessMemoryChunk tc = ProcessMemoryChunk.Alloc(sw.Process, 2001);
                LVITEM tvi = new LVITEM();
                tvi.hItem = handle;
                //tvi.mask = TVIF_TEXT;
                tvi.cchTextMax = 2000;
                tvi.pszText = tc.Location;
                ProcessMemoryChunk ic = ProcessMemoryChunk.AllocStruct(sw.Process, tvi);

                var count = SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();

                SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), LVM_GETITEMCOUNT, IntPtr.Zero, ic.Location);

                tvi = (LVITEM)ic.ReadToStructure(0, typeof(LVITEM));
                if (tvi.pszText != tc.Location) MessageBox.Show(tvi.pszText + " != " + tc.Location);
                string result = Encoding.Default.GetString(tc.Read());
                if (result.IndexOf('\0') != -1) result = result.Substring(0, result.IndexOf('\0'));
                ic.Dispose();
                tc.Dispose();
                return result;

                /*
                ProcessMemoryChunk tc = ProcessMemoryChunk.Alloc(sw.Process, 2001);
                TVITEM tvi = new TVITEM();
                tvi.hItem = handle;
                tvi.mask = TVIF_TEXT;
                tvi.cchTextMax = 2000;
                tvi.pszText = tc.Location;
                ProcessMemoryChunk ic = ProcessMemoryChunk.AllocStruct(sw.Process, tvi);
                SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), TVM_GETITEM, IntPtr.Zero, ic.Location);
                tvi = (TVITEM)ic.ReadToStructure(0, typeof(TVITEM));
                if (tvi.pszText != tc.Location) MessageBox.Show(tvi.pszText + " != " + tc.Location);
                string result = Encoding.Default.GetString(tc.Read());
                if (result.IndexOf('\0') != -1) result = result.Substring(0, result.IndexOf('\0'));
                ic.Dispose();
                tc.Dispose();
                return result;
                */
            }
        }

        /// <summary>
        /// All child items of that item.
        /// </summary>
        public SystemTreeViewItem[] Children
        {
            get { return SystemTreeView.FindSubItems(sw, handle); }
        }

        #region PInvoke Declarations

        private static readonly uint TVM_GETITEM = 0x1100 + 12;
        private static readonly uint TVIF_TEXT = 1;


        private static readonly uint WM_GETTEXTLENGTH = 0xE;
        private static readonly uint WM_GETTEXT = 0xD;

        private static readonly uint LVM_FIRST = 0x1000;
        private static readonly uint LVM_GETITEMCOUNT = (LVM_FIRST + 4);
        private static readonly uint LVM_GETITEMA = (LVM_FIRST + 5);
        private static readonly uint LVM_GETHEADER = (LVM_FIRST + 31);
        private static readonly uint LVM_GETITEMW = (LVM_FIRST + 75);
        private static readonly uint LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54);
        private static readonly uint LVM_SETITEMSTATE = (LVM_FIRST + 43);
        private static readonly uint LVM_GETITEMTEXTA = (LVM_FIRST + 45);
        private static readonly uint LVM_GETITEMTEXTW = (LVM_FIRST + 115);

        [StructLayout(LayoutKind.Sequential)]
        private struct TVITEM
        {
            public UInt32 mask;
            public IntPtr hItem;
            public UInt32 state;
            public UInt32 stateMask;
            public IntPtr pszText;
            public Int32 cchTextMax;
            public Int32 iImage;
            public Int32 iSelectedImage;
            public Int32 cChildren;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LVITEM
        {
            public IntPtr hItem;
            public uint mask;          //attributes of this data structure
            public int iItem;         // index of the item to which this structure refers
            public int iSubItem;      // index of the subitem to which this structure refers
            public int state;         // Specifies the current state of the item
            public int stateMask;     // Specifies the bits of the state member that are valid.
            public IntPtr pszText;    // Pointer to a null-terminated string that contains the item text 
            public int cchTextMax;    //Size of the buffer pointed to by the pszText member
            public int iImage;        //index of the list view item's icon 
            public IntPtr lParam;        //32-bit value to associate with item 
            public int iIndent;
        }

        #endregion
    }
}
