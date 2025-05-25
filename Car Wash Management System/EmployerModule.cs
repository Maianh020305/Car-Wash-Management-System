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
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Car_Wash_Management_System
{
    public partial class EmployerModule: Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        bool check = false;
        Employer employer;
        public EmployerModule(Employer emp)
        {
            InitializeComponent();
            employer = emp;
            cbRole.SelectedIndex = 2;
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal 
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
     
        // to create a function for clear all field
        #region method
        public void Clear()
        {
            txtAddress.Clear();
            txtName.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            txtSalary.Clear();

            dtDob.Value = DateTime.Now;
            cbRole.SelectedIndex = 2;//default is worker
        }

        //to check data field and date of birth
        public void checkField()
        {
            if (txtAddress.Text == "" || txtName.Text == "" || txtPhone.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Required data Field!", "Warning");
                return; // return to the data field and form
            }

            if (checkAge(dtDob.Value) < 18)
            {
                MessageBox.Show("Employer is under 18!", "Warning");
                return;
            }
            check = true;
        }

        // to check the age and calculate for under 18
        private static int checkAge(DateTime dateofBirth)
        {
            int age = DateTime.Now.Year - dateofBirth.Year;
            if (DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
                age = age - 1;
            return age;
        }



        #endregion method

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên?", "Đăng ký nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbEmployer(name,phone,address,dob,gender,role,salary,password)VALUES(@name,@phone,@address,@dob,@gender,@role,@salary,@password)", dbcon.connect());
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@gender", rdMale.Checked ? "Male" : "Female");//like if condition
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@salary", txtSalary.Text);
                        cm.Parameters.AddWithValue("@password", txtPassword.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Nhân viên đã được đăng ký thành công!", title);
                        check = false;
                        Clear();//to clear data field, after data inserted into the database                        
                                //employer.loadEmployer(); // refresh the employer list after insert data in the table
                        check = false;
                    }
                }
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
                    if (MessageBox.Show("Bạn có chắc chắn muốn cấp nhật nhân viên ?", "Cập nhật nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbEmployer SET name=@name, phone=@phone, address=@address, dob=@dob, gender=@gender, role=@role, salary=@salary, password=@password WHERE id=@id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblEid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@gender", rdMale.Checked ? "Male" : "Female");//like if condition
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@salary", txtSalary.Text);
                        cm.Parameters.AddWithValue("@password", txtPassword.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Nhân viên đã được cập nhật thành công!", title);
                        Clear();//to clear data field, after data inserted into the database
                        this.Dispose();
                        employer.loadEmployer();
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
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }

        private void cbRole_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbRole.Text == "Supervisor" || cbRole.Text == "Worker")
            {
                this.Height = 453 - 30;
                txtPassword.Clear();
                lblPass.Visible = false;// to hide password label and textbox
                txtPassword.Visible = false;
            }
            else
            {
                lblPass.Visible = true;
                txtPassword.Visible = true;
                this.Height = 453;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Dispose();

        }
    }
}
