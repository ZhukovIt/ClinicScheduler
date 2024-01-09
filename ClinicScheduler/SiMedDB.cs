using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Windows.Forms;
using ClinicScheduler;

namespace SiMed.Clinic.DataModel
{
    class SiMedDB : SiMed.Clinic.DataModel.BaseSiMedDatabase
    {
        //--------------------------------------------------------------
        dtsDictionaries m_dtsDictionaries;
        dtsOrganization m_dtsOrganizations;
        dtsCodificators m_dtsCodificators;
        dtsHazards m_dtsHazards;
        dtsPersons m_dtsPersons;
        dtsDocuments m_dtsDocuments;
        dtsRequests m_dtsRequests;
        dtsFinalActs m_dtsFinalcts;
        dtsMKB10 m_dtsMKB10;
        dtsWorker m_dtsWorker;
        dtsAutoComplete m_dtsAutoComplete;
        dtsWorkerSchedule m_dtsWorkerSchedule;
        dtsReceptionRecords m_dtsReceptionRecords;
        dtsOptions m_dtsOptions;
        dtsSecurity m_dtsSecurity;
        dtsPayDocuments m_dtsPayDocuments;
        dtsMedicalRecords m_dtsMedicalRecords;
        dtsMedicalRecordTemplates m_dtsMedicalRecordTemplates;
        dtsMedicalLaboratoryOrders m_dtsMedicalLaboratoryOrders;
        dtsBudgets m_dtsBudgets;

        private Benefits m_Benefits;
        private BloodGroups m_BloodGroups;
        private Disabilities m_Disabilities;
        private DispensaryGroups m_DispensaryGroups;
        private TimePeriods m_TimePeriods;
        private Inspections m_Inspections;
        private Discounts m_Discounts;
        private DiscountCardTypes m_DiscountCardTypes;
        private Doctors m_Doctors;
        private Services m_Services;
        private ScheduleModels m_ScheduleModels;
        private Price m_Price;
        private PriceService m_PriceService;
        private Measures m_Measures;
        private Materials m_Materials;
        private Statements m_Statements;
        private PriceTypes m_PriceTypes;
        private SubDocTypes m_SubDocTypes;
        private DocTypes m_DocTypes;
        private PayAccounts m_PayAccounts;
        private PayCashes m_PayCashes;
        private PayTerminals m_PayTerminals;
        private PayFundsFlow m_PayFundsFlows;
        private Projects m_Projects;
        private SalaryCalculationTypes m_SalaryCalculationTypes;
        private PayWorkplaces m_PayWorkplaces;
        private WorkerPosts m_WorkerPosts;
        private ServiceGroups m_ServiceGroups;
        private Branches m_Branches;
        private Departments m_Departments;
        private Cabinets m_Cabinets;
        private SalesChannels m_SalesChannels;
        private Promotions m_Promotions;
        private PersonStates m_PersonStates;
        private PersonCategories m_PersonCategories; 

        private Organizations m_Organizations;
        private Workers m_Workers;
        private Hazards m_Hazards;
        private MKB10 m_MKB10;

        private WorkerSchedule m_WorkerSchedule;
        private ReceptionRecords m_ReceptionRecords;
        private MedicalRecords m_MedicalRecords;
        private MedicalRecordTemplates m_MedicalRecordTemplates;
        private MedicalLaboratoryOrders m_MedicalLaboratoryOrders;

        private Persons m_Persons;
        private Documents m_Documents;
        private Requests m_Requests;
        private Codificators m_Codificators;
        private FinalActs m_FinalActs;
        private PayDocuments m_PayDocuments;
        private Budgets m_Budgets;
        private Security m_Security;

        private ServicesManagementModule m_ServicesManagementModule;

        private DBAutoComplete m_DBAutoComplete;
        //--------------------------------------------------------------
        public dtsOptions dtsOptions
        {
            get { return m_dtsOptions; }
        }
        //--------------------------------------------------------------
        public DBAutoComplete DBAutoComplete
        {
            get { return m_DBAutoComplete; }
        }
        //--------------------------------------------------------------
        public FinalActs FinalActs
        {
            get { return m_FinalActs; }
        }
        //--------------------------------------------------------------
        public Requests Requests
        {
            get { return m_Requests; }
        }
        //--------------------------------------------------------------
        public Documents Documents
        {
            get { return m_Documents; }
        }
        //--------------------------------------------------------------
        public PayDocuments PayDocuments
        {
            get { return m_PayDocuments; }
        }
        //--------------------------------------------------------------
        public Persons Persons
        {
            get { return m_Persons; }
        }
        //--------------------------------------------------------------
        public Codificators Codificators
        {
            get { return m_Codificators; }
        }
        //--------------------------------------------------------------
        public Inspections Inspections
        {
            get { return m_Inspections; }
        }
        //--------------------------------------------------------------
        public Hazards Hazards
        {
            get { return m_Hazards; }
        }
        //--------------------------------------------------------------
        public MKB10 MKB10
        {
            get { return m_MKB10; }
        }
        //--------------------------------------------------------------
        public Organizations Organizations
        {
            get { return m_Organizations; }
        }
        //--------------------------------------------------------------
        public Workers Workers
        {
            get { return m_Workers; }
        }
        //--------------------------------------------------------------
        public WorkerSchedule WorkerSchedule
        {
            get { return m_WorkerSchedule; }
        }
        //--------------------------------------------------------------
        public ReceptionRecords ReceptionRecords
        {
            get { return m_ReceptionRecords; }
        }
        //--------------------------------------------------------------
        public MedicalRecords MedicalRecords
        {
            get { return m_MedicalRecords; }
        }
        //--------------------------------------------------------------
        public MedicalRecordTemplates MedicalRecordTemplates
        {
            get { return m_MedicalRecordTemplates; }
        }
        //--------------------------------------------------------------
        public MedicalLaboratoryOrders MedicalLaboratoryOrders
        {
            get { return m_MedicalLaboratoryOrders; }
        }
        //--------------------------------------------------------------
        public Benefits Benefits
        {
            get { return m_Benefits; }
        }
        //--------------------------------------------------------------
        public BloodGroups BloodGroups
        {
            get { return m_BloodGroups; }
        }
        //--------------------------------------------------------------
        public Disabilities Disabilities
        {
            get { return m_Disabilities; }
        }
        //--------------------------------------------------------------
        public DispensaryGroups DispensaryGroups
        {
            get { return m_DispensaryGroups; }
        }
        //--------------------------------------------------------------
        public TimePeriods TimePeriods
        {
            get { return m_TimePeriods; }
        }
        //--------------------------------------------------------------
        public Discounts Discounts
        {
            get { return m_Discounts; }
        }
        //--------------------------------------------------------------
        public Price Price
        {
            get { return m_Price; }
        }
        //--------------------------------------------------------------
        public PriceService PriceService
        {
            get { return m_PriceService; }
        }
        //--------------------------------------------------------------
        public DiscountCardTypes DiscountCardTypes
        {
            get { return m_DiscountCardTypes; }
        }
        //--------------------------------------------------------------
        public Doctors Doctors
        {
            get { return m_Doctors; }
        }
        //--------------------------------------------------------------
        public Services Services
        {
            get { return m_Services; }
        }
        //--------------------------------------------------------------
        public ScheduleModels ScheduleModels
        {
            get { return m_ScheduleModels; }
        }
        //--------------------------------------------------------------
        public Measures Measures
        {
            get { return m_Measures; }
        }
        //--------------------------------------------------------------
        public Materials Materials
        {
            get { return m_Materials; }
        }
        //--------------------------------------------------------------
        public Statements Statements
        {
            get { return m_Statements; }
        }
        //--------------------------------------------------------------
        public PriceTypes PriceTypes
        {
            get { return m_PriceTypes; }
        }
        //--------------------------------------------------------------
        public SubDocTypes SubDocTypes
        {
            get { return m_SubDocTypes; }
        }
        //--------------------------------------------------------------
        public DocTypes DocTypes
        {
            get { return m_DocTypes; }
        }
        //--------------------------------------------------------------
        public PayAccounts PayAccounts
        {
            get { return m_PayAccounts; }
        }
        //--------------------------------------------------------------
        public PayCashes PayCashes
        {
            get { return m_PayCashes; }
        }
        //--------------------------------------------------------------
        public PayTerminals PayTerminals
        {
            get { return m_PayTerminals; }
        }
        //--------------------------------------------------------------
        public PayFundsFlow PayFundsFlows
        {
            get { return m_PayFundsFlows; }
        }
        //--------------------------------------------------------------
        public Projects Projects
        {
            get { return m_Projects; }
        }
        //--------------------------------------------------------------
        public SalaryCalculationTypes SalaryCalculationTypes
        {
            get { return m_SalaryCalculationTypes; }
        }
        //--------------------------------------------------------------
        public PayWorkplaces PayWorkplaces
        {
            get { return m_PayWorkplaces; }
        }
        //--------------------------------------------------------------
        public WorkerPosts WorkerPosts
        {
            get { return m_WorkerPosts; }
        }
        //--------------------------------------------------------------
        public ServiceGroups ServiceGroups
        {
            get { return m_ServiceGroups; }
        }
        //--------------------------------------------------------------
        public Security Security
        {
            get { return m_Security; }
        }
        //--------------------------------------------------------------
        public Branches Branches
        {
            get { return m_Branches; }
        }
        //--------------------------------------------------------------
        public Departments Departments
        {
            get { return m_Departments; }
        }
        //--------------------------------------------------------------
        public Cabinets Cabinets
        {
            get { return m_Cabinets; }
        }
        //--------------------------------------------------------------
        public SalesChannels SalesChannels
        {
            get { return m_SalesChannels; }
        }
        //--------------------------------------------------------------
        public Promotions Promotions
        {
            get { return m_Promotions; }
        }
        //--------------------------------------------------------------
        public PersonStates PersonStates
        {
            get { return m_PersonStates; }
        }
        //--------------------------------------------------------------
        public PersonCategories PersonCategories
        {
            get { return m_PersonCategories; }
        }
        //--------------------------------------------------------------
        public Budgets Budgets
        {
            get { return m_Budgets; }
        }
        //--------------------------------------------------------------
        public SiMedDB()
        {
            m_dtsDictionaries = new dtsDictionaries();
            m_dtsOrganizations = new dtsOrganization();
            m_dtsCodificators = new dtsCodificators();
            m_dtsHazards = new dtsHazards();
            m_dtsPersons = new dtsPersons();
            m_dtsDocuments = new dtsDocuments();
            m_dtsRequests = new dtsRequests();
            m_dtsFinalcts = new dtsFinalActs();
            m_dtsMKB10 = new dtsMKB10();
            m_dtsWorker = new dtsWorker();
            m_dtsAutoComplete = new dtsAutoComplete();
            m_dtsWorkerSchedule = new dtsWorkerSchedule();
            m_dtsReceptionRecords = new dtsReceptionRecords();
            m_dtsOptions = new dtsOptions();
            m_dtsSecurity = new dtsSecurity();
            m_dtsPayDocuments = new dtsPayDocuments();
            m_dtsMedicalRecords = new dtsMedicalRecords();
            m_dtsMedicalRecordTemplates = new dtsMedicalRecordTemplates();
            m_dtsMedicalLaboratoryOrders = new dtsMedicalLaboratoryOrders();
            m_dtsBudgets = new dtsBudgets();

            m_Codificators = new DataModel.Codificators(m_dtsCodificators);

            m_Benefits = new Benefits(m_dtsDictionaries.BENEFIT);
            m_Disabilities = new Disabilities(m_dtsDictionaries.DISABILITY);
            m_DispensaryGroups = new DispensaryGroups(m_dtsDictionaries.DISPENSARYGROUP);
            m_BloodGroups = new BloodGroups(m_dtsDictionaries.BLOODGROUP);
            m_TimePeriods = new TimePeriods(m_dtsDictionaries.TIME_PERIOD);
            m_Discounts = new Discounts(m_dtsDictionaries.DISCOUNT);
            m_Price = new Price(m_dtsDictionaries.PRICE, m_dtsDictionaries.PRICE_INSPECTION);
            m_DiscountCardTypes = new DiscountCardTypes(m_dtsDictionaries.DISCOUNT_CARD_TYPE);
            m_ServicesManagementModule = new ServicesManagementModule(m_dtsDictionaries);
            m_Statements = new Statements(m_dtsDictionaries.STATEMENT);
            m_PriceTypes = new PriceTypes(m_dtsDictionaries.PRICE_TYPE, m_dtsDictionaries.PRICE_TYPE_FOR_FILTER);
            m_SubDocTypes = new DataModel.SubDocTypes(m_dtsDictionaries.SUBDOCTYPE);
            m_DocTypes = new DataModel.DocTypes(m_dtsDictionaries.DOCTYPE);
            m_PayAccounts = new DataModel.PayAccounts(m_dtsDictionaries.PAY_ACCOUNT);
            m_PayCashes = new DataModel.PayCashes(m_dtsDictionaries.PAY_CASH);
            m_PayTerminals = new DataModel.PayTerminals(m_dtsDictionaries.PAY_TERMINAL);
            m_PayFundsFlows = new PayFundsFlow(m_dtsDictionaries.PAY_FUNDS_FLOW);
            m_Projects = new DataModel.Projects(m_dtsDictionaries.PROJECT);
            m_SalaryCalculationTypes = new DataModel.SalaryCalculationTypes(m_dtsDictionaries.SALARY_CALCULATION_TYPE);
            m_PayWorkplaces = new DataModel.PayWorkplaces(m_dtsDictionaries);
            m_WorkerPosts = new DataModel.WorkerPosts(m_dtsDictionaries.WORKER_POST);
            m_ServiceGroups = new DataModel.ServiceGroups(m_dtsDictionaries.SERVICE_GROUPS);
            m_Branches = new DataModel.Branches(m_dtsDictionaries.BRANCHES);
            m_Departments = new DataModel.Departments(m_dtsDictionaries.DEPARTMENT);
            m_Cabinets = new DataModel.Cabinets(m_dtsDictionaries.CABINET);
            m_SalesChannels = new DataModel.SalesChannels(m_dtsDictionaries.SALES_CHANNEL);
            m_Promotions = new DataModel.Promotions(m_dtsDictionaries);
            m_PersonStates = new DataModel.PersonStates(m_dtsDictionaries.PERSON_STATE);
            m_PersonCategories = new DataModel.PersonCategories(m_dtsDictionaries.PERSON_CATEGORY);

            m_Doctors = new Doctors(m_ServicesManagementModule, m_dtsDictionaries.DOCTOR, m_dtsDictionaries.SERVICE, m_dtsDictionaries.MATERIAL_NORMS);
            m_Services = new Services(m_ServicesManagementModule, m_dtsDictionaries.SERVICE, m_dtsDictionaries.SERVICES_FOR_CHOOSE);
            m_PriceService = new DataModel.PriceService(m_ServicesManagementModule, m_dtsDictionaries.PRICE_SERVICE, m_dtsDictionaries.PRICE_SERVICE_SIMPLE);
            m_Inspections = new Inspections(m_ServicesManagementModule, m_dtsDictionaries.INSPECTION, m_dtsDictionaries.INSPECTION_FOR_FILTER, m_dtsDictionaries.ADDITION_INSPECTION);

            m_ScheduleModels = new ScheduleModels(m_dtsDictionaries.SCHEDULE_MODEL);
            m_Measures = new DataModel.Measures(m_dtsDictionaries.MEASURE);
            m_Materials = new DataModel.Materials(m_dtsDictionaries.MATERIAL, m_dtsDictionaries.MATERIAL_GROUPS, m_dtsDictionaries.MATERIAL_TREE, m_dtsDictionaries.PRICE_MATERIAL);

            m_Organizations = new Organizations(m_dtsOrganizations);
            m_Workers = new DataModel.Workers(m_dtsWorker);
            m_Hazards = new Hazards(m_dtsHazards);
            m_MKB10 = new MKB10(m_dtsMKB10);

            m_WorkerSchedule = new WorkerSchedule(m_dtsWorkerSchedule);
            m_ReceptionRecords = new ReceptionRecords(m_dtsReceptionRecords);
            m_MedicalRecords = new DataModel.MedicalRecords(m_dtsMedicalRecords);
            m_MedicalRecordTemplates = new DataModel.MedicalRecordTemplates(m_dtsMedicalRecordTemplates);
            m_MedicalLaboratoryOrders = new MedicalLaboratoryOrders(m_dtsMedicalLaboratoryOrders);
            m_Budgets = new Budgets(m_dtsBudgets);

            m_Persons = new Persons(m_dtsPersons);
            m_Documents = new Documents(m_dtsDocuments);
            m_Requests = new Requests(m_dtsRequests);
            m_FinalActs = new FinalActs(m_dtsFinalcts);
            m_PayDocuments = new DataModel.PayDocuments(m_dtsPayDocuments);

            m_DBAutoComplete = new DBAutoComplete(m_dtsAutoComplete, Singleton.ClinicConnection.SecondaryConnection);

            m_Security = new Security(m_dtsSecurity);
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Инициализация БД: загрузка справочников, которые будут хранится в DataSet в ходе всей работы
        /// программы
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            bool bRes = true;
            bRes = bRes & m_Codificators.Load();
            bRes = bRes & LoadDictionaries();
            bRes = bRes & m_Organizations.Load();
            bRes = bRes & m_Workers.Load();
            bRes = bRes & m_Hazards.Load();
            //bRes = bRes & m_Persons.Load(true);
            //bRes = bRes & m_Documents.Load();
            //bRes = bRes & m_Requests.Load();
            //bRes = bRes & m_FinalActs.Load();
            bRes = bRes & m_MKB10.Load();
            bRes = bRes & m_DBAutoComplete.Load();
            bRes = bRes & m_Security.Load();
            //bRes = bRes & m_MedicalRecordTemplates.Load();
            bRes = bRes & m_Budgets.Load();
            
            return bRes;
        }
        //--------------------------------------------------------------
        public override bool LoadPrices()
        {
            bool bRes = true;
            bRes = bRes & m_PriceTypes.Load();
            bRes = bRes & m_Price.Load();
            return bRes;
        }
        //--------------------------------------------------------------
#region Загрузка справочников
        private bool LoadDictionaries()
        {
            bool bRes = true;
            bRes = bRes & m_Benefits.Load();
            bRes = bRes & m_Disabilities.Load();
            bRes = bRes & m_DispensaryGroups.Load();
            bRes = bRes & m_BloodGroups.Load();
            bRes = bRes & m_TimePeriods.Load();
            bRes = bRes & m_Discounts.Load();
            bRes = bRes & m_Price.Load();
            bRes = bRes & m_DiscountCardTypes.Load();
            bRes = bRes & m_ServicesManagementModule.Load();
            //bRes = bRes & m_Doctors.Load(); - загружать не обязательно, т.к. они уже прогрузились при загрузке модуля управления услугами
            //bRes = bRes & m_Services.Load(); - загружать не обязательно, т.к. они уже прогрузились при загрузке модуля управления услугами
            //bRes = bRes & m_PriceService.Load(); - загружать не обязательно, т.к. они уже прогрузились при загрузке модуля управления услугами
            //bRes = bRes & m_Inspections.Load(); - загружать не обязательно, т.к. они уже прогрузились при загрузке модуля управления услугами
            bRes = bRes & m_Inspections.LoadInspectionsForChoose();
            bRes = bRes & m_ScheduleModels.Load();
            bRes = bRes & m_Measures.Load();
            bRes = bRes & m_Materials.Load();
            bRes = bRes & m_Statements.Load();
            bRes = bRes & m_PriceTypes.Load();
            bRes = bRes & m_SubDocTypes.Load();
            bRes = bRes & m_DocTypes.Load();
            bRes = bRes & m_PayAccounts.Load();
            bRes = bRes & m_PayCashes.Load();
            bRes = bRes & m_PayTerminals.Load();
            bRes = bRes & m_PayFundsFlows.Load();
            bRes = bRes & m_Projects.Load();
            bRes = bRes & m_SalaryCalculationTypes.Load();
            bRes = bRes & m_PayWorkplaces.Load();
            bRes = bRes & m_WorkerPosts.Load();
            bRes = bRes & m_ServiceGroups.Load();
            bRes = bRes & m_Branches.Load();
            bRes = bRes & m_Departments.Load();
            bRes = bRes & m_Cabinets.Load();
            bRes = bRes & m_SalesChannels.Load();
            bRes = bRes & m_Promotions.Load();
            bRes = bRes & m_PersonStates.Load();
            bRes = bRes & m_PersonCategories.Load();
            
            return bRes;
        }
        //--------------------------------------------------------------
#endregion
        //--------------------------------------------------------------
        public override int GetNewID(string _SequenceName)
        {
            int? ID = 0;
            dtsOrganizationTableAdapters.StoredFunctionTableAdapter taStoredFunction = new dtsOrganizationTableAdapters.StoredFunctionTableAdapter();
            taStoredFunction.Connection = Singleton.ClinicConnection.MainConnection;
            taStoredFunction.GetNewSeqVal(_SequenceName, ref ID);
            return (int)ID;
        }
        //--------------------------------------------------------------
        public override int GetCurrentSequenceValue(string _SequenceName)
        {
            int? ID = 0;
            dtsOrganizationTableAdapters.StoredFunctionTableAdapter taStoredFunction = new dtsOrganizationTableAdapters.StoredFunctionTableAdapter();
            taStoredFunction.Connection = Singleton.ClinicConnection.MainConnection;
            taStoredFunction.GetCurrentSeqVal(_SequenceName, ref ID);
            return (int)ID;
        }
        //--------------------------------------------------------------
        public override void SetCurrentSequenceValue(string _SequenceName, int _NewID)
        {
            dtsOrganizationTableAdapters.StoredFunctionTableAdapter taStoredFunction = new dtsOrganizationTableAdapters.StoredFunctionTableAdapter();
            taStoredFunction.Connection = Singleton.ClinicConnection.MainConnection;
            taStoredFunction.SetCurrentSeqVal(_SequenceName, _NewID);
        }
        //--------------------------------------------------------------
        public override int GetNextNumber(string _NumeratorName, DateTime _Date, string _MedOrgPrefix, string _SubmissionPrefix, string _Submission2Prefix)
        {
            return 0;
        }
        //--------------------------------------------------------------
        public override bool ResetNumerator(string _NumeratorName)
        {
            return true;
        }
        //--------------------------------------------------------------
        public override bool SetNextNumber(int _NewNumber, string _NumeratorName, DateTime _Date, string _MedOrgPrefix, string _SubmissionPrefix, string _Submission2Prefix)
        {
            return true;
        }
        //--------------------------------------------------------------
        public override int GetNextBSONumber(string _NumeratorName, DateTime _Date, string _MedOrgPrefix, string _SubmissionPrefix, string _Submission2Prefix)
        {
            return 0;
        }
        //--------------------------------------------------------------
        public override bool IsNumberMustBeChanged(string _NumeratorName, bool _bDateIsChanged, bool _bMedOrgIsChanged, bool _bSubmissionIsChanged, bool _bSubmission2IsChanged)
        {
            bool? Result = false;
            dtsOrganizationTableAdapters.StoredFunctionTableAdapter taStoredFunction = new dtsOrganizationTableAdapters.StoredFunctionTableAdapter();
            taStoredFunction.Connection = Singleton.ClinicConnection.MainConnection;
            taStoredFunction.IsNumberMustBeChanged(_NumeratorName, _bDateIsChanged, _bMedOrgIsChanged, _bSubmissionIsChanged, _bSubmission2IsChanged, ref Result);
            return (bool)Result;
        }
        //--------------------------------------------------------------
        public override int ValidateNumber(int _Number, int _Object_ID, string _NumeratorName, DateTime _Date, string _MedOrgPrefix, string _SubmissionPrefix, string _Submission2Prefix)
        {
            return 0;
        }
        //--------------------------------------------------------------
        public override bool OnPayAccountsChanged()
        {
            try
            {
                bool bRes = true;
                bRes = bRes & PayCashes.Load();
                bRes = bRes & PayTerminals.Load();
                bRes = bRes & PayFundsFlows.Load();
                return bRes;
            }
            catch (Exception ex)
            {
                Logger.LogEvent.SaveExceptionToLog(ex, this.GetType().Name);
                return false;
            }
        }
        //--------------------------------------------------------------
        public override DataRow GetDictionaryByID(DictionaryType _DictionaryType, int _ID, int _Parent_ID = 0)
        {
            switch (_DictionaryType)
            {
                case DictionaryType.Service:
                    return Services.GetServiceByID(_ID);
                case DictionaryType.Inspection:
                    return Inspections.GetInspectionByID(_ID);
                case DictionaryType.PriceType:
                    return PriceTypes.GetPriceTypeByID(_ID);
                case DictionaryType.Measure:
                    return Measures.GetMeasureByID(_ID);
                case DictionaryType.Doctor:
                    return Doctors.FindByID(_ID);
                case DictionaryType.ScheduleModel:
                    return ScheduleModels.GetScheduleModelByID(_ID);
                case DictionaryType.Project:
                    return Projects.GetByID(_ID);
                case DictionaryType.PayFoundFlow:
                    return PayFundsFlows.FindByID(_ID);
                case DictionaryType.PayCash:
                    return PayCashes.FindByID(_ID);
                case DictionaryType.PayTerminal:
                    return PayTerminals.FindByID(_ID);
                case DictionaryType.Promotion:
                    return Promotions.FindByID(_ID);
                case DictionaryType.Material:
                    return Materials.GetMaterialByID(_ID);
                case DictionaryType.Cabinet:
                    return Cabinets.GetByID(_ID);
                case DictionaryType.DMSSpecificationService:
                    return Organizations.GetDMSSecificationServiceByServiceID(_ID, _Parent_ID);
                case DictionaryType.WorkerPost:
                    return WorkerPosts.FindByID(_ID);
                case DictionaryType.DispensaryGroup:
                    return DispensaryGroups.GetByID(_ID);
                case DictionaryType.PersonCategory:
                    return PersonCategories.GetByID(_ID);
                case DictionaryType.SubDocType:
                    return SubDocTypes.GetByID(_ID);
                case DictionaryType.Hazard:
                    return Hazards.GetHazardByID(_ID);
                default:
                    throw new NotImplementedException();
            }
        }
        //--------------------------------------------------------------
        public override DataRow GetDictionaryByCode(DictionaryType _DictionaryType, string _Code, int _Parent_ID)
        {
            switch (_DictionaryType)
            {
                case DictionaryType.Hazard:
                    return Hazards.GetHazardByCode(_Code, (HazardType)_Parent_ID);
                case DictionaryType.Service:
                    return Services.FindByCode(_Code, _Parent_ID);
                case DictionaryType.DMSSpecificationService:
                    return Organizations.GetDMSSecificationServiceByCode(_Code, _Parent_ID);
                default:
                    throw new NotImplementedException();
            }
        }
        //--------------------------------------------------------------
        public override DataRow[] GetDictionariesByName(DictionaryType _DictionaryType, string _Name, bool _bFullEqual, int _Parent_ID = 0)
        {
            switch (_DictionaryType)
            {
                case DictionaryType.Service:
                    return Services.FindByName(_Name, _Parent_ID);
                case DictionaryType.Doctor:
                    return Doctors.FindByName(_Name, _bFullEqual);
                case DictionaryType.ServiceGroup:
                    return ServiceGroups.FindByName(_Name, _bFullEqual);
                case DictionaryType.DMSSpecificationService:
                    return Organizations.GetDMSSecificationServicesByName(_Name, _Parent_ID);
                case DictionaryType.WorkerPost:
                    return WorkerPosts.FindByName(_Name, _bFullEqual);
                default:
                    throw new NotImplementedException();
            }
        }
        //--------------------------------------------------------------
        public override int GetDefaultPriceTypeIDForMedOrganization(int _MedOrg_ID)
        {
            return Organizations.GetDefaultPriceTypeIDForMedOrganization(_MedOrg_ID);
        }
        //--------------------------------------------------------------
        public override  double GetPriceForDocument(DocType _DocType, int _DocSubType, string _Sex, int _PRICE_TYPE_ID)
        {
            return Price.GetPriceForDocument(_DocType, _DocSubType, _Sex, _PRICE_TYPE_ID);
        }
        //--------------------------------------------------------------
        public override double GetPriceForService(int _SERV_ID, int _PRICE_TYPE_ID, int _WorkerDegree, string _Sex)
        {
            return PriceService.GetPriceForService(_SERV_ID, _PRICE_TYPE_ID, (Degree)_WorkerDegree, _Sex);
        }
        //--------------------------------------------------------------

        public override List<int> GetInspectionsForDocument(DocType _DocType, int _SubDocType_ID, int _Age, string _Sex)
        {
            return Price.GetInspectionsForDocument(_DocType, _SubDocType_ID, _Age, _Sex).Keys.ToList<int>();
        }
        //--------------------------------------------------------------
        public DataRow GetHazardByCode(string _Code, HazardType _HazardType)
        {
            return Hazards.GetHazardByCode(_Code, _HazardType);
        }
        //--------------------------------------------------------------
        public override DataRow GetCodificatorByID(CodificatorType _CodificatorType, int _ID)
        {
            switch (_CodificatorType)
            {
                case CodificatorType.ServiceType:
                    return Codificators.GetServiceTypeByID(_ID);
                case CodificatorType.Degree:
                    return Codificators.GetDegreeByID(_ID);
                case CodificatorType.Qualification:
                    return Codificators.GetQualificationByID(_ID);
                case CodificatorType.DiagnosisType:
                    return Codificators.GetDiagnosisTypeByID(_ID);
                case CodificatorType.DiagnosisKind:
                    return Codificators.GetDiagnosisKindByID(_ID);
                case CodificatorType.MedCommissionResult:
                    return Codificators.GetMedComissionResultByID(_ID);
                case CodificatorType.InspectionResult:
                    return Codificators.GetInspectionResultByID(_ID);
                case CodificatorType.PaymentType:
                    return Codificators.GetPaymentTypeByID(_ID);
                case CodificatorType.BudgetType:
                    return Codificators.GetBudgetTypeByID(_ID);
                default:
                    throw new NotImplementedException();
            }
        }
        //--------------------------------------------------------------
        public override DataRow[] GetCodificatorsByName(CodificatorType _CodificatorType, string _Name, bool _bFullEqual)
        {
            switch (_CodificatorType)
            {
                case CodificatorType.ServiceType:
                    return Codificators.GetServiceTypeByName(_Name, _bFullEqual);
                case CodificatorType.Degree:
                    return Codificators.GetDegreeByName(_Name, _bFullEqual);
                case CodificatorType.Qualification:
                    return Codificators.GetQualificationByName(_Name, _bFullEqual);
                default:
                    throw new NotImplementedException();
            }
        }
        //--------------------------------------------------------------
        public override DataRow CheckPassword(string _Login, string _Password)
        {
            return Security.CheckPassword(_Login, _Password);
        }
        //--------------------------------------------------------------
        public override object GetDataModelElement(DataModelElementType _Type)
        {
            switch (_Type)
            {
                case DataModelElementType.DocType:
                    return m_DocTypes;
                case DataModelElementType.Benefit:
                    return Benefits;
                case DataModelElementType.BloodGroup:
                    return BloodGroups;
                case DataModelElementType.Branch:
                    return Branches;
                case DataModelElementType.Cabinet:
                    return Cabinets;
                case DataModelElementType.Department:
                    return Departments;
                case DataModelElementType.Disability:
                    return Disabilities;
                case DataModelElementType.DiscountCardType:
                    return DiscountCardTypes;
                case DataModelElementType.Discount:
                    return Discounts;
                case DataModelElementType.DispensaryGroup:
                    return DispensaryGroups;
                case DataModelElementType.Doctor:
                    return Doctors;
                case DataModelElementType.Inspection:
                    return Inspections;
                case DataModelElementType.Material:
                    return Materials;
                case DataModelElementType.MaterialGroup:
                    return Materials;
                case DataModelElementType.Measure:
                    return Measures;
                case DataModelElementType.PayAccount:
                    return PayAccounts;
                case DataModelElementType.PayCash:
                    return PayCashes;
                case DataModelElementType.PayFundsFlow:
                    return PayFundsFlows;
                case DataModelElementType.PayTerminal:
                    return PayTerminals;
                case DataModelElementType.PayWorkplace:
                    return PayWorkplaces;
                case DataModelElementType.PriceType:
                    return PriceTypes;
                case DataModelElementType.Project:
                    return Projects;
                case DataModelElementType.Promotion:
                    return Promotions;
                case DataModelElementType.PersonState:
                    return PersonStates;
                case DataModelElementType.SalaryCalculationType:
                    return SalaryCalculationTypes;
                case DataModelElementType.SaleChannel:
                    return SalesChannels;
                case DataModelElementType.ScheduleModel:
                    return ScheduleModels;
                case DataModelElementType.ServiceGroup:
                    return ServiceGroups;
                case DataModelElementType.Service:
                    return Services;
                case DataModelElementType.Statement:
                    return Statements;
                case DataModelElementType.SubDocType:
                    return SubDocTypes;
                case DataModelElementType.TimePeriod:
                    return TimePeriods;
                case DataModelElementType.WorkerPost:
                    return WorkerPosts;
                case DataModelElementType.SalesChannel:
                    return SalesChannels;
                case DataModelElementType.Security:
                    return Security;
                case DataModelElementType.Codificator:
                    return Codificators;
                case DataModelElementType.Worker:
                    return Workers;
                case DataModelElementType.WorkerSchedule:
                    return WorkerSchedule;
                case DataModelElementType.MKB10:
                    return MKB10;
                case DataModelElementType.Organization:
                    return Organizations;
                case DataModelElementType.MedOrganization:
                    return Organizations;
                case DataModelElementType.Hazard:
                    return Hazards;
                case DataModelElementType.Person:
                    return Persons;
                case DataModelElementType.Price:
                    return Price;
                case DataModelElementType.PriceService:
                    return PriceService;
                case DataModelElementType.Document:
                    return Documents;
                case DataModelElementType.MedicalRecord:
                    return MedicalRecords;
                case DataModelElementType.ReceptionRecord:
                    return ReceptionRecords;
                case DataModelElementType.PayDocument:
                    return PayDocuments;
                case DataModelElementType.DBAutoComplete:
                    return DBAutoComplete;
                case DataModelElementType.FinalAct:
                    return FinalActs;
                case DataModelElementType.Request:
                    return Requests;
                case DataModelElementType.MedicalRecordTemplate:
                    return MedicalRecordTemplates;
                case DataModelElementType.MedicalLaboratoryOrder:
                    return MedicalLaboratoryOrders;
                case DataModelElementType.PersonCategory:
                    return PersonCategories;
                case DataModelElementType.Budget:
                    return Budgets;
                default:
                    throw new NotImplementedException();
            }
        }
        //--------------------------------------------------------------
    }
}
