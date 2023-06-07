using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALP
{
    public partial class Insert : Form
    {
        public Insert()
        {
            InitializeComponent();
        }
        public InsertPasien ip { get; set; }
        public InsertDokter id { get; set; }
        public InsertPerawat ipr { get; set; }
        public InsertObat io { get; set; }

        private void btnPasien_Click(object sender, EventArgs e)
        {
            ip.Show();
            this.Hide();
        }
        private void btnPerawat_Click(object sender, EventArgs e)
        {
            ipr.Show();
            this.Hide();
        }
        private void btnDokter_Click(object sender, EventArgs e)
        {
            id.Show();
            this.Hide();
        }

        private void btnobat_Click(object sender, EventArgs e)
        {
            io.Show();
            this.Hide();
        }

        private void Insert_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
        }
    }
}
