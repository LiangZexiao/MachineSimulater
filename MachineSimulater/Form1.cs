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
    public partial class Form1 : Form
    {
        public string URL = "";
        
        public Form1()
        {         
            InitializeComponent();
            toolStripStatusLabel1.Text = "URL:" + URL;
        }

        private void 添加机器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Machine _machine = new Machine();
            _machine.Number = "1";
            _machine.IpAddress = "192.168.1.1";
            _machine.ProductionCycle = "182";
            _machine.ParameterList = "parameter1:test1,paramter2:test2;parameter3:test3";
            Child _child = new Child(_machine);
            _child.MdiParent = this;
            //this.LayoutMdi(MdiLayout.TileHorizontal);
            _child.Show();


        }

        private void 设置URLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupUrl setupForm = new SetupUrl(this);
            setupForm.ShowDialog();
            toolStripStatusLabel1.Text = "URL:" + URL;
        }


    }
}
