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

namespace Tsystem_ADO.NET_Demo
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=NILESH\SQLEXPRESS;Database=T_System_PracticeDB;Integrated Security=True");
        }
        public DataSet GetAllEmployess()
        {
            // select query which returns all records
            da = new SqlDataAdapter("select * from Employee", con);
            // MissingSchemaAction is used to set PK to the col which we have added in DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Emp"); // name given to the Table which is in the DataSet
            // Fill() open the connection fetch records & closes the connection
            return ds;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                DataRow row = ds.Tables["Emp"].NewRow();
                row["Id"] = txtId.Text;
                row["Name"] = txtName.Text;
                row["Designation"] = txtDesignation.Text;
                row["Salary"] = txtSalary.Text;
                ds.Tables["Emp"].Rows.Add(row);
                int result = da.Update(ds.Tables["Emp"]); // this will reflect the changes in to the DB
                if (result == 1)
                {
                    MessageBox.Show("Inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                //Id -PK  we can use Find()
                DataRow row = ds.Tables["Emp"].Rows.Find(Convert.ToInt32(txtId.Text));
                if (row != null)
                {
                    txtId.Text = row["Id"].ToString();
                    txtName.Text = row["Name"].ToString();
                    txtDesignation.Text = row["Designation"].ToString();
                    txtSalary.Text = row["Salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                DataRow row = ds.Tables["Emp"].Rows.Find(Convert.ToInt32(txtId.Text));

                row["Name"] = txtName.Text;
                row["Designation"] = txtDesignation.Text;
                row["Salary"] = txtSalary.Text;

                int result = da.Update(ds.Tables["Emp"]); // this will reflect the changes in to the DB
                if (result == 1)
                {
                    MessageBox.Show("updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                DataRow row = ds.Tables["Emp"].Rows.Find(Convert.ToInt32(txtId.Text));
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["Emp"]); // this will reflect the changes in to the DB
                    if (result == 1)
                    {
                        MessageBox.Show("deleted");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
