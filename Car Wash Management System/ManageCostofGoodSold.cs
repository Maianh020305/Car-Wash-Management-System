    using System;
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
    public partial class ManageCostofGoodSold: Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();  
        string title = "Car Wash Management System";
        Setting setting;
        bool check = false;
        public ManageCostofGoodSold(Setting sett)
        {
            InitializeComponent();
            setting = sett;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn đăng ký chi phí?", "Đăng ký chi phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCostofGood(costname,cost,date)VALUES(@costname,@cost,@date)", dbcon.connect());
                        cm.Parameters.AddWithValue("@costname", txtCostName.Text);
                        cm.Parameters.AddWithValue("@cost", txtCost.Text);
                        cm.Parameters.AddWithValue("@date", dtCoG.Value);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Chi phí đã được đăng ký thành công!", title);
                        Clear();//to clear data field, after data inserted into the database
                        setting.loadCostofGood();
                    }
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
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn chỉnh sửa chi phí?", "Chỉnh sửa chi phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbCostofGood SET costname=@costname, cost=@cost, date=@date WHERE id=@id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblCid.Text);
                        cm.Parameters.AddWithValue("@costname", txtCostName.Text);
                        cm.Parameters.AddWithValue("@cost", txtCost.Text);
                        cm.Parameters.AddWithValue("@date", dtCoG.Value);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Chi phí đã được chỉnh sửa thành công!", title);
                        Clear();//to clear data field, after data inserted into the database
                        this.Dispose();
                    }
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

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #region method
        public void checkField()
        {
            if (txtCostName.Text == "" || txtCost.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập dữ liệu!", "Cảnh báo");
                return; // return to the data field and form
            }
            check = true;
        }

        public void Clear()
        {
            txtCost.Clear();
            txtCostName.Clear();
            dtCoG.Value = DateTime.Now;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method
    }
}
