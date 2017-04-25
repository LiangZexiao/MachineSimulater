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
    public partial class Parameters : Form
    {
        private Machine _machine;
        public Parameters(ref Machine machine)
        {
            _machine = machine;
            InitializeComponent();
            FormatListView();
            
        }
        private void FormatListView()
        {
            listView1.View = View.Details;
            listView1.BeginUpdate();
            listView1.Columns.Add("参数名称", listView1.Width, HorizontalAlignment.Left);
            foreach (string item in _machine.ParameterNames)
            {
                ListViewItem name = new ListViewItem();
                name.Text = item;
                listView1.Items.Add(name);
            }
            listView1.EndUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem();
            item.Text = textBox1.Text.Trim();
            _machine.ParameterNames.Add(textBox1.Text.Trim());
            listView1.Items.Add(item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //essageBox.Show(listView1.SelectedItems[0].Text);
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                _machine.ParameterNames.Remove(item.Text);
            }
            listView1.Clear();
            FormatListView();
        }
    }
}
