using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;


namespace Simulater2
{
    public partial class Form1 : Form
    {
        private DataSet dataSet = new DataSet();

        public List<Machine> MachineList;
        public DataTable MachineTable;
        public DataTable ValuesList;
        public Form1()
        {
            InitializeComponent();
            FormatTable();
            
        }
        private void FormatTable()
        {
            MachineTable = MyTableHelper.GetMachineListTable("MachineList");
            ValuesList = MyTableHelper.GetValuesListTable("ValuesList");
            dataSet.Tables.Add(MachineTable);
            dataSet.Tables.Add(ValuesList);
            dataGridView1.DataSource = dataSet.Tables["MachineList"];
            dataGridView2.DataSource = dataSet.Tables["ValuesList"];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = dataSet.Tables["MachineList"];
            string result = XMLHelper.ConvertDataTableToXML(dt);
            if(XMLHelper.XMLToFile(result))
            {
                MessageBox.Show(result);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = XMLHelper.FileToXML();
            dataSet = XMLHelper.ConvertXMLToDataSet(result);
            dataGridView1.DataSource = dataSet.Tables["MachineList"];
            
        }

        private void 添加机器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Machine _machine = new Machine();
            _machine.Name = "machine1";
            _machine.IpAddress = "192.168.1.1";
            _machine.ProductionCycle = 10000;
            _machine.ParameterList = "parameter1,parameter2,parameter3";

            MyTableHelper.AddMachine(this.MachineTable, _machine);

            MachineList = DataTable2Entities<Machine>(MachineTable);

            dataGridView1.DataSource = dataSet.Tables["MachineList"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.RowCount.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string name = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            foreach (Machine item in MachineList)
            {
                if(item.Name == name)
                {
                    item.updateValues();
                    ValuesList = item.ValuesList.Clone();

                }
            }
            dataGridView2.DataSource = ValuesList;
            MessageBox.Show("success");

        }

        //DataTable to List<Machine>
        public static List<T> DataTable2Entities<T>(DataTable table)
        {

            if (null == table || table.Rows.Count <= 0) return default(List<T>);

            List<T> list = new List<T>();

            List<string> keys = new List<string>();

            foreach (DataColumn c in table.Columns)
            {

                keys.Add(c.ColumnName.ToLower());

            }

            for (int i = 0; i < table.Rows.Count; i++)
            {

                T entity = Activator.CreateInstance<T>();

                PropertyInfo[] attrs = entity.GetType().GetProperties();

                foreach (PropertyInfo p in attrs)
                {

                    if (keys.Contains(p.Name.ToLower()))
                    {

                        if (!DBNull.Value.Equals(table.Rows[i][p.Name]))
                        {

                            p.SetValue(entity, Convert.ChangeType(table.Rows[i][p.Name], p.PropertyType), null);

                        }

                    }

                }

                list.Add(entity);

            }

            return list;

        }


    }
}
