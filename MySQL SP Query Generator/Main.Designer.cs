namespace MySQL_SP_Query_Generator
{
    partial class Main
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clbTables = new System.Windows.Forms.CheckedListBox();
            this.gbSelectTables = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbxDollarSign = new System.Windows.Forms.CheckBox();
            this.cbxRowsAffected = new System.Windows.Forms.CheckBox();
            this.cbxLasterInsertId = new System.Windows.Forms.CheckBox();
            this.cbxCount = new System.Windows.Forms.CheckBox();
            this.cbxDeleteFKUN = new System.Windows.Forms.CheckBox();
            this.cbxUpdateFKUN = new System.Windows.Forms.CheckBox();
            this.cbxSelectFKUN = new System.Windows.Forms.CheckBox();
            this.rbDrop = new System.Windows.Forms.RadioButton();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panelCreateAlter = new System.Windows.Forms.Panel();
            this.rbAlter = new System.Windows.Forms.RadioButton();
            this.rbCreate = new System.Windows.Forms.RadioButton();
            this.cbxDelete = new System.Windows.Forms.CheckBox();
            this.cbxUpdate = new System.Windows.Forms.CheckBox();
            this.cbxInsert = new System.Windows.Forms.CheckBox();
            this.cbxSelect = new System.Windows.Forms.CheckBox();
            this.rbStoredProc = new System.Windows.Forms.RadioButton();
            this.rbInline = new System.Windows.Forms.RadioButton();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbSelectTables.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.panelCreateAlter.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtDB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUserId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Information";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(778, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(609, 23);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(150, 22);
            this.txtDB.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(576, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "DB";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(494, 23);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(73, 22);
            this.txtPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(419, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(341, 23);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(72, 22);
            this.txtUserId.TabIndex = 5;
            this.txtUserId.Text = "root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "User ID";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(213, 23);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(50, 22);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "3306";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(67, 23);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 22);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // clbTables
            // 
            this.clbTables.CheckOnClick = true;
            this.clbTables.FormattingEnabled = true;
            this.clbTables.Location = new System.Drawing.Point(3, 52);
            this.clbTables.Name = "clbTables";
            this.clbTables.ScrollAlwaysVisible = true;
            this.clbTables.Size = new System.Drawing.Size(161, 395);
            this.clbTables.TabIndex = 2;
            // 
            // gbSelectTables
            // 
            this.gbSelectTables.Controls.Add(this.btnSelectAll);
            this.gbSelectTables.Controls.Add(this.clbTables);
            this.gbSelectTables.Enabled = false;
            this.gbSelectTables.Location = new System.Drawing.Point(12, 87);
            this.gbSelectTables.Name = "gbSelectTables";
            this.gbSelectTables.Size = new System.Drawing.Size(167, 455);
            this.gbSelectTables.TabIndex = 3;
            this.gbSelectTables.TabStop = false;
            this.gbSelectTables.Text = "Select Tables";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(30, 21);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(105, 23);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbxDollarSign);
            this.gbOptions.Controls.Add(this.cbxRowsAffected);
            this.gbOptions.Controls.Add(this.cbxLasterInsertId);
            this.gbOptions.Controls.Add(this.cbxCount);
            this.gbOptions.Controls.Add(this.cbxDeleteFKUN);
            this.gbOptions.Controls.Add(this.cbxUpdateFKUN);
            this.gbOptions.Controls.Add(this.cbxSelectFKUN);
            this.gbOptions.Controls.Add(this.rbDrop);
            this.gbOptions.Controls.Add(this.btnGenerate);
            this.gbOptions.Controls.Add(this.panelCreateAlter);
            this.gbOptions.Controls.Add(this.cbxDelete);
            this.gbOptions.Controls.Add(this.cbxUpdate);
            this.gbOptions.Controls.Add(this.cbxInsert);
            this.gbOptions.Controls.Add(this.cbxSelect);
            this.gbOptions.Controls.Add(this.rbStoredProc);
            this.gbOptions.Controls.Add(this.rbInline);
            this.gbOptions.Enabled = false;
            this.gbOptions.Location = new System.Drawing.Point(188, 87);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(689, 226);
            this.gbOptions.TabIndex = 4;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbxDollarSign
            // 
            this.cbxDollarSign.AutoSize = true;
            this.cbxDollarSign.Enabled = false;
            this.cbxDollarSign.Location = new System.Drawing.Point(295, 76);
            this.cbxDollarSign.Name = "cbxDollarSign";
            this.cbxDollarSign.Size = new System.Drawing.Size(99, 21);
            this.cbxDollarSign.TabIndex = 13;
            this.cbxDollarSign.Text = "Use $ Sign";
            this.cbxDollarSign.UseVisualStyleBackColor = true;
            // 
            // cbxRowsAffected
            // 
            this.cbxRowsAffected.AutoSize = true;
            this.cbxRowsAffected.Location = new System.Drawing.Point(508, 149);
            this.cbxRowsAffected.Name = "cbxRowsAffected";
            this.cbxRowsAffected.Size = new System.Drawing.Size(120, 21);
            this.cbxRowsAffected.TabIndex = 12;
            this.cbxRowsAffected.Text = "Rows Affected";
            this.cbxRowsAffected.UseVisualStyleBackColor = true;
            // 
            // cbxLasterInsertId
            // 
            this.cbxLasterInsertId.AutoSize = true;
            this.cbxLasterInsertId.Location = new System.Drawing.Point(508, 121);
            this.cbxLasterInsertId.Name = "cbxLasterInsertId";
            this.cbxLasterInsertId.Size = new System.Drawing.Size(111, 21);
            this.cbxLasterInsertId.TabIndex = 11;
            this.cbxLasterInsertId.Text = "Last Insert Id";
            this.cbxLasterInsertId.UseVisualStyleBackColor = true;
            // 
            // cbxCount
            // 
            this.cbxCount.AutoSize = true;
            this.cbxCount.Location = new System.Drawing.Point(508, 93);
            this.cbxCount.Name = "cbxCount";
            this.cbxCount.Size = new System.Drawing.Size(62, 21);
            this.cbxCount.TabIndex = 10;
            this.cbxCount.Text = "Total";
            this.cbxCount.UseVisualStyleBackColor = true;
            // 
            // cbxDeleteFKUN
            // 
            this.cbxDeleteFKUN.AutoSize = true;
            this.cbxDeleteFKUN.Location = new System.Drawing.Point(508, 65);
            this.cbxDeleteFKUN.Name = "cbxDeleteFKUN";
            this.cbxDeleteFKUN.Size = new System.Drawing.Size(141, 21);
            this.cbxDeleteFKUN.TabIndex = 9;
            this.cbxDeleteFKUN.Text = "Delete For FK/UN";
            this.cbxDeleteFKUN.UseVisualStyleBackColor = true;
            // 
            // cbxUpdateFKUN
            // 
            this.cbxUpdateFKUN.AutoSize = true;
            this.cbxUpdateFKUN.Location = new System.Drawing.Point(508, 39);
            this.cbxUpdateFKUN.Name = "cbxUpdateFKUN";
            this.cbxUpdateFKUN.Size = new System.Drawing.Size(146, 21);
            this.cbxUpdateFKUN.TabIndex = 8;
            this.cbxUpdateFKUN.Text = "Update For FK/UN";
            this.cbxUpdateFKUN.UseVisualStyleBackColor = true;
            // 
            // cbxSelectFKUN
            // 
            this.cbxSelectFKUN.AutoSize = true;
            this.cbxSelectFKUN.Location = new System.Drawing.Point(508, 15);
            this.cbxSelectFKUN.Name = "cbxSelectFKUN";
            this.cbxSelectFKUN.Size = new System.Drawing.Size(139, 21);
            this.cbxSelectFKUN.TabIndex = 7;
            this.cbxSelectFKUN.Text = "Select For FK/UN";
            this.cbxSelectFKUN.UseVisualStyleBackColor = true;
            // 
            // rbDrop
            // 
            this.rbDrop.AutoSize = true;
            this.rbDrop.Location = new System.Drawing.Point(165, 14);
            this.rbDrop.Name = "rbDrop";
            this.rbDrop.Size = new System.Drawing.Size(111, 21);
            this.rbDrop.TabIndex = 2;
            this.rbDrop.TabStop = true;
            this.rbDrop.Text = "Drop All SP\'s";
            this.rbDrop.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(583, 197);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(94, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // panelCreateAlter
            // 
            this.panelCreateAlter.Controls.Add(this.rbAlter);
            this.panelCreateAlter.Controls.Add(this.rbCreate);
            this.panelCreateAlter.Enabled = false;
            this.panelCreateAlter.Location = new System.Drawing.Point(290, 10);
            this.panelCreateAlter.Name = "panelCreateAlter";
            this.panelCreateAlter.Size = new System.Drawing.Size(107, 60);
            this.panelCreateAlter.TabIndex = 6;
            // 
            // rbAlter
            // 
            this.rbAlter.AutoSize = true;
            this.rbAlter.Location = new System.Drawing.Point(5, 28);
            this.rbAlter.Name = "rbAlter";
            this.rbAlter.Size = new System.Drawing.Size(58, 21);
            this.rbAlter.TabIndex = 1;
            this.rbAlter.TabStop = true;
            this.rbAlter.Text = "Alter";
            this.rbAlter.UseVisualStyleBackColor = true;
            // 
            // rbCreate
            // 
            this.rbCreate.AutoSize = true;
            this.rbCreate.Location = new System.Drawing.Point(5, 6);
            this.rbCreate.Name = "rbCreate";
            this.rbCreate.Size = new System.Drawing.Size(71, 21);
            this.rbCreate.TabIndex = 0;
            this.rbCreate.TabStop = true;
            this.rbCreate.Text = "Create";
            this.rbCreate.UseVisualStyleBackColor = true;
            // 
            // cbxDelete
            // 
            this.cbxDelete.AutoSize = true;
            this.cbxDelete.Location = new System.Drawing.Point(418, 96);
            this.cbxDelete.Name = "cbxDelete";
            this.cbxDelete.Size = new System.Drawing.Size(71, 21);
            this.cbxDelete.TabIndex = 5;
            this.cbxDelete.Text = "Delete";
            this.cbxDelete.UseVisualStyleBackColor = true;
            // 
            // cbxUpdate
            // 
            this.cbxUpdate.AutoSize = true;
            this.cbxUpdate.Location = new System.Drawing.Point(418, 69);
            this.cbxUpdate.Name = "cbxUpdate";
            this.cbxUpdate.Size = new System.Drawing.Size(76, 21);
            this.cbxUpdate.TabIndex = 4;
            this.cbxUpdate.Text = "Update";
            this.cbxUpdate.UseVisualStyleBackColor = true;
            // 
            // cbxInsert
            // 
            this.cbxInsert.AutoSize = true;
            this.cbxInsert.Location = new System.Drawing.Point(418, 43);
            this.cbxInsert.Name = "cbxInsert";
            this.cbxInsert.Size = new System.Drawing.Size(65, 21);
            this.cbxInsert.TabIndex = 3;
            this.cbxInsert.Text = "Insert";
            this.cbxInsert.UseVisualStyleBackColor = true;
            // 
            // cbxSelect
            // 
            this.cbxSelect.AutoSize = true;
            this.cbxSelect.Location = new System.Drawing.Point(418, 17);
            this.cbxSelect.Name = "cbxSelect";
            this.cbxSelect.Size = new System.Drawing.Size(69, 21);
            this.cbxSelect.TabIndex = 2;
            this.cbxSelect.Text = "Select";
            this.cbxSelect.UseVisualStyleBackColor = true;
            // 
            // rbStoredProc
            // 
            this.rbStoredProc.AutoSize = true;
            this.rbStoredProc.Location = new System.Drawing.Point(24, 43);
            this.rbStoredProc.Name = "rbStoredProc";
            this.rbStoredProc.Size = new System.Drawing.Size(141, 21);
            this.rbStoredProc.TabIndex = 1;
            this.rbStoredProc.TabStop = true;
            this.rbStoredProc.Text = "Stored Procedure";
            this.rbStoredProc.UseVisualStyleBackColor = true;
            this.rbStoredProc.CheckedChanged += new System.EventHandler(this.rbStoredProc_CheckedChanged);
            // 
            // rbInline
            // 
            this.rbInline.AutoSize = true;
            this.rbInline.Location = new System.Drawing.Point(24, 18);
            this.rbInline.Name = "rbInline";
            this.rbInline.Size = new System.Drawing.Size(105, 21);
            this.rbInline.TabIndex = 0;
            this.rbInline.TabStop = true;
            this.rbInline.Text = "Inline Query";
            this.rbInline.UseVisualStyleBackColor = true;
            this.rbInline.CheckedChanged += new System.EventHandler(this.rbInline_CheckedChanged);
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Enabled = false;
            this.gbResult.Location = new System.Drawing.Point(188, 319);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(692, 226);
            this.gbResult.TabIndex = 5;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.White;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(3, 18);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(686, 205);
            this.txtResult.TabIndex = 0;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 560);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.gbSelectTables);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MySQL Inline Queries & SP Generator - Version: 1.1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbSelectTables.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.panelCreateAlter.ResumeLayout(false);
            this.panelCreateAlter.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckedListBox clbTables;
        private System.Windows.Forms.GroupBox gbSelectTables;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.RadioButton rbStoredProc;
        private System.Windows.Forms.RadioButton rbInline;
        private System.Windows.Forms.CheckBox cbxDelete;
        private System.Windows.Forms.CheckBox cbxUpdate;
        private System.Windows.Forms.CheckBox cbxInsert;
        private System.Windows.Forms.CheckBox cbxSelect;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Panel panelCreateAlter;
        private System.Windows.Forms.RadioButton rbAlter;
        private System.Windows.Forms.RadioButton rbCreate;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RadioButton rbDrop;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox cbxDeleteFKUN;
        private System.Windows.Forms.CheckBox cbxUpdateFKUN;
        private System.Windows.Forms.CheckBox cbxSelectFKUN;
        private System.Windows.Forms.CheckBox cbxCount;
        private System.Windows.Forms.CheckBox cbxRowsAffected;
        private System.Windows.Forms.CheckBox cbxLasterInsertId;
        private System.Windows.Forms.CheckBox cbxDollarSign;
    }
}

