using System;

namespace ClinicScheduler
{
    partial class F_PropertiesSend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_PropertiesSend));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.L_Error = new System.Windows.Forms.Label();
            this.B_Error = new System.Windows.Forms.Button();
            this.B_TestSend = new System.Windows.Forms.Button();
            this.TB_SenderAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_FromPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_FromAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_AttachedFilesDirectory = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_MailBodyEncoding = new System.Windows.Forms.TextBox();
            this.L_Status = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_EnableSSL = new System.Windows.Forms.CheckBox();
            this.CB_IsBodyHTML = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DGV_Tasks = new System.Windows.Forms.DataGridView();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddressesTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reports = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunsOnShedule = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSB_AddTask = new System.Windows.Forms.ToolStripButton();
            this.TSB_EditTask = new System.Windows.Forms.ToolStripButton();
            this.TSB_RemoveTask = new System.Windows.Forms.ToolStripButton();
            this.TSB_SheduledSender = new System.Windows.Forms.ToolStripButton();
            this.TSB_ExecuteTask = new System.Windows.Forms.ToolStripButton();
            this.TB_SmtpServer = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Tasks)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_SmtpServer);
            this.groupBox1.Controls.Add(this.L_Error);
            this.groupBox1.Controls.Add(this.B_Error);
            this.groupBox1.Controls.Add(this.B_TestSend);
            this.groupBox1.Controls.Add(this.TB_SenderAddress);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TB_FromPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TB_FromAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TB_AttachedFilesDirectory);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TB_Port);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TB_MailBodyEncoding);
            this.groupBox1.Controls.Add(this.L_Status);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CB_EnableSSL);
            this.groupBox1.Controls.Add(this.CB_IsBodyHTML);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Основные настройки для отправки писем";
            // 
            // L_Error
            // 
            this.L_Error.AutoSize = true;
            this.L_Error.Location = new System.Drawing.Point(577, 130);
            this.L_Error.Name = "L_Error";
            this.L_Error.Size = new System.Drawing.Size(41, 13);
            this.L_Error.TabIndex = 5;
            this.L_Error.Text = "L_Error";
            this.L_Error.Visible = false;
            // 
            // B_Error
            // 
            this.B_Error.Location = new System.Drawing.Point(618, 125);
            this.B_Error.Name = "B_Error";
            this.B_Error.Size = new System.Drawing.Size(28, 23);
            this.B_Error.TabIndex = 4;
            this.B_Error.Text = "...";
            this.B_Error.UseVisualStyleBackColor = true;
            this.B_Error.Visible = false;
            this.B_Error.Click += new System.EventHandler(this.B_Error_Click);
            // 
            // B_TestSend
            // 
            this.B_TestSend.Location = new System.Drawing.Point(299, 125);
            this.B_TestSend.Name = "B_TestSend";
            this.B_TestSend.Size = new System.Drawing.Size(103, 23);
            this.B_TestSend.TabIndex = 10;
            this.B_TestSend.Text = "Тест отправки";
            this.B_TestSend.UseVisualStyleBackColor = true;
            this.B_TestSend.Click += new System.EventHandler(this.B_TestSend_Click);
            // 
            // TB_SenderAddress
            // 
            this.TB_SenderAddress.Location = new System.Drawing.Point(110, 103);
            this.TB_SenderAddress.Name = "TB_SenderAddress";
            this.TB_SenderAddress.Size = new System.Drawing.Size(151, 20);
            this.TB_SenderAddress.TabIndex = 4;
            this.TB_SenderAddress.Text = "author@mail.ru";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "От кого";
            // 
            // TB_FromPassword
            // 
            this.TB_FromPassword.Location = new System.Drawing.Point(110, 77);
            this.TB_FromPassword.Name = "TB_FromPassword";
            this.TB_FromPassword.PasswordChar = '♪';
            this.TB_FromPassword.Size = new System.Drawing.Size(151, 20);
            this.TB_FromPassword.TabIndex = 3;
            this.TB_FromPassword.Text = "some_password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Пароль";
            // 
            // TB_FromAddress
            // 
            this.TB_FromAddress.Location = new System.Drawing.Point(110, 51);
            this.TB_FromAddress.Name = "TB_FromAddress";
            this.TB_FromAddress.Size = new System.Drawing.Size(151, 20);
            this.TB_FromAddress.TabIndex = 2;
            this.TB_FromAddress.Text = "author@mail.ru";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Логин (e-mail)";
            // 
            // TB_AttachedFilesDirectory
            // 
            this.TB_AttachedFilesDirectory.Location = new System.Drawing.Point(496, 103);
            this.TB_AttachedFilesDirectory.Name = "TB_AttachedFilesDirectory";
            this.TB_AttachedFilesDirectory.Size = new System.Drawing.Size(151, 20);
            this.TB_AttachedFilesDirectory.TabIndex = 9;
            this.TB_AttachedFilesDirectory.Text = "Tmp\\";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(299, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(191, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Директория для временных файлов";
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(496, 77);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(151, 20);
            this.TB_Port.TabIndex = 8;
            this.TB_Port.Text = "587";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Порт";
            // 
            // TB_MailBodyEncoding
            // 
            this.TB_MailBodyEncoding.Location = new System.Drawing.Point(496, 51);
            this.TB_MailBodyEncoding.Name = "TB_MailBodyEncoding";
            this.TB_MailBodyEncoding.Size = new System.Drawing.Size(151, 20);
            this.TB_MailBodyEncoding.TabIndex = 7;
            this.TB_MailBodyEncoding.Text = "UTF-8";
            // 
            // L_Status
            // 
            this.L_Status.AutoSize = true;
            this.L_Status.Location = new System.Drawing.Point(421, 130);
            this.L_Status.Name = "L_Status";
            this.L_Status.Size = new System.Drawing.Size(44, 13);
            this.L_Status.TabIndex = 1;
            this.L_Status.Text = "Статус:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Кодировка письма";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сервер SMTP";
            // 
            // CB_EnableSSL
            // 
            this.CB_EnableSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CB_EnableSSL.Checked = true;
            this.CB_EnableSSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_EnableSSL.Location = new System.Drawing.Point(11, 129);
            this.CB_EnableSSL.Name = "CB_EnableSSL";
            this.CB_EnableSSL.Size = new System.Drawing.Size(250, 17);
            this.CB_EnableSSL.TabIndex = 0;
            this.CB_EnableSSL.TabStop = false;
            this.CB_EnableSSL.Text = "Использовать SSL";
            this.CB_EnableSSL.UseVisualStyleBackColor = true;
            // 
            // CB_IsBodyHTML
            // 
            this.CB_IsBodyHTML.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CB_IsBodyHTML.Enabled = false;
            this.CB_IsBodyHTML.Location = new System.Drawing.Point(299, 23);
            this.CB_IsBodyHTML.Name = "CB_IsBodyHTML";
            this.CB_IsBodyHTML.Size = new System.Drawing.Size(347, 24);
            this.CB_IsBodyHTML.TabIndex = 0;
            this.CB_IsBodyHTML.TabStop = false;
            this.CB_IsBodyHTML.Text = "Тело письма - HTML разметка";
            this.CB_IsBodyHTML.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 534);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 31);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.B_OK);
            this.panel2.Controls.Add(this.B_Cancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(444, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 31);
            this.panel2.TabIndex = 0;
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(12, 6);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(63, 23);
            this.B_OK.TabIndex = 12;
            this.B_OK.Text = "ОК";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(120, 6);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(63, 23);
            this.B_Cancel.TabIndex = 13;
            this.B_Cancel.Text = "Отмена";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DGV_Tasks);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(653, 377);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Задания на отправку писем";
            // 
            // DGV_Tasks
            // 
            this.DGV_Tasks.AllowUserToAddRows = false;
            this.DGV_Tasks.AllowUserToDeleteRows = false;
            this.DGV_Tasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Tasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskName,
            this.AddressesTo,
            this.Reports,
            this.RunsOnShedule});
            this.DGV_Tasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Tasks.Location = new System.Drawing.Point(3, 41);
            this.DGV_Tasks.MultiSelect = false;
            this.DGV_Tasks.Name = "DGV_Tasks";
            this.DGV_Tasks.ReadOnly = true;
            this.DGV_Tasks.Size = new System.Drawing.Size(647, 333);
            this.DGV_Tasks.TabIndex = 11;
            this.DGV_Tasks.TabStop = false;
            // 
            // TaskName
            // 
            this.TaskName.Frozen = true;
            this.TaskName.HeaderText = "Название задачи";
            this.TaskName.Name = "TaskName";
            this.TaskName.ReadOnly = true;
            this.TaskName.Width = 150;
            // 
            // AddressesTo
            // 
            this.AddressesTo.Frozen = true;
            this.AddressesTo.HeaderText = "Кому";
            this.AddressesTo.Name = "AddressesTo";
            this.AddressesTo.ReadOnly = true;
            this.AddressesTo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AddressesTo.Width = 150;
            // 
            // Reports
            // 
            this.Reports.Frozen = true;
            this.Reports.HeaderText = "Отчеты";
            this.Reports.Name = "Reports";
            this.Reports.ReadOnly = true;
            this.Reports.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Reports.Width = 200;
            // 
            // RunsOnShedule
            // 
            this.RunsOnShedule.Frozen = true;
            this.RunsOnShedule.HeaderText = "Выполняется по расписанию";
            this.RunsOnShedule.Name = "RunsOnShedule";
            this.RunsOnShedule.ReadOnly = true;
            this.RunsOnShedule.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RunsOnShedule.Width = 110;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_AddTask,
            this.TSB_EditTask,
            this.TSB_RemoveTask,
            this.TSB_SheduledSender,
            this.TSB_ExecuteTask});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(647, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TSB_AddTask
            // 
            this.TSB_AddTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_AddTask.Image = ((System.Drawing.Image)(resources.GetObject("TSB_AddTask.Image")));
            this.TSB_AddTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_AddTask.Name = "TSB_AddTask";
            this.TSB_AddTask.Size = new System.Drawing.Size(23, 22);
            this.TSB_AddTask.Text = "Добавить задачу";
            this.TSB_AddTask.Click += new System.EventHandler(this.TSB_AddTask_Click);
            // 
            // TSB_EditTask
            // 
            this.TSB_EditTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_EditTask.Image = ((System.Drawing.Image)(resources.GetObject("TSB_EditTask.Image")));
            this.TSB_EditTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_EditTask.Name = "TSB_EditTask";
            this.TSB_EditTask.Size = new System.Drawing.Size(23, 22);
            this.TSB_EditTask.Text = "Редактировать задачу";
            this.TSB_EditTask.Click += new System.EventHandler(this.TSB_EditTask_Click);
            // 
            // TSB_RemoveTask
            // 
            this.TSB_RemoveTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_RemoveTask.Image = ((System.Drawing.Image)(resources.GetObject("TSB_RemoveTask.Image")));
            this.TSB_RemoveTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_RemoveTask.Name = "TSB_RemoveTask";
            this.TSB_RemoveTask.Size = new System.Drawing.Size(23, 22);
            this.TSB_RemoveTask.Text = "Удалить задачу";
            this.TSB_RemoveTask.Click += new System.EventHandler(this.TSB_RemoveTask_Click);
            // 
            // TSB_SheduledSender
            // 
            this.TSB_SheduledSender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_SheduledSender.Image = ((System.Drawing.Image)(resources.GetObject("TSB_SheduledSender.Image")));
            this.TSB_SheduledSender.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_SheduledSender.Name = "TSB_SheduledSender";
            this.TSB_SheduledSender.Size = new System.Drawing.Size(23, 22);
            this.TSB_SheduledSender.Text = "Отправка по расписанию";
            this.TSB_SheduledSender.Click += new System.EventHandler(this.TSB_SheduledSender_Click);
            // 
            // TSB_ExecuteTask
            // 
            this.TSB_ExecuteTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_ExecuteTask.Image = ((System.Drawing.Image)(resources.GetObject("TSB_ExecuteTask.Image")));
            this.TSB_ExecuteTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_ExecuteTask.Name = "TSB_ExecuteTask";
            this.TSB_ExecuteTask.Size = new System.Drawing.Size(23, 22);
            this.TSB_ExecuteTask.Text = "Выполнить выбранное задание";
            this.TSB_ExecuteTask.Click += new System.EventHandler(this.TSB_ExecuteTask_Click);
            // 
            // TB_SmtpServer
            // 
            this.TB_SmtpServer.FormattingEnabled = true;
            this.TB_SmtpServer.Location = new System.Drawing.Point(110, 25);
            this.TB_SmtpServer.Name = "TB_SmtpServer";
            this.TB_SmtpServer.Size = new System.Drawing.Size(151, 21);
            this.TB_SmtpServer.TabIndex = 11;
            // 
            // F_PropertiesSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 568);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(675, 800);
            this.MinimumSize = new System.Drawing.Size(675, 577);
            this.Name = "F_PropertiesSend";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки отправки писем";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_PropertiesSend_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Tasks)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TB_SenderAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_FromPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_FromAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CB_IsBodyHTML;
        private System.Windows.Forms.TextBox TB_MailBodyEncoding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CB_EnableSSL;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_AttachedFilesDirectory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DGV_Tasks;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSB_AddTask;
        private System.Windows.Forms.ToolStripButton TSB_EditTask;
        private System.Windows.Forms.ToolStripButton TSB_RemoveTask;
        private System.Windows.Forms.Button B_TestSend;
        private System.Windows.Forms.Label L_Status;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_Error;
        private System.Windows.Forms.Label L_Error;
        private System.Windows.Forms.ToolStripButton TSB_SheduledSender;
        private System.Windows.Forms.ToolStripButton TSB_ExecuteTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressesTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reports;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RunsOnShedule;
        private System.Windows.Forms.ComboBox TB_SmtpServer;
    }
}