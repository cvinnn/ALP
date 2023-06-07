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
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace ALP
{
    public partial class InsertPasien : Form
    {
        public InsertPasien()
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

        string newidpasien;
        string newidpj;
        string newnokamar;
        string newidnota;

        public void cbLoader()
        {
            dt = new DataTable();

            query = "SELECT d.id_dokter as 'id', d.nama_dokter as 'nat' FROM dokter d;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            cbDokter.DataSource = dt;
            cbDokter.ValueMember = "id";
            cbDokter.DisplayMember = "nat";

            cbDokter.SelectedItem = "";
            cbDokter.SelectedValue = "";

            dt = new DataTable();

            query = "select id_jenis, nama_jenis from jenis_kamar";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            cbDokter.DataSource = dt;
            cbDokter.ValueMember = "id_jenis";
            cbDokter.DisplayMember = "nama_jenis";

            cbDokter.SelectedItem = "";
            cbDokter.SelectedValue = "";

            dt = new DataTable();

            query = "select hubungan from penanggung_jawab group by 1 order by 1";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            cbDokter.DataSource = dt;
            cbDokter.ValueMember = "hubungan";
            cbDokter.DisplayMember = "hubungan";

            cbDokter.SelectedItem = "";
            cbDokter.SelectedValue = "";

            cbalatkelamin.Items.Add("Male");
            cbalatkelamin.Items.Add("Female");
        }

        public void dgvloader()
        {
            dt = new DataTable();

            query = "SELECT p.id_pasien as 'idpasien', k.no_kamar as 'No. Kamar', p.nama_pasien as 'Pasien', j.nama_PJ as 'Penanggung Jawab', d.nama_dokter as 'Dokter' FROM pasien p, kamar k, penanggung_jawab j, dokter d, nota n WHERE n.id_pasien = p.id_pasien and n.id_kamar = k.id_kamar and n.id_dokter = d.id_dokter and j.id_penanggungJawab = p.id_penanggungJawab;";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["idpasien"].Visible = false;
        }

        public void removepasien(string idpasien)
        {
            query = $"update pasien set status 'N' where id_pasien = {idpasien}";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void generateidpasien()
        {
            dt = new DataTable();

            query = "SELECT p.id_pasien as 'idpasien' FROM pasien p;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            List<string> listkamar = new List<string>();

            string lastid;
            string depan;
            string zero = "";

            int id;

            foreach (DataRow row in dt.Rows)
            {
                listkamar.Add(row.ToString());
            }

            lastid = listkamar.Last();
            depan = lastid.Substring(0, 1);
            id = Convert.ToInt32(lastid.Substring(1, lastid.Length - 1)) + 1;


            for (int i = 0; i < (lastid.Length) - (id.ToString().Length) - 1; i++)
            {
                zero += "0";
            }

            newidpj = depan + zero + id;
        }

        public void generateidpj()
        {
            dt = new DataTable();

            query = "SELECT p.id_penanggungjawab as 'idpj' FROM penanggung_jawab p;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            List<string> listkamar = new List<string>();

            string lastid;
            string depan;
            string zero = "";

            int id;

            foreach (DataRow row in dt.Rows)
            {
                listkamar.Add(row.ToString());
            }

            lastid = listkamar.Last();
            depan = lastid.Substring(0, 1);
            id = Convert.ToInt32(lastid.Substring(1, lastid.Length - 1)) + 1;


            for (int i = 0; i < (lastid.Length) - (id.ToString().Length) - 1; i++)
            {
                zero += "0";
            }

            newidpj = depan + zero + id;
        }

        public void generateidkamar(string idjenis)
        {
            dt = new DataTable();

            query = $"select id_kamar from kamar where id_jenis = '{idjenis}' order by 1";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            List<string> listkamar = new List<string>();

            string lastid;
            string depan;
            string zero = "";

            int id;

            foreach (DataRow row in dt.Rows)
            {
                listkamar.Add(row.ToString());
            }

            lastid = listkamar.Last();
            depan = lastid.Substring(0,1);
            id = Convert.ToInt32(lastid.Substring(1, lastid.Length-1)) + 1;


            for (int i = 0; i < (lastid.Length) - (id.ToString().Length) - 1; i++)
            {
                zero += "0";
            }

            newnokamar = depan + zero + id;
        }
        public void generateidnota()
        {
            dt = new DataTable();

            query = $"select id_nota from nota order by 1";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            List<string> listkamar = new List<string>();

            string lastid;
            string depan;
            string zero = "";

            int id;

            foreach (DataRow row in dt.Rows)
            {
                listkamar.Add(row.ToString());
            }

            lastid = listkamar.Last();
            depan = lastid.Substring(0, 1);
            id = Convert.ToInt32(lastid.Substring(1, lastid.Length - 1)) + 1;


            for (int i = 0; i < (lastid.Length) - (id.ToString().Length) - 1; i++)
            {
                zero += "0";
            }

            newidnota = depan + zero + id;
        }
        public void insertpasien(string namapasien, string namapj, string tgllahir, string kelamin, string alamat,string kota, string notelp, string iddokter, string notelppj, string hub, string tglmsk, string tglkeluar, string idjenis)
        {
            generateidpasien();
            generateidpj();
            generateidkamar(idjenis);
            generateidnota();

            query = $"insert into pasien values ('{newidpasien}', '{namapasien}', '{tgllahir}, '{alamat}', '{kota}', '{notelp}', '{newidpj}', '{kelamin}', '0'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            query = $"insert into penanggung_jawab values ('{newidpj}', '{namapj}', '{notelppj}, '{hub}'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            query = $"insert into nota values ('{newidnota}', '{newidpasien}', '{iddokter}, '{newnokamar}', '{tglmsk}', '{tglkeluar}', 0, '0'";
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

        private void txtnotelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dtptgllahir.Value.ToString() == "" && dtptglmsk.Value.ToString() == "" && dtptglkluar.Value.ToString() == "" && txtnamapasien.Text == "" && txtnamapj.Text == "" && txtnotelp.Text == "" && txtalamat.Text == "" && cbalatkelamin.SelectedText == "" && cbDokter.SelectedText == "" && txtkota.Text == "" && cbJnsKmr.SelectedText == "" && txtnopj.Text == "" && cbhub.SelectedText == "")
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                string tgllahir = dtptgllahir.Value.ToString("yyyy-MM-dd");
                string tglmasuk = dtptglmsk.Value.ToString("yyyy-MM-dd");
                string tglkeluar = dtptglkluar.Value.ToString("yyyy-MM-dd");

                string namapasien = txtnamapasien.Text.ToString();
                string namapj = txtnamapj.Text.ToString();
                string notelp = txtnotelp.Text.ToString();
                string alamat = txtalamat.Text.ToString();
                string kelamin = cbalatkelamin.SelectedText.ToString().Substring(0,1);
                string dokter = cbDokter.SelectedValue.ToString();
                string kota = txtkota.Text.ToString();
                string idjenis = cbJnsKmr.SelectedValue.ToString();
                string notelppj = txtnopj.Text.ToString();
                string hub = cbhub.SelectedText.ToString();

                insertpasien(namapasien, namapj, tgllahir, kelamin, alamat, kota, notelp, dokter, notelppj, hub, tglmasuk, tglkeluar, idjenis);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                string selectedValue = selectedRow.Cells["idpasien"].Value.ToString();

                removepasien(selectedValue);

                dgvloader();

                MessageBox.Show("Pasien Sudah di Hapus", "Berhasil", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Silahkan Pilih Pasien Yang Mau di Hapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
