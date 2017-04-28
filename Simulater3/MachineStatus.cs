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
        Machine _machine;
        public MachineStatus(Machine tempMachine)
        {
            _machine = tempMachine;
            InitializeComponent();

            timer1.Interval = 1000;
            

            textBox1.Text = _machine.id.ToString();
            textBox2.Text = _machine.Name;
            textBox3.Text = _machine.IpAddress;
            textBox4.Text = _machine.ProductionCycle.ToString();
            checkBox1.Checked = _machine.isRun;

            textBox1.ReadOnly = true;
            checkBox1.Enabled = false;

            if(_machine.isRun)
            {
                button4.Text = "停止";
            }
            else
            {
                button4.Text = "运行";
            }
            FormatListView();
            
        }
        //吴杰
        private void FormatListView()
        {
            listView1.Clear();
            listView1.View = View.Details;
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Text = "参数名";
            ch1.Width = 120;
            ch1.TextAlign = HorizontalAlignment.Left;
            ColumnHeader ch2 = new ColumnHeader();
            ch2.Text = "参数值";
            ch2.Width = 120;
            ch2.TextAlign = HorizontalAlignment.Left;
            ColumnHeader ch3 = new ColumnHeader();
            this.listView1.Columns.Add(ch1);
            this.listView1.Columns.Add(ch2);
            
            listView1.BeginUpdate();

             for (int i = 0; i < _machine.ParameterNames.Count; i++)
             {
                 this.listView1.Items.Add(_machine.ParameterNames[i].ToString());
                 if (_machine.ParameterValues == null)
                 {
                     this.listView1.Items[i].SubItems.Add("机器未运行");
                 }
                 else
                 {
                     this.listView1.Items[i].SubItems.Add(i <_machine.ParameterValues.Count ? _machine.ParameterValues[i].ToString() : "未生成");
                 }
                 

              }
            listView1.EndUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _machine.ParameterNames.Add(textBox5.Text.Trim());

            FormatListView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int Index = this.listView1.SelectedItems[0].Index;
                listView1.Items[Index].Remove();
                _machine.ParameterNames.RemoveAt(Index);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if(_machine.ParameterValues == null)
                {
                    listView1.Items[i].SubItems[1].Text = "机器未运行";
                }
                else
                {
                    listView1.Items[i].SubItems[1].Text =i<_machine.ParameterValues.Count? _machine.ParameterValues[i].ToString():"未生成";
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value;
        }

        private void MachineStatus_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void MachineStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _machine.Name = textBox2.Text.Trim();
            _machine.IpAddress = textBox3.Text.Trim();
            _machine.ProductionCycle =Convert.ToInt32(textBox4.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(_machine.isRun)
            {
                _machine.Stop();
                checkBox1.Checked = _machine.isRun;
                button4.Text = "运行";
            }
            else
            {
                _machine.Run();
                checkBox1.Checked = _machine.isRun;
                button4.Text = "停止";
            }
        }
    }
}
