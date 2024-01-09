namespace ClinicScheduler
{
    partial class F_ReportSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_ReportSelect));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewImageButtonColumn1 = new CustomControls.DataGridViewImageButtonColumn();
            this.dataGridViewImageButtonColumn2 = new CustomControls.DataGridViewImageButtonColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DGV_ReportTypes = new System.Windows.Forms.DataGridView();
            this.ReportID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ReportTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 389);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 47);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(357, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(213, 47);
            this.panel3.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(3, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(126, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 326);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(570, 63);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки файла";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TB_FileName);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(174, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(386, 30);
            this.panel4.TabIndex = 3;
            // 
            // TB_FileName
            // 
            this.TB_FileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_FileName.Location = new System.Drawing.Point(0, 0);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(386, 20);
            this.TB_FileName.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(10, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 30);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название файла с отчетом";
            // 
            // dataGridViewImageButtonColumn1
            // 
            this.dataGridViewImageButtonColumn1.DisabledImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageButtonColumn1.DisabledImage")));
            this.dataGridViewImageButtonColumn1.HeaderText = "";
            this.dataGridViewImageButtonColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageButtonColumn1.Image")));
            this.dataGridViewImageButtonColumn1.Name = "dataGridViewImageButtonColumn1";
            this.dataGridViewImageButtonColumn1.Width = 25;
            // 
            // dataGridViewImageButtonColumn2
            // 
            this.dataGridViewImageButtonColumn2.DisabledImage = global::ClinicScheduler.Properties.Resources.X;
            this.dataGridViewImageButtonColumn2.HeaderText = "";
            this.dataGridViewImageButtonColumn2.Image = global::ClinicScheduler.Properties.Resources.X;
            this.dataGridViewImageButtonColumn2.Name = "dataGridViewImageButtonColumn2";
            this.dataGridViewImageButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageButtonColumn2.Width = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DGV_ReportTypes);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 326);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Доступные типы отчетов";
            // 
            // DGV_ReportTypes
            // 
            this.DGV_ReportTypes.AllowUserToAddRows = false;
            this.DGV_ReportTypes.AllowUserToDeleteRows = false;
            this.DGV_ReportTypes.AllowUserToResizeRows = false;
            this.DGV_ReportTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_ReportTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReportID,
            this.ReportName});
            this.DGV_ReportTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_ReportTypes.Location = new System.Drawing.Point(3, 16);
            this.DGV_ReportTypes.MultiSelect = false;
            this.DGV_ReportTypes.Name = "DGV_ReportTypes";
            this.DGV_ReportTypes.ReadOnly = true;
            this.DGV_ReportTypes.Size = new System.Drawing.Size(564, 307);
            this.DGV_ReportTypes.TabIndex = 0;
            this.DGV_ReportTypes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_ReportTypes_CellDoubleClick);
            this.DGV_ReportTypes.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_ReportTypes_RowEnter);
            // 
            // ReportID
            // 
            this.ReportID.HeaderText = "ReportID";
            this.ReportID.Name = "ReportID";
            this.ReportID.ReadOnly = true;
            this.ReportID.Visible = false;
            this.ReportID.Width = 200;
            // 
            // ReportName
            // 
            this.ReportName.HeaderText = "Название отчета";
            this.ReportName.Name = "ReportName";
            this.ReportName.ReadOnly = true;
            this.ReportName.Width = 500;
            // 
            // F_ReportSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 436);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "F_ReportSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор типа отчета";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ReportTypes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox TB_FileName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private CustomControls.DataGridViewImageButtonColumn dataGridViewImageButtonColumn1;
        private CustomControls.DataGridViewImageButtonColumn dataGridViewImageButtonColumn2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DGV_ReportTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportName;
    }
}