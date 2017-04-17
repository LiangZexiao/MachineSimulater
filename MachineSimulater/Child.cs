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
    public partial class Child : Form
    {
        public Child(Machine _machine)
        {
            if (_machine.Number==null) MessageBox.Show("_machine is null"); 

            InitializeComponent();

            textBox1.Text = _machine.Number;
            textBox2.Text = _machine.IpAddress;
            textBox3.Text = _machine.ProductionCycle;
            richTextBox1.Text = _machine.ParameterList;
        }
    }
}
