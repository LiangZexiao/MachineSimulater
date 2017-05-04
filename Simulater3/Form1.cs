using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Simulater3
{
    public partial class Form1 : Form
    {
        private List<Machine> MachineList;
        private int machineID = 0;

        private int firstByte = 192;
        private int secondByte = 168;
        private int thirdByte = 1;
        private int fourByte = 0;

        public Form1()
        {
            MachineList = new List<Machine>();
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }
        //生成IP地址
        private string GetIP()
        {
            string ipStr = "";
            if(fourByte<255)
            {
                fourByte++;
            }
            else
            {
                fourByte = 1;
                if(thirdByte<255)
                {
                    thirdByte++;
                }
                else
                {
                    thirdByte = 1;
                    if (secondByte < 255)
                    {
                        secondByte++;
                    }
                    else
                    {
                        secondByte = 1;
                        if (firstByte < 255)
                        {
                            firstByte++;
                        }
                        else
                        {
                            MessageBox.Show("超出范围");
                        }
                    }
                }
            }
            ipStr = firstByte.ToString()+ '.' + secondByte.ToString()+ '.' + thirdByte.ToString()+'.' + fourByte.ToString();
            return ipStr;
        }

        //新建机器
        private void button1_Click(object sender, EventArgs e)
        {
            Machine _machine = new Machine();

            _machine.IpAddress = GetIP();
            _machine.ProductionCycle = (int)numericUpDown1.Value;
            _machine.isRun = false;

            _machine.ParameterNames.Add("Parameter1");

            _machine.ParameterNames.Add("Parameter2");


            _machine.id = machineID++;
            _machine.Name = textBox2.Text + _machine.id;
            MachineList.Add(_machine);

            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["isRun"].ReadOnly = true;
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

            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((int)numericUpDown2.Value == 0) MessageBox.Show("请输入大于0的机器数目");
            else
            {
                for (int i = 0; i < (int)numericUpDown2.Value; i++)
                {
                    Machine _machine = new Machine();

                    _machine.IpAddress = GetIP();
                    _machine.ProductionCycle = (int)numericUpDown1.Value;
                    _machine.isRun = false;

                    _machine.ParameterNames.Add("Parameter1");

                    _machine.ParameterNames.Add("Parameter2");


                    _machine.id = machineID++;
                    _machine.Name = textBox2.Text + _machine.id;
                    MachineList.Add(_machine);
                }
                //List转换成Datatable
                DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
                dataGridView1.DataSource = dt;
            }
        }

        private void 运行ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Machine item in MachineList)
            {
                item.Run();
            }
            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
        }

        private void 停止ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Machine item in MachineList)
            {
                item.Stop();
            }
            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            运行ToolStripMenuItem1_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            停止ToolStripMenuItem1_Click(sender, e);
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineList.Clear();
            //List转换成Datatable
            DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
            dataGridView1.DataSource = dt;
        }

        private void 输出文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"data\";
            //MessageBox.Show(path);
            ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/root," + path;
            System.Diagnostics.Process.Start(psi);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
            Machine _machine = (from machine in MachineList
                                where machine.id == id
                                select machine).Single<Machine>();
            switch (dataGridView1.Columns[e.ColumnIndex].HeaderText)
            {
                case "Name":
                    {
                        _machine.Name = dataGridView1.CurrentCell.Value.ToString();
                    }break;
                case "IpAddress":
                    {
                        _machine.IpAddress = dataGridView1.CurrentCell.Value.ToString();
                    }break;
                case "ProductionCycle":
                    {
                        _machine.ProductionCycle = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                    }break;
                default:
                    break;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>=1)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    int id = (int)dataGridView1.SelectedRows[i].Cells["id"].Value;
                    Machine _machine = (from machine in MachineList
                                        where machine.id == id
                                        select machine).Single<Machine>();
                    _machine.ProductionCycle = (int)numericUpDown3.Value;

                    
                }
                //List转换成Datatable
                DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("请选中要修改的机器");
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = TableHelper.ToDataTable<Machine>(MachineList);
                string xmlString = XMLHelper.ConvertDataTableToXML(dt);
                XMLHelper.XMLToFile(xmlString, AppDomain.CurrentDomain.BaseDirectory + @"MachineConfig\" + DateTime.Now.ToLongDateString()+".xml");
                MessageBox.Show("保存成功");
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void 载入机器列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"MachineConfig\";
            openFileDialog.Filter = "xml配置文件|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(fName);

                DataSet ds = new DataSet();
                ds = XMLHelper.ConvertXMLToDataSet(XMLHelper.ConvertXmlToString(doc));
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

    }
}
