﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Car_Wash_Management_System
{
    public partial class Login : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Wash Management System";
        public Login()
        {
            InitializeComponent();
            loadCompany();
        }
        public void loadCompany()
        {
            cm = new SqlCommand("SELECT * FROM tbCompany", dbcon.connect());
            dbcon.open();
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblCompany.Text = dr["name"].ToString();
                lblAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            dbcon.close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cm = new SqlCommand("SELECT name FROM tbEmployer WHERE name ='" + txtName.Text + "' AND password ='" + txtPassword.Text + "'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show("Đăng nhập thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MainForm main = new MainForm();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dbcon.close();
                dr.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtPassword.UseSystemPasswordChar = false;
            else
                txtPassword.UseSystemPasswordChar = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPassword.Clear();
            txtName.Focus();
        }
    }
}
