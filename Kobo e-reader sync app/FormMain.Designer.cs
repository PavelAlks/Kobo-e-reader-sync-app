namespace KoboSync
{
    partial class MainForm
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
            this.ButtonSync = new System.Windows.Forms.Button();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogMain = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonFrom = new System.Windows.Forms.Button();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.buttonTo = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.comboBoxDriveList = new System.Windows.Forms.ComboBox();
            this.labelDriveLetter = new System.Windows.Forms.Label();
            this.checkBoxClearDiSource = new System.Windows.Forms.CheckBox();
            this.checkBoxLongSheldNames = new System.Windows.Forms.CheckBox();
            this.comboBoxSpace = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ButtonSync
            // 
            this.ButtonSync.Location = new System.Drawing.Point(5, 246);
            this.ButtonSync.Name = "ButtonSync";
            this.ButtonSync.Size = new System.Drawing.Size(89, 38);
            this.ButtonSync.TabIndex = 10;
            this.ButtonSync.Text = "Copy books";
            this.ButtonSync.UseVisualStyleBackColor = true;
            this.ButtonSync.Click += new System.EventHandler(this.buttonSync_Click);
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(127, 9);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.ReadOnly = true;
            this.textBoxFrom.Size = new System.Drawing.Size(288, 20);
            this.textBoxFrom.TabIndex = 1;
            // 
            // folderBrowserDialogMain
            // 
            this.folderBrowserDialogMain.Description = "Выберите исходную папку";
            // 
            // buttonFrom
            // 
            this.buttonFrom.Location = new System.Drawing.Point(421, 9);
            this.buttonFrom.Name = "buttonFrom";
            this.buttonFrom.Size = new System.Drawing.Size(28, 20);
            this.buttonFrom.TabIndex = 2;
            this.buttonFrom.Text = "...";
            this.buttonFrom.UseVisualStyleBackColor = true;
            this.buttonFrom.Click += new System.EventHandler(this.buttonFrom_Click);
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(127, 35);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.ReadOnly = true;
            this.textBoxTo.Size = new System.Drawing.Size(288, 20);
            this.textBoxTo.TabIndex = 3;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(2, 9);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(101, 13);
            this.labelFrom.TabIndex = 0;
            this.labelFrom.Text = "Select source folder";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(2, 37);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(120, 13);
            this.labelTo.TabIndex = 0;
            this.labelTo.Text = "Select destination folder";
            // 
            // buttonTo
            // 
            this.buttonTo.Location = new System.Drawing.Point(421, 35);
            this.buttonTo.Name = "buttonTo";
            this.buttonTo.Size = new System.Drawing.Size(28, 20);
            this.buttonTo.TabIndex = 4;
            this.buttonTo.Text = "...";
            this.buttonTo.UseVisualStyleBackColor = true;
            this.buttonTo.Click += new System.EventHandler(this.buttonTo_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.Color.Gainsboro;
            this.richTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxLog.Location = new System.Drawing.Point(5, 88);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(444, 152);
            this.richTextBoxLog.TabIndex = 6;
            this.richTextBoxLog.Text = "";
            // 
            // comboBoxDriveList
            // 
            this.comboBoxDriveList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDriveList.FormattingEnabled = true;
            this.comboBoxDriveList.Location = new System.Drawing.Point(127, 61);
            this.comboBoxDriveList.Name = "comboBoxDriveList";
            this.comboBoxDriveList.Size = new System.Drawing.Size(42, 21);
            this.comboBoxDriveList.TabIndex = 5;
            this.comboBoxDriveList.SelectedIndexChanged += new System.EventHandler(this.comboBoxDriveList_SelectedIndexChanged);
            this.comboBoxDriveList.Click += new System.EventHandler(this.comboBoxDriveList_Click);
            // 
            // labelDriveLetter
            // 
            this.labelDriveLetter.AutoSize = true;
            this.labelDriveLetter.Location = new System.Drawing.Point(2, 64);
            this.labelDriveLetter.Name = "labelDriveLetter";
            this.labelDriveLetter.Size = new System.Drawing.Size(101, 13);
            this.labelDriveLetter.TabIndex = 0;
            this.labelDriveLetter.Text = "Internal ebook drive";
            // 
            // checkBoxClearDiSource
            // 
            this.checkBoxClearDiSource.AutoSize = true;
            this.checkBoxClearDiSource.Location = new System.Drawing.Point(100, 250);
            this.checkBoxClearDiSource.Name = "checkBoxClearDiSource";
            this.checkBoxClearDiSource.Size = new System.Drawing.Size(192, 17);
            this.checkBoxClearDiSource.TabIndex = 7;
            this.checkBoxClearDiSource.Text = "Clear destination folder before copy";
            this.checkBoxClearDiSource.UseVisualStyleBackColor = true;
            // 
            // checkBoxLongSheldNames
            // 
            this.checkBoxLongSheldNames.AutoSize = true;
            this.checkBoxLongSheldNames.Location = new System.Drawing.Point(100, 267);
            this.checkBoxLongSheldNames.Name = "checkBoxLongSheldNames";
            this.checkBoxLongSheldNames.Size = new System.Drawing.Size(235, 17);
            this.checkBoxLongSheldNames.TabIndex = 8;
            this.checkBoxLongSheldNames.Text = "Shelf name include all upper shelf names via";
            this.checkBoxLongSheldNames.UseVisualStyleBackColor = true;
            // 
            // comboBoxSpace
            // 
            this.comboBoxSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpace.FormattingEnabled = true;
            this.comboBoxSpace.Items.AddRange(new object[] {
            "-",
            "|",
            ">",
            "/",
            " "});
            this.comboBoxSpace.Location = new System.Drawing.Point(329, 265);
            this.comboBoxSpace.MaxLength = 1;
            this.comboBoxSpace.Name = "comboBoxSpace";
            this.comboBoxSpace.Size = new System.Drawing.Size(32, 21);
            this.comboBoxSpace.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 287);
            this.Controls.Add(this.comboBoxSpace);
            this.Controls.Add(this.checkBoxLongSheldNames);
            this.Controls.Add(this.checkBoxClearDiSource);
            this.Controls.Add(this.labelDriveLetter);
            this.Controls.Add(this.comboBoxDriveList);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.buttonTo);
            this.Controls.Add(this.buttonFrom);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.ButtonSync);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Kobo ebook synchronize";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonSync;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMain;
        private System.Windows.Forms.Button buttonFrom;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Button buttonTo;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.ComboBox comboBoxDriveList;
        private System.Windows.Forms.Label labelDriveLetter;
        private System.Windows.Forms.CheckBox checkBoxClearDiSource;
        private System.Windows.Forms.CheckBox checkBoxLongSheldNames;
        private System.Windows.Forms.ComboBox comboBoxSpace;
    }
}

