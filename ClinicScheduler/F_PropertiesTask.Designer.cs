namespace ClinicScheduler
{
    partial class F_PropertiesTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_PropertiesTask));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DGV_Reports = new CustomControls.DataGridViewEx();
            this.ReportType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportCaption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportPreview = new CustomControls.DataGridViewImageButtonColumn();
            this.ReportEdit = new CustomControls.DataGridViewImageButtonColumn();
            this.ReportDelete = new CustomControls.DataGridViewImageButtonColumn();
            this.ReportGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.TB_LetterMessage = new System.Windows.Forms.TextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.TB_CopyToAddresses = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.TB_ToAddresses = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.TB_LetterCaption = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TB_TaskName = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewImageButtonColumn1 = new CustomControls.DataGridViewImageButtonColumn();
            this.dataGridViewImageButtonColumn2 = new CustomControls.DataGridViewImageButtonColumn();
            this.dataGridViewImageButtonColumn3 = new CustomControls.DataGridViewImageButtonColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Reports)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 535);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 31);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.B_OK);
            this.panel2.Controls.Add(this.B_Cancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(708, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 31);
            this.panel2.TabIndex = 1;
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(12, 6);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(63, 23);
            this.B_OK.TabIndex = 0;
            this.B_OK.Text = "ОК";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(120, 6);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(63, 23);
            this.B_Cancel.TabIndex = 0;
            this.B_Cancel.Text = "Отмена";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(917, 532);
            this.panel3.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DGV_Reports);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(917, 231);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Отправляемые отчеты";
            // 
            // DGV_Reports
            // 
            this.DGV_Reports.AllowUserToDeleteRows = false;
            this.DGV_Reports.AllowUserToResizeColumns = false;
            this.DGV_Reports.AllowUserToResizeRows = false;
            this.DGV_Reports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Reports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReportType,
            this.ReportCaption,
            this.ReportFileName,
            this.ReportComment,
            this.ReportPreview,
            this.ReportEdit,
            this.ReportDelete,
            this.ReportGUID});
            this.DGV_Reports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Reports.Location = new System.Drawing.Point(3, 16);
            this.DGV_Reports.MultiSelect = false;
            this.DGV_Reports.Name = "DGV_Reports";
            this.DGV_Reports.Size = new System.Drawing.Size(911, 212);
            this.DGV_Reports.TabIndex = 3;
            this.DGV_Reports.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Reports_CellContentClick);
            this.DGV_Reports.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DGV_Reports_CellValidating);
            // 
            // ReportType
            // 
            this.ReportType.HeaderText = "ReportID";
            this.ReportType.Name = "ReportType";
            this.ReportType.Visible = false;
            // 
            // ReportCaption
            // 
            this.ReportCaption.HeaderText = "Отчет";
            this.ReportCaption.Name = "ReportCaption";
            this.ReportCaption.ReadOnly = true;
            this.ReportCaption.Width = 300;
            // 
            // ReportFileName
            // 
            this.ReportFileName.HeaderText = "Название файла";
            this.ReportFileName.Name = "ReportFileName";
            this.ReportFileName.Width = 175;
            // 
            // ReportComment
            // 
            this.ReportComment.HeaderText = "Комментарий";
            this.ReportComment.Name = "ReportComment";
            this.ReportComment.Width = 300;
            // 
            // ReportPreview
            // 
            this.ReportPreview.DisabledImage = global::ClinicScheduler.Properties.Resources.search_32;
            this.ReportPreview.HeaderText = "";
            this.ReportPreview.Image = global::ClinicScheduler.Properties.Resources.search_32;
            this.ReportPreview.Name = "ReportPreview";
            this.ReportPreview.Width = 25;
            // 
            // ReportEdit
            // 
            this.ReportEdit.DisabledImage = ((System.Drawing.Image)(resources.GetObject("ReportEdit.DisabledImage")));
            this.ReportEdit.HeaderText = "";
            this.ReportEdit.Image = ((System.Drawing.Image)(resources.GetObject("ReportEdit.Image")));
            this.ReportEdit.Name = "ReportEdit";
            this.ReportEdit.NullRowButtonEnabled = true;
            this.ReportEdit.Width = 25;
            // 
            // ReportDelete
            // 
            this.ReportDelete.DisabledImage = global::ClinicScheduler.Properties.Resources.X;
            this.ReportDelete.HeaderText = "";
            this.ReportDelete.Image = global::ClinicScheduler.Properties.Resources.X;
            this.ReportDelete.Name = "ReportDelete";
            this.ReportDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReportDelete.Width = 25;
            // 
            // ReportGUID
            // 
            this.ReportGUID.HeaderText = "ReportGUID";
            this.ReportGUID.Name = "ReportGUID";
            this.ReportGUID.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel7);
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(917, 253);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки письма";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.panel11);
            this.panel7.Controls.Add(this.panel10);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(125, 18);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(787, 230);
            this.panel7.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.TB_LetterMessage);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 125);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(787, 99);
            this.panel8.TabIndex = 12;
            // 
            // TB_LetterMessage
            // 
            this.TB_LetterMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_LetterMessage.Location = new System.Drawing.Point(0, 0);
            this.TB_LetterMessage.Multiline = true;
            this.TB_LetterMessage.Name = "TB_LetterMessage";
            this.TB_LetterMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_LetterMessage.Size = new System.Drawing.Size(787, 99);
            this.TB_LetterMessage.TabIndex = 3;
            this.TB_LetterMessage.Text = "Содержание письма";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.TB_CopyToAddresses);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 75);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel11.Size = new System.Drawing.Size(787, 50);
            this.panel11.TabIndex = 11;
            // 
            // TB_CopyToAddresses
            // 
            this.TB_CopyToAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_CopyToAddresses.Location = new System.Drawing.Point(0, 0);
            this.TB_CopyToAddresses.Multiline = true;
            this.TB_CopyToAddresses.Name = "TB_CopyToAddresses";
            this.TB_CopyToAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_CopyToAddresses.Size = new System.Drawing.Size(787, 45);
            this.TB_CopyToAddresses.TabIndex = 5;
            this.TB_CopyToAddresses.Text = "copy_receiver@mail.ru";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.TB_ToAddresses);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 25);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel10.Size = new System.Drawing.Size(787, 50);
            this.panel10.TabIndex = 10;
            // 
            // TB_ToAddresses
            // 
            this.TB_ToAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_ToAddresses.Location = new System.Drawing.Point(0, 0);
            this.TB_ToAddresses.Multiline = true;
            this.TB_ToAddresses.Name = "TB_ToAddresses";
            this.TB_ToAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_ToAddresses.Size = new System.Drawing.Size(787, 45);
            this.TB_ToAddresses.TabIndex = 4;
            this.TB_ToAddresses.Text = "receiver@mail.ru";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.TB_LetterCaption);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(787, 25);
            this.panel9.TabIndex = 8;
            // 
            // TB_LetterCaption
            // 
            this.TB_LetterCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_LetterCaption.Location = new System.Drawing.Point(0, 0);
            this.TB_LetterCaption.Name = "TB_LetterCaption";
            this.TB_LetterCaption.Size = new System.Drawing.Size(787, 20);
            this.TB_LetterCaption.TabIndex = 2;
            this.TB_LetterCaption.Text = "Текст заголовка письма";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(5, 18);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(120, 230);
            this.panel6.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Копия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Тема письма";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Содержание письма";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Кому:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(917, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Основные настройки";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.TB_TaskName);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(125, 18);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 25);
            this.panel5.TabIndex = 3;
            // 
            // TB_TaskName
            // 
            this.TB_TaskName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_TaskName.Location = new System.Drawing.Point(0, 0);
            this.TB_TaskName.Name = "TB_TaskName";
            this.TB_TaskName.Size = new System.Drawing.Size(787, 20);
            this.TB_TaskName.TabIndex = 1;
            this.TB_TaskName.Text = "Название_задачи";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(5, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 25);
            this.panel4.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название задачи";
            // 
            // dataGridViewImageButtonColumn1
            // 
            this.dataGridViewImageButtonColumn1.DisabledImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageButtonColumn1.DisabledImage")));
            this.dataGridViewImageButtonColumn1.HeaderText = "";
            this.dataGridViewImageButtonColumn1.Image = global::ClinicScheduler.Properties.Resources.Preview_icon_16;
            this.dataGridViewImageButtonColumn1.Name = "dataGridViewImageButtonColumn1";
            this.dataGridViewImageButtonColumn1.Width = 25;
            // 
            // dataGridViewImageButtonColumn2
            // 
            this.dataGridViewImageButtonColumn2.DisabledImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageButtonColumn2.DisabledImage")));
            this.dataGridViewImageButtonColumn2.HeaderText = "";
            this.dataGridViewImageButtonColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageButtonColumn2.Image")));
            this.dataGridViewImageButtonColumn2.Name = "dataGridViewImageButtonColumn2";
            this.dataGridViewImageButtonColumn2.NullRowButtonEnabled = true;
            this.dataGridViewImageButtonColumn2.Width = 25;
            // 
            // dataGridViewImageButtonColumn3
            // 
            this.dataGridViewImageButtonColumn3.DisabledImage = global::ClinicScheduler.Properties.Resources.X;
            this.dataGridViewImageButtonColumn3.HeaderText = "";
            this.dataGridViewImageButtonColumn3.Image = global::ClinicScheduler.Properties.Resources.X;
            this.dataGridViewImageButtonColumn3.Name = "dataGridViewImageButtonColumn3";
            this.dataGridViewImageButtonColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageButtonColumn3.Width = 25;
            // 
            // F_PropertiesTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 569);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "F_PropertiesTask";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки задачи";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_PropertiesTask_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Reports)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TB_TaskName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private CustomControls.DataGridViewEx DGV_Reports;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox TB_LetterMessage;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox TB_CopyToAddresses;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox TB_ToAddresses;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox TB_LetterCaption;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private CustomControls.DataGridViewImageButtonColumn dataGridViewImageButtonColumn1;
        private CustomControls.DataGridViewImageButtonColumn dataGridViewImageButtonColumn2;
        private CustomControls.DataGridViewImageButtonColumn dataGridViewImageButtonColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportCaption;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportComment;
        private CustomControls.DataGridViewImageButtonColumn ReportPreview;
        private CustomControls.DataGridViewImageButtonColumn ReportEdit;
        private CustomControls.DataGridViewImageButtonColumn ReportDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportGUID;
    }
}