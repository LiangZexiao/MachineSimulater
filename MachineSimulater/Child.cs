using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MachineSimulater
{
    public partial class Child : Form
    {
        private Machine _machine;

        public Child(Machine machine)
        {
            _machine = machine;
            InitializeComponent();

            textBox1.Text = _machine.Name;
            textBox2.Text = _machine.Number;
            textBox3.Text = _machine.IpAddress;
            textBox4.Text = _machine.ProductionCycle;
            richTextBox1.Text = _machine.ParameterList;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Send()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _machine.Name = textBox1.Text;
            _machine.Number = textBox2.Text;
            _machine.IpAddress = textBox3.Text;
            _machine.ProductionCycle = textBox4.Text;
        }

    }
}
