using SiMed.Clinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinicScheduler
{
    public partial class F_EmployerWebTaskConfig : Form
    {
        private DataView users;
        private AppConfig config;
        private CClinicApp clinicApp;
        public F_EmployerWebTaskConfig(CClinicApp _App, AppConfig _Config)
        {
            InitializeComponent();

            clinicApp = _App;
            config = _Config;

            tbTasksNew.Text = config.EmployerWebTasksProperties.TaskNewFolder;
            tbTasksProcess.Text = config.EmployerWebTasksProperties.TaskProcessFolder;
            tbTasksArchive.Text = config.EmployerWebTasksProperties.TaskArchiveFolder;
            tbTasksResult.Text = config.EmployerWebTasksProperties.TaskResultFolder;
            tbTasksTmpFolder.Text = config.EmployerWebTasksProperties.TaskTmpFolder;
            tbSendSpeedKbps.Text = config.EmployerWebTasksProperties.SendSpeedKbps.ToString();
            cbZipMIResultWithExtraction.Checked = config.EmployerWebTasksProperties.ZipMIResultWithExtraction;

            fillUsers();

            try
            {
                cb_User.SelectedValue = config.EmployerWebTasksProperties.MedInspectionRequestsCreatedFromUserId.Value;
            }
            catch (Exception) { }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbTasksNew.Text))
            {
                MessageBox.Show("Выберите директорию для хранения новых задач", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(tbTasksProcess.Text))
            {
                MessageBox.Show("Выберите директорию для хранения задач в процессе обработки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(tbTasksArchive.Text))
            {
                MessageBox.Show("Выберите директорию для хранения обработанных задач", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(tbTasksResult.Text))
            {
                MessageBox.Show("Выберите директорию для хранения результатов обработанных задач", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(tbTasksTmpFolder.Text))
            {
                MessageBox.Show("Выберите директорию для временных файлов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(tbTasksNew.Text))
            {
                MessageBox.Show("Директория для хранения новых задач не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(tbTasksProcess.Text))
            {
                MessageBox.Show("Директория для хранения задач в процессе обработки не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(tbTasksArchive.Text))
            {
                MessageBox.Show("Директория для хранения обработанных задач не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(tbTasksResult.Text))
            {
                MessageBox.Show("Директория для хранения результатов обработанных задач не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(tbTasksTmpFolder.Text))
            {
                MessageBox.Show("Директория для временных файлов не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(tbSendSpeedKbps.Text))
            {
                MessageBox.Show("Введите максимальную скорость передачи данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int tmp_int;
            if (!Int32.TryParse(tbSendSpeedKbps.Text, out tmp_int) || tmp_int < 100 || tmp_int > 1000000)
            {
                MessageBox.Show("Введенная максимальная скорость передачи данных не является корректным числом в диапазоне [100; 1000000]", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            config.EmployerWebTasksProperties.TaskNewFolder = tbTasksNew.Text;
            config.EmployerWebTasksProperties.TaskProcessFolder = tbTasksProcess.Text;
            config.EmployerWebTasksProperties.TaskArchiveFolder = tbTasksArchive.Text;
            config.EmployerWebTasksProperties.TaskResultFolder = tbTasksResult.Text;
            config.EmployerWebTasksProperties.TaskTmpFolder = tbTasksTmpFolder.Text;
            config.EmployerWebTasksProperties.SendSpeedKbps = tmp_int;

            config.EmployerWebTasksProperties.ZipMIResultWithExtraction = cbZipMIResultWithExtraction.Checked;

            config.EmployerWebTasksProperties.MedInspectionRequestsCreatedFromUserId = (int?)cb_User.SelectedValue;

            DialogResult = DialogResult.OK;
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

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
