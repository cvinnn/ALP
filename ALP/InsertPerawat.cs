using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ALP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=biocare";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        public void cbLoader()
        {
            cbLantai.Items.Add("1");
            cbLantai.Items.Add("2");
            cbLantai.Items.Add("3");
            cbLantai.Items.Add("4");
            cbLantai.Items.Add("5");

            cbalatkelamin.Items.Add("Male");
            cbalatkelamin.Items.Add("Female");
        }

        public void dgvloader()
        {
            dt = new DataTable();

            query = "SELECT * FROM biocare.perawat;";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["id_perawat"].Visible = false;
        }

        public void removeperawat(string idperawat)
        {
            query = $"update pasien set status 'N'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void insertperawat(string namaperawat, string kelamin, string lantai)
        {
            query = $"INSERT INTO perawat values (id, '{namaperawat}', '{kelamin}', 'Y');";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            MessageBox.Show("Pasien Berhasil di Input");
        }

        private void InsertPasien_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;

            cbLoader();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            dgvloader();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtnamaperawat.Text == "" && cbalatkelamin.SelectedText == "" && cbLantai.SelectedText == "")
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                string namaperawat = txtnamaperawat.Text.ToString();
                string kelamin = cbalatkelamin.SelectedText.ToString();
                string lantai = cbLantai.SelectedText.ToString();

                insertperawat(namaperawat, kelamin, lantai);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                string selectedValue = selectedRow.Cells["id_perawat"].Value.ToString();

                removeperawat(selectedValue);

                dgvloader();

                MessageBox.Show("Perawat Sudah di Hapus", "Berhasil", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Silahkan Pilih Perawat Yang Mau di Hapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
