using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SiMed.Clinic;

namespace ClinicScheduler
{
    public partial class F_ResultTaskManager : Form
    {
        const string TaskName = "CheckLaboratoryTestsResults";

        int clinicId_Value = 0;
        int branchId_Value = 0;
        int numberOfDays_Value = 0;

        bool isTaskExist = false;
        private string taskDataXML;
        private DaysOfWeekChecked taskDataObject;
        private AppConfig config;
        private DataView branches;
        private DataView organizations;
        private DataView users;
        private CClinicApp clinicApp;
        private int selectedOrganizationId;
        private int selectedBranchId;
        private int selectedUserId;

        public F_ResultTaskManager(AppConfig config)
        {
            this.config = config;
            InitializeComponent();


            isTaskExist = checkTaskExistence();
            if (isTaskExist)
            {
                try
                {
                    taskDataObject = parseTask(taskDataXML);
                    enableTaskGroupBox(true);
                }
                catch (Exception ex)
                {
                    taskDataObject = new DaysOfWeekChecked();
                    enableTaskGroupBox(true);

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                taskDataObject = new DaysOfWeekChecked();
                enableTaskGroupBox(false);
            }

            fillControls();

        }



        public F_ResultTaskManager(AppConfig config, CClinicApp clinicApp)
        {
            this.clinicApp = clinicApp;
            this.config = config;

            InitializeComponent();

            fillOrganizations();
            fillBranches(selectedOrganizationId);
            fillUsers();




            isTaskExist = checkTaskExistence();
            if (isTaskExist)
            {
                try
                {
                    taskDataObject = parseTask(taskDataXML);
                    enableTaskGroupBox(true);
                }
                catch (Exception ex)
                {
                    taskDataObject = new DaysOfWeekChecked();
                    enableTaskGroupBox(true);

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                taskDataObject = new DaysOfWeekChecked();
                enableTaskGroupBox(false);
            }

            fillControls();
        }

        private void fillBranches(int selectedOrganizationId)
        {
            this.branches = clinicApp.GetBranches(selectedOrganizationId);
            var branchesList = convertBranchesDataView(branches);
            this.cb_Branch.DisplayMember = "Name";
            this.cb_Branch.ValueMember = "Id";
            this.cb_Branch.DataSource = branchesList;
        }

        private void fillOrganizations()
        {
            organizations = clinicApp.GetOrganizations();
            var organizationsList = convertOrganizationsDataView(organizations);
            
            //organizations.Table.Rows.Add(0,-1,"<Все мед. организации>");
            this.cb_Medical.DisplayMember = "Name";
            this.cb_Medical.ValueMember = "Id";
            this.cb_Medical.DataSource = organizationsList;
        }
        private void fillUsers()
        {
            users = clinicApp.GetUsers();
            var usersList = convertUsersDataView(users);

            //organizations.Table.Rows.Add(0,-1,"<Все мед. организации>");
            this.cb_User.DisplayMember = "Name";
            this.cb_User.ValueMember = "Id";
            this.cb_User.DataSource = usersList;
        }
        private List<OrganizationSimpleClass> convertOrganizationsDataView(DataView organizationDataView)
        {
            List<OrganizationSimpleClass > model = new List<OrganizationSimpleClass>();
            foreach (DataRowView rowView in organizationDataView)
            {
                DataRow row = rowView.Row;
                model.Add(new OrganizationSimpleClass((int)row["MEDORG_ID"], (string)row["MEDORG_NAME"] ));
            }
            return model;
        }
        private List<BranchSimpleClass> convertBranchesDataView(DataView branchDataView)
        {
            List<BranchSimpleClass> model = new List<BranchSimpleClass>();
            foreach (DataRowView rowView in branchDataView)
            {
                DataRow row = rowView.Row;
                model.Add(new BranchSimpleClass((int)row["BRA_ID"], (string)row["BRA_NAME"], (int)row["MEDORG_ID"]));
            }
            return model;
        }
        private List<UserClass> convertUsersDataView(DataView usersDataView)
        {
            List<UserClass> model = new List<UserClass>();
            foreach (DataRowView rowView in usersDataView)
            {
                DataRow row = rowView.Row;
                model.Add(new UserClass((int)row["SEC_USER_ID"], (string)row["SEC_USER_LOGIN"]));
            }
            return model;
        }

        private void fillControls()
        {
            if (config != null)
            {
                if (config.LaboratoryTestsResultsProperties != null)
                {
                    this.cb_Medical.SelectedValue = config.LaboratoryTestsResultsProperties.ClinicId;

                    this.cb_Branch.SelectedValue = config.LaboratoryTestsResultsProperties.BranchId;
                    if(config.LaboratoryTestsResultsProperties.UserId != 0)
                        this.cb_User.SelectedValue = config.LaboratoryTestsResultsProperties.UserId;

                    this.tb_NubmerOfDays.Text = config.LaboratoryTestsResultsProperties.NumberOfDays.ToString();
                }
            }
        }

        bool checkTaskExistence()
        {
            taskDataXML = TaskManager.GetTaskData(TaskName);

            if (string.IsNullOrWhiteSpace(taskDataXML))
                return false;

            return true;
        }
        private void enableTaskGroupBox(bool isEnable)
        {
            if (isEnable)
            {
                groupBox1.Enabled = true;
                cbScheduleEnabled.Checked = true;
                if (taskDataObject != null)
                {
                    dateTimePicker1.Value = taskDataObject.Time;

                    chb_Friday.Checked = taskDataObject.Friday;
                    chb_Monday.Checked = taskDataObject.Monday;
                    chb_Saturday.Checked = taskDataObject.Saturday;
                    chb_Sunday.Checked = taskDataObject.Sunday;
                    chb_Thursday.Checked = taskDataObject.Thursday;
                    chb_Tuesday.Checked = taskDataObject.Tuesday;
                    chb_Wednesday.Checked = taskDataObject.Wednesday;
                    cbRunHourly.Checked = taskDataObject.Hourly;
                    nudMinutes.Value = taskDataObject.Minutes;
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now;

                    chb_Friday.Checked = false;
                    chb_Monday.Checked = false;
                    chb_Saturday.Checked = false;
                    chb_Sunday.Checked = false;
                    chb_Thursday.Checked = false;
                    chb_Tuesday.Checked = false;
                    chb_Wednesday.Checked = false;
                    cbRunHourly.Checked = false;
                    nudMinutes.Value = 30;
                }
            }
            else
            {
                groupBox1.Enabled = false;
                cbScheduleEnabled.Checked = false;
            }
        }



        private DaysOfWeekChecked parseTask(string task)
        {
            DaysOfWeekChecked days = new DaysOfWeekChecked();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(taskDataXML);
                {
                    XmlNode node = doc.GetElementsByTagName("StartBoundary")[0];
                    days.Time = DateTime.Parse(node.InnerText);

                    XmlNodeList nodes = doc.GetElementsByTagName("Repetition");
                    if (nodes.Count > 0)
                    {
                        days.Hourly = true;
                        nodes = doc.GetElementsByTagName("Interval");
                        if (nodes.Count > 0)
                        {
                            TimeSpan ts = XmlConvert.ToTimeSpan(nodes[0].InnerText);
                            days.Minutes = ts.Hours * 60 + ts.Minutes;
                        }
                    }
                }

                if (doc.GetElementsByTagName("DaysOfWeek").Count > 0)
                {
                    XmlNodeList nodes = doc.GetElementsByTagName("DaysOfWeek")[0].ChildNodes;
                    foreach (XmlNode node in nodes)
                    {
                        switch (node.Name)
                        {
                            case "Monday":
                                days.Monday = true;
                                break;
                            case "Tuesday":
                                days.Tuesday = true;
                                break;
                            case "Wednesday":
                                days.Wednesday = true;
                                break;
                            case "Thursday":
                                days.Thursday = true;
                                break;
                            case "Friday":
                                days.Friday = true;
                                break;
                            case "Saturday":
                                days.Saturday = true;
                                break;
                            case "Sunday":
                                days.Sunday = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidExpressionException("Невозможно прочитать данные задачи \"Получение результатов лабораторных исследований\"" +
                    Environment.NewLine
                    + "Ошибка: " + ex.Message);
            }
            return days;
        }

        private bool validationControls()
        {
            int branchId = (int)cb_Medical.SelectedValue;
            string numberOfDaysId = tb_NubmerOfDays.Text;

            bool isValid = true;

            if (!validationClinicId())
                isValid = false;

            if (!validationBranchId())
                isValid = false;

            if (!validationNumberOfDays())
                isValid = false;

            return isValid;
        }
        private bool validationClinicId()
        {
            //string clinicId = tb_ClinicId.Text;
            bool isValid = true;

            if(cb_Medical.SelectedIndex<0)
                errorProvider1.SetError(cb_Medical, "Необходимо выбрать медицинское учреждение");

            /* if (string.IsNullOrWhiteSpace(clinicId))
             {
                 isValid = false;
                 errorProvider1.SetError(tb_ClinicId, "Поле идентификатора клиники не должно быть пустым");
             }
             if (!int.TryParse(clinicId, out clinicId_Value))
             {
                 isValid = false;
                 errorProvider1.SetError(tb_ClinicId, "Поле идентификатора должно быть числом");
             }
             if (clinicId_Value < 0)
             {
                 isValid = false;
                 errorProvider1.SetError(tb_ClinicId, "Поле идентификатора должно быть положительным числом");
             }
             */
            return isValid;
        }
        private bool validationBranchId()
        {
            //string branchId = tb_BranchId.Text;
            bool isValid = true;
            if (cb_Branch.SelectedIndex < 0)
                errorProvider1.SetError(cb_Branch, "Необходимо выбрать филиал");

            //if (string.IsNullOrWhiteSpace(branchId))
            //{
            //    isValid = false;
            //    errorProvider1.SetError(tb_BranchId, "Поле идентификатора клиники не должно быть пустым");
            //}
            //if (!int.TryParse(branchId, out branchId_Value))
            //{
            //    isValid = false;
            //    errorProvider1.SetError(tb_BranchId, "Поле идентификатора должно быть числом");
            //}
            //if (branchId_Value < 0)
            //{
            //    isValid = false;
            //    errorProvider1.SetError(tb_BranchId, "Поле идентификатора должно быть не меньше нуля");
            //}

            return isValid;
        }
        private bool validationNumberOfDays()
        {
            string numberOfDays = tb_NubmerOfDays.Text;
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(numberOfDays))
            {
                isValid = false;
                errorProvider1.SetError(tb_NubmerOfDays, "Поле идентификатора клиники не должно быть пустым");
            }
            if (!int.TryParse(numberOfDays, out numberOfDays_Value))
            {
                isValid = false;
                errorProvider1.SetError(tb_NubmerOfDays, "Поле идентификатора должно быть числом");
            }
            if (numberOfDays_Value <= 0)
            {
                isValid = false;
                errorProvider1.SetError(tb_NubmerOfDays, "Поле идентификатора должно быть положительным числом");
            }

            return isValid;
        }

        private void tb_NubmerOfDays_Validating(object sender, CancelEventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (validationControls())
            {
                if (cbScheduleEnabled.Checked)
                {
                    if (isTaskExist)
                        TaskManager.DeleteTask(TaskName);
                    
                    
                        string command = GenerateTaskCommand();
                        TaskManager.AddTask(TaskName, command);
                    
                }
                if (config == null)
                    config = new AppConfig();
                if (config.LaboratoryTestsResultsProperties == null)
                    config.LaboratoryTestsResultsProperties = new LaboratoryTestsResultsProperties();

                config.LaboratoryTestsResultsProperties.BranchId = selectedBranchId;
                config.LaboratoryTestsResultsProperties.ClinicId = selectedOrganizationId;
                config.LaboratoryTestsResultsProperties.NumberOfDays = numberOfDays_Value;
                config.LaboratoryTestsResultsProperties.UserId = selectedUserId;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private string GenerateTaskCommand()
        {
            string command = "/C SCHTASKS ";
            command += "/Create ";

            if (cbScheduleEnabled.Checked)
            {
                command += "/SC ";

                if (cbRunHourly.Checked)
                    command += "MINUTE /MO " + String.Format("{0:N0}", nudMinutes.Value) + " ";
                else
                {
                    command += "WEEKLY /D \"";
                    if (chb_Monday.Checked) command += "MON ";
                    if (chb_Tuesday.Checked) command += "TUE ";
                    if (chb_Wednesday.Checked) command += "WED ";
                    if (chb_Thursday.Checked) command += "THU ";
                    if (chb_Friday.Checked) command += "FRI ";
                    if (chb_Saturday.Checked) command += "SAT ";
                    if (chb_Sunday.Checked) command += "SUN ";
                    command += "\" ";
                }
            }

            command += "/ST " + dateTimePicker1.Text + " /TN \"SiMed_Задача_" + TaskName + "\" /TR \"'" +
                Application.ExecutablePath + "' CheckResults\"";

            return command;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = cbScheduleEnabled.Checked;
            SetScheduleWeeklyEnabled();

            if (!cbScheduleEnabled.Checked)
            {
                if (isTaskExist)
                {
                    DialogResult result = MessageBox.Show("Удалить задачу из планировщика задач?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        if (TaskManager.DeleteTask(TaskName))
                            isTaskExist = false;
                    }
                    else
                        cbScheduleEnabled.Checked = true;
                }
            }
        }

        private void chb_Monday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Monday = chb_Monday.Checked;
        }

        private void chb_Tuesday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Tuesday = chb_Tuesday.Checked;
        }

        private void chb_Wednesday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Wednesday = chb_Wednesday.Checked;
        }

        private void chb_Thursday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Thursday = chb_Thursday.Checked;
        }

        private void chb_Friday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Friday = chb_Friday.Checked;
        }

        private void chb_Saturday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Saturday = chb_Saturday.Checked;
        }

        private void chb_Sunday_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Sunday = chb_Sunday.Checked;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Time = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Windows\system32\taskschd.msc");
        }

        private void cb_Medical_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cb_Medical != null && cb_Medical.SelectedValue != null)
            {
                this.selectedOrganizationId = (int)cb_Medical.SelectedValue;
                fillBranches(selectedOrganizationId);
            }
        }

        class OrganizationSimpleClass
        {
            private int id;
            private string name;

            public OrganizationSimpleClass(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public int Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }

            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }
        }

        class UserClass
        {
            private int id;
            private string name;

            public UserClass(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public int Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }

            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }
        }

        class BranchSimpleClass
        {
            private int id;
            private string name;
            private int medOrgID;
            public BranchSimpleClass(int id, string name, int medOrgID)
            {
                this.Id = id;
                this.Name = name;
                this.MedOrgId = medOrgID;
            }

            public int Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }
            public int MedOrgId
            {
                get
                {
                    return medOrgID;
                }

                set
                {
                    medOrgID = value;
                }
            }

            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }
        }

        private void cb_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Branch != null && cb_Branch.SelectedValue != null)
            {
                this.selectedBranchId = (int)cb_Branch.SelectedValue;
            }
        }

        private void cb_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_User != null && cb_User.SelectedValue != null)
            {
                this.selectedUserId = (int)cb_User.SelectedValue;
            }
        }

        private void cbRunHourly_CheckedChanged(object sender, EventArgs e)
        {
            if (taskDataObject != null)
                taskDataObject.Hourly = cbRunHourly.Checked;

            SetScheduleWeeklyEnabled();
        }

        private void SetScheduleWeeklyEnabled()
        {
            gbWeekly.Enabled = !cbRunHourly.Checked && cbScheduleEnabled.Checked;
        }
    }
}
