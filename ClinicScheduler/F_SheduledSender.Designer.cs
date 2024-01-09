namespace ClinicScheduler
{
    partial class F_SheduledSender
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
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_Monday = new System.Windows.Forms.CheckBox();
            this.chb_Tuesday = new System.Windows.Forms.CheckBox();
            this.chb_Wednesday = new System.Windows.Forms.CheckBox();
            this.chb_Thursday = new System.Windows.Forms.CheckBox();
            this.chb_Friday = new System.Windows.Forms.CheckBox();
            this.chb_Saturday = new System.Windows.Forms.CheckBox();
            this.chb_Sunday = new System.Windows.Forms.CheckBox();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.B_Delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(68, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(61, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Повторять каждую неделю в ";
            // 
            // chb_Monday
            // 
            this.chb_Monday.AutoSize = true;
            this.chb_Monday.Location = new System.Drawing.Point(25, 98);
            this.chb_Monday.Name = "chb_Monday";
            this.chb_Monday.Size = new System.Drawing.Size(94, 17);
            this.chb_Monday.TabIndex = 3;
            this.chb_Monday.Text = "Понедельник";
            this.chb_Monday.UseVisualStyleBackColor = true;
            // 
            // chb_Tuesday
            // 
            this.chb_Tuesday.AutoSize = true;
            this.chb_Tuesday.Location = new System.Drawing.Point(133, 98);
            this.chb_Tuesday.Name = "chb_Tuesday";
            this.chb_Tuesday.Size = new System.Drawing.Size(68, 17);
            this.chb_Tuesday.TabIndex = 4;
            this.chb_Tuesday.Text = "Вторник";
            this.chb_Tuesday.UseVisualStyleBackColor = true;
            this.chb_Tuesday.CheckedChanged += new System.EventHandler(this.chb_Tuesday_CheckedChanged);
            // 
            // chb_Wednesday
            // 
            this.chb_Wednesday.AutoSize = true;
            this.chb_Wednesday.Location = new System.Drawing.Point(224, 98);
            this.chb_Wednesday.Name = "chb_Wednesday";
            this.chb_Wednesday.Size = new System.Drawing.Size(57, 17);
            this.chb_Wednesday.TabIndex = 5;
            this.chb_Wednesday.Text = "Среда";
            this.chb_Wednesday.UseVisualStyleBackColor = true;
            // 
            // chb_Thursday
            // 
            this.chb_Thursday.AutoSize = true;
            this.chb_Thursday.Location = new System.Drawing.Point(307, 98);
            this.chb_Thursday.Name = "chb_Thursday";
            this.chb_Thursday.Size = new System.Drawing.Size(68, 17);
            this.chb_Thursday.TabIndex = 6;
            this.chb_Thursday.Text = "Четверг";
            this.chb_Thursday.UseVisualStyleBackColor = true;
            // 
            // chb_Friday
            // 
            this.chb_Friday.AutoSize = true;
            this.chb_Friday.Location = new System.Drawing.Point(25, 134);
            this.chb_Friday.Name = "chb_Friday";
            this.chb_Friday.Size = new System.Drawing.Size(69, 17);
            this.chb_Friday.TabIndex = 7;
            this.chb_Friday.Text = "Пятница";
            this.chb_Friday.UseVisualStyleBackColor = true;
            // 
            // chb_Saturday
            // 
            this.chb_Saturday.AutoSize = true;
            this.chb_Saturday.Location = new System.Drawing.Point(133, 134);
            this.chb_Saturday.Name = "chb_Saturday";
            this.chb_Saturday.Size = new System.Drawing.Size(67, 17);
            this.chb_Saturday.TabIndex = 8;
            this.chb_Saturday.Text = "Суббота";
            this.chb_Saturday.UseVisualStyleBackColor = true;
            // 
            // chb_Sunday
            // 
            this.chb_Sunday.AutoSize = true;
            this.chb_Sunday.Location = new System.Drawing.Point(224, 134);
            this.chb_Sunday.Name = "chb_Sunday";
            this.chb_Sunday.Size = new System.Drawing.Size(93, 17);
            this.chb_Sunday.TabIndex = 9;
            this.chb_Sunday.Text = "Воскресенье";
            this.chb_Sunday.UseVisualStyleBackColor = true;
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(25, 175);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(94, 23);
            this.B_OK.TabIndex = 10;
            this.B_OK.Text = "ОК";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(307, 175);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(94, 23);
            this.B_Cancel.TabIndex = 11;
            this.B_Cancel.Text = "Отмена";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 57);
            this.button1.TabIndex = 12;
            this.button1.Text = "Открыть планировщик задач";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.B_OpenSheduler_Click);
            // 
            // B_Delete
            // 
            this.B_Delete.Location = new System.Drawing.Point(147, 175);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(134, 23);
            this.B_Delete.TabIndex = 13;
            this.B_Delete.Text = "Убрать из расписания";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // F_SheduledSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 218);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.chb_Sunday);
            this.Controls.Add(this.chb_Saturday);
            this.Controls.Add(this.chb_Friday);
            this.Controls.Add(this.chb_Thursday);
            this.Controls.Add(this.chb_Wednesday);
            this.Controls.Add(this.chb_Tuesday);
            this.Controls.Add(this.chb_Monday);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_SheduledSender";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Расписание автоматической отправки отчетов";
            this.Load += new System.EventHandler(this.F_SheduledSender_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_Monday;
        private System.Windows.Forms.CheckBox chb_Tuesday;
        private System.Windows.Forms.CheckBox chb_Wednesday;
        private System.Windows.Forms.CheckBox chb_Thursday;
        private System.Windows.Forms.CheckBox chb_Friday;
        private System.Windows.Forms.CheckBox chb_Saturday;
        private System.Windows.Forms.CheckBox chb_Sunday;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button B_Delete;
    }
}