namespace ScrappingDataApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_RefreshProcessesList = new System.Windows.Forms.Button();
            this.lv_Processes = new System.Windows.Forms.ListView();
            this.col_ProcessId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ProcessName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ProcessHandel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ProcessHandelCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_MainWindowHandle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_MainWindowTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chk_OnlyMainWindowHandel = new System.Windows.Forms.CheckBox();
            this.btn_ReadData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdo_HEX = new System.Windows.Forms.RadioButton();
            this.rdo_decimal = new System.Windows.Forms.RadioButton();
            this.rdo_Binary = new System.Windows.Forms.RadioButton();
            this.rdo_ASCII = new System.Windows.Forms.RadioButton();
            this.rdo_UTF8 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ReadSize = new System.Windows.Forms.TextBox();
            this.txt_ReadAddress = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_WriteData = new System.Windows.Forms.Button();
            this.rdo_HEX_Write = new System.Windows.Forms.RadioButton();
            this.txt_WriteData = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rdo_ASCII_Write = new System.Windows.Forms.RadioButton();
            this.rdo_UTF8_Write = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_WriteSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_WriteAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_RefreshProcessesList
            // 
            this.btn_RefreshProcessesList.Location = new System.Drawing.Point(9, 95);
            this.btn_RefreshProcessesList.Name = "btn_RefreshProcessesList";
            this.btn_RefreshProcessesList.Size = new System.Drawing.Size(191, 23);
            this.btn_RefreshProcessesList.TabIndex = 2;
            this.btn_RefreshProcessesList.Text = "Refresh Processes";
            this.btn_RefreshProcessesList.UseVisualStyleBackColor = true;
            this.btn_RefreshProcessesList.Click += new System.EventHandler(this.btn_RefreshProcessesList_Click);
            // 
            // lv_Processes
            // 
            this.lv_Processes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_ProcessId,
            this.col_ProcessName,
            this.col_ProcessHandel,
            this.col_ProcessHandelCount,
            this.col_MainWindowHandle,
            this.col_MainWindowTitle});
            this.lv_Processes.FullRowSelect = true;
            this.lv_Processes.GridLines = true;
            this.lv_Processes.HideSelection = false;
            this.lv_Processes.Location = new System.Drawing.Point(12, 12);
            this.lv_Processes.Name = "lv_Processes";
            this.lv_Processes.Size = new System.Drawing.Size(827, 219);
            this.lv_Processes.TabIndex = 3;
            this.lv_Processes.UseCompatibleStateImageBehavior = false;
            this.lv_Processes.View = System.Windows.Forms.View.Details;
            // 
            // col_ProcessId
            // 
            this.col_ProcessId.Text = "Process Id";
            this.col_ProcessId.Width = 65;
            // 
            // col_ProcessName
            // 
            this.col_ProcessName.Text = "ProcessName";
            this.col_ProcessName.Width = 200;
            // 
            // col_ProcessHandel
            // 
            this.col_ProcessHandel.Text = "ProcessHandel";
            this.col_ProcessHandel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_ProcessHandel.Width = 100;
            // 
            // col_ProcessHandelCount
            // 
            this.col_ProcessHandelCount.Text = "HandelsCount";
            this.col_ProcessHandelCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_ProcessHandelCount.Width = 90;
            // 
            // col_MainWindowHandle
            // 
            this.col_MainWindowHandle.Text = "MainWindowHandle";
            this.col_MainWindowHandle.Width = 110;
            // 
            // col_MainWindowTitle
            // 
            this.col_MainWindowTitle.Text = "MainWindowTitle";
            this.col_MainWindowTitle.Width = 300;
            // 
            // chk_OnlyMainWindowHandel
            // 
            this.chk_OnlyMainWindowHandel.AutoSize = true;
            this.chk_OnlyMainWindowHandel.Checked = true;
            this.chk_OnlyMainWindowHandel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_OnlyMainWindowHandel.Location = new System.Drawing.Point(9, 29);
            this.chk_OnlyMainWindowHandel.Name = "chk_OnlyMainWindowHandel";
            this.chk_OnlyMainWindowHandel.Size = new System.Drawing.Size(191, 17);
            this.chk_OnlyMainWindowHandel.TabIndex = 4;
            this.chk_OnlyMainWindowHandel.Text = "Display Only MainWindow Handels";
            this.chk_OnlyMainWindowHandel.UseVisualStyleBackColor = true;
            // 
            // btn_ReadData
            // 
            this.btn_ReadData.Location = new System.Drawing.Point(172, 17);
            this.btn_ReadData.Name = "btn_ReadData";
            this.btn_ReadData.Size = new System.Drawing.Size(58, 98);
            this.btn_ReadData.TabIndex = 5;
            this.btn_ReadData.Text = "Read Data From Memory Address";
            this.btn_ReadData.UseVisualStyleBackColor = true;
            this.btn_ReadData.Click += new System.EventHandler(this.btn_ReadData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdo_HEX);
            this.groupBox1.Controls.Add(this.rdo_decimal);
            this.groupBox1.Controls.Add(this.btn_ReadData);
            this.groupBox1.Controls.Add(this.rdo_Binary);
            this.groupBox1.Controls.Add(this.rdo_ASCII);
            this.groupBox1.Controls.Add(this.rdo_UTF8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_ReadSize);
            this.groupBox1.Controls.Add(this.txt_ReadAddress);
            this.groupBox1.Location = new System.Drawing.Point(226, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 124);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Read Memory";
            // 
            // rdo_HEX
            // 
            this.rdo_HEX.AutoSize = true;
            this.rdo_HEX.Checked = true;
            this.rdo_HEX.Location = new System.Drawing.Point(103, 16);
            this.rdo_HEX.Name = "rdo_HEX";
            this.rdo_HEX.Size = new System.Drawing.Size(44, 17);
            this.rdo_HEX.TabIndex = 8;
            this.rdo_HEX.TabStop = true;
            this.rdo_HEX.Text = "Hex";
            this.rdo_HEX.UseVisualStyleBackColor = true;
            // 
            // rdo_decimal
            // 
            this.rdo_decimal.AutoSize = true;
            this.rdo_decimal.Location = new System.Drawing.Point(103, 37);
            this.rdo_decimal.Name = "rdo_decimal";
            this.rdo_decimal.Size = new System.Drawing.Size(63, 17);
            this.rdo_decimal.TabIndex = 8;
            this.rdo_decimal.Text = "Decimal";
            this.rdo_decimal.UseVisualStyleBackColor = true;
            // 
            // rdo_Binary
            // 
            this.rdo_Binary.AutoSize = true;
            this.rdo_Binary.Location = new System.Drawing.Point(103, 59);
            this.rdo_Binary.Name = "rdo_Binary";
            this.rdo_Binary.Size = new System.Drawing.Size(54, 17);
            this.rdo_Binary.TabIndex = 8;
            this.rdo_Binary.Text = "Binary";
            this.rdo_Binary.UseVisualStyleBackColor = true;
            // 
            // rdo_ASCII
            // 
            this.rdo_ASCII.AutoSize = true;
            this.rdo_ASCII.Location = new System.Drawing.Point(103, 98);
            this.rdo_ASCII.Name = "rdo_ASCII";
            this.rdo_ASCII.Size = new System.Drawing.Size(52, 17);
            this.rdo_ASCII.TabIndex = 8;
            this.rdo_ASCII.Text = "ASCII";
            this.rdo_ASCII.UseVisualStyleBackColor = true;
            // 
            // rdo_UTF8
            // 
            this.rdo_UTF8.AutoSize = true;
            this.rdo_UTF8.Location = new System.Drawing.Point(103, 78);
            this.rdo_UTF8.Name = "rdo_UTF8";
            this.rdo_UTF8.Size = new System.Drawing.Size(52, 17);
            this.rdo_UTF8.TabIndex = 8;
            this.rdo_UTF8.Text = "UTF8";
            this.rdo_UTF8.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Read Format";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Data Size (Bytes)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Address (Hex)";
            // 
            // txt_ReadSize
            // 
            this.txt_ReadSize.Location = new System.Drawing.Point(9, 77);
            this.txt_ReadSize.Name = "txt_ReadSize";
            this.txt_ReadSize.Size = new System.Drawing.Size(81, 20);
            this.txt_ReadSize.TabIndex = 6;
            this.txt_ReadSize.Text = "4";
            // 
            // txt_ReadAddress
            // 
            this.txt_ReadAddress.Location = new System.Drawing.Point(9, 36);
            this.txt_ReadAddress.Name = "txt_ReadAddress";
            this.txt_ReadAddress.Size = new System.Drawing.Size(81, 20);
            this.txt_ReadAddress.TabIndex = 6;
            this.txt_ReadAddress.Text = "0x00403298";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_OnlyMainWindowHandel);
            this.groupBox2.Controls.Add(this.btn_RefreshProcessesList);
            this.groupBox2.Location = new System.Drawing.Point(12, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 124);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Process List";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_WriteData);
            this.groupBox3.Controls.Add(this.rdo_HEX_Write);
            this.groupBox3.Controls.Add(this.txt_WriteData);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.rdo_ASCII_Write);
            this.groupBox3.Controls.Add(this.rdo_UTF8_Write);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txt_WriteSize);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_WriteAddress);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(469, 241);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(370, 124);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Write Memory";
            // 
            // btn_WriteData
            // 
            this.btn_WriteData.Location = new System.Drawing.Point(306, 20);
            this.btn_WriteData.Name = "btn_WriteData";
            this.btn_WriteData.Size = new System.Drawing.Size(55, 95);
            this.btn_WriteData.TabIndex = 17;
            this.btn_WriteData.Text = "Write Data Into Memory Address";
            this.btn_WriteData.UseVisualStyleBackColor = true;
            this.btn_WriteData.Click += new System.EventHandler(this.btn_WriteData_Click);
            // 
            // rdo_HEX_Write
            // 
            this.rdo_HEX_Write.AutoSize = true;
            this.rdo_HEX_Write.Checked = true;
            this.rdo_HEX_Write.Location = new System.Drawing.Point(94, 36);
            this.rdo_HEX_Write.Name = "rdo_HEX_Write";
            this.rdo_HEX_Write.Size = new System.Drawing.Size(44, 17);
            this.rdo_HEX_Write.TabIndex = 12;
            this.rdo_HEX_Write.TabStop = true;
            this.rdo_HEX_Write.Text = "Hex";
            this.rdo_HEX_Write.UseVisualStyleBackColor = true;
            // 
            // txt_WriteData
            // 
            this.txt_WriteData.Location = new System.Drawing.Point(163, 32);
            this.txt_WriteData.Multiline = true;
            this.txt_WriteData.Name = "txt_WriteData";
            this.txt_WriteData.Size = new System.Drawing.Size(137, 83);
            this.txt_WriteData.TabIndex = 8;
            this.txt_WriteData.Text = "75 68 8B 35";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Data Write";
            // 
            // rdo_ASCII_Write
            // 
            this.rdo_ASCII_Write.AutoSize = true;
            this.rdo_ASCII_Write.Location = new System.Drawing.Point(94, 80);
            this.rdo_ASCII_Write.Name = "rdo_ASCII_Write";
            this.rdo_ASCII_Write.Size = new System.Drawing.Size(52, 17);
            this.rdo_ASCII_Write.TabIndex = 15;
            this.rdo_ASCII_Write.Text = "ASCII";
            this.rdo_ASCII_Write.UseVisualStyleBackColor = true;
            // 
            // rdo_UTF8_Write
            // 
            this.rdo_UTF8_Write.AutoSize = true;
            this.rdo_UTF8_Write.Location = new System.Drawing.Point(94, 57);
            this.rdo_UTF8_Write.Name = "rdo_UTF8_Write";
            this.rdo_UTF8_Write.Size = new System.Drawing.Size(52, 17);
            this.rdo_UTF8_Write.TabIndex = 16;
            this.rdo_UTF8_Write.Text = "UTF8";
            this.rdo_UTF8_Write.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Data Size (Bytes)";
            // 
            // txt_WriteSize
            // 
            this.txt_WriteSize.Enabled = false;
            this.txt_WriteSize.Location = new System.Drawing.Point(6, 75);
            this.txt_WriteSize.Name = "txt_WriteSize";
            this.txt_WriteSize.Size = new System.Drawing.Size(81, 20);
            this.txt_WriteSize.TabIndex = 10;
            this.txt_WriteSize.Text = "4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Address (Hex)";
            // 
            // txt_WriteAddress
            // 
            this.txt_WriteAddress.Location = new System.Drawing.Point(6, 34);
            this.txt_WriteAddress.Name = "txt_WriteAddress";
            this.txt_WriteAddress.Size = new System.Drawing.Size(73, 20);
            this.txt_WriteAddress.TabIndex = 6;
            this.txt_WriteAddress.Text = "0x00403298";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Write Format";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 371);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lv_Processes);
            this.Name = "Form1";
            this.Text = "Read/Write From Process Memory";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_RefreshProcessesList;
        private System.Windows.Forms.ListView lv_Processes;
        private System.Windows.Forms.ColumnHeader col_ProcessId;
        private System.Windows.Forms.ColumnHeader col_ProcessName;
        private System.Windows.Forms.ColumnHeader col_ProcessHandel;
        private System.Windows.Forms.ColumnHeader col_ProcessHandelCount;
        private System.Windows.Forms.ColumnHeader col_MainWindowHandle;
        private System.Windows.Forms.ColumnHeader col_MainWindowTitle;
        private System.Windows.Forms.CheckBox chk_OnlyMainWindowHandel;
        private System.Windows.Forms.Button btn_ReadData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ReadAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ReadSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_WriteData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_WriteAddress;
        private System.Windows.Forms.RadioButton rdo_UTF8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdo_HEX;
        private System.Windows.Forms.RadioButton rdo_decimal;
        private System.Windows.Forms.RadioButton rdo_Binary;
        private System.Windows.Forms.RadioButton rdo_ASCII;
        private System.Windows.Forms.Button btn_WriteData;
        private System.Windows.Forms.RadioButton rdo_HEX_Write;
        private System.Windows.Forms.RadioButton rdo_ASCII_Write;
        private System.Windows.Forms.RadioButton rdo_UTF8_Write;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_WriteSize;
        private System.Windows.Forms.Label label7;
    }
}

