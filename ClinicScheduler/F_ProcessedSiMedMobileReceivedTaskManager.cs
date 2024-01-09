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
    public partial class F_ProcessedSiMedMobileReceivedTaskManager : Form
    {
        //const string TaskName = "ProcessMobileReceived";

        int clinicId_Value = 0;

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
        public BaseSiMedTaskProperties m_Options;
        private string m_TaskName;
        private string m_SchedulerKey;

        public F_ProcessedSiMedMobileReceivedTaskManager(AppConfig _Config, BaseSiMedTaskProperties _Options, string _TaskName, string _SchedulerKey)
        {
            config = _Config;
            m_Options = _Options;
            m_TaskName = _TaskName;
            m_SchedulerKey = _SchedulerKey;

            InitializeComponent();

            this.Text = $"Настройка задачи {m_TaskName}";

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



        public F_ProcessedSiMedMobileReceivedTaskManager(CClinicApp _ClinicApp, AppConfig _Config, BaseSiMedTaskProperties _Options, string _TaskName, string _SchedulerKey)
        {
            clinicApp = _ClinicApp;
            config = _Config;
            m_Options = _Options;
            m_TaskName = _TaskName;
            m_SchedulerKey = _SchedulerKey;

            InitializeComponent();

            this.Text = $"Настройка задачи {m_TaskName}";

            fillOrganizations();
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

        private void fillOrganizations()
        {
            organizations = clinicApp.GetOrganizations();
            var organizationsList = convertOrganizationsDataView(organizations);

            organizationsList.Insert(0, new OrganizationSimpleClass(-1, "Все мед.организации"));
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
            if (m_Options != null)
            {
                this.cb_Medical.SelectedValue = m_Options.MedOrgId;

                if(m_Options.UserId != 0)
                    this.cb_User.SelectedValue = m_Options.UserId;
            }
        }

        bool checkTaskExistence()
        {
            taskDataXML = TaskManager.GetTaskData(m_TaskName);

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
                throw new InvalidExpressionException($"Невозможно прочитать данные задачи {m_TaskName}" +
                    Environment.NewLine
                    + "Ошибка: " + ex.Message);
            }
            return days;
        }

        private bool validationControls()
        {
            bool isValid = true;

            if (!validationClinicId())
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
                        TaskManager.DeleteTask(m_TaskName);
                    
                    
                        string command = GenerateTaskCommand();
                        bool bRes = TaskManager.AddTask(m_TaskName, command);
                    
                }

                m_Options.MedOrgId = selectedOrganizationId;
                m_Options.UserId = selectedUserId;
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

            command += "/ST " + dateTimePicker1.Text + " /TN \"SiMed_Задача_" + m_TaskName + "\" /TR \"'" +
                Application.ExecutablePath + "' " + m_SchedulerKey + "\"";

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
                        if (TaskManager.DeleteTask(m_TaskName))
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
