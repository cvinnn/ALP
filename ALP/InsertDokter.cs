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
    public partial class InsertDokter : Form
    {
        public InsertDokter()
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
            dt = new DataTable();

            query = "";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            cbSpecialis.DataSource = dt;
            cbSpecialis.ValueMember = "id";
            cbSpecialis.DisplayMember = "nat";

            cbSpecialis.SelectedItem = "";
            cbSpecialis.SelectedValue = "";

            cbalatkelamin.Items.Add("Male");
            cbalatkelamin.Items.Add("Female");
        }

        public void dgvloader()
        {
            dt = new DataTable();

            query = "SELECT * FROM biocare.dokter;";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["id_dokter"].Visible = false;
        }

        public void removedokter(string iddokter)
        {
            query = $"update pasien set status 'N'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void insertdokter(string namadokter, string kelamin, string spesialis, string harga)
        {
            query = $"";
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
            if (txtnamaDokter.Text == "" && cbalatkelamin.SelectedText == "" && cbSpecialis.SelectedText == "" && txtHarga.Text == "")
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                string namaperawat = txtnamaDokter.Text.ToString();
                string kelamin = cbalatkelamin.SelectedText.ToString();
                string spesialis = cbSpecialis.SelectedText.ToString();
                string harga = txtHarga.Text.ToString();

                insertdokter(namaperawat, kelamin, spesialis, harga);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                string selectedValue = selectedRow.Cells["id_dokter"].Value.ToString();

                removedokter(selectedValue);

                dgvloader();

                MessageBox.Show("Dokter Sudah di Hapus", "Berhasil", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Silahkan Pilih Dokter Yang Mau di Hapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
