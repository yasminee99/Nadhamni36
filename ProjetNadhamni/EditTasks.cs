using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetNadhamni
{
    public partial class EditTasks : Form
    {
        public EditTasks()
        {
            InitializeComponent();
        }
        String starth, startm, endh, endm;
        String starttime, endtime;

        private void button1_Click(object sender, EventArgs e)
        {
            
            /*SqlConnection con = new SqlConnection();

            con.ConnectionString = @"Data Source=DESKTOP-69MM1NJ\SQLEXPRESS;Initial Catalog=NadhamniDB;Integrated Security=True;Pooling=False";
            con.Open();
            MessageBox.Show("DB connected");
            SqlCommand cmd = new SqlCommand(" select * from Tasks WHERE id='" + int.Parse(username.Text) + "'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                firstname.Text = (dr["TaskName"].ToString());
                birthday.Text = Convert.ToDateTime(dr["DateOfTask"]).ToShortDateString();
            }
            con.Close();*/

        }

        private void EditTasks_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            starth = cmb_startTimeTaskH.Text;
            startm = cmb_startTimeTaskM.Text;
            starttime = starth + startm;
            endh = cmb_endTimeTaskH.Text;
            endm = cmb_endTimeTaskM.Text;
            endtime = endh + endm;


            try
            {
                con.ConnectionString = @"Data Source=DESKTOP-69MM1NJ\SQLEXPRESS;Initial Catalog=NadhamniDB;Integrated Security=True;Pooling=False";

                string[] InvPerEdit = rb_invoPer.Lines;
                string inv = string.Join(",", InvPerEdit);
                con.Open();
                
                SqlCommand cmd = new SqlCommand("update Tasks set TaskName=@TaskName,Category=@Category,DateOfTask=@DateOfTask,StartTime=@StartTime,EndTime=@EndTime,ToleranceTime=@ToleranceTime,Location=@Location,InvolvedPersons=@InvolvedPersons,TaskType=@TaskType,Done=@Done where UserName='" + Home.FK + "' and id='" + int.Parse(txt_idTask.Text) + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@TaskName", txt_taskName.Text);
                cmd.Parameters.AddWithValue("@Category", cmb_category.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DateOfTask", DateTask.Value.ToString());
                cmd.Parameters.AddWithValue("@StartTime", starttime);
                cmd.Parameters.AddWithValue("@EndTime", endtime);
                cmd.Parameters.AddWithValue("@ToleranceTime", txt_tolerTime.Text);
                cmd.Parameters.AddWithValue("@Location", txt_taskLocation.Text);
                cmd.Parameters.AddWithValue("@InvolvedPersons", inv);
                cmd.Parameters.AddWithValue("@TaskType", cmb_taskType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Done", "no");
                
                cmd.ExecuteNonQuery();
                
                MessageBox.Show("success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void icon_delay_Tick(object sender, EventArgs e)
        {
            //btn_OK.Visible = true;

            icon.Enabled = false;
            icon_delay.Stop();
        }

        private void bunifuFormFadeTransition1_TransitionEnd(object sender, EventArgs e)
        {
            icon_delay.Start();
            icon.Enabled = true;
        }

        private void txt_idTask_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_taskName_Click(object sender, EventArgs e)
        {
            txt_taskName.Clear();
        }

        private void txt_category_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_dateTask_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_tolerTime_Click(object sender, EventArgs e)
        {
            txt_tolerTime.Clear();
        }

        private void txt_taskLocation_Click(object sender, EventArgs e)
        {
            txt_taskLocation.Clear();
        }

        private void rb_invoPer_Click(object sender, EventArgs e)
        {
            rb_invoPer.Clear();
        }

        

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();

           con.ConnectionString = @"Data Source=DESKTOP-69MM1NJ\SQLEXPRESS;Initial Catalog=NadhamniDB;Integrated Security=True;Pooling=False";
           con.Open();
            //MessageBox.Show("Data base connected");
            DBconnecting db = new DBconnecting();
            db.Show();
            SqlCommand cmd = new SqlCommand(" select * from Tasks WHERE id='" + int.Parse(txt_idTask.Text) + "'", con);
           cmd.CommandType = CommandType.Text;
           SqlDataReader dr = cmd.ExecuteReader();
           if (dr.Read())
           {
               txt_taskName.Text = (dr["TaskName"].ToString());
               txt_category.Text = (dr["Category"].ToString());
               txt_dateTask.Text = Convert.ToDateTime(dr["DateOfTask"]).ToShortDateString();
               txt_startTime.Text= (dr["StartTime"].ToString());
               txt_endTime.Text  = (dr["EndTime"].ToString());
               txt_taskType.Text = (dr["TaskType"].ToString());
               txt_tolerTime.Text= (dr["ToleranceTime"].ToString());
               txt_taskLocation.Text= (dr["Location"].ToString());
               rb_invoPer.Text= (dr["InvolvedPersons"].ToString());

            }
           con.Close();
        }
    }
}
