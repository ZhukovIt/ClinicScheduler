using SiMed.Clinic;
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

namespace ClinicScheduler
{
    public partial class F_AutoCheckAccountingActsTaskManager : Form
    {
        private CClinicApp m_clinicApp;
        private AppConfig m_config;
        private SiMedAutoCheckAccountingActsProperties m_Options;
        private string m_TaskName;
        private string m_SchedulerKey;
        private DataView m_MedOrganizations;
        private DataView m_SecUsers;
        private string m_TaskDataXML;
        private DaysOfWeekChecked m_TaskDataObject;
        private bool m_IsTaskExists;
        private int m_SelectedValueMedOrganizations;
        private int m_SelectedValueSecUsers;
        //---------------------------------------------
        public F_AutoCheckAccountingActsTaskManager(CClinicApp _ClinicApp, AppConfig _Config, SiMedAutoCheckAccountingActsProperties _Options, string _TaskName, string _SchedulerKey)
        {
            m_clinicApp = _ClinicApp;
            m_config = _Config;
            m_Options = _Options;
            m_TaskName = _TaskName;
            m_SchedulerKey = _SchedulerKey;

            InitializeComponent();

            FillMedOrganizations();
            FillSecUsers();

            m_IsTaskExists = CheckTaskExistence();
            cbScheduleEnabled.Checked = m_IsTaskExists;
            if (m_IsTaskExists)
            {
                try
                {
                    m_TaskDataObject = ParseTask(m_TaskDataXML);
                    EnableTaskGroupBox(true);
                }
                catch (Exception ex)
                {
                    m_TaskDataObject = new DaysOfWeekChecked();
                    EnableTaskGroupBox(true);

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                m_TaskDataObject = new DaysOfWeekChecked();
                EnableTaskGroupBox(false);
            }

            FillMembers();
        }
        //---------------------------------------------
        private void EnableTaskGroupBox(bool isEnable)
        {
            if (isEnable)
            {
                groupBox1.Enabled = true;
                cbScheduleEnabled.Checked = true;
                if (m_TaskDataObject != null)
                {
                    dateTimePicker1.Value = m_TaskDataObject.Time;

                    chb_Friday.Checked = m_TaskDataObject.Friday;
                    chb_Monday.Checked = m_TaskDataObject.Monday;
                    chb_Saturday.Checked = m_TaskDataObject.Saturday;
                    chb_Sunday.Checked = m_TaskDataObject.Sunday;
                    chb_Thursday.Checked = m_TaskDataObject.Thursday;
                    chb_Tuesday.Checked = m_TaskDataObject.Tuesday;
                    chb_Wednesday.Checked = m_TaskDataObject.Wednesday;
                    cbRunHourly.Checked = m_TaskDataObject.Hourly;
                    nudMinutes.Value = m_TaskDataObject.Minutes;
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
        //---------------------------------------------
        private DaysOfWeekChecked ParseTask(string task)
        {
            DaysOfWeekChecked days = new DaysOfWeekChecked();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(m_TaskDataXML);
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
        //---------------------------------------------
        private void FillMedOrganizations()
        {
            m_MedOrganizations = m_clinicApp.GetOrganizations();
            var _MedOrganizationsList = ConvertSimpleDataView(m_MedOrganizations, "MEDORG_ID", "MEDORG_NAME");

            cmb_MedOrganizations.DisplayMember = "Name";
            cmb_MedOrganizations.ValueMember = "Id";
            cmb_MedOrganizations.DataSource = _MedOrganizationsList;
        }
        //---------------------------------------------
        private void FillSecUsers()
        {
            m_SecUsers = m_clinicApp.GetUsers();
            var _SecUsersList = ConvertSimpleDataView(m_SecUsers, "SEC_USER_ID", "SEC_USER_LOGIN");

            cmb_SecUsers.DisplayMember = "Name";
            cmb_SecUsers.ValueMember = "Id";
            cmb_SecUsers.DataSource = _SecUsersList;
        }
        //---------------------------------------------
        private bool CheckTaskExistence()
        {
            m_TaskDataXML = TaskManager.GetTaskData(m_TaskName);

            if (string.IsNullOrWhiteSpace(m_TaskDataXML))
                return false;

            return true;
        }
        //---------------------------------------------
        private void FillMembers()
        {
            if (m_Options != null)
            {
                cmb_MedOrganizations.SelectedValue = m_Options.MedOrgId;
                cmb_SecUsers.SelectedValue = m_Options.UserId;
                tBox_CopyEmailAddress.Text = m_Options.CopyToEmailAddress;
                chkSignPretensionEDocuments.Checked = m_Options.SignPretensionEDocuments;
                chkCreatePretensionEDocuments.Checked = m_Options.CreatePretensionEDocuments;
            }
        }
        //---------------------------------------------
        private class BaseSimpleClass
        {
            public int Id { get; private set; }

            public string Name { get; private set; }

            public BaseSimpleClass(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
        //---------------------------------------------
        private List<BaseSimpleClass> ConvertSimpleDataView(DataView _DataView, string _RowViewId, string _RowViewName)
        {
            List<BaseSimpleClass> model = new List<BaseSimpleClass>();

            foreach (DataRowView row in _DataView)
            {
                model.Add(new BaseSimpleClass(row.Row.Field<int>(_RowViewId), row.Row.Field<string>(_RowViewName)));
            }

            return model;
        }
        //---------------------------------------------
        private string GenerateTaskCommand()
        {
            string command = "/C SCHTASKS /Create ";

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
        //---------------------------------------------
        private bool CheckValidation()
        {
            errorProvider.Clear();

            if (m_SelectedValueMedOrganizations <= 0)
            {
                errorProvider.SetError(cmb_MedOrganizations, "Необходимо выбрать мед. организацию");
                return false;
            }

            return true;
        }
        //---------------------------------------------
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                if (cbScheduleEnabled.Checked)
                {
                    if (m_IsTaskExists)
                    {
                        TaskManager.DeleteTask(m_TaskName);
                    }

                    string _Command = GenerateTaskCommand();

                    TaskManager.AddTask(m_TaskName, _Command);
                }

                m_Options.MedOrgId = m_SelectedValueMedOrganizations;
                m_Options.UserId = m_SelectedValueSecUsers;
                m_Options.CopyToEmailAddress = tBox_CopyEmailAddress.Text;
                m_Options.CreatePretensionEDocuments = chkCreatePretensionEDocuments.Checked;
                m_Options.SignPretensionEDocuments = chkSignPretensionEDocuments.Checked;
                DialogResult = DialogResult.OK;
            }
        }
        //---------------------------------------------
        private void cmb_MedOrganizations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _Sender = (ComboBox)sender;
            if (_Sender.SelectedValue == null)
            {
                _Sender.SelectedIndex = 0;
            }
            m_SelectedValueMedOrganizations = (int)_Sender.SelectedValue;
        }
        //---------------------------------------------
        private void cmb_SecUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _Sender = (ComboBox)sender;
            if (_Sender.SelectedValue == null)
            {
                _Sender.SelectedIndex = 0;
            }
            m_SelectedValueSecUsers = (int)_Sender.SelectedValue;
        }
        //---------------------------------------------
        private void btnOpenWindowsScheduler_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Windows\system32\taskschd.msc");
        }
        //---------------------------------------------
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        //---------------------------------------------
        private void cbScheduleEnabled_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = cbScheduleEnabled.Checked;
            SetScheduleWeeklyEnabled();

            if (!cbScheduleEnabled.Checked)
            {
                if (m_IsTaskExists)
                {
                    DialogResult result = MessageBox.Show("Удалить задачу из планировщика задач?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        if (TaskManager.DeleteTask(m_TaskName))
                            m_IsTaskExists = false;
                    }
                    else
                        cbScheduleEnabled.Checked = true;
                }
            }
        }
        //---------------------------------------------
        private void SetScheduleWeeklyEnabled()
        {
            gbWeekly.Enabled = !cbRunHourly.Checked && cbScheduleEnabled.Checked;
        }
        //---------------------------------------------
        private void cbRunHourly_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
                m_TaskDataObject.Hourly = cbRunHourly.Checked;

            SetScheduleWeeklyEnabled();
        }
        //---------------------------------------------
        private void chb_Monday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Monday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chb_Tuesday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Tuesday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chb_Wednesday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Wednesday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chb_Thursday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Thursday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chb_Friday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Friday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chb_Saturday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Saturday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chb_Sunday_CheckedChanged(object sender, EventArgs e)
        {
            if (m_TaskDataObject != null)
            {
                m_TaskDataObject.Sunday = ((CheckBox)sender).Checked;
            }
        }
        //---------------------------------------------
        private void chkCreatePretensionDocument_CheckedChanged(object sender, EventArgs e)
        {
            chkSignPretensionEDocuments.Enabled = chkCreatePretensionEDocuments.Checked;
        }
        //---------------------------------------------
    }
}
