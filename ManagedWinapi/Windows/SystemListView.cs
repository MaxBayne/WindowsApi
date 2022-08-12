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
    public class SystemListViewCore
    {
        /// <summary>
        /// Messages
        /// </summary>
        public struct Messages
        {
            internal static uint LVM_FIRST => 0x1000;
            internal static uint LVM_GETITEMRECT => (LVM_FIRST + 14);
            internal static uint LVM_SETITEMPOSITION => (LVM_FIRST + 15);
            internal static uint LVM_GETITEMPOSITION => (LVM_FIRST + 16);
            internal static uint LVM_GETITEMCOUNT => (LVM_FIRST + 4);
            internal static uint LVM_ENSUREVISIBLE => (LVM_FIRST + 19);
            internal static uint LVM_GETCOLUMN => (LVM_FIRST + 25);
            internal static uint LVM_SETITEMSTATE => (LVM_FIRST + 43);
            internal static uint LVM_GETSELECTEDCOUNT => (LVM_FIRST + 50);
            internal static uint LVM_GETITEMW => (LVM_FIRST + 75);
            internal static uint LVM_GETITEM => 0x1005;
            internal static uint LVM_GETITEMTEXTA => (LVM_FIRST + 45);
            internal static uint LVM_GETITEMTEXTW => (LVM_FIRST + 115);
            internal static uint LVM_GETNEXTITEM => (LVM_FIRST + 12);
            

            internal static uint HDM_FIRST => 0x1200;
            internal static uint HDM_GETITEMCOUNT = (HDM_FIRST + 0);

        }

        /// <summary>
        /// Flages used inside Mask
        /// </summary>
        public struct Flags
        {
            /// <summary>
            /// The pszText member is valid or must be set
            /// </summary>
            internal static uint LVIF_TEXT => 0x1;

            /// <summary>
            /// The iImage member is valid or must be set
            /// </summary>
            internal static uint LVIF_IMAGE => 0x2;

            /// <summary>
            /// The lParam member is valid or must be set
            /// </summary>
            internal static uint LVIF_PARAM => 0x4;

            /// <summary>
            /// The state member is valid or must be set
            /// </summary>
            internal static uint LVIF_STATE => 0x8;

            /// <summary>
            /// The iIndent member is valid or must be set
            /// </summary>
            internal static uint LVIF_INDENT => 0x10;

            internal static uint LVCF_FMT => 0x1;
            internal static uint LVCF_WIDTH => 0x2;
            internal static uint LVCF_TEXT => 0x4;
            internal static uint LVCF_SUBITEM => 0x8;

        }

        
        [Flags]
        public enum ListViewItemState
        {
            LVIS_FOCUSED = 0x0001,
            LVIS_SELECTED = 0x0002
        }

        /// <summary>
        /// Structure For ListView Column
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct LVCOLUMN
        {
            public uint mask;
            public int fmt;
            public int cx;
            public IntPtr pszText;
            public uint cchTextMax;
            public int iSubItem;
        }

        /// <summary>
        /// Structure For ListView Item(Row)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct LVITEM
        {
            /// <summary>
            /// Set Flags that Define  which Member of this Structure Contain Data To Be Sent or Requested
            /// </summary>
            public uint mask;

            /// <summary>
            /// Zero-based index of the item to which this structure refers
            /// </summary>
            public int iItem;

            /// <summary>
            /// One-based index of the subitem to which this structure refers, or zero if this structure refers to an item rather than a subitem
            /// </summary>
            public int iSubItem;

            /// <summary>
            /// Indicates the item's state, state image, and overlay image
            /// </summary>
            public uint state;


            public uint stateMask;

            /// <summary>
            /// pointer to a null-terminated string containing the item text
            /// </summary>
            public IntPtr pszText;

            /// <summary>
            /// Number of TCHARs in the buffer pointed to by pszText, including the terminating NULL
            /// </summary>
            public int cchTextMax;

            /// <summary>
            /// Index of the item's icon in the control's image list
            /// </summary>
            public int iImage;

            /// <summary>
            /// Value specific to the item. If you use the LVM_SORTITEMS message
            /// </summary>
            public IntPtr lParam;

            /// <summary>
            /// Number of image widths to indent the item
            /// </summary>
            public int iIndent;

            /// <summary>
            /// Identifier of the group that the item belongs to
            /// </summary>
            public int iGroupId;

            /// <summary>
            /// Number of data columns (subitems) to display for this item in tile view
            /// </summary>
            public uint cColumns;
        }


    }


    public class SystemListView: SystemListViewCore
    {
        /// <summary>
        /// Get a SystemListView reference from a SystemWindow (which is a list view)
        /// </summary>
        public static SystemListView FromSystemWindow(SystemWindow sw)
        {
            if (sw.SendGetMessage(Messages.LVM_GETITEMCOUNT) == 0 && sw.ClassName != "SysListView32") return null;
            return new SystemListView(sw);
        }

        readonly SystemWindow sw;

        private SystemListView(SystemWindow sw)
        {
            this.sw = sw;
        }

        /// <summary>
        /// The number of items (icons) in this list view.
        /// </summary>
        public int RowsCount
        {
            get
            {
                return sw.SendGetMessage(Messages.LVM_GETITEMCOUNT);
            }
        }

        /// <summary>
        /// The number of Columns in this list view.
        /// </summary>
        public int ColumnsCount
        {
            get
            {
                return sw.SendGetMessage(Messages.HDM_GETITEMCOUNT);
            }
        }

        /// <summary>
        /// The number of selected items (icons) in this list view.
        /// </summary>
        public int SelectedItemsCount
        {
            get
            {
                return sw.SendGetMessage(Messages.LVM_GETSELECTEDCOUNT);
            }
        }

        /// <summary>
        /// An item of this list view.
        /// </summary>
        public SystemListViewItem this[int index]
        {
            get
            {
                return this[index, 0];
            }
        }

        /// <summary>
        /// Ensures that a list-view item is either entirely or partially visible, scrolling the list-view control if necessary.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="entirelyVisible">if set to <c>true</c> the item will be entirely visible.</param>
        public void EnsureItemIsVisible(int rowIndex, bool entirelyVisible)
        {
            SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_ENSUREVISIBLE, new IntPtr(rowIndex), new IntPtr(entirelyVisible ? 1 : 0));
        }

        /// <summary>
        /// Sets the focused-state of the item.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="focused">if set to <c>true</c> the item will be focused.</param>
        public void SetItemFocusedState(int rowIndex, bool focused)
        {
            LVITEM lvi = new LVITEM();
            lvi.stateMask = (uint)(ListViewItemState.LVIS_FOCUSED);
            lvi.state = focused ? (uint)(ListViewItemState.LVIS_FOCUSED) : 0;
            ProcessMemoryChunk lc = ProcessMemoryChunk.AllocStruct(sw.Process, lvi);
            SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_SETITEMSTATE, new IntPtr(rowIndex), lc.Location);
            lc.Dispose();
        }

        /// <summary>
        /// Sets the selected-state of the item.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="selected">if set to <c>true</c> the item will be selected.</param>
        public void SetItemSelectedState(int rowIndex, bool selected)
        {
            LVITEM lvi = new LVITEM();
            lvi.stateMask = (uint)(ListViewItemState.LVIS_SELECTED);
            lvi.state = selected ? (uint)(ListViewItemState.LVIS_SELECTED) : 0;
            ProcessMemoryChunk lc = ProcessMemoryChunk.AllocStruct(sw.Process, lvi);
            SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_SETITEMSTATE, new IntPtr(rowIndex), lc.Location);
            lc.Dispose();
        }

        /// <summary>
        /// Clears the current selection by unselecting all selected rows.
        /// </summary>
        public void ClearSelection()
        {
            for (int rowIndex = 0; rowIndex < this.RowsCount; rowIndex++)
            {
                SetItemSelectedState(rowIndex, false);
            }
        }

        /// <summary>
        /// Sets the given row as single-selection and ensures that the row is visible.
        /// </summary>
        /// <param name="rowIndex">Index of the row ttaht should be highlighted.</param>
        public void HighlightRow(int rowIndex)
        {
            ClearSelection();
            SetItemSelectedState(rowIndex, true);
            SetItemFocusedState(rowIndex, true);
            EnsureItemIsVisible(rowIndex, false);
        }

        /// <summary>
        /// A subitem (a column value) of an item of this list view.
        /// </summary>
        public SystemListViewItem this[int index, int subIndex]
        {
            get
            {

                //Allocat Memory inside Target Memory to Store the List Item Data
                ProcessMemoryChunk memoryStore = ProcessMemoryChunk.Alloc(sw.Process, 256);

                
                LVITEM lvi = new LVITEM();

                lvi.mask = Flags.LVIF_IMAGE | Flags.LVIF_STATE | Flags.LVIF_TEXT;
                lvi.iItem = index;
                lvi.iSubItem = subIndex;
                lvi.state = 0;
                lvi.stateMask = 0xffffffff;
                lvi.pszText = memoryStore.Location;
                lvi.cchTextMax = 256;
                lvi.iIndent = 0;

                //Allocated Memory Inside Target Process Heap To Avoid Access Violation From Another Process
                ProcessMemoryChunk memory = ProcessMemoryChunk.AllocStruct(sw.Process, lvi);


                //Send Win32 Message (LVM_GETITEMW) To ListView Handel and Store the Message Data inside Memory Location
                SystemWindow.SendMessage(sw.HWnd, Messages.LVM_GETITEMW, IntPtr.Zero, memory.Location);

                //Read Result Data
                lvi = (LVITEM)memory.ReadToStructure(0, typeof(LVITEM));

                memory.Dispose();

                //if (lvi.pszText != memoryStore.Location)
                //{
                //    memoryStore.Dispose();
                //    memoryStore = new ProcessMemoryChunk(sw.Process, lvi.pszText, lvi.cchTextMax);
                //}
                byte[] tmp = memoryStore.Read();
                
                string title = Encoding.Unicode.GetString(tmp);

                

                if (title.IndexOf('\0') != -1) title = title.Substring(0, title.IndexOf('\0'));
                int image = lvi.iImage;
                uint state = lvi.state;
                memoryStore.Dispose();
                return new SystemListViewItem(sw, index, title, state, image);


                /*
                LVITEM lvi = new LVITEM();
                lvi.cchTextMax = 300;
                lvi.iItem = index;
                lvi.iSubItem = subIndex;
                lvi.stateMask = 0xffffffff;
                lvi.mask = LVIF_IMAGE | LVIF_STATE | LVIF_TEXT;
                ProcessMemoryChunk tc = ProcessMemoryChunk.Alloc(sw.Process, 301);
                lvi.pszText = tc.Location;
                ProcessMemoryChunk lc = ProcessMemoryChunk.AllocStruct(sw.Process, lvi);
                ApiHelper.FailIfZero(SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), SystemListView.LVM_GETITEM, IntPtr.Zero, lc.Location));
                lvi = (LVITEM)lc.ReadToStructure(0, typeof(LVITEM));
                lc.Dispose();
                if (lvi.pszText != tc.Location)
                {
                    tc.Dispose();
                    tc = new ProcessMemoryChunk(sw.Process, lvi.pszText, lvi.cchTextMax);
                }
                byte[] tmp = tc.Read();
                //string title = Encoding.Default.GetString(tmp);
                string title = Encoding.Default.GetString(tmp);

                if (title.IndexOf('\0') != -1) title = title.Substring(0, title.IndexOf('\0'));
                int image = lvi.iImage;
                uint state = lvi.state;
                tc.Dispose();
                return new SystemListViewItem(sw, index, title, state, image);
                */
            }
        }

        /// <summary>
        /// All columns of this list view, if it is in report view.
        /// </summary>
        public SystemListViewColumn[] Columns
        {
            get
            {
                List<SystemListViewColumn> result = new List<SystemListViewColumn>();
                LVCOLUMN lvc = new LVCOLUMN();
                lvc.cchTextMax = 300;
                lvc.mask = Flags.LVCF_FMT | Flags.LVCF_SUBITEM | Flags.LVCF_TEXT | Flags.LVCF_WIDTH;
                ProcessMemoryChunk tc = ProcessMemoryChunk.Alloc(sw.Process, 301);
                lvc.pszText = tc.Location;
                ProcessMemoryChunk lc = ProcessMemoryChunk.AllocStruct(sw.Process, lvc);
                for (int i = 0; ; i++)
                {
                    IntPtr ok = SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_GETCOLUMN, new IntPtr(i), lc.Location);
                    if (ok == IntPtr.Zero) break;
                    lvc = (LVCOLUMN)lc.ReadToStructure(0, typeof(LVCOLUMN));
                    byte[] tmp = tc.Read();
                    string title = Encoding.Default.GetString(tmp);
                    if (title.IndexOf('\0') != -1) title = title.Substring(0, title.IndexOf('\0'));
                    result.Add(new SystemListViewColumn(lvc.fmt, lvc.cx, lvc.iSubItem, title));
                }
                tc.Dispose();
                lc.Dispose();
                return result.ToArray();
            }
        }
    }

    

    /// <summary>
    /// An item of a list view.
    /// </summary>
    public class SystemListViewItem: SystemListViewCore
    {
        readonly string title;
        readonly uint state;
        readonly int image, index;
        readonly SystemWindow sw;

        internal SystemListViewItem(SystemWindow sw, int index, string title, uint state, int image)
        {
            this.sw = sw;
            this.index = index;
            this.title = title;
            this.state = state;
            this.image = image;
        }

        /// <summary>
        /// The title of this item
        /// </summary>
        public string Title { get { return title; } }

        /// <summary>
        /// The index of this item's image in the image list of this list view.
        /// </summary>
        public int Image { get { return image; } }

        /// <summary>
        /// The appearance of a selected item depends on whether it has the focus and also on the system colors used for selection.
        /// </summary>
        public bool IsSelected { get { return (state & (uint)ListViewItemState.LVIS_SELECTED) != 0; } }

        /// <summary>
        /// The item has the focus, so it is surrounded by a standard focus rectangle. Although more than one item may be selected, only one item can have the focus.
        /// </summary>
        public bool IsFocused { get { return (state & (uint)ListViewItemState.LVIS_FOCUSED) != 0; } }

        /// <summary>
        /// State bits of this item.
        /// </summary>
        [CLSCompliant(false)]
        public uint State { get { return state; } }

        /// <summary>
        /// Position of the upper left corner of this item.
        /// </summary>
        public Point Position
        {
            get
            {
                POINT pt = new POINT();
                ProcessMemoryChunk c = ProcessMemoryChunk.AllocStruct(sw.Process, pt);
                ApiHelper.FailIfZero(SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_GETITEMPOSITION, new IntPtr(index), c.Location));
                pt = (POINT)c.ReadToStructure(0, typeof(POINT));
                return new Point(pt.X, pt.Y);
            }
            set
            {
                SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_SETITEMPOSITION, new IntPtr(index), new IntPtr(value.X + (value.Y << 16)));
            }
        }

        /// <summary>
        /// Bounding rectangle of this item.
        /// </summary>
        public RECT Rectangle
        {
            get
            {
                RECT r = new RECT();
                ProcessMemoryChunk c = ProcessMemoryChunk.AllocStruct(sw.Process, r);
                SystemWindow.SendMessage(new HandleRef(sw, sw.HWnd), Messages.LVM_GETITEMRECT, new IntPtr(index), c.Location);
                r = (RECT)c.ReadToStructure(0, typeof(RECT));
                return r;
            }
        }
    }

    /// <summary>
    /// A column of a list view.
    /// </summary>
    public class SystemListViewColumn: SystemListViewCore
    {
        readonly int format;
        readonly int width;
        readonly int subIndex;
        readonly string title;

        internal SystemListViewColumn(int format, int width, int subIndex, string title)
        {
            this.format = format; this.width = width; this.subIndex = subIndex; this.title = title;
        }

        /// <summary>
        /// The format (like left justified) of this column.
        /// </summary>
        public int Format
        {
            get { return format; }
        }

        /// <summary>
        /// The width of this column.
        /// </summary>
        public int Width
        {
            get { return width; }
        }

        /// <summary>
        /// The subindex of the subitem displayed in this column. Note
        /// that the second column does not necessarily display the second
        /// subitem - especially when the columns can be reordered by the user.
        /// </summary>
        public int SubIndex
        {
            get { return subIndex; }
        }

        /// <summary>
        /// The title of this column.
        /// </summary>
        public string Title
        {
            get { return title; }
        }
    }
}
