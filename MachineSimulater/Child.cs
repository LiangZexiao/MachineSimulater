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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MachineSimulater
{
    public partial class Child : Form
    {
        private Machine _machine;

        public Child(Machine machine)
        {
            _machine = machine;
            

            InitializeComponent();
            timer1.Interval = _machine.ProductionCycle;
            textBox1.Text = _machine.Name;
            textBox2.Text = _machine.Number;
            textBox3.Text = _machine.IpAddress;
            numericUpDown1.Value = _machine.ProductionCycle;
            richTextBox1.Text = _machine.ParameterList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!timer1.Enabled)
            {
                timer1.Start();
                toolStripStatusLabel1.Text = "Timer:" + timer1.Enabled.ToString();
                button2.Text = "暂停发送";
            }
            else
            {
                timer1.Stop();
                toolStripStatusLabel1.Text = "Timer:" + timer1.Enabled.ToString();
                button2.Text = "开始发送";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            _machine.Name = textBox1.Text;
            _machine.Number = textBox2.Text;
            _machine.IpAddress = textBox3.Text;
            _machine.ProductionCycle = (int)numericUpDown1.Value;
        }

        private void Send()
        {
            JObject result = Helper.PostMachine(this._machine, Form1.URL);
            
            if((bool)result["success"])
            {
                toolStripStatusLabel2.Text = "success"; 
            }
            else
            {
                toolStripStatusLabel2.Text = "error";
                //MessageBox.Show((string)result["resmeg"]);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Send();
        }
    }
}
