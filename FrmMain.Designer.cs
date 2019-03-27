namespace IliasSync
{
    partial class FrmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.lblSyncPath = new System.Windows.Forms.Label();
            this.txtSyncPath = new System.Windows.Forms.TextBox();
            this.cmdSync = new System.Windows.Forms.Button();
            this.gbPersonalDesktop = new System.Windows.Forms.GroupBox();
            this.dgvCourses = new System.Windows.Forms.DataGridView();
            this.clmnSync = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnSemester = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.cmdLogin = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.gbSettings.SuspendLayout();
            this.gbPersonalDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).BeginInit();
            this.gbLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.lblSyncPath);
            this.gbSettings.Controls.Add(this.txtSyncPath);
            this.gbSettings.Location = new System.Drawing.Point(257, 14);
            this.gbSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbSettings.Size = new System.Drawing.Size(584, 121);
            this.gbSettings.TabIndex = 6;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Einstellungen";
            // 
            // lblSyncPath
            // 
            this.lblSyncPath.AutoSize = true;
            this.lblSyncPath.Location = new System.Drawing.Point(5, 25);
            this.lblSyncPath.Name = "lblSyncPath";
            this.lblSyncPath.Size = new System.Drawing.Size(98, 17);
            this.lblSyncPath.TabIndex = 6;
            this.lblSyncPath.Text = "&Standardpfad:";
            // 
            // txtSyncPath
            // 
            this.txtSyncPath.Location = new System.Drawing.Point(109, 21);
            this.txtSyncPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSyncPath.Name = "txtSyncPath";
            this.txtSyncPath.Size = new System.Drawing.Size(453, 22);
            this.txtSyncPath.TabIndex = 5;
            this.txtSyncPath.TextChanged += new System.EventHandler(this.txtSyncPath_TextChanged);
            // 
            // cmdSync
            // 
            this.cmdSync.Enabled = false;
            this.cmdSync.Location = new System.Drawing.Point(15, 580);
            this.cmdSync.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdSync.Name = "cmdSync";
            this.cmdSync.Size = new System.Drawing.Size(1548, 30);
            this.cmdSync.TabIndex = 7;
            this.cmdSync.Text = "&Synchronisieren";
            this.cmdSync.UseVisualStyleBackColor = true;
            this.cmdSync.Click += new System.EventHandler(this.cmdSync_Click);
            // 
            // gbPersonalDesktop
            // 
            this.gbPersonalDesktop.Controls.Add(this.dgvCourses);
            this.gbPersonalDesktop.Location = new System.Drawing.Point(15, 139);
            this.gbPersonalDesktop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbPersonalDesktop.Name = "gbPersonalDesktop";
            this.gbPersonalDesktop.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbPersonalDesktop.Size = new System.Drawing.Size(1548, 436);
            this.gbPersonalDesktop.TabIndex = 5;
            this.gbPersonalDesktop.TabStop = false;
            this.gbPersonalDesktop.Text = "Meine Kurse";
            // 
            // dgvCourses
            // 
            this.dgvCourses.AllowUserToAddRows = false;
            this.dgvCourses.AllowUserToDeleteRows = false;
            this.dgvCourses.AllowUserToResizeRows = false;
            this.dgvCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCourses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnSync,
            this.clmnSemester,
            this.clmnId,
            this.clmnName,
            this.clmnPath});
            this.dgvCourses.Location = new System.Drawing.Point(12, 21);
            this.dgvCourses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvCourses.Name = "dgvCourses";
            this.dgvCourses.RowHeadersVisible = false;
            this.dgvCourses.RowTemplate.Height = 24;
            this.dgvCourses.Size = new System.Drawing.Size(1531, 400);
            this.dgvCourses.TabIndex = 5;
            this.dgvCourses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCourses_CellContentClick);
            this.dgvCourses.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCourses_CellValueChanged);
            this.dgvCourses.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvCourses_CurrentCellDirtyStateChanged);
            // 
            // clmnSync
            // 
            this.clmnSync.HeaderText = "Sync";
            this.clmnSync.Name = "clmnSync";
            this.clmnSync.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmnSync.Width = 60;
            // 
            // clmnSemester
            // 
            this.clmnSemester.HeaderText = "Semester";
            this.clmnSemester.Items.AddRange(new object[] {
            "0",
            "1. Semester",
            "2. Semester",
            "3. Semester",
            "4. Semester",
            "5. Semester",
            "6. Semester",
            "7. Semester",
            "Tutor"});
            this.clmnSemester.Name = "clmnSemester";
            this.clmnSemester.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmnId
            // 
            this.clmnId.HeaderText = "Id";
            this.clmnId.Name = "clmnId";
            // 
            // clmnName
            // 
            this.clmnName.HeaderText = "Name";
            this.clmnName.Name = "clmnName";
            this.clmnName.ReadOnly = true;
            this.clmnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnName.Width = 250;
            // 
            // clmnPath
            // 
            this.clmnPath.HeaderText = "Pfad";
            this.clmnPath.Name = "clmnPath";
            this.clmnPath.Width = 600;
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.cmdLogin);
            this.gbLogin.Controls.Add(this.lblPassword);
            this.gbLogin.Controls.Add(this.lblUsername);
            this.gbLogin.Controls.Add(this.txtPassword);
            this.gbLogin.Controls.Add(this.txtUsername);
            this.gbLogin.Location = new System.Drawing.Point(15, 14);
            this.gbLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbLogin.Size = new System.Drawing.Size(237, 121);
            this.gbLogin.TabIndex = 4;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Login";
            // 
            // cmdLogin
            // 
            this.cmdLogin.Location = new System.Drawing.Point(13, 78);
            this.cmdLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(215, 30);
            this.cmdLogin.TabIndex = 3;
            this.cmdLogin.Text = "&Login";
            this.cmdLogin.UseVisualStyleBackColor = true;
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(9, 52);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 17);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "&Passwort:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(9, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(77, 17);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "&Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(92, 49);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(135, 22);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(92, 21);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(135, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.Black;
            this.rtbLog.Location = new System.Drawing.Point(15, 615);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(1547, 243);
            this.rtbLog.TabIndex = 8;
            this.rtbLog.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1575, 874);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.cmdSync);
            this.Controls.Add(this.gbPersonalDesktop);
            this.Controls.Add(this.gbLogin);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IliasSyncClient (c) Alexander Zabel";
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbPersonalDesktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).EndInit();
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label lblSyncPath;
        private System.Windows.Forms.TextBox txtSyncPath;
        private System.Windows.Forms.Button cmdSync;
        private System.Windows.Forms.GroupBox gbPersonalDesktop;
        private System.Windows.Forms.DataGridView dgvCourses;
        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.Button cmdLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnSync;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmnSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnPath;
    }
}

