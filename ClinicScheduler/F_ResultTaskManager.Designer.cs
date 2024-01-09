namespace ClinicScheduler
{
    partial class F_ResultTaskManager
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_NubmerOfDays = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chb_Sunday = new System.Windows.Forms.CheckBox();
            this.chb_Saturday = new System.Windows.Forms.CheckBox();
            this.cbRunHourly = new System.Windows.Forms.CheckBox();
            this.chb_Friday = new System.Windows.Forms.CheckBox();
            this.chb_Thursday = new System.Windows.Forms.CheckBox();
            this.chb_Wednesday = new System.Windows.Forms.CheckBox();
            this.chb_Tuesday = new System.Windows.Forms.CheckBox();
            this.chb_Monday = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cbScheduleEnabled = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cb_Medical = new System.Windows.Forms.ComboBox();
            this.cb_Branch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_User = new System.Windows.Forms.ComboBox();
            this.gbWeekly = new System.Windows.Forms.GroupBox();
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbWeekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Медицинское учреждение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Филиал:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Запрашивать результаты за последние N дней:";
            // 
            // tb_NubmerOfDays
            // 
            this.tb_NubmerOfDays.Location = new System.Drawing.Point(270, 132);
            this.tb_NubmerOfDays.Name = "tb_NubmerOfDays";
            this.tb_NubmerOfDays.Size = new System.Drawing.Size(73, 20);
            this.tb_NubmerOfDays.TabIndex = 3;
            this.tb_NubmerOfDays.Text = "1";
            this.tb_NubmerOfDays.Validating += new System.ComponentModel.CancelEventHandler(this.tb_NubmerOfDays_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudMinutes);
            this.groupBox1.Controls.Add(this.gbWeekly);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbRunHourly);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 186);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Расписание автоматического получения результатов";
            // 
            // button1
            // 
            this.button1.Image = global::ClinicScheduler.Properties.Resources.time1;
            this.button1.Location = new System.Drawing.Point(168, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 39);
            this.button1.TabIndex = 23;
            this.button1.Text = "Открыть планировщик задач";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chb_Sunday
            // 
            this.chb_Sunday.AutoSize = true;
            this.chb_Sunday.Location = new System.Drawing.Point(186, 55);
            this.chb_Sunday.Name = "chb_Sunday";
            this.chb_Sunday.Size = new System.Drawing.Size(93, 17);
            this.chb_Sunday.TabIndex = 11;
            this.chb_Sunday.Text = "Воскресенье";
            this.chb_Sunday.UseVisualStyleBackColor = true;
            this.chb_Sunday.CheckedChanged += new System.EventHandler(this.chb_Sunday_CheckedChanged);
            // 
            // chb_Saturday
            // 
            this.chb_Saturday.AutoSize = true;
            this.chb_Saturday.Location = new System.Drawing.Point(112, 55);
            this.chb_Saturday.Name = "chb_Saturday";
            this.chb_Saturday.Size = new System.Drawing.Size(67, 17);
            this.chb_Saturday.TabIndex = 10;
            this.chb_Saturday.Text = "Суббота";
            this.chb_Saturday.UseVisualStyleBackColor = true;
            this.chb_Saturday.CheckedChanged += new System.EventHandler(this.chb_Saturday_CheckedChanged);
            // 
            // cbRunHourly
            // 
            this.cbRunHourly.AutoSize = true;
            this.cbRunHourly.Location = new System.Drawing.Point(9, 73);
            this.cbRunHourly.Name = "cbRunHourly";
            this.cbRunHourly.Size = new System.Drawing.Size(122, 17);
            this.cbRunHourly.TabIndex = 9;
            this.cbRunHourly.Text = "Запускать каждые";
            this.cbRunHourly.UseVisualStyleBackColor = true;
            this.cbRunHourly.CheckedChanged += new System.EventHandler(this.cbRunHourly_CheckedChanged);
            // 
            // chb_Friday
            // 
            this.chb_Friday.AutoSize = true;
            this.chb_Friday.Location = new System.Drawing.Point(12, 55);
            this.chb_Friday.Name = "chb_Friday";
            this.chb_Friday.Size = new System.Drawing.Size(69, 17);
            this.chb_Friday.TabIndex = 9;
            this.chb_Friday.Text = "Пятница";
            this.chb_Friday.UseVisualStyleBackColor = true;
            this.chb_Friday.CheckedChanged += new System.EventHandler(this.chb_Friday_CheckedChanged);
            // 
            // chb_Thursday
            // 
            this.chb_Thursday.AutoSize = true;
            this.chb_Thursday.Location = new System.Drawing.Point(254, 19);
            this.chb_Thursday.Name = "chb_Thursday";
            this.chb_Thursday.Size = new System.Drawing.Size(68, 17);
            this.chb_Thursday.TabIndex = 8;
            this.chb_Thursday.Text = "Четверг";
            this.chb_Thursday.UseVisualStyleBackColor = true;
            this.chb_Thursday.CheckedChanged += new System.EventHandler(this.chb_Thursday_CheckedChanged);
            // 
            // chb_Wednesday
            // 
            this.chb_Wednesday.AutoSize = true;
            this.chb_Wednesday.Location = new System.Drawing.Point(186, 19);
            this.chb_Wednesday.Name = "chb_Wednesday";
            this.chb_Wednesday.Size = new System.Drawing.Size(57, 17);
            this.chb_Wednesday.TabIndex = 7;
            this.chb_Wednesday.Text = "Среда";
            this.chb_Wednesday.UseVisualStyleBackColor = true;
            this.chb_Wednesday.CheckedChanged += new System.EventHandler(this.chb_Wednesday_CheckedChanged);
            // 
            // chb_Tuesday
            // 
            this.chb_Tuesday.AutoSize = true;
            this.chb_Tuesday.Location = new System.Drawing.Point(112, 19);
            this.chb_Tuesday.Name = "chb_Tuesday";
            this.chb_Tuesday.Size = new System.Drawing.Size(68, 17);
            this.chb_Tuesday.TabIndex = 6;
            this.chb_Tuesday.Text = "Вторник";
            this.chb_Tuesday.UseVisualStyleBackColor = true;
            this.chb_Tuesday.CheckedChanged += new System.EventHandler(this.chb_Tuesday_CheckedChanged);
            // 
            // chb_Monday
            // 
            this.chb_Monday.AutoSize = true;
            this.chb_Monday.Location = new System.Drawing.Point(12, 19);
            this.chb_Monday.Name = "chb_Monday";
            this.chb_Monday.Size = new System.Drawing.Size(94, 17);
            this.chb_Monday.TabIndex = 5;
            this.chb_Monday.Text = "Понедельник";
            this.chb_Monday.UseVisualStyleBackColor = true;
            this.chb_Monday.CheckedChanged += new System.EventHandler(this.chb_Monday_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(52, 28);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(61, 20);
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Время";
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Location = new System.Drawing.Point(74, 377);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(99, 32);
            this.btn_Save.TabIndex = 12;
            this.btn_Save.Text = "Сохранить";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Location = new System.Drawing.Point(189, 377);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(99, 32);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // cbScheduleEnabled
            // 
            this.cbScheduleEnabled.AutoSize = true;
            this.cbScheduleEnabled.Location = new System.Drawing.Point(12, 162);
            this.cbScheduleEnabled.Name = "cbScheduleEnabled";
            this.cbScheduleEnabled.Size = new System.Drawing.Size(282, 17);
            this.cbScheduleEnabled.TabIndex = 14;
            this.cbScheduleEnabled.Text = "Включить автоматическое получение результатов";
            this.cbScheduleEnabled.UseVisualStyleBackColor = true;
            this.cbScheduleEnabled.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // cb_Medical
            // 
            this.cb_Medical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Medical.FormattingEnabled = true;
            this.cb_Medical.Location = new System.Drawing.Point(17, 25);
            this.cb_Medical.Name = "cb_Medical";
            this.cb_Medical.Size = new System.Drawing.Size(326, 21);
            this.cb_Medical.TabIndex = 15;
            this.cb_Medical.SelectedIndexChanged += new System.EventHandler(this.cb_Medical_SelectedIndexChanged);
            // 
            // cb_Branch
            // 
            this.cb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Branch.FormattingEnabled = true;
            this.cb_Branch.Location = new System.Drawing.Point(17, 65);
            this.cb_Branch.Name = "cb_Branch";
            this.cb_Branch.Size = new System.Drawing.Size(326, 21);
            this.cb_Branch.TabIndex = 15;
            this.cb_Branch.SelectedIndexChanged += new System.EventHandler(this.cb_Branch_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Пользователь:";
            // 
            // cb_User
            // 
            this.cb_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_User.FormattingEnabled = true;
            this.cb_User.Location = new System.Drawing.Point(17, 105);
            this.cb_User.Name = "cb_User";
            this.cb_User.Size = new System.Drawing.Size(326, 21);
            this.cb_User.TabIndex = 17;
            this.cb_User.SelectedIndexChanged += new System.EventHandler(this.cb_User_SelectedIndexChanged);
            // 
            // gbWeekly
            // 
            this.gbWeekly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWeekly.Controls.Add(this.chb_Wednesday);
            this.gbWeekly.Controls.Add(this.chb_Monday);
            this.gbWeekly.Controls.Add(this.chb_Sunday);
            this.gbWeekly.Controls.Add(this.chb_Tuesday);
            this.gbWeekly.Controls.Add(this.chb_Saturday);
            this.gbWeekly.Controls.Add(this.chb_Thursday);
            this.gbWeekly.Controls.Add(this.chb_Friday);
            this.gbWeekly.Location = new System.Drawing.Point(9, 96);
            this.gbWeekly.Name = "gbWeekly";
            this.gbWeekly.Size = new System.Drawing.Size(322, 84);
            this.gbWeekly.TabIndex = 24;
            this.gbWeekly.TabStop = false;
            this.gbWeekly.Text = "Повторять каждую неделю в выбранные дни";
            // 
            // nudMinutes
            // 
            this.nudMinutes.Location = new System.Drawing.Point(132, 72);
            this.nudMinutes.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            this.nudMinutes.Size = new System.Drawing.Size(57, 20);
            this.nudMinutes.TabIndex = 25;
            this.nudMinutes.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "мин";
            // 
            // F_ResultTaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 419);
            this.Controls.Add(this.cb_User);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_Branch);
            this.Controls.Add(this.cb_Medical);
            this.Controls.Add(this.cbScheduleEnabled);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_NubmerOfDays);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_ResultTaskManager";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка задачи получения результатов исследований";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbWeekly.ResumeLayout(false);
            this.gbWeekly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_NubmerOfDays;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chb_Sunday;
        private System.Windows.Forms.CheckBox chb_Saturday;
        private System.Windows.Forms.CheckBox chb_Friday;
        private System.Windows.Forms.CheckBox chb_Thursday;
        private System.Windows.Forms.CheckBox chb_Wednesday;
        private System.Windows.Forms.CheckBox chb_Tuesday;
        private System.Windows.Forms.CheckBox chb_Monday;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbScheduleEnabled;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cb_Branch;
        private System.Windows.Forms.ComboBox cb_Medical;
        private System.Windows.Forms.ComboBox cb_User;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbRunHourly;
        private System.Windows.Forms.GroupBox gbWeekly;
        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.Label label4;
    }
}