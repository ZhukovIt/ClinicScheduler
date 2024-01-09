
namespace ClinicScheduler
{
    partial class F_AutoCreateAccountingActsTaskManager
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
            this.cmb_SecUsers = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_MedOrganizations = new System.Windows.Forms.ComboBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tBox_CopyEmailAddress = new System.Windows.Forms.TextBox();
            this.cbScheduleEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.gbWeekly = new System.Windows.Forms.GroupBox();
            this.chb_Wednesday = new System.Windows.Forms.CheckBox();
            this.chb_Monday = new System.Windows.Forms.CheckBox();
            this.chb_Sunday = new System.Windows.Forms.CheckBox();
            this.chb_Tuesday = new System.Windows.Forms.CheckBox();
            this.chb_Saturday = new System.Windows.Forms.CheckBox();
            this.chb_Thursday = new System.Windows.Forms.CheckBox();
            this.chb_Friday = new System.Windows.Forms.CheckBox();
            this.btnOpenWindowsScheduler = new System.Windows.Forms.Button();
            this.cbRunHourly = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_SignEDocuments = new System.Windows.Forms.CheckBox();
            this.chb_SendEDocuments = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            this.gbWeekly.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_SecUsers
            // 
            this.cmb_SecUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SecUsers.FormattingEnabled = true;
            this.cmb_SecUsers.Location = new System.Drawing.Point(15, 65);
            this.cmb_SecUsers.Name = "cmb_SecUsers";
            this.cmb_SecUsers.Size = new System.Drawing.Size(336, 21);
            this.cmb_SecUsers.TabIndex = 1;
            this.cmb_SecUsers.SelectedIndexChanged += new System.EventHandler(this.cmb_SecUsers_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Пользователь:";
            // 
            // cmb_MedOrganizations
            // 
            this.cmb_MedOrganizations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MedOrganizations.FormattingEnabled = true;
            this.cmb_MedOrganizations.Location = new System.Drawing.Point(15, 25);
            this.cmb_MedOrganizations.Name = "cmb_MedOrganizations";
            this.cmb_MedOrganizations.Size = new System.Drawing.Size(336, 21);
            this.cmb_MedOrganizations.TabIndex = 0;
            this.cmb_MedOrganizations.SelectedIndexChanged += new System.EventHandler(this.cmb_MedOrganizations_SelectedIndexChanged);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(199, 424);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(99, 32);
            this.btn_Cancel.TabIndex = 21;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(36, 424);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(99, 32);
            this.btn_Save.TabIndex = 20;
            this.btn_Save.Text = "Сохранить";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Медицинское учреждение:";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // tBox_CopyEmailAddress
            // 
            this.tBox_CopyEmailAddress.Location = new System.Drawing.Point(15, 105);
            this.tBox_CopyEmailAddress.Name = "tBox_CopyEmailAddress";
            this.tBox_CopyEmailAddress.Size = new System.Drawing.Size(336, 20);
            this.tBox_CopyEmailAddress.TabIndex = 2;
            // 
            // cbScheduleEnabled
            // 
            this.cbScheduleEnabled.AutoSize = true;
            this.cbScheduleEnabled.Location = new System.Drawing.Point(15, 200);
            this.cbScheduleEnabled.Name = "cbScheduleEnabled";
            this.cbScheduleEnabled.Size = new System.Drawing.Size(264, 17);
            this.cbScheduleEnabled.TabIndex = 5;
            this.cbScheduleEnabled.Text = "Включить автоматическое выполнение задачи";
            this.cbScheduleEnabled.UseVisualStyleBackColor = true;
            this.cbScheduleEnabled.CheckedChanged += new System.EventHandler(this.cbScheduleEnabled_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudMinutes);
            this.groupBox1.Controls.Add(this.gbWeekly);
            this.groupBox1.Controls.Add(this.btnOpenWindowsScheduler);
            this.groupBox1.Controls.Add(this.cbRunHourly);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(11, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 186);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Расписание задачи";
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
            this.nudMinutes.TabIndex = 2;
            this.nudMinutes.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
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
            this.gbWeekly.Size = new System.Drawing.Size(323, 84);
            this.gbWeekly.TabIndex = 3;
            this.gbWeekly.TabStop = false;
            this.gbWeekly.Text = "Повторять каждую неделю в выбранные дни";
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
            // btnOpenWindowsScheduler
            // 
            this.btnOpenWindowsScheduler.Image = global::ClinicScheduler.Properties.Resources.time1;
            this.btnOpenWindowsScheduler.Location = new System.Drawing.Point(168, 19);
            this.btnOpenWindowsScheduler.Name = "btnOpenWindowsScheduler";
            this.btnOpenWindowsScheduler.Size = new System.Drawing.Size(164, 39);
            this.btnOpenWindowsScheduler.TabIndex = 23;
            this.btnOpenWindowsScheduler.Text = "Открыть планировщик задач";
            this.btnOpenWindowsScheduler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenWindowsScheduler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenWindowsScheduler.UseVisualStyleBackColor = true;
            this.btnOpenWindowsScheduler.Click += new System.EventHandler(this.btnOpenWindowsScheduler_Click);
            // 
            // cbRunHourly
            // 
            this.cbRunHourly.AutoSize = true;
            this.cbRunHourly.Location = new System.Drawing.Point(9, 73);
            this.cbRunHourly.Name = "cbRunHourly";
            this.cbRunHourly.Size = new System.Drawing.Size(122, 17);
            this.cbRunHourly.TabIndex = 1;
            this.cbRunHourly.Text = "Запускать каждые";
            this.cbRunHourly.UseVisualStyleBackColor = true;
            this.cbRunHourly.CheckedChanged += new System.EventHandler(this.cbRunHourly_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(52, 28);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(61, 20);
            this.dateTimePicker1.TabIndex = 0;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "мин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Адрес почты для отправки копии письма:";
            // 
            // chb_SignEDocuments
            // 
            this.chb_SignEDocuments.AutoSize = true;
            this.chb_SignEDocuments.Location = new System.Drawing.Point(15, 136);
            this.chb_SignEDocuments.Name = "chb_SignEDocuments";
            this.chb_SignEDocuments.Size = new System.Drawing.Size(210, 17);
            this.chb_SignEDocuments.TabIndex = 3;
            this.chb_SignEDocuments.Text = "Подписывать акты с помощью ЭЦП";
            this.chb_SignEDocuments.UseVisualStyleBackColor = true;
            // 
            // chb_SendEDocuments
            // 
            this.chb_SendEDocuments.AutoSize = true;
            this.chb_SendEDocuments.Location = new System.Drawing.Point(15, 164);
            this.chb_SendEDocuments.Name = "chb_SendEDocuments";
            this.chb_SendEDocuments.Size = new System.Drawing.Size(227, 17);
            this.chb_SendEDocuments.TabIndex = 4;
            this.chb_SendEDocuments.Text = "Отправлять акты на почту организации";
            this.chb_SendEDocuments.UseVisualStyleBackColor = true;
            // 
            // F_AutoCreateAccountingActsTaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 465);
            this.Controls.Add(this.tBox_CopyEmailAddress);
            this.Controls.Add(this.chb_SendEDocuments);
            this.Controls.Add(this.chb_SignEDocuments);
            this.Controls.Add(this.cbScheduleEnabled);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_SecUsers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmb_MedOrganizations);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label1);
            this.Name = "F_AutoCreateAccountingActsTaskManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка формирования актов услуг";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            this.gbWeekly.ResumeLayout(false);
            this.gbWeekly.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_SecUsers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_MedOrganizations;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox tBox_CopyEmailAddress;
        private System.Windows.Forms.CheckBox cbScheduleEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.GroupBox gbWeekly;
        private System.Windows.Forms.CheckBox chb_Wednesday;
        private System.Windows.Forms.CheckBox chb_Monday;
        private System.Windows.Forms.CheckBox chb_Sunday;
        private System.Windows.Forms.CheckBox chb_Tuesday;
        private System.Windows.Forms.CheckBox chb_Saturday;
        private System.Windows.Forms.CheckBox chb_Thursday;
        private System.Windows.Forms.CheckBox chb_Friday;
        private System.Windows.Forms.Button btnOpenWindowsScheduler;
        private System.Windows.Forms.CheckBox cbRunHourly;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_SendEDocuments;
        private System.Windows.Forms.CheckBox chb_SignEDocuments;
    }
}