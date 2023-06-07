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
            cbLoader();
            cbalatkelamin.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSpecialis.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=biocare";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        string newiddokter;

        public void cbLoader()
        {
            dt = new DataTable();

            query = "SELECT d.id_dokter as 'id', d.speciality as 'nat' FROM dokter d;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            cbSpecialis.DataSource = dt;
            cbSpecialis.ValueMember = "id";
            cbSpecialis.DisplayMember = "nat";

            cbSpecialis.SelectedIndex = -1;

            cbalatkelamin.Items.Add("Male");
            cbalatkelamin.Items.Add("Female");
        }

        public void dgvloader()
        {
            dt = new DataTable();

            query = "SELECT * FROM biocare.dokter where `remove` = '0';";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["id_dokter"].Visible = false;
            dataGridView1.Columns["remove"].Visible = false;
        }

        public void generateiddokter()
        {
            dt = new DataTable();

            query = $"select id_dokter from dokter order by 1";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            List<string> listid = new List<string>();

            string lastid;
            string depan;
            string zero = "";

            int id;

            foreach (DataRow row in dt.Rows)
            {
                string iddokter = row["id_dokter"].ToString();
                listid.Add(iddokter);
            }

            lastid = listid[listid.Count - 1];
            depan = lastid.Substring(0, 1);
            id = Convert.ToInt32(lastid.Substring(1, lastid.Length - 1)) + 1;


            for (int i = 0; i < (lastid.Length) - (id.ToString().Length) - 1; i++)
            {
                zero += "0";
            }

            newiddokter = depan + zero + id;
        }

        public void removedokter(string iddokter)
        {
            query = $"update dokter set `remove` = '1' where id_dokter = '{iddokter}'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void insertdokter(string namadokter, string kelamin, string spesialis, string harga)
        {
            generateiddokter();
            query = $"INSERT INTO dokter values ('{newiddokter}', '{namadokter}', '{spesialis}', '{kelamin}', {harga}, '0');";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            MessageBox.Show("Dokter Berhasil di Input");
        }

        private void InsertPasien_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
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
            if (txtnamaDokter.Text == "" && cbalatkelamin.SelectedIndex != -1 && cbSpecialis.SelectedIndex != -1 && txtHarga.Text == "")
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                string namaperawat = txtnamaDokter.Text.ToString();
                string kelamin = cbalatkelamin.Text.ToString().Substring(0, 1);
                string spesialis = cbSpecialis.Text.ToString();
                string harga = txtHarga.Text.ToString();

                insertdokter(namaperawat, kelamin, spesialis, harga);

                this.Hide();

                Insert insrt = new Insert();
                insrt.ShowDialog();

                this.Close();
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

                this.Hide();
                Form1 parentForm = new Form1();
                parentForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Silahkan Pilih Dokter Yang Mau di Hapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
