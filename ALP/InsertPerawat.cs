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
    public partial class InsertPerawat : Form
    {
        public InsertPerawat()
        {
            InitializeComponent();
            cbalatkelamin.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLantai.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=biocare";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        string newidperawat;

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

            query = "SELECT * FROM biocare.perawat where `remove` = '0';";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["id_perawat"].Visible = false;
            dataGridView1.Columns["remove"].Visible = false;
        }

        public void generateidperawat()
        {
            dt = new DataTable();

            query = $"select id_perawat from perawat order by 1";
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
                string idperawat = row["id_perawat"].ToString();
                listid.Add(idperawat);
            }

            lastid = listid[listid.Count - 1];
            depan = lastid.Substring(0, 1);
            id = Convert.ToInt32(lastid.Substring(1, lastid.Length - 1)) + 1;


            for (int i = 0; i < (lastid.Length) - (id.ToString().Length) - 1; i++)
            {
                zero += "0";
            }

            newidperawat = depan + zero + id;
        }

        public void removeperawat(string idperawat)
        {
            query = $"update perawat set `remove` = '1' where id_pasien = '{idperawat}'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void insertperawat(string namaperawat, string kelamin, string lantai)
        {
            generateidperawat();
            query = $"INSERT INTO perawat values ('{newidperawat}', '{namaperawat}', '{kelamin}', '0');";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            MessageBox.Show("Perawat Berhasil di Input");

            Insert insrt = new Insert();
            insrt.Show();
        }

        private void InsertPasien_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;
            this.panel2.Visible = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            dgvloader();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtnamaperawat.Text == "" && cbalatkelamin.SelectedIndex != -1 && cbLantai.SelectedIndex != -1)
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                string namaperawat = txtnamaperawat.Text.ToString();
                string kelamin = cbalatkelamin.Text.ToString().Substring(0, 1);
                string lantai = cbLantai.Text.ToString();
                generateidperawat();

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

                Insert insrt = new Insert();
                insrt.Show();
            }
            else
            {
                MessageBox.Show("Silahkan Pilih Perawat Yang Mau di Hapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
