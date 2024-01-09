using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace ClinicScheduler
{
    public partial class F_SheduledSender : Form
    {
        string TaskName;
        public bool TaskExist;
        public F_SheduledSender(string TaskName)
        {
            this.TaskName = TaskName;
            InitializeComponent();
            TaskExist = IsTaskExist(TaskName);
            if (!TaskExist)
                B_Delete.Enabled = false;
        }

        bool IsTaskExist(string taskName)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C SCHTASKS /Query /XML /TN SiMed_Задача_" + taskName;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            startInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding(866);

            process.StartInfo = startInfo;
            process.Start();
            string res = process.StandardOutput.ReadToEnd();
            if (res == "")
                return false;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            {
                XmlNode node = doc.GetElementsByTagName("StartBoundary")[0];
                dateTimePicker1.Value = DateTime.Parse(node.InnerText);
            }

            XmlNodeList nodes = doc.GetElementsByTagName("DaysOfWeek")[0].ChildNodes;
            foreach (XmlNode node in nodes)
            {
                switch (node.Name)
                {
                    case "Monday":
                        chb_Monday.Checked = true;
                        break;
                    case "Tuesday":
                        chb_Tuesday.Checked = true;
                        break;
                    case "Wednesday":
                        chb_Wednesday.Checked = true;
                        break;
                    case "Thursday":
                        chb_Thursday.Checked = true;
                        break;
                    case "Friday":
                        chb_Friday.Checked = true;
                        break;
                    case "Saturday":
                        chb_Saturday.Checked = true;
                        break;
                    case "Sunday":
                        chb_Sunday.Checked = true;
                        break;
                }
            }
            return true;
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            if (TaskExist)
                DeleteTask();
            string command = "/C SCHTASKS ";
            command += "/Create ";
            command += "/SC WEEKLY /D \"";
            if (chb_Monday.Checked) command += "MON ";
            if (chb_Tuesday.Checked) command += "TUE ";
            if (chb_Wednesday.Checked) command += "WED ";
            if (chb_Thursday.Checked) command += "THU ";
            if (chb_Friday.Checked) command += "FRI ";
            if (chb_Saturday.Checked) command += "SAT ";
            if (chb_Sunday.Checked) command += "SUN ";
            command += "\"" + " /ST " + dateTimePicker1.Text + " /TN \"SiMed_Задача_" + TaskName + "\" /TR \"" +
                Application.ExecutablePath + " SendReports " + TaskName + "\"";

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            TaskExist = IsTaskExist(TaskName);

            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void B_OpenSheduler_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Windows\system32\taskschd.msc");
        }

        private void B_Delete_Click(object sender, EventArgs e)
        {
            DeleteTask();
            TaskExist = IsTaskExist(TaskName);
            this.Close();
        }

        void DeleteTask()
        {
            string command = "/C SCHTASKS /Delete /TN \"SiMed_Задача_" + TaskName + "\" /F";

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = command;
            p.StartInfo.Verb = "runas";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.WaitForExit();
        }

        private void F_SheduledSender_Load(object sender, EventArgs e)
        {

        }

        private void chb_Tuesday_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
