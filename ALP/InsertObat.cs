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

            List<string> medicationNames = GetMedicationNames();
            List<string> medicationIDs = GetMedicationIds();

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
                textBox.Tag = i;
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
            quantityTextBoxes[index].Text = (currentValue + 1).ToString();
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;
            int currentValue = int.Parse(quantityTextBoxes[index].Text);
            if (currentValue > 0)
                quantityTextBoxes[index].Text = (currentValue - 1).ToString();
        }

        private List<string> GetMedicationNames()
        {
            List<string> medicationNames = new List<string>();

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

            return medicationNames;
        }

        private List<string> GetMedicationIds()
        {
            List<string> medicationIds = new List<string>();

            query = "SELECT id_obat FROM obat;";
            conn = new MySqlConnection(strconn);
            cmd = new MySqlCommand(query, conn);

            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string medicationId = reader.GetString(0);
                medicationIds.Add(medicationId);
            }

            reader.Close();
            conn.Close();

            return medicationIds;
        }
        private List<string> GetSelectedMedicationIds()
        {
            List<string> selectedMedicationIds = new List<string>();

            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox textBox && textBox.Tag is string medicationId)
                {
                    if (int.TryParse(textBox.Text, out int quantity) && quantity > 0)
                    {
                        selectedMedicationIds.Add(medicationId);
                    }
                }
            }
            return selectedMedicationIds;
        }


        public void insertUseObat(string idobat, int qty, string idnota)
        {

        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            selectedid = GetSelectedMedicationIds();


        }
    }
}
