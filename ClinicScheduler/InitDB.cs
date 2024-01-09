using System;
using System.Data;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration.Install;
using System.Configuration;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Management;
using System.Data.SqlClient;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using Simplex.Licensing;
using SiMed.Clinic.DataModel;

namespace SiMed.Clinic
{
    //*
    //namespace Properties
    //{
    //    partial class Settings
    //    {
    //        public void SetProperty(string _PropertyName, string _Value)
    //        {
    //            this[_PropertyName] = _Value;
    //        }

    //    }//* /
    //}
    //---------------------------------------------
    public class CClinicApp
    {

        private SiMedDB m_CurrentDB;
        private Logger.CLogger m_Logger;
        //private List<CPrintForm> m_PrintForms;

        private string m_SettingsFileName = "settings.xml";
        private string m_ReportsMappingFileName = "ReportsMapping.xml";
        public string cData;
        public string cSetupID;

        private ClinicLicenseInfo m_LicenseInfo;
        private GUI.FormProgressBar m_FormProgressBar;
        private AutoResetEvent m_StopProgressBar;



        //---------------------------------------------
        public ClinicLicenseInfo LicenseInfo
        {
            get { return m_LicenseInfo; }
        }
        //---------------------------------------------
        public DataModel.SiMedDB CurrentDB
        {
            get { return m_CurrentDB; }
        }
        //---------------------------------------------
        public Logger.CLogger Logger
        {
            get { return m_Logger; }
        }
        //---------------------------------------------
        public void UpdateConnectionStrings(string csName, string connectionString)
        {
            Singleton.ClinicConnection.MainConnection.ConnectionString = connectionString;
            Singleton.ClinicConnection.SecondaryConnection.ConnectionString = connectionString;
        }
        //---------------------------------------------
        public int GetVersionDB()
        {
            string str = ClinicScheduler.Properties.Settings.Default.clinicConnectionString;
            SqlConnectionStringBuilder SCSB = new SqlConnectionStringBuilder(str);
            SqlConnection DBConnection = new SqlConnection(SCSB.ConnectionString);

            string strSQL = "SELECT MAX(VERSION) as ver FROM DB_VERSION WHERE TYPE=1";
            SqlCommand Command = new SqlCommand(strSQL, DBConnection);
            SqlDataReader DataReader = null;
            try
            {
                if (DBConnection.State != ConnectionState.Open)
                    DBConnection.Open();
                DataReader = Command.ExecuteReader();
                int Version = 0;
                while (DataReader.Read())
                {
                    Version = (int)DataReader["ver"];
                    break;
                }
                DataReader.Close();
                DBConnection.Close();
                return Version;
            }
            catch (Exception ex)
            {
                string Err = "Ошибка получения версии базы данных СиМед-Клиника :" + ex.Message;
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm", "Ошибка получения версии базы данных СиМед-Клиника");
                MessageBox.Show(Err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }
        //---------------------------------------------
        public bool LoadSettings()
        {
            try
            {
                string SettingsFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                        ProductInfo.AppDataFolder,
                                        m_SettingsFileName);
                if (!System.IO.File.Exists(SettingsFileName))
                    SettingsFileName = m_SettingsFileName;
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(SettingsFileName);
                Singleton.Reports.MainReportsSettings.LoadSettings(doc);
                Singleton.Reports.SmartButtons.LoadSettings(doc);
            }
            catch (Exception ex)
            {
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm", "Ошибка чтения настроек печатных форм");
                return false;
            }
            return true;
        }
        //---------------------------------------------
        public bool Init()
        {
            ProductInfo.Trial = true;

            Singleton.ClinicOptions.MainOptions = new Options.OptionsManager();
            Singleton.ClinicOptions.MainOptions.LoadLocalOptions();

            m_Logger = new Logger.CLogger();

            Clinic.Logger.LogEvent.SaveInformationToLog("Запуск программы", "Programm");

            ProductInfo.Trial = false;
            m_LicenseInfo = new ClinicLicenseInfo();
            this.LicenseInfo.MainModule = true;
            Singleton.ClinicOptions.MainOptions.GeneralUserOptions.nMaxShowingPersonsCount = 100;

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config == null)
                throw (new System.IO.FileNotFoundException("Не найден конфигурационный файл"));

            string str = config.ConnectionStrings.ConnectionStrings["SiMed.Clinic.Properties.Settings.clinicConnectionString"].ConnectionString;
            SqlConnectionStringBuilder SCSB = new SqlConnectionStringBuilder(str);

            SqlConnection con = new SqlConnection(SCSB.ConnectionString);
            bool bConnected = false;
            DatabaseInteractionException ConnectionException = null;
            try
            {
                DbHelpers.ProcessAction(con.Open);
                //con.Open();

                con.Close();
                bConnected = true;
            }
            catch (DatabaseInteractionException ex)
            {
                ConnectionException = ex;
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm");
            }
            catch (Exception ex)
            {
                string ErrMes = "Не удалось подключиться к SQL-серверу. Работа программы невозможна. " +
                                Environment.NewLine + "Получено следующее сообщение об ошибке: " + Environment.NewLine +
                                ex.Message;
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm", ErrMes);
                MessageBox.Show(ErrMes, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            Singleton.ClinicConnection.MainConnection = new SqlConnection(str);
            Singleton.ClinicConnection.SecondaryConnection = new SqlConnection(str);
            Singleton.ClinicConnection.PersonConnection = new SqlConnection(str);

            m_CurrentDB = new DataModel.SiMedDB();
            Singleton.ClinicDatabase.SiMedDatabase = m_CurrentDB;            
        
            bool b = m_CurrentDB.Init();

            //Загружаем общесистемные настройки из БД
            Singleton.ClinicOptions.MainOptions.LoadSystemOptions();

            b = Singleton.Reports.LoadReports();

            Singleton.Reports.PathToDocs = ClinicScheduler.Properties.Settings.Default.PathToDocs;
            Singleton.Reports.PathToReports = ClinicScheduler.Properties.Settings.Default.PathToReports;

            LoadSettings();
            Singleton.Reports.LoadReportsMapping(m_ReportsMappingFileName);

            //GUI.FormUserLogin form = new GUI.FormUserLogin();
            //DialogResult UserAuthenticateResult = form.ShowDialog();
            //if (UserAuthenticateResult != DialogResult.OK)
            //    return false;
            return true;
        }
        //---------------------------------------------
        public bool InitSiMedDB()
        {
            ProductInfo.Trial = true;

             

            Singleton.ClinicOptions.MainOptions = new Options.OptionsManager();
            Singleton.ClinicOptions.MainOptions.LoadLocalOptions();

            m_Logger = new Logger.CLogger();

            Clinic.Logger.LogEvent.SaveInformationToLog("Запуск программы", "Programm");

            ProductInfo.Trial = false;
            m_LicenseInfo = new ClinicLicenseInfo();
            this.LicenseInfo.MainModule = true;
            Singleton.ClinicOptions.MainOptions.GeneralUserOptions.nMaxShowingPersonsCount = 100;

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config == null)
                throw (new System.IO.FileNotFoundException("Не найден конфигурационный файл"));

            string str = config.ConnectionStrings.ConnectionStrings["SiMed.Clinic.Properties.Settings.clinicConnectionString"].ConnectionString;
            SqlConnectionStringBuilder SCSB = new SqlConnectionStringBuilder(str);

            SqlConnection con = new SqlConnection(SCSB.ConnectionString);
            bool bConnected = false;
            DatabaseInteractionException ConnectionException = null;
            try
            {
                DbHelpers.ProcessAction(con.Open);
                //con.Open();

                con.Close();
                bConnected = true;
            }
            catch (DatabaseInteractionException ex)
            {
                ConnectionException = ex;
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm");
            }
            catch (Exception ex)
            {
                string ErrMes = "Не удалось подключиться к SQL-серверу. Работа программы невозможна. " +
                                Environment.NewLine + "Получено следующее сообщение об ошибке: " + Environment.NewLine +
                                ex.Message;
                ClinicScheduler.ProgramStart.WriteToLog(ErrMes, true);
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm", ErrMes);
                MessageBox.Show(ErrMes, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //Проверяем версию БД
            /* отключаем проверку на версию базы данных и ПО
            try
            {
                int Version = GetVersionDB();
                if (Version < CurrentDBVersion)
                {
                    string ErrMes = "Версия используемой Вами базы данных СиМед-Клиника устарела. Для получения информации о способах обновления базы данных СиМед-Клиника обратитесь на сайт разработчика: www.simplex48.ru";
                    Clinic.Logger.LogEvent.SaveWarrningToLog(ErrMes, "Programm");
                    MessageBox.Show(ErrMes, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                else if (Version > CurrentDBVersion)
                {
                    string ErrMes = "Версия используемого Вами программного обеспечения СиМед-Клиника устарела. Для получения информации о способах обновления програмного обеспечения СиМед-Клиника обратитесь на сайт разработчика: www.simplex48.ru";
                    Clinic.Logger.LogEvent.SaveWarrningToLog(ErrMes, "Programm");
                    MessageBox.Show(ErrMes, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Clinic.Logger.LogEvent.SaveExceptionToLog(ex, "Programm");
            }
            //*/

            Singleton.LaboratoryManager.MainManager = new SiMed.Clinic.GUI.LaboratoryManager();
            Singleton.ClinicConnection.MainConnection = new SqlConnection(str);
            Singleton.ClinicConnection.SecondaryConnection = new SqlConnection(str);
            Singleton.ClinicConnection.PersonConnection = new SqlConnection(str);

            m_CurrentDB = new DataModel.SiMedDB();
            Singleton.ClinicDatabase.SiMedDatabase = m_CurrentDB;

            bool b = m_CurrentDB.Init();

            //Загружаем общесистемные настройки из БД
            bool loadResult =  Singleton.ClinicOptions.MainOptions.LoadSystemOptions();

            b = Singleton.Reports.LoadReports();

            Singleton.Reports.PathToDocs = ClinicScheduler.Properties.Settings.Default.PathToDocs;
            Singleton.Reports.PathToReports = ClinicScheduler.Properties.Settings.Default.PathToReports;

            LoadSettings();
            Singleton.Reports.LoadReportsMapping(m_ReportsMappingFileName);
            Singleton.MessengerService.MainManager.Init();
            Singleton.ConferenceManager.MainManager.Init();
            Singleton.Cryptography.MainManager.Init(Singleton.ClinicOptions.MainOptions.CryptOptions.cCryptoProvider);

            if (Singleton.ClinicOptions.MainOptions.ExternalStorageOptions.nUseExternalStorage == 1)
            {
                if (!Singleton.ExternalStorageManager.InitExternalStorage(Singleton.ClinicOptions.MainOptions.ExternalStorageOptions.cExternalStorageType, Singleton.ClinicOptions.MainOptions.ExternalStorageOptions.cExternalStorageOptions, Singleton.ClinicOptions.MainOptions.ExternalStorageLocalOptions.cExternalStorageOptions))
                {
                    string ErrStr = "Ошибка инициализации модуля внешнего хранилища: " + Singleton.ExternalStorageManager.LastException.Message;
                    ClinicScheduler.ProgramStart.WriteToLog(ErrStr, true);
                    //MessageBox.Show(ErrStr, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //return false;
                }
            }

            try
            {
                bool bEnableStatisticsCollection = Singleton.ClinicOptions.MainOptions.StatisticModuleOptions.nUseStatisticModule == 1;
                StatisticsCollectionSystemClient.ClientCounter.SetEnableSending(bEnableStatisticsCollection);
                StatisticsCollectionSystemClient.ClientCounter.SetOptions(Singleton.ClinicOptions.MainOptions.StatisticModuleOptions.cStatisticModuleOptions);
                StatisticsCollectionSystemClient.ClientCounter.SetProduct(ProductInfo.ProductName, ProductInfo.ProductVersion);
                if (DataModel.Organizations.CurrentMedOrganization != null)
                {
                    DataModel.MedOrganization medOrganization = (DataModel.MedOrganization)(Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(DataModel.DataModelElementType.Organization) as DataModel.Organizations).GetOrganization(DataModel.OrgType.Medical, DataModel.Organizations.CurrentMedOrganization.ORG_ID);
                    string OGRN = null;
                    string KPP = null;
                    if (medOrganization != null && !medOrganization.OrganizationInfo.IsORG_OGRNNull() && !String.IsNullOrEmpty(medOrganization.OrganizationInfo.ORG_OGRN))
                        OGRN = medOrganization.OrganizationInfo.ORG_OGRN;
                    if (medOrganization != null && !medOrganization.OrganizationInfo.IsORG_KPPNull() && !String.IsNullOrEmpty(medOrganization.OrganizationInfo.ORG_KPP))
                        KPP = medOrganization.OrganizationInfo.ORG_KPP;
                    StatisticsCollectionSystemClient.ClientCounter.SetClient(DataModel.Organizations.CurrentMedOrganization.MEDORG_NAME, OGRN, KPP);
                }
            }
            catch { }

            //GUI.FormUserLogin form = new GUI.FormUserLogin();
            //DialogResult UserAuthenticateResult = form.ShowDialog();
            //if (UserAuthenticateResult != DialogResult.OK)
            //    return false;
            //var organizations = (Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(DataModelElementType.Organization) as DataModel.Organizations).CreateMedOrganizationsDataView();
            return true;
        }
        //---------------------------------------------
        public DataView GetOrganizations()
        {
            var organizations = (Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(DataModelElementType.Organization) as DataModel.Organizations)
                .CreateMedOrganizationsDataView();
            return organizations;
        }
        public DataView GetUsers()
        {
            var users = (Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(DataModelElementType.Security) as DataModel.Security)
                .CreateUserDataView();
            return users;
        }
        public DataView GetBranches(int organizationId)
        {
            var dbranches = (Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(DataModelElementType.Branch) as DataModel.Branches)
                .CreateDataView(organizationId, organizationId!=0);
            return dbranches;
        }

        //---------------------------------------------
        public void ExitThread()
        {
            Clinic.Logger.LogEvent.SaveInformationToLog("Завершение работы программы", "Programm");

            Singleton.Reports.PrintForms.Clear();
        }
        //---------------------------------------------
    }
}
