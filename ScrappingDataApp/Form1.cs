using ManagedWinapi.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrappingDataApp
{
    public partial class Form1 : Form
    {
        static IntPtr hwndTreeView = (IntPtr)0x001A0A68;
        static IntPtr hwndListView = (IntPtr)0x004D0A62;
        static IntPtr hwndButtonNext = (IntPtr)0x001A0BA4;
        static IntPtr hwndProgressbar = (IntPtr)0x00300932;

        public Form1()
        {
            InitializeComponent();

            ScrapListView();
        }

        

        static void ScrapListView()
        {
            // Create a SystemWindow object from the HWND of the ListView
            SystemWindow lvWindow = new SystemWindow(hwndListView);

            // Create a ListView object from the SystemWindow object
            var lv = SystemListView.FromSystemWindow(lvWindow);

          
            Debug.WriteLine("===================================================================================================================");
            
            //Extract ListView Header Text ------------------------
            foreach (var column in lv.Columns)
            {
                if (column.SubIndex == 0)
                {
                    Debug.Write("# | ");
                }

                Debug.Write($"{column.Title}");

                if (column.SubIndex < lv.Columns.Length - 1)
                {
                    Debug.Write(" | ");
                }
            }
            
            Debug.WriteLine("");
            Debug.WriteLine("");

            int counter = 0;

            string rowContent = string.Empty;

            //Extract ListView Items Text -------------------------
            for (int rowIndex = 0; rowIndex < lv.RowsCount; rowIndex++)
            {

                //Row Number
                Debug.Write($"{++counter} | ");

                rowContent += counter;

                foreach (var column in lv.Columns)
                {
                    var item = lv[rowIndex, column.SubIndex];

                    Debug.Write(item.Title);

                    rowContent += item.Title;

                    if (column.SubIndex < lv.Columns.Length - 1)
                    {
                        Debug.Write(" | ");
                    }
                }


                Debug.WriteLine("");


            }
            
            Debug.WriteLine("===================================================================================================================");
        }

        static void PressNextButton()
        {

        }
    }
}
