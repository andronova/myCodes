using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace InsertUpdateDeleteDemo
{
    
    public partial class frmMain : Form
    {
       
     
            OracleConnection con = new OracleConnection("Data Source=(DESCRIPTION ="
                                        + "(ADDRESS = (PROTOCOL = TCP)(HOST = 127.0.0.1)(PORT = 1521))"
                                        + "(CONNECT_DATA =" + "(SERVER = DEDICATED)"
                                        + "(SERVICE_NAME = BELSOFT)));"
                                        + "User Id= SAMPLE;Password=BELSOFT;");
             OracleCommand cmd;
             OracleDataAdapter adapt;
             int ID = 0;
        public frmMain()
        {
            InitializeComponent();
            DisplayData();
        }
        
        private void btn_Insert_Click(object sender, EventArgs e)
        {        

            con.Open();
            if (txt_Name.Text != "" && txt_State.Text != "")
            {
                string sSql = "Insert into TBL_RECORD(TCNO,NAME,STATE)" + "Values('" + txt_TcNo.Text + "','" + txt_Name.Text + "','" + txt_State.Text +"')";
                cmd = new OracleCommand(sSql,con);       
               try
                {
                    cmd.ExecuteNonQuery();  
                    MessageBox.Show("Record Inserted Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();
                }
                catch (Exception exp)
                {
                    MessageBox.Show ("Code: " + exp.Source.ToString() + "\n" + "Message: " + exp.Message.ToString() + "\n" + "An exception occurred. Please contact your system administrator.");
                    con.Close();
                }

            }
            else
            {
                MessageBox.Show("Please Provide Details!");
                con.Close();
            }
        }
        //Display Data in DataGridView  
        private void DisplayData()
        {
            string sSql = "select * from tbl_Record";
            con.Open();
            DataTable dt = new DataTable();
            adapt = new OracleDataAdapter(sSql, con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            txt_TcNo.Text = "";
            txt_Name.Text = "";
            txt_State.Text = "";
            ID = 0;
        }
        //dataGridView1 RowHeaderMouseClick Event  
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            { 
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txt_TcNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_State.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            catch(Exception exp)
            {
               // MessageBox.Show("Code: " + exp.Source.ToString() + "\n" + "Message: " + exp.Message.ToString() + "\n" + "An exception occurred. Please contact your system administrator.");
                con.Close();
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text != "" && txt_State.Text != "")
            {
                con.Open();
                string sSql = "update tbl_Record set TCNO='" + txt_TcNo.Text +"', NAME= '"+ txt_Name.Text +"', STATE= '"+ txt_State.Text +"' WHERE ID="+ID;
                cmd = new OracleCommand(sSql, con);
                   
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }
        //Delete Record  
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                con.Open();
                string sSql = "delete tbl_Record where ID="+ID;
                cmd = new OracleCommand(sSql, con);
                
            
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

    }

}
