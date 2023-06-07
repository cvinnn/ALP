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

            InsertPerawat ipr = new InsertPerawat();
            ipr.TopLevel = false;
            ipr.Parent = this;

            InsertObat io =  new InsertObat();
            io.TopLevel = false;
            io.Parent = this;

            insrt.ip = ip;
            insrt.id = id;
            insrt.ipr = ipr;
            insrt.io = io;
        }

    }
}
