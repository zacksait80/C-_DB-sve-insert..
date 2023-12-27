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

namespace dbReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-T7U32P7;Initial Catalog=DBssave;Integrated Security=True");


        private void Form1_Load(object sender, EventArgs e)
        {
            bind_data();
        }
        private void bind_data()
        {
            SqlCommand cmd = new SqlCommand("SELECT ID , NAME  ,PHONE, PASSWORD FROM dbsve", conn);
            SqlDataAdapter sda  = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("tohome", 12, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("arial",12,FontStyle.Bold);

        }

        private void save_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into dbsve (ID,NAME , PHONE , PASSWORD)VALUES(@ID,@NAME  ,@PHONE, @PASSWORD )", conn);
           if(txtId.Text == ""|| txtNm.Text == "" || txtPho.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("buuxi meesha banan..");
            }
           else
            {
                cmd.Parameters.AddWithValue("ID", txtId.Text);
                cmd.Parameters.AddWithValue("NAME", txtNm.Text);
                cmd.Parameters.AddWithValue("PHONE", txtPho.Text);
                cmd.Parameters.AddWithValue("PASSWORD", txtPass.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                bind_data();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update dbsve Set NAME=@NAME , PHONE=@PHONE , PASSWORD=@PASSWORD  where ID=@ID", conn);
            cmd.Parameters.AddWithValue("ID", txtId.Text);
            cmd.Parameters.AddWithValue("NAME", txtNm.Text);
            cmd.Parameters.AddWithValue("PHONE", txtPho.Text);
            cmd.Parameters.AddWithValue("PASSWORD", txtPass.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            bind_data();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtId.Text = selectedRow.Cells[0].Value.ToString();
            txtNm.Text = selectedRow.Cells[1].Value.ToString();
            txtPho.Text = selectedRow.Cells[2].Value.ToString();
            txtPass.Text = selectedRow.Cells[3].Value.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from dbsve where ID= @ID ", conn);
            cmd.Parameters.AddWithValue("ID",txtId.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            bind_data();

        }

        private void grid(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID , NAME  ,PHONE, PASSWORD FROM dbsve WHERE ID LIKE @ID+'%' OR NAME LIKE @NAME+'%'", conn);
            cmd.Parameters.AddWithValue("ID", txtSearch.Text);
            cmd.Parameters.AddWithValue("NAME",txtSearch.Text);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("tohome", 12, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("arial", 12, FontStyle.Bold);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            e.Graphics.DrawImage(bm, 120, 20);

        }
    }
}
