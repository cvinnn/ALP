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
    public partial class InsertObat : Form
    {
        public InsertObat()
        {
            InitializeComponent();
            InitializeMedicationPanel();
            cbPasien.SelectedIndexChanged += cbPasien_SelectedIndexChanged;
            panel1.Visible = false;
            cbPasien.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=biocare";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        List<string> medicationNames;
        List<string> medicationIDs;


        public void cbLoader()
        {
            List<string> listid = new List<string>();
            List<string> listnama = new List<string>();
            List<string> listidnama = new List<string>();

            dt = new DataTable();

            query = "SELECT p.id_pasien as 'id', p.nama_pasien as 'nat' FROM pasien p;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);


            cbPasien.DataSource = dt;
            cbPasien.ValueMember = "id";
            cbPasien.DisplayMember = "nat";

            cbPasien.SelectedItem = "";
            cbPasien.SelectedValue = "";
        }

        private void InsertObat_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;

            cbLoader();
            InitializeMedicationPanel();
            cbPasien.SelectedIndexChanged += cbPasien_SelectedIndexChanged;
            panel1.Visible = false;
        }

        private List<Label> medicationLabels;
        private List<TextBox> quantityTextBoxes;
        private List<Button> plusButtons;
        private List<Button> minusButtons;

        public List<string> selectedid = new List<string>();

        private void InitializeMedicationPanel()
        {
            panel1.Size = new Size(1155, 546);

            medicationLabels = new List<Label>();
            quantityTextBoxes = new List<TextBox>();
            plusButtons = new List<Button>();
            minusButtons = new List<Button>();

            GetMedicationIDs();
            GetMedicationNames();

            for (int i = 0; i < medicationNames.Count; i++)
            {
                int row = i % 10;
                int col = i / 10;
                
                Label label = new Label();
                label.Text = medicationNames[i];
                label.AutoSize = true;
                label.Location = new Point(10 + col * 110, 10 + row * 50);
                medicationLabels.Add(label);
                panel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Text = "0";
                textBox.Size = new Size(40, 20);
                textBox.Location = new Point(10 + col * 110, 30 + row * 50);
                textBox.Tag = medicationNames[i];
                quantityTextBoxes.Add(textBox);
                panel1.Controls.Add(textBox);
                textBox.KeyPress += TextBox_KeyPress;

                Button plusButton = new Button();
                plusButton.Text = "+";
                plusButton.Size = new Size(20, 20);
                plusButton.Location = new Point(55 + col * 110, 30 + row * 50);
                plusButton.Tag = i; 
                plusButton.Click += PlusButton_Click;
                plusButtons.Add(plusButton);
                panel1.Controls.Add(plusButton);

                Button minusButton = new Button();
                minusButton.Text = "-";
                minusButton.Size = new Size(20, 20); 
                minusButton.Location = new Point(80 + col * 110, 30 + row * 50);
                minusButton.Tag = i; 
                minusButton.Click += MinusButton_Click; 
                minusButtons.Add(minusButton);
                panel1.Controls.Add(minusButton);
            }

        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbPasien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPasien.SelectedIndex != -1 && cbPasien.DroppedDown == false)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;
            int currentValue = int.Parse(quantityTextBoxes[index].Text);
            {
                quantityTextBoxes[index].Text = (currentValue + 1).ToString();
            }
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;
            int currentValue = int.Parse(quantityTextBoxes[index].Text);
            if (currentValue > 0)
            {
                quantityTextBoxes[index].Text = (currentValue - 1).ToString();
            }
        }

        private void GetMedicationNames()
        {
            medicationNames = new List<string>();

            query = "SELECT nama_obat FROM obat;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);

            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string medicationName = reader.GetString(0);
                medicationNames.Add(medicationName);
            }

            reader.Close();
            conn.Close();
        }

        private void GetMedicationIDs()
        {
            medicationIDs = new List<string>();

            query = "SELECT id_obat FROM obat;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);

            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string medicationid = reader.GetString(0);
                medicationIDs.Add(medicationid);
            }

            reader.Close();
            conn.Close();
        }

        public void GetSelectedObat()
        {
            GetSelectedTag = new List<string>();
            GetSelectedQty = new List<int>();

            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    if (Convert.ToInt16(txt.Text) > 0)
                    {
                        GetSelectedTag.Add(txt.Tag.ToString());
                        GetSelectedQty.Add(Convert.ToInt16(txt.Text));
                    }
                }
            }
        }

        public string GetSelectedID;
        public List<string> GetSelectedTag;
        public List<int> GetSelectedQty;

        private void GenerateIDUO(string idpasien)
        {
            dt = new DataTable();

            query = $"select id_nota, no_kamar, id_useobat from nota n join kamar k on k.id_kamar = n.id_kamar where n.id_pasien = '{idpasien}'";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);
            adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dt);

            idnota = dt.Rows[0]["id_nota"].ToString();
            nokamar = dt.Rows[0]["no_kamar"].ToString();
            iduseobat = dt.Rows[0]["id_useobat"].ToString();
        }

        public void InsertUseObat(string idobat, int qty)
        {
            query = $"INSERT INTO useobat VALUES ('{iduseobat}', '{idobat}', {qty}, '{idnota}')";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            MessageBox.Show("Obat Berhasil Di Input!");
        }

        public string iduseobat;
        public string nokamar;
        public string idnota;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            GetSelectedObat();
            if (cbPasien.SelectedText == "" && GetSelectedTag.Count <= 0)
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                string idpasien = cbPasien.SelectedValue.ToString();
                GenerateIDUO(idpasien);
                for (int i = 0; i < GetSelectedTag.Count; i++)
                {
                    string idobat = medicationIDs[medicationNames.IndexOf(GetSelectedTag[i])];
                    int qty = GetSelectedQty[i];
                    InsertUseObat(idobat, qty);
                }

                this.Close();
            }
        }
    }
}
