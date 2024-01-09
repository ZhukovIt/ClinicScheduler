using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using SiMed.Clinic;
using System.Threading;

namespace ClinicScheduler
{
    public class AppConfig
    {
        const string mutex_id = "Global\\{31D6530E-4CC4-4BE3-9C51-547371B406C2}";

        const string NewConfigFileName = "AppConfig.xml";

        public MailGlobalProperties MailGlobalProperties;
        public List<SendReportTaskProperties> SendReportTasksProperties;
        public LaboratoryTestsResultsProperties LaboratoryTestsResultsProperties;
        public SiMedMobileReceivedProcessedProperties SiMedMobileReceivedProcessedProperties;
        public SiMedAutoCreateAccountingActsProperties SiMedAutoCreateAccountingActsProperties;
        public SiMedAutoCheckAccountingActsProperties SiMedAutoCheckAccountingActsProperties;
        public ReceptionRecordsReminderProperties ReceptionRecordsReminderProperties;
        public SendDefferedNotificationsProperties SendDefferedNotificationsProperties;
        public EmployerWebTasksProperties EmployerWebTasksProperties;
        public CheckIEMKRecordsStateProperties CheckIEMKRecordsStateProperties;

        public AppConfig()
        {
            MailGlobalProperties = new MailGlobalProperties();
            SendReportTasksProperties = new List<SendReportTaskProperties>();
        }
        public static void GenerateConfigFile()
        {
            using (var mutex = new Mutex(false, mutex_id))
            {
                bool mutex_wait_result = false;
                try
                {
                    mutex_wait_result = mutex.WaitOne(TimeSpan.FromSeconds(5));
                    if (!mutex_wait_result)
                        throw new Exception("Метод AppConfig.GenerateConfigFile. Не удалось получить Mutex для доступа к файлу настроек в течение 5 секунд");

                    string Directory = Application.StartupPath + "\\";
                    XmlSerializer ser = new XmlSerializer(typeof(AppConfig));
                    if (File.Exists(Directory + NewConfigFileName))
                    {
                        if (MessageBox.Show("Файл с настройками уже существует. Хотите заменить его?", "Внимание", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            return;
                        File.Delete(Directory + NewConfigFileName);
                    }

                    AppConfig properties = new AppConfig();
                    Stream stream = new FileStream(Directory + NewConfigFileName, FileMode.Create);
                    ser.Serialize(stream, properties);
                    stream.Flush();
                    stream.Close();
                }
                finally
                {
                    if (mutex_wait_result)
                        mutex.ReleaseMutex();
                }
            }
        }

        public void SaveConfigFile()
        {
            using (var mutex = new Mutex(false, mutex_id))
            {
                bool mutex_wait_result = false;
                try
                {
                    mutex_wait_result = mutex.WaitOne(TimeSpan.FromSeconds(5));
                    if (!mutex_wait_result)
                        throw new Exception("Метод AppConfig.GenerateConfigFile. Не удалось получить Mutex для доступа к файлу настроек в течение 5 секунд");

                    string Directory = Application.StartupPath + "\\";

                    XmlSerializer ser = new XmlSerializer(typeof(AppConfig));
                    if (File.Exists(Directory + NewConfigFileName))
                        File.Delete(Directory + NewConfigFileName);

                    Stream stream = new FileStream(Directory + NewConfigFileName, FileMode.Create);
                    ser.Serialize(stream, this);
                    stream.Flush();
                    stream.Close();
                }
                finally
                {
                    if (mutex_wait_result)
                        mutex.ReleaseMutex();
                }
            }
        }

        public static AppConfig GetPropertiesFromConfigFile()
        {
            using (var mutex = new Mutex(false, mutex_id))
            {
                bool mutex_wait_result = false;
                try
                {
                    mutex_wait_result = mutex.WaitOne(TimeSpan.FromSeconds(5));
                    if (!mutex_wait_result)
                        throw new Exception("Метод AppConfig.GenerateConfigFile. Не удалось получить Mutex для доступа к файлу настроек в течение 5 секунд");

                    string Directory = Application.StartupPath + "\\";

                    XmlSerializer ser = new XmlSerializer(typeof(AppConfig));
                    AppConfig properties = new AppConfig();
                    Stream stream = new FileStream(Directory + NewConfigFileName, FileMode.Open);
                    try
                    {
                        properties = (AppConfig)ser.Deserialize(stream);
                    }
                    catch (Exception)
                    {
                        properties = new AppConfig();
                    }
                    stream.Flush();
                    stream.Close();
                    return properties;
                }
                finally
                {
                    if (mutex_wait_result)
                        mutex.ReleaseMutex();
                }
            }
        }
    }

    public class LaboratoryTestsResultsProperties
    {
        public int ClinicId;
        public int BranchId;
        public int NumberOfDays;
        public int UserId;
    }

    public class CheckIEMKRecordsStateProperties
    {
        public int ClinicId;
        public int BranchId;
        public int NumberOfDays;
        public int UserId;
    }

    public class BaseSiMedTaskProperties
    {
        public int MedOrgId;
        public int UserId;
    }

    public class SiMedAutoCreateAccountingActsProperties : BaseSiMedTaskProperties
    {
        public bool SignEDocuments;
        public bool SendEDocuments;
        public string CopyToEmailAddress;
    }

    public class SiMedAutoCheckAccountingActsProperties : BaseSiMedTaskProperties
    {
        public string CopyToEmailAddress;
        public bool CreatePretensionEDocuments;
        public bool SignPretensionEDocuments;
    }

    public class SiMedMobileReceivedProcessedProperties : BaseSiMedTaskProperties { }

    public class ReceptionRecordsReminderProperties : BaseSiMedTaskProperties { }

    public class SendDefferedNotificationsProperties : BaseSiMedTaskProperties { }

    public class EmployerWebTasksProperties : BaseSiMedTaskProperties
    {
        public string TaskNewFolder;
        public string TaskProcessFolder;
        public string TaskArchiveFolder;
        public string TaskResultFolder;
        public string TaskTmpFolder;
        public bool ZipMIResultWithExtraction;
        public int SendSpeedKbps;

        public int? MedInspectionRequestsCreatedFromUserId;
    }
    public class SendReportTaskProperties
    {
        public string TaskName;
        public MailProperties MailProperties;
        public List<SendReportProperties> SendReportsProperties;
        public bool RunsOnShedule;

        public SendReportTaskProperties() { }
        public SendReportTaskProperties(string TaskName)
        {
            this.TaskName = TaskName;
            MailProperties = new MailProperties();
            SendReportsProperties = new List<SendReportProperties>();
            RunsOnShedule = false;
        }

        public SendReportTaskProperties Clone()
        {
            SendReportTaskProperties properties = new SendReportTaskProperties();
            properties.TaskName = TaskName;
            properties.MailProperties = MailProperties.Clone();
            properties.SendReportsProperties = new List<SendReportProperties>();
            properties.RunsOnShedule = RunsOnShedule;
            for (int i = 0; i < SendReportsProperties.Count; i++)
                properties.SendReportsProperties.Add(SendReportsProperties[i].Clone());
            return properties;
        }
    }

    public class SendReportProperties
    {
        public ClinicReportType ReportType;
        public string ReportComment;
        public string GUID;
        public ReportParameters ReportParameters;

        public SendReportProperties() { }
        public SendReportProperties(ClinicReportType ReportType, ReportParameters parameters)
        {
            this.ReportType = ReportType;
            ReportParameters = parameters;
            GUID = Guid.NewGuid().ToString();
        }

        public string GetValueFromName (string ParamName)
        {
            for (int i = 0; i < ReportParameters.Parameters.Count; i++)
            {
                if (ReportParameters.Parameters[i].ParamName == ParamName)
                {
                    return ReportParameters.Parameters[i].ParamValue;
                }
            }
            return "";
        }

        public SendReportProperties Clone()
        {
            SendReportProperties properties = new SendReportProperties();

            properties.ReportType = ReportType;
            properties.ReportComment = ReportComment;
            properties.GUID = GUID;
            properties.ReportParameters = new SiMed.Clinic.ReportParameters();
            for (int i = 0; i < ReportParameters.Parameters.Count; i++)
                properties.ReportParameters.Parameters.Add(ReportParameters.Parameters[i]);

            return properties;
        }
    }

    public class MailGlobalProperties
    {
        public string SmtpServer = "smtp.mail.ru";
        public string FromAddress = "author@mail.ru";
        public string FromPassword = "some_password";
        public string SenderAddress = "author@mail.ru";
        public string MailBodyEncoding = "UTF-8";
        public bool EnableSSL = true;
        public int Port = 587;
        public bool IsBodyHTML = false;
        public string AttachedFilesDirectory = @"Tmp\";
    }

    public class MailProperties
    {
        public List<string> ToAddresses;
        public List<string> CopyToAddresses;
        public string LetterCaption = "Letter Caption";
        public string LetterMessage = "Letter Message";

        public MailProperties()
        {
            ToAddresses = new List<string>();
            CopyToAddresses = new List<string>();
        }

        public MailProperties Clone()
        {
            MailProperties properties = new MailProperties();
            for (int i = 0; i < ToAddresses.Count; i++)
                properties.ToAddresses.Add(ToAddresses[i]);
            for (int i = 0; i < CopyToAddresses.Count; i++)
                properties.CopyToAddresses.Add(CopyToAddresses[i]);
            properties.LetterCaption = LetterCaption;
            properties.LetterMessage = LetterMessage;
            return properties;
        }
    }
    public class DaysOfWeekChecked
    {
        public bool Monday;
        public bool Tuesday;
        public bool Wednesday;
        public bool Thursday;
        public bool Friday;
        public bool Saturday;
        public bool Sunday;

        public bool Hourly;
        public int Minutes = 30;

        public DateTime Time;
    }
}
