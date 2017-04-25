using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulater3
{
    public partial class MachineStatus : Form
    {
        public MachineStatus(Machine _machine)
        {
            InitializeComponent();
            richTextBox1.Text = _machine.ToString();
        }
    }
}
