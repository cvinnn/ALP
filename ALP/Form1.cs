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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Insert insrt = new Insert();
            insrt.TopLevel = false;
            insrt.Parent = this;
            insrt.Show();

            InsertPasien ip = new InsertPasien();
            ip.TopLevel = false;
            ip.Parent = this;

            InsertDokter id = new InsertDokter();
            id.TopLevel = false;
            id.Parent = this;

            Form2 f2 = new Form2();
            f2.TopLevel = false;
            f2.Parent = this;

            insrt.ip = ip;
            insrt.id = id;
            insrt.f2 = f2;
        }

        private void ipload()
        {
            InsertPasien insrt = new InsertPasien();
            insrt.TopLevel = false;
            insrt.Parent = this;
            insrt.Show();
        }

    }
}
