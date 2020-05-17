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
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }
        String starth, startm, endh, endm;
        String starttime, endtime;

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            Dashboard dsh2 = new Dashboard();
            this.Hide();
            dsh2.Show();
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
            Profile pf2 = new Profile();
            this.Hide();
            pf2.Show();
        }

        private void btn_statistics_Click(object sender, EventArgs e)
        {
            Statistics stc2 = new Statistics();
            this.Hide();
            stc2.Show();
        }

        private void btn_parameters_Click(object sender, EventArgs e)
        {
            Settings st2 = new Settings();
            this.Hide();
            st2.Show();
        }

        private void ExitDashboard_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_taskName_Click(object sender, EventArgs e)
        {
            txt_taskName.Clear();
        }

        private void txt_tolerTime_Click(object sender, EventArgs e)
        {
            txt_tolerTime.Clear();
        }

        private void txt_taskLocation_Click(object sender, EventArgs e)
        {
            txt_taskLocation.Clear();
        }

        private void btn_SaveTasks_Click(object sender, EventArgs e)
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
              
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Tasks(TaskName,Category,DateOfTask,StartTime,EndTime,ToleranceTime,Location,InvolvedPersons,TaskType,Done,UserName)values(@TaskName,@Category,@DateOfTask,@StartTime,@EndTime,@ToleranceTime,@Location,@InvolvedPersons,@TaskType,@Done,@UserName)", con);
                //the User is no more new
                Home.NewUser = false;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@TaskName", txt_taskName.Text);
                cmd.Parameters.AddWithValue("@Category", cmb_category.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DateOfTask", DateTask.Value.ToString());
                cmd.Parameters.AddWithValue("@StartTime", starttime);
                cmd.Parameters.AddWithValue("@EndTime", endtime);
                cmd.Parameters.AddWithValue("@ToleranceTime", txt_tolerTime.Text);
                cmd.Parameters.AddWithValue("@Location", txt_taskLocation.Text);
                string[] InvPer = rb_invoPer.Lines;
                string inv = string.Join(",", InvPer);
                cmd.Parameters.AddWithValue("@InvolvedPersons", inv);
                cmd.Parameters.AddWithValue("@TaskType", cmb_taskType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Done", "no");
                cmd.Parameters.AddWithValue("@UserName", Home.FK);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Task added");
                SuccessTaskAdded tsk = new SuccessTaskAdded();
                tsk.Show();
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
    }
}
