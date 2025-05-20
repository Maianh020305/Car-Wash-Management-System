using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Wash_Management_System
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnDashboard.Height;
            panelSlide.Top = btnDashboard.Top;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnLogout.Height;
            panelSlide.Top = btnLogout.Top;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnCustomer.Height;
            panelSlide.Top = btnCustomer.Top;
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnService.Height;
            panelSlide.Top = btnService.Top;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnCash.Height;
            panelSlide.Top = btnCash.Top;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnReport.Height;
            panelSlide.Top = btnReport.Top;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnSetting.Height;
            panelSlide.Top = btnSetting.Top;
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnEmployee.Height;
            panelSlide.Top = btnEmployee.Top;
        }
    }
}
