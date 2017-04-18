using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineSimulater
{

    public partial class SetupUrl : Form
    {
        private Form1 _form1;
        public SetupUrl(Form1 fom)
        {
            _form1 = fom;

            InitializeComponent();
            textBox1.Text = _form1.URL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _form1.URL = textBox1.Text;
            this.Close();
        }
    }
}
