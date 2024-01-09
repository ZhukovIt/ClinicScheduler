using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SiMed.Clinic;
using SiMed.Clinic.Singleton;

namespace ClinicScheduler
{
    public partial class F_PropertiesTask : Form
    {
        private SendReportTaskProperties properties;
        public List<string> TaskNamesExists;
        private bool CancelSave;

        public string TaskName
        {
            get { return TB_TaskName.Text; }
        }

        public F_PropertiesTask(SendReportTaskProperties properties)
        {
            InitializeComponent();

            CancelSave = false;

            if (properties == null)
                this.properties = new SendReportTaskProperties("new_task");
            else
                this.properties = properties.Clone();

            TB_TaskName.Text = this.properties.TaskName;
            TB_LetterCaption.Text = this.properties.MailProperties.LetterCaption;
            TB_LetterMessage.Text = this.properties.MailProperties.LetterMessage;
            string tmp = "";
            for (int i = 0; i < this.properties.MailProperties.ToAddresses.Count; i++)
            {
                if (i > 0)
                    tmp += "\r\n";
                tmp += this.properties.MailProperties.ToAddresses[i];
            }
            TB_ToAddresses.Text = tmp;
            tmp = "";
            for (int i = 0; i < this.properties.MailProperties.CopyToAddresses.Count; i++)
            {
                if (i > 0)
                    tmp += "\r\n";
                tmp += this.properties.MailProperties.CopyToAddresses[i];
            }
            TB_CopyToAddresses.Text = tmp;

            ClinicReportsCollection collection = Reports.GetClinicReports();

            //добавление в дата грид настроенных отчетов
            for (int i = 0; properties != null && i < properties.SendReportsProperties.Count; i++)
            {
                ClinicReport report = collection.Find(o => o.ReportType == properties.SendReportsProperties[i].ReportType);
                DGV_Reports.Rows.Add();
                DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportType"].Value = report.ReportType;
                DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportCaption"].Value = report.Name;
                DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportFileName"].Value = properties.SendReportsProperties[i].GetValueFromName("FileName");
                DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportComment"].Value = properties.SendReportsProperties[i].ReportComment;
                DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportGUID"].Value = properties.SendReportsProperties[i].GUID;
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

        private bool SaveValidate()
        {
            if (!ValidateTaskName(TB_TaskName.Text))
            {
                MessageBox.Show("Название задачи содержит пробелы или специальные символы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (TaskNamesExists.IndexOf(TB_TaskName.Text) != -1)
            {
                MessageBox.Show("Задача с таким названием уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string[] Addresses = TB_ToAddresses.Text.Replace("\r\n", "\n").Split('\n');
            if (Addresses[0] == "")
            {
                MessageBox.Show("В поле \"Кому\" должен быть записан хотя бы один адрес", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            for (int i = 0; i < Addresses.Length; i++)
            {
                if (!Helpers.ValidateEMail(Addresses[i]))
                {
                    MessageBox.Show("Адреса получателей \"Кому\" должны быть написаны через перенос строки и соответствовать формату электронного адреса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            Addresses = TB_CopyToAddresses.Text.Replace("\r\n", "\n").Split('\n');
            if (Addresses[0] != "" || Addresses.Length > 1)
            {
                for (int i = 0; i < Addresses.Length; i++)
                {
                    if (!Helpers.ValidateEMail(Addresses[i]))
                    {
                        MessageBox.Show("Адреса получателей \"Копия\" должны быть написаны через перенос строки и соответствовать формату электронного адреса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            List<string> FileNames = new List<string>();
            string NewFileName;
            for (int i = 0; i < DGV_Reports.Rows.Count - 1; i++)
            {
                NewFileName = DGV_Reports.Rows[i].Cells["ReportFileName"].Value.ToString();

                if (!ValidateFileName(NewFileName))
                {
                    MessageBox.Show("В имени файла \"" + NewFileName + "\" обнаружены недопустимые символы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (FileNames.IndexOf(NewFileName) != -1)
                {
                    MessageBox.Show("Обнаружены одинаковые имена файлов \"" + NewFileName + "\". В рамках одной задачи это не допустимо.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                FileNames.Add(NewFileName);
            }


            return true;
        }

        private bool ValidateFileName(string FileName)
        {
            if (FileName.IndexOfAny(Path.GetInvalidPathChars()) == -1)
                return true;
            else
                return false;
        }

        private bool ValidateTaskName(string TaskName)
        {
            if (TaskName.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                return false;
            if (TaskName.IndexOf(' ') != -1)
                return false;

            return true;
        }


        private void B_OK_Click(object sender, EventArgs e)
        {
            if (!SaveValidate())
                return;

            properties.TaskName = TB_TaskName.Text;
            properties.MailProperties.LetterCaption = TB_LetterCaption.Text;
            properties.MailProperties.LetterMessage = TB_LetterMessage.Text;
            string[] tmp = TB_ToAddresses.Text.Replace("\r\n", "\n").Split('\n');
            properties.MailProperties.ToAddresses.Clear();
            for (int i = 0; i < tmp.Length; i++)
                properties.MailProperties.ToAddresses.Add(tmp[i]);
            tmp = TB_CopyToAddresses.Text.Replace("\r\n", "\n").Split('\n');
            properties.MailProperties.CopyToAddresses.Clear();
            for (int i = 0; tmp[0] != "" && i < tmp.Length; i++)
                properties.MailProperties.CopyToAddresses.Add(tmp[i]);


            ClinicReportsCollection collection = Reports.GetClinicReports();

            //сохранить имена файлов и комментариев (для отчетов)
            for (int i = 0; i < properties.SendReportsProperties.Count; i++)
            {
                for (int j = 0; j < DGV_Reports.Rows.Count - 1; j++)
                {
                    string ReportGUID = properties.SendReportsProperties[i].GUID;
                    if (ReportGUID == DGV_Reports.Rows[j].Cells["ReportGUID"].Value.ToString())
                    {
                        if (DGV_Reports.Rows[j].Cells["ReportComment"].Value == null)
                            properties.SendReportsProperties[i].ReportComment = "";
                        else
                            properties.SendReportsProperties[i].ReportComment = DGV_Reports.Rows[j].Cells["ReportComment"].Value.ToString();
                        string FileName = DGV_Reports.Rows[j].Cells["ReportFileName"].Value.ToString();
                        if (FileName == "")
                            FileName = "unnamed.xls";
                        if (FileName.Substring(FileName.Length - 4) != ".xls")
                            FileName += ".xls";
                        SetFileName(properties.SendReportsProperties[i].ReportType, FileName, ReportGUID); 
                    }
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public SendReportTaskProperties GetNewProperties()
        {
            return properties;
        }

        private void DGV_Reports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //удаление существующих настроек для отчета
            if (DGV_Reports.Columns[e.ColumnIndex].Name == "ReportDelete" && e.RowIndex < DGV_Reports.Rows.Count - 1)
            {
                ClinicReportType ReportType = (ClinicReportType)Enum.Parse(typeof(ClinicReportType), DGV_Reports.Rows[e.RowIndex].Cells["ReportType"].Value.ToString());
                string ReportGUID = DGV_Reports.Rows[e.RowIndex].Cells["ReportGUID"].Value.ToString();
                for (int i = 0; i < properties.SendReportsProperties.Count; i++)
                    if (properties.SendReportsProperties[i].GUID == ReportGUID)
                    {
                        properties.SendReportsProperties.Remove(properties.SendReportsProperties[i]);
                        DGV_Reports.Rows.Remove(DGV_Reports.Rows[e.RowIndex]);
                        break;
                    }
            }
            //предпросмотр отчета
            else if (DGV_Reports.Columns[e.ColumnIndex].Name == "ReportPreview" && e.RowIndex < DGV_Reports.Rows.Count - 1)
            {
                ClinicReportType ReportType = (ClinicReportType)Enum.Parse(typeof(ClinicReportType), DGV_Reports.Rows[e.RowIndex].Cells["ReportType"].Value.ToString());
                string ReportGUID = DGV_Reports.Rows[e.RowIndex].Cells["ReportGUID"].Value.ToString();
                for (int i = 0; i < properties.SendReportsProperties.Count; i++)
                    if (properties.SendReportsProperties[i].GUID == ReportGUID)
                    {
                        SiMed.Clinic.GUI.RepForm.ExecuteReport(properties.SendReportsProperties[i].ReportType, "", properties.SendReportsProperties[i].ReportParameters);
                        break;
                    }
            }
            else if (DGV_Reports.Columns[e.ColumnIndex].Name == "ReportEdit")
            {
                if (e.RowIndex == DGV_Reports.Rows.Count - 1) //добавление нового отчета
                {
                    F_ReportSelect form = new F_ReportSelect();

                    if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    ClinicReportType ReportType = form.ReportType;
                    string ReportCaption = form.ReportCaption;
                    string FileName = GenereteFileName(form.FileName);

                    string ReportGUID = SetReportParameters(false, ReportType, "");

                    if (String.IsNullOrEmpty(ReportGUID))
                        return;

                    DGV_Reports.Rows.Add();
                    DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportType"].Value = ReportType.ToString();
                    DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportCaption"].Value = ReportCaption.ToString();
                    DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportFileName"].Value = FileName.ToString();

                    DGV_Reports.Rows[DGV_Reports.Rows.Count - 2].Cells["ReportGUID"].Value = ReportGUID;

                    SetFileName(ReportType, FileName, ReportGUID);
                }
                else if (e.RowIndex >= 0) //редактирование существующих настроек
                {
                    ClinicReportType ReportType = (ClinicReportType)Enum.Parse(typeof(ClinicReportType), DGV_Reports.Rows[e.RowIndex].Cells["ReportType"].Value.ToString());
                    string FileName = DGV_Reports.Rows[e.RowIndex].Cells["ReportFileName"].Value.ToString();
                    string ReportGUID = DGV_Reports.Rows[e.RowIndex].Cells["ReportGUID"].Value.ToString();

                    ReportGUID = SetReportParameters(true, ReportType, ReportGUID);

                    if (String.IsNullOrEmpty(ReportGUID))
                        return;

                    DGV_Reports.Rows[e.RowIndex].Cells["ReportGUID"].Value = ReportGUID;

                    SetFileName(ReportType, FileName, ReportGUID);
                }
            }
        }

        private string SetReportParameters(bool pChangeParameters, ClinicReportType ReportType, string ReportGUID)
        {
            SiMed.Clinic.GUI.FormReport form = null;
            SendReportProperties srp = new SendReportProperties();
            int OldParamsIndex = -1;
            ReportParameters OldParameters = new ReportParameters();
            ReportParameters NewParameters = null;

            for (int i = 0; pChangeParameters == true && i < properties.SendReportsProperties.Count; i++)
                if (properties.SendReportsProperties[i].GUID == ReportGUID)
                {
                    srp = properties.SendReportsProperties[i];
                    OldParameters = srp.ReportParameters;
                    OldParamsIndex = i;
                    break;
                }

            form = SiMed.Clinic.GUI.ClinicGUIReportManager.GetFormReport(ReportType, OldParameters);

            if (form == null)
                return "";

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NewParameters = form.ReportParameters;
                SendReportProperties item = new SendReportProperties(ReportType, NewParameters);
                if (OldParamsIndex != -1)
                    properties.SendReportsProperties.Remove(properties.SendReportsProperties[OldParamsIndex]);
                properties.SendReportsProperties.Add(item);
                return item.GUID;
            }

            return "";
        }

        private void SetFileName(ClinicReportType ReportType, string FileName, string ReportGUID)
        {
            if (String.IsNullOrEmpty(ReportGUID))
                return;
            SendReportProperties srp = new SendReportProperties();
            for (int i = 0; i < properties.SendReportsProperties.Count; i++)
                if (properties.SendReportsProperties[i].GUID == ReportGUID)
                {
                    for (int j = 0; j < properties.SendReportsProperties[i].ReportParameters.Parameters.Count; j++)
                        if (properties.SendReportsProperties[i].ReportParameters.Parameters[j].ParamName == "FileName")
                        {
                            properties.SendReportsProperties[i].ReportParameters.Parameters[j].ParamValue = FileName;
                            return;
                        }
                    properties.SendReportsProperties[i].ReportParameters.Parameters.Add(new Params("FileName", FileName));
                    return;
                }
        }

        private void DGV_Reports_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (DGV_Reports.Rows[e.RowIndex].Cells["ReportType"].Value == null)
            {
                DGV_Reports.CancelEdit();
            }
        }

        string GenereteFileName(string FileName)
        {
            string NewFileName = FileName;
            string BodyName = NewFileName;
            if (BodyName.Substring(BodyName.Length - 4) == ".xls")
                BodyName = BodyName.Substring(0, BodyName.Length - 4);

            bool CheckFiles = false;
            for (int i = 0; !CheckFiles; i++)
            {
                CheckFiles = true;

                if (i > 0)
                    NewFileName = BodyName + "_" + i.ToString() + ".xls";

                for (int f = 0; f < DGV_Reports.Rows.Count - 1; f++)
                {
                    if (NewFileName == DGV_Reports.Rows[f].Cells["ReportFileName"].Value.ToString())
                    {
                        CheckFiles = false;
                        break;
                    }
                }
            }
            return NewFileName;
        }

        private void F_PropertiesTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!CancelSave)
                {
                    System.Windows.Forms.DialogResult dr = MessageBox.Show("Вы желаете сохранить настройки?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!SaveValidate())
                            e.Cancel = true;
                        else
                            B_OK_Click(sender, new EventArgs());
                    }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                        e.Cancel = true;
                    else
                        DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }
    }
}
