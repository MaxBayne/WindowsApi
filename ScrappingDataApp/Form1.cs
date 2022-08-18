using ManagedWinapi;
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
            //btn_RefreshProcessesList_Click(null, null);
            //ScrapListView();
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

        private void btn_RefreshProcessesList_Click(object sender, EventArgs e)
        {
            lv_Processes.Items.Clear();

            var lstProcesses = Process.GetProcesses();

            foreach (var process in lstProcesses)
            {
                var listViewItem = new ListViewItem();

                listViewItem.Text = process.Id.ToString();
                listViewItem.SubItems.Add(process.ProcessName);

                try
                {
                    listViewItem.SubItems.Add(process.Handle.ToString("x"));
                }
                catch (Exception)
                {
                    //Ignored
                }
                
                listViewItem.SubItems.Add(process.HandleCount.ToString());
                listViewItem.SubItems.Add(process.MainWindowHandle.ToString("x"));
                listViewItem.SubItems.Add(process.MainWindowTitle);

                if (chk_OnlyMainWindowHandel.Checked)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        lv_Processes.Items.Add(listViewItem);
                    }
                }
                else
                {
                    lv_Processes.Items.Add(listViewItem);
                }
                
            }
            
        }

        private void btn_WriteData_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_Processes.SelectedItems.Count != 1) return;

                //Get Selected Process Id
                var processId = Convert.ToInt32(lv_Processes.SelectedItems[0].SubItems[0].Text);

                //Open Process and Get Process Handel
                var processHandel = ProcessMemoryChunk.OpenProcessById(processId);

                //Write Data Into Address of Process Memory
                var memory = new ProcessMemoryChunk();

                var numberOfBytesWrite = 0;
                var writeAddress = Convert.ToInt32(txt_WriteAddress.Text, 16);
                byte[] writeBuffer = { };

                if (rdo_ASCII_Write.Checked)
                {
                    writeBuffer = Encoding.ASCII.GetBytes(txt_WriteData.Text);
                }
                else if (rdo_UTF8_Write.Checked)
                {
                    writeBuffer = Encoding.UTF8.GetBytes(txt_WriteData.Text);
                }
                else if (rdo_HEX_Write.Checked)
                {
                    //Convert Hex string To Bytes
                    writeBuffer = txt_WriteData.Text
                                .Split(' ')            // Split into items 
                                .Select(item => Convert.ToByte(item, 16)) // Convert each item into byte
                                .ToArray();
                }
               
                memory.WriteDataIntoProcessMemory(processHandel.ToInt32(), writeAddress, writeBuffer, writeBuffer.Length, ref numberOfBytesWrite);


                MessageBox.Show(numberOfBytesWrite.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btn_ReadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_Processes.SelectedItems.Count != 1) return;

                //Get Selected Process Id
                var processId = Convert.ToInt32(lv_Processes.SelectedItems[0].SubItems[0].Text);

                //Open Process and Get Process Handel
                var processHandel = ProcessMemoryChunk.OpenProcessById(processId);

                //Read Data From Address of Process Memory
                var memory = new ProcessMemoryChunk();

                var readAddress = Convert.ToInt32(txt_ReadAddress.Text,16);
                var bufferSize = Convert.ToInt32(txt_ReadSize.Text);
                var readBuffer = new byte[bufferSize];
                var numberOfBytesRead = 0;

                memory.ReadDataFromProcessMemory(processHandel.ToInt32(), readAddress, readBuffer, readBuffer.Length, ref numberOfBytesRead);


                //Encoding Bytes Array To String

                string result=string.Empty;

                if (rdo_Binary.Checked)
                {
                    result = Encoding.Default.GetString(readBuffer);
                }
                else if (rdo_decimal.Checked)
                {
                    result = Convert.ToDecimal(BitConverter.ToDouble(readBuffer, 0)).ToString();

                }
                else if (rdo_HEX.Checked)
                {
                    result=BitConverter.ToString(readBuffer).Replace("-"," ");
                }
                else if (rdo_UTF8.Checked)
                {
                    result = Encoding.UTF8.GetString(readBuffer);
                }
                else if (rdo_ASCII.Checked)
                {
                    
                    result = Encoding.ASCII.GetString(readBuffer);
                }






                MessageBox.Show(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
