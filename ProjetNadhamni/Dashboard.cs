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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        Profile pf = new Profile();
        SqlConnection con = new SqlConnection();

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dashboard_Shown(object sender, EventArgs e)
        {

            try
            {

                con.ConnectionString = @"Data Source=DESKTOP-69MM1NJ\SQLEXPRESS;Initial Catalog=NadhamniDB;Integrated Security=True;Pooling=False";
                con.Open();
                if (Home.NewUser == false)
                {

                    SqlCommand cmd = new SqlCommand("select * from Tasks where UserName = '" + Home.FK + "'", con);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewTasks.Rows.Add(dr[0], dr[2], dr[1], dr[3], dr[4], dr[5], dr[7], dr[8], dr[6], dr[9]);
                        
                    }
                    dr.Close();
                }

                // Dashboard Notes
               if (!Home.Note)
                {

                    SqlCommand cmd = new SqlCommand("select * from Notes where UserName = '" + Home.FK + "'", con);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewNotes.Rows.Add(dr[1], dr[2]);
                    }
                    dr.Close();
                }
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            PnlPassage.Top = btn_dashboard.Top;
            PnlPassage.Height = btn_dashboard.Height;


        }



        private void btn_tasks_Click(object sender, EventArgs e)
        {
            PnlPassage.Top = btn_taskDash.Top;
            PnlPassage.Height = btn_taskDash.Height;
            pnlDashboard.Visible = false;
            this.Hide();
            Tasks tsk = new Tasks();
            tsk.Show();

        }

        private void btn_statistics_Click(object sender, EventArgs e)
        {
            PnlPassage.Top = btn_statDash.Top;
            PnlPassage.Height = btn_statDash.Height;
            this.Hide();
            Statistics stc = new Statistics();
            stc.Show();
        }

        private void btn_parameters_Click(object sender, EventArgs e)
        {
            PnlPassage.Top = btn_settingsDash.Top;
            PnlPassage.Height = btn_settingsDash.Height;
        }

        private void btn_profile_Click_1(object sender, EventArgs e)
        {
            PnlPassage.Top = btn_profileDash.Top;
            PnlPassage.Height = btn_profileDash.Height;
            Profile pf = new Profile();
            this.Hide();
            pf.Show();
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            PnlPassage.Top = btn_settingsDash.Top;
            PnlPassage.Height = btn_settingsDash.Height;
            this.Hide();
            Settings set = new Settings();
            set.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExitDashboard_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pnlDashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            EditTasks et = new EditTasks();
            et.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            
            
        }

        private void btn_saveNote_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=DESKTOP-69MM1NJ\\SQLEXPRESS;Initial Catalog=NadhamniDB;Integrated Security=True;Pooling=False";
                con.Open();
                //new note is added
                Home.Note = false;
                SqlCommand cmd = new SqlCommand("insert into Notes (Title,Description,UserName)values(@Title,@Description,@UserName)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Title", txt_title.Text);
                string[] Desc = rb_notes.Lines;
                string description = string.Join("", Desc);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@UserName", Home.FK);
                cmd.ExecuteNonQuery();
                //interface
                MessageBox.Show("done");



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

        private void btn_clearNote_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                rb_notes.Clear();
                txt_title.Clear();
                SqlCommand cmd = new SqlCommand("select * from Notes where UserName = '" + Home.FK + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ViewNotes.Rows.Add(dr[1], dr[2]);
                }
                dr.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_titleNotes_Click(object sender, EventArgs e)
        {
            txt_title.Clear();
        }
    }
    
}
