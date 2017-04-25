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
    public partial class Form1 : Form
    {
        private List<Machine> MachineList;
        private int machineID = 0;
        public Form1()
        {
            MachineList = new List<Machine>();
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //新建机器
        private void button1_Click(object sender, EventArgs e)
        {
            Machine _machine = new Machine();
            
            _machine.IpAddress = textBox3.Text;
            _machine.ProductionCycle = (int)numericUpDown1.Value;
            _machine.isRun = false;

            _machine.ParameterNames.Add("Parameter1");

            _machine.ParameterNames.Add("Parameter2");


            _machine.id = machineID++;
            _machine.Name = textBox2.Text+_machine.id;
            MachineList.Add(_machine);

            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["Name"].ReadOnly = true;
            dataGridView1.Columns["isRun"].ReadOnly = true;

        }

        //List转换成Datatable
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
        }

        private void 运行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int id = (int)dataGridView1.SelectedRows[i].Cells["id"].Value;
                Machine _machine = (from machine in MachineList
                                    where machine.id == id
                                    select machine).Single<Machine>();
               // _machine.isRun = true;
                _machine.Run();
            }
            
            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;

        }
        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int id = (int)dataGridView1.SelectedRows[i].Cells["id"].Value;
                Machine _machine = (from machine in MachineList
                                    where machine.id == id
                                    select machine).Single<Machine>();

                _machine.Stop();
            }

            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
        }
        private void 删除机器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int id = (int)dataGridView1.SelectedRows[i].Cells["id"].Value;
                Machine _machine = (from machine in MachineList
                                    where machine.id == id
                                    select machine).Single<Machine>();
                MachineList.Remove(_machine);
            }
            
            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
        }

        private void 机器状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
            Machine _machine = (from machine in MachineList
                                where machine.id == id
                                select machine).Single<Machine>();
            MachineStatus statusForm = new MachineStatus(_machine);
            statusForm.ShowDialog();

        }

        private void 配置参数列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
            Machine _machine = (from machine in MachineList
                                where machine.id == id
                                select machine).Single<Machine>();
            Parameters parametersForm = new Parameters(ref _machine);
            parametersForm.ShowDialog();
        }   

    }
}
