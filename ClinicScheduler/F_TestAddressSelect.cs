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
    public partial class F_TestAddressSelect : Form
    {
        public string address;
        public F_TestAddressSelect(string address)
        {
            InitializeComponent();
            textBox1.Text = address;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            address = textBox1.Text;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
