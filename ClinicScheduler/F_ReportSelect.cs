using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SiMed.Clinic;
using SiMed.Clinic.Singleton;

namespace ClinicScheduler
{
    public partial class F_ReportSelect : Form
    {
        public ClinicReportType ReportType;
        public string ReportCaption;
        public string FileName;

        public F_ReportSelect()
        {
            InitializeComponent();
            ReportType = ClinicReportType.None;

            ClinicReportsCollection collection = Reports.GetClinicReports();
            for (int i = 0; i < collection.Count; i++)
            {
                DGV_ReportTypes.Rows.Add();
                DGV_ReportTypes.Rows[i].Cells["ReportID"].Value = collection[i].ReportType.ToString();
                DGV_ReportTypes.Rows[i].Cells["ReportName"].Value = collection[i].Name;
            }

            DGV_ReportTypes.Sort(DGV_ReportTypes.Columns["ReportName"], ListSortDirection.Ascending);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ReportType == ClinicReportType.None)
            {
                MessageBox.Show("Необходимо выбрать тип отчета", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileName = TB_FileName.Text;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void DGV_ReportTypes_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ReportType = (ClinicReportType)Enum.Parse(typeof(ClinicReportType), DGV_ReportTypes.Rows[e.RowIndex].Cells["ReportID"].Value.ToString());
                TB_FileName.Text = DGV_ReportTypes.Rows[e.RowIndex].Cells["ReportID"].Value.ToString() + ".xls";
                ReportCaption = DGV_ReportTypes.Rows[e.RowIndex].Cells["ReportName"].Value.ToString();
            }
        }

        private void DGV_ReportTypes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOk_Click(sender, new EventArgs());
        }
    }
}
