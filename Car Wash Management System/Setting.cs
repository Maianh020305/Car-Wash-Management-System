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
    public partial class Setting : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Wash Management System";
        bool hasdetail = false;
        public Setting()
        {
            InitializeComponent();
            loadVehicleType();
            loadCostofGood();
            loadCompany();
        }

        #region VehicleType

        private void dgvVehicleType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void txtSearchVT_TextChanged(object sender, EventArgs e)
        {
            loadVehicleType();
        }

        private void btnAddVT_Click(object sender, EventArgs e)
        {
            ManageVehicleType module = new ManageVehicleType(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }
        public void loadVehicleType()
        {
            try
            {
                int i = 0; // to show number for vehicle list
                dgvVehicleType.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbVehicleType WHERE CONCAT (name,class) LIKE '%" + txtSearchVT.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    // to add data to the datagridview from the database
                    dgvVehicleType.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }



        #endregion VehicleType

        #region CostofGoodSold
        private void btnAddCoG_Click(object sender, EventArgs e)
        {
            ManageCostofGoodSold module = new ManageCostofGoodSold(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void txtSearchCoG_TextChanged(object sender, EventArgs e)
        {
            loadCostofGood();

        }
        //Cost of good sold mới đúng
        private void dgvControlGoodSold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCostofGoodSold.Columns[e.ColumnIndex].Name;
            if (colName == "EditCoG")
            {
                //to sent cost of good sold data to the manage cost of good sold module 
                ManageCostofGoodSold module = new ManageCostofGoodSold(this);
                module.lblCid.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtCostName.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtCost.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.dtCoG.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[4].Value.ToString();


                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "DeleteCoG") // if you want to delete the record to click the delete icon on the datagridview
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("DELETE FROM tbCostofGood WHERE id LIKE'" + dgvCostofGoodSold.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Cost of good sold data has been successfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadCostofGood();// to reload the cost of good sold list after edit and update the record
        }

        public void loadCostofGood()
        {
            try
            {
                int i = 0; // to show number for cost of good sole
                dgvCostofGoodSold.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbCostofGood WHERE CONCAT (costname,date) LIKE '%" + txtSearchCoG.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    // to add data to the datagridview from the database
                    dgvCostofGoodSold.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), DateTime.Parse(dr[3].ToString()).ToShortDateString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion CostofGoodSold

        #region CompanyName

        public void loadCompany()
        {
            try
            {
                dbcon.open();
                cm = new SqlCommand("SELECT * FROM tbCompany", dbcon.connect());
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    hasdetail = true;
                    txtComName.Text = dr["name"].ToString();
                    txtComAddress.Text = dr["address"].ToString();
                }
                else
                {
                    txtComName.Clear();
                    txtComAddress.Clear();
                }
                dr.Close();
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }


            #endregion CompanyName
        }

 
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Lưu chi tiết cửa hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {// now we create a function for execute querry only one line 
                    if (hasdetail)
                    {
                        dbcon.executeQuery("UPDATE tbCompany SET name='" + txtComName.Text + "', address='" + txtComAddress.Text + "'");
                    }
                    else
                    {
                        dbcon.executeQuery("INSERT INTO tbCompany (name,address)VALUES('" + txtComName.Text + "','" + txtComAddress.Text + "')");
                    }
                    MessageBox.Show("Chi tiết cửa hàng đã được lưu thành công!", "Lưu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
              txtComName.Clear();
            txtComAddress.Clear();
        }
    }
}
