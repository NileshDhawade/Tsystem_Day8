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
    public partial class Form1 : Form
    {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dr;

            public Form1()
            {
                InitializeComponent();
                con = new SqlConnection(@"Server=NILESH\SQLEXPRESS;Database=T_System_PracticeDB;Integrated Security=True");

            }
            public void ClearData()
            {
                txtId.Clear();
                txtName.Clear();
                txtDesignation.Clear();
                txtSalary.Clear();
            }

            private void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //to pass values into the insert query we will use paeameters(parametric query)
                    string qry = "insert into Employee values(@id,@name,@designation,@salary)";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        MessageBox.Show("successfully saved the record");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            private void btnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    string qry = "select * from Employee where Id=@id";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                    dr = cmd.ExecuteReader();
                    //first will check that record is present
                    if (dr.HasRows)
                    {
                        //read the record from dr object
                        if (dr.Read())
                        {
                            txtName.Text = dr["Name"].ToString();
                            txtDesignation.Text = dr["Designation"].ToString();
                            txtSalary.Text = dr["Salary"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            private void btnUpdate_Click(object sender, EventArgs e)
            {
                try
                {
                    //to pass values into the insert query we will use paeameters(parametric query)
                    string qry = "update Employee set Name=@name,Designation=@designation,Salary=@salary where Id=@id";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        MessageBox.Show("successfully updated the record");
                    }
                    ClearData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            private void btnDelete_Click(object sender, EventArgs e)
            {
                try
                {
                    //to pass values into the insert query we will use paeameters(parametric query)
                    string qry = "delete from Employee where Id=@id";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));

                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        MessageBox.Show("successfully deleted the record");
                    }
                    ClearData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }

            private void btnAddNew_Click(object sender, EventArgs e)
            {
                try
                {
                    string qry = "select MAX(Id) from Employee";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    object obj = cmd.ExecuteScalar();
                    //DBNull is used to check existance og value in the obj
                    if (obj == DBNull.Value)
                    {
                        txtId.Text = "1";
                    }
                    else
                    {
                        int id = Convert.ToInt32(obj);//will get the MAX id from Employee
                        id++;
                        txtId.Text = id.ToString();
                    }
                    txtId.Enabled = false; ;
                    ClearData();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();

                }
            }
        }

    }

