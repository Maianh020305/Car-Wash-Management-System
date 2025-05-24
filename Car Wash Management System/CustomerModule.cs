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
    public partial class CustomerModule : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        bool check = false;
        public int vid = 0;
        Customer customer;
        public CustomerModule(Customer cust)
        {
            InitializeComponent();
            customer = cust;
        }

        private void CustomerModule_Load(object sender, EventArgs e)
        {
            // to add vehicle list in the combobox
            cbCarType.DataSource = vehicleType();
            cbCarType.DisplayMember = "name";
            cbCarType.ValueMember = "id";
            if (vid > 0)
                cbCarType.SelectedValue = vid;

        }
        #region method
        // to create a function vehicle type for return data table of vehicle type
        public DataTable vehicleType()
        {
            cm = new SqlCommand("SELECT * FROM tbVehicleType", dbcon.connect());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            adapter.SelectCommand = cm;
            adapter.Fill(dataTable);

            return dataTable;
        }


        // to create a function for data field
        public void Clear()
        {
            txtAddress.Clear();
            txtCarModel.Clear();
            txtCarNo.Clear();
            txtName.Clear();
            txtPhone.Clear();

            cbCarType.SelectedIndex = 0;
            udPoints.Value = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void checkField()
        {
            if (txtAddress.Text == "" || txtName.Text == "" || txtPhone.Text == "" || txtCarNo.Text == "" || txtCarModel.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo");
                return; // return to the data field and form
            }

            check = true;
        }
        #endregion method

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn đăng ký khách hàng?", "Đăng ký khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCustomer(vid,name,phone,carno,carmodel,address,points)VALUES(@vid,@name,@phone,@carno,@carmodel,@address,@points)", dbcon.connect());
                        cm.Parameters.AddWithValue("@vid", cbCarType.SelectedValue);// to save id number of vehicle type
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@carno", txtCarNo.Text);
                        cm.Parameters.AddWithValue("@carmodel", txtCarModel.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@points", udPoints.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Khách hàng đã đăng ký thành công!", title);
                        check = false;
                        Clear();//to clear data field, after data inserted into the database                        
                    }
                }
                customer.loadCustomer();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin khách hàng?", "Cập nhật thông tin khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbCustomer SET vid=@vid, name=@name, phone=@phone, carno=@carno, carmodel=@carmodel, address=@address, points=@points WHERE id=@id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblCid.Text);
                        cm.Parameters.AddWithValue("@vid", cbCarType.SelectedValue);// to save id number of vehicle type
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@carno", txtCarNo.Text);
                        cm.Parameters.AddWithValue("@carmodel", txtCarModel.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@points", udPoints.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Thông tin khách hàng đã được cập nhật thành công!", title);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
