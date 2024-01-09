using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinicScheduler
{
    public partial class F_PropertiesSend : Form
    {
        private AppConfig config;
        private bool CancelSave;

        public F_PropertiesSend(AppConfig config)
        {
            InitializeComponent();

            CancelSave = false;

            this.TB_SmtpServer.Items.Add("smtp.gmail.com");
            this.TB_SmtpServer.Items.Add("smtp.mail.ru");
            this.TB_SmtpServer.Items.Add("smtp.rambler.ru");
            this.TB_SmtpServer.Items.Add("smtp.yandex.ru");    

            this.config = config;
            this.TB_SmtpServer.Text = config.MailGlobalProperties.SmtpServer;
            this.TB_FromAddress.Text = config.MailGlobalProperties.FromAddress;
            this.TB_FromPassword.Text = config.MailGlobalProperties.FromPassword;
            this.TB_SenderAddress.Text = config.MailGlobalProperties.SenderAddress;
            this.CB_EnableSSL.Checked = config.MailGlobalProperties.EnableSSL;
            this.CB_IsBodyHTML.Checked = config.MailGlobalProperties.IsBodyHTML;
            this.TB_MailBodyEncoding.Text = config.MailGlobalProperties.MailBodyEncoding;
            this.TB_Port.Text = config.MailGlobalProperties.Port.ToString();
            this.TB_AttachedFilesDirectory.Text = config.MailGlobalProperties.AttachedFilesDirectory;

            DGV_Tasks_Update();
        }

        private void DGV_Tasks_Update()
        {
            DGV_Tasks.Rows.Clear();
            for (int i = 0; i < config.SendReportTasksProperties.Count; i++)
            {
                DGV_Tasks.Rows.Add();
                string tmp = "";

                DGV_Tasks.Rows[i].Cells["TaskName"].Value = config.SendReportTasksProperties[i].TaskName;
                for (int k = 0; k < config.SendReportTasksProperties[i].MailProperties.ToAddresses.Count; k++)
                {
                    if (k > 0)
                        tmp += ", ";
                    tmp += config.SendReportTasksProperties[i].MailProperties.ToAddresses[k];
                }
                DGV_Tasks.Rows[i].Cells["AddressesTo"].Value = tmp;
                tmp = "";
                for (int k = 0; k < config.SendReportTasksProperties[i].SendReportsProperties.Count; k++)
                {
                    if (k > 0)
                        tmp += ", ";
                    tmp += config.SendReportTasksProperties[i].SendReportsProperties[k].ReportType.ToString();
                }
                DGV_Tasks.Rows[i].Cells["Reports"].Value = tmp;
                DGV_Tasks.Rows[i].Cells["RunsOnShedule"].Value = config.SendReportTasksProperties[i].RunsOnShedule;
            }
        }

        private void TSB_AddTask_Click(object sender, EventArgs e)
        {
            F_PropertiesTask f_task = new F_PropertiesTask(null);
            f_task.TaskNamesExists = new List<string>();
            for (int i = 0; i < config.SendReportTasksProperties.Count; i++)
                f_task.TaskNamesExists.Add(config.SendReportTasksProperties[i].TaskName);

            bool test = false;

            while (!test)
            {
                test = true;

                if (f_task.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                if (f_task.TaskName == "")
                {
                    test = false;
                    MessageBox.Show("Необходимо ввести название задачи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                for (int i = 0; i < DGV_Tasks.Rows.Count; i++)
                    if (DGV_Tasks.Rows[i].Cells["TaskName"].Value.ToString() == f_task.TaskName)
                    {
                        test = false;
                        break;
                    }
                if (!test)
                {
                    MessageBox.Show("Задача с таким названием уже существует. Задайте другое название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
            }

            config.SendReportTasksProperties.Add(f_task.GetNewProperties());
            DGV_Tasks_Update();
        }

        private void TSB_EditTask_Click(object sender, EventArgs e)
        {
            int Task_Index = -1;
            if (DGV_Tasks.SelectedRows.Count > 0)
                Task_Index = DGV_Tasks.SelectedRows[0].Index;
            else if (DGV_Tasks.SelectedCells.Count > 0)
                Task_Index = DGV_Tasks.SelectedCells[0].RowIndex;

            if (Task_Index == -1)
            {
                MessageBox.Show("Выберите задачу для редактирования");
                return;
            }

            F_PropertiesTask f_task = new F_PropertiesTask(config.SendReportTasksProperties[Task_Index]);
            f_task.TaskNamesExists = new List<string>();
            for (int i = 0; i < config.SendReportTasksProperties.Count; i++)
            {
                if (i == Task_Index)
                    continue;
                f_task.TaskNamesExists.Add(config.SendReportTasksProperties[i].TaskName);
            }

            bool test = false;

            while (!test)
            {
                test = true;

                if (f_task.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                if (f_task.TaskName == "")
                {
                    test = false;
                    MessageBox.Show("Необходимо ввести название задачи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                for (int i = 0; i < DGV_Tasks.Rows.Count; i++)
                    if (Task_Index != i && DGV_Tasks.Rows[i].Cells["TaskName"].Value.ToString() == f_task.TaskName)
                    {
                        test = false;
                        break;
                    }
                if (!test)
                {
                    MessageBox.Show("Задача с таким названием уже существует. Задайте другое название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
            }

            config.SendReportTasksProperties.Remove(config.SendReportTasksProperties[Task_Index]);
            config.SendReportTasksProperties.Insert(Task_Index, f_task.GetNewProperties());
            DGV_Tasks_Update();
        }

        private void TSB_RemoveTask_Click(object sender, EventArgs e)
        {
            int Task_Index = -1;
            if (DGV_Tasks.SelectedRows.Count > 0)
                Task_Index = DGV_Tasks.SelectedRows[0].Index;
            else if (DGV_Tasks.SelectedCells.Count > 0)
                Task_Index = DGV_Tasks.SelectedCells[0].RowIndex;

            if (Task_Index == -1)
            {
                MessageBox.Show("Выберите задачу для удаления");
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить задачу со всеми ее настройками?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
                return;

            config.SendReportTasksProperties.Remove(config.SendReportTasksProperties[Task_Index]);
            DGV_Tasks_Update();
        }

        private void TSB_SheduledSender_Click(object sender, EventArgs e)
        {
            int ind = DGV_Tasks.SelectedCells[0].RowIndex;
            F_SheduledSender f_sender = new F_SheduledSender(DGV_Tasks.Rows[ind].Cells["TaskName"].Value.ToString());
            f_sender.ShowDialog();
            if(config.SendReportTasksProperties[ind].RunsOnShedule != f_sender.TaskExist)
            {
                config.SendReportTasksProperties[ind].RunsOnShedule = f_sender.TaskExist;
                config.SaveConfigFile();
                DGV_Tasks.Rows[ind].Cells["RunsOnShedule"].Value = f_sender.TaskExist;
            }

        }

        private void B_TestSend_Click(object sender, EventArgs e)
        {
            F_TestAddressSelect f_address_select = new F_TestAddressSelect(TB_FromAddress.Text);
            string AddressTo;
            if (f_address_select.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            AddressTo = f_address_select.address;
            List<string> AddressesTo = new List<string>();
            AddressesTo.Add(AddressTo);

            try
            {
                SendMail mail = new SendMail(pSmtpServer: TB_SmtpServer.Text,
                                             pFromAddress: TB_FromAddress.Text,
                                             pFromPassword: TB_FromPassword.Text,
                                             pSenderAddress: TB_SenderAddress.Text,
                                             pEnableSSL: CB_EnableSSL.Checked,
                                             pPort: Convert.ToInt32(TB_Port.Text),
                                             pMailBodyEncoding: TB_MailBodyEncoding.Text,
                                             pIsBodyHTML: CB_IsBodyHTML.Checked);

                mail.Send(ptoAddresses: AddressesTo,
                    pCopyToAddresses: new List<string>(),
                    pletterCaption: "Тестовое сообщение",
                    pletterMessage: "Данное сообщение пришло из конфигуратора настоек отчетов для отправки. На него отвечать не нужно",
                    pattachFile: new List<string>());

                if (mail.ErrorMessage != "")
                {
                    L_Status.Text = "Статус: ОШИБКА";
                    L_Error.Text = mail.ErrorMessage;
                    B_Error.Visible = true;
                }
                else
                {
                    L_Status.Text = "Статус: ОК";
                    L_Error.Text = "";
                    B_Error.Visible = false;
                }
            }
            catch (Exception exc)
            {
                L_Status.Text = "Статус: ОШИБКА";
                L_Error.Text = exc.Message;
                B_Error.Visible = true;
            }
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти без сохранения изменений?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                CancelSave = true;
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            config.MailGlobalProperties.SmtpServer = this.TB_SmtpServer.Text;
            config.MailGlobalProperties.FromAddress = this.TB_FromAddress.Text;
            config.MailGlobalProperties.FromPassword = this.TB_FromPassword.Text;
            config.MailGlobalProperties.SenderAddress = this.TB_SenderAddress.Text;
            config.MailGlobalProperties.EnableSSL = this.CB_EnableSSL.Checked;
            config.MailGlobalProperties.IsBodyHTML = this.CB_IsBodyHTML.Checked;
            config.MailGlobalProperties.MailBodyEncoding = this.TB_MailBodyEncoding.Text;
            config.MailGlobalProperties.Port = Convert.ToInt32(this.TB_Port.Text);
            config.MailGlobalProperties.AttachedFilesDirectory = this.TB_AttachedFilesDirectory.Text;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void B_Error_Click(object sender, EventArgs e)
        {
            MessageBox.Show(L_Error.Text);
        }

        private void F_PropertiesSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!CancelSave)
                {
                    System.Windows.Forms.DialogResult dr = MessageBox.Show("Вы желаете сохранить настройки?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                        B_OK_Click(sender, new EventArgs());
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                        e.Cancel = true;
                    else
                        DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }


        private void TSB_ExecuteTask_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("Отправить выбранное письмо?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AppConfig config = AppConfig.GetPropertiesFromConfigFile();
            config.SendReportTasksProperties = this.config.SendReportTasksProperties;
            config.MailGlobalProperties.SmtpServer = this.TB_SmtpServer.Text;
            config.MailGlobalProperties.FromAddress = this.TB_FromAddress.Text;
            config.MailGlobalProperties.FromPassword = this.TB_FromPassword.Text;
            config.MailGlobalProperties.SenderAddress = this.TB_SenderAddress.Text;
            config.MailGlobalProperties.EnableSSL = this.CB_EnableSSL.Checked;
            config.MailGlobalProperties.IsBodyHTML = this.CB_IsBodyHTML.Checked;
            config.MailGlobalProperties.MailBodyEncoding = this.TB_MailBodyEncoding.Text;
            config.MailGlobalProperties.Port = Convert.ToInt32(this.TB_Port.Text);
            config.MailGlobalProperties.AttachedFilesDirectory = this.TB_AttachedFilesDirectory.Text;
            ProgramStart.SendReports(DGV_Tasks.SelectedCells[0].RowIndex, ref config);
        }
    }
}
