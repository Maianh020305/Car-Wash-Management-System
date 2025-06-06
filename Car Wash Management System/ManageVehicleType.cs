﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Wash_Management_System
{
    public partial class ManageVehicleType: Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        Setting setting;
        public ManageVehicleType(Setting sett)
        {
            InitializeComponent();
            setting = sett;
            cbClass.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Required vehicle type name!", "Warning");
                    return; // return to the data field and form
                }
                if (MessageBox.Show("Bạn có chắc muốn đăng ký loại xe?", "Đăng ký loại xe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbVehicleType(name,class)VALUES(@name,@class)", dbcon.connect());
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@class", cbClass.Text);

                    dbcon.open();// to open connection
                    cm.ExecuteNonQuery();
                    dbcon.close();// to close connection
                    MessageBox.Show("Loại xe đã được đăng ký thành công!", title);
                    Clear();//to clear data field, after data inserted into the database
                    setting.loadVehicleType();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Yêu cầu nhập loại xe!", "Cảnh báo");
                    return; // return to the data field and form
                }
                if (MessageBox.Show("Bạn có muốn cập nhật loại xe?", "Cập nhật loại xe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbVehicleType SET name=@name, class=@class WHERE id=@id", dbcon.connect());
                    cm.Parameters.AddWithValue("@id", lblVid.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@class", cbClass.Text);

                    dbcon.open();// to open connection
                    cm.ExecuteNonQuery();
                    dbcon.close();// to close connection
                    MessageBox.Show("Cập nhật loại xe thành công!", title);
                    Clear();//to clear data field, after data inserted into the database
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region method
        public void Clear()
        {
            txtName.Clear();
            cbClass.SelectedIndex = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method
    }
}
