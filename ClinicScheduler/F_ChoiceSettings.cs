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
    public partial class F_ChoiceSettings : Form
    {
        public string SelectedConfigName;

        public F_ChoiceSettings()
        {
            InitializeComponent();
        }

        private void bConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "Config";
            DialogResult = DialogResult.OK;
        }

        private void bCheckResultsConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "CheckResultsConfig";
            DialogResult = DialogResult.OK;
        }

        private void bProcessMobileReceivedConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "ProcessMobileReceivedConfig";
            DialogResult = DialogResult.OK;
        }

        private void bSendReceptionRecordsReminderConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "SendReceptionRecordsReminderConfig";
            DialogResult = DialogResult.OK;
        }

        private void bSendReceptionRecordsNotificationConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "SendDefferedNotificationsConfig";
            DialogResult = DialogResult.OK;
        }

        private void bCheckEmployerWebTasksConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "CheckEmployerWebTasksConfig";
            DialogResult = DialogResult.OK;
        }

        private void bCheckIEMKRecordsStateConfig_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "CheckIEMKRecordsStateConfig";
            DialogResult = DialogResult.OK;
        }

        private void bAutoCreateAccountingActs_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "AutoCreateAccountingActsConfig";
            DialogResult = DialogResult.OK;
        }

        private void bAutoCheckPaymentAccountingActs_Click(object sender, EventArgs e)
        {
            SelectedConfigName = "AutoCheckPaymentAccountingActsConfig";
            DialogResult = DialogResult.OK;
        }
    }
}
