using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Simulater2
{
    public partial class Form1 : Form
    {
        private DataSet dataSet = new DataSet();
        public DataTable MachineList = new DataTable("MachineList");
        public DataTable ValuesList = new DataTable("ValuesList");
        public Form1()
        {
            InitializeComponent();
            FormatMachineListDatatable();
            FormatValuesListDatatable();
            
        }
        private void FormatMachineListDatatable()
        {
            
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.AutoIncrement = true;
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            MachineList.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.AutoIncrement = false;
            column.Caption = "机器名字";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            MachineList.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IpAddress";
            column.Caption = "IP地址";
            MachineList.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ProductionCycle";
            column.Caption = "生产周期";
            MachineList.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ParameterList";
            column.Caption = "参数列表";
            MachineList.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = MachineList.Columns["id"];
            MachineList.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            //DataSet dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(MachineList);

            // Create three new DataRow objects and add 
            // them to the DataTable
            /*for (int i = 0; i <= 2; i++)
            {
                row = MachineList.NewRow();
                row["id"] = i;
                row["Name"] = "Name " + i;
                MachineList.Rows.Add(row);
            }
            */
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "MachineList";


        }
        public void FormatValuesListDatatable()
        {
            
            //参数名称
            DataColumn itemnameColumn = new DataColumn();
            itemnameColumn.DataType = System.Type.GetType("System.String");
            itemnameColumn.ColumnName = "ItemName";
            //参数中文名
            DataColumn itemDescColumn = new DataColumn();
            itemDescColumn.DataType = System.Type.GetType("System.String");
            itemDescColumn.ColumnName = "ItemDesc";
            //参数值
            DataColumn itemvalueColumn = new DataColumn();
            itemvalueColumn.DataType = System.Type.GetType("System.String");
            itemvalueColumn.ColumnName = "ItemValue";
            //出模序号
            DataColumn ProductQTColumn = new DataColumn();
            ProductQTColumn.DataType = System.Type.GetType("System.Int32");
            ProductQTColumn.ColumnName = "ProductQT";
            //机器编号             
            DataColumn machineColumn = new DataColumn();
            machineColumn.DataType = System.Type.GetType("System.String");
            machineColumn.ColumnName = "Machine";
            //参数获取时间
            DataColumn updatetimeColumn = new DataColumn();
            updatetimeColumn.DataType = System.Type.GetType("System.String");
            updatetimeColumn.ColumnName = "Updatetime";
            //创建时间
            DataColumn createdatetimeColumn = new DataColumn();
            createdatetimeColumn.DataType = System.Type.GetType("System.String");
            createdatetimeColumn.ColumnName = "Createtime";
            //写入数据成功
            DataColumn DBFlagColumn = new DataColumn();
            DBFlagColumn.DataType = System.Type.GetType("System.Boolean");
            DBFlagColumn.ColumnName = "DBFlag";
            //写入时间
            DataColumn DBTimeColumn = new DataColumn();
            DBTimeColumn.DataType = System.Type.GetType("System.DateTime");
            DBTimeColumn.ColumnName = "DBTime";
            //一模数据接收完成
            DataColumn ReceiveOverColumn = new DataColumn();
            ReceiveOverColumn.DataType = System.Type.GetType("System.Boolean");
            ReceiveOverColumn.ColumnName = "ReceiveOverFlag";

            ValuesList.Columns.Add(itemDescColumn);
            ValuesList.Columns.Add(itemnameColumn);
            ValuesList.Columns.Add(itemvalueColumn);
            ValuesList.Columns.Add(ProductQTColumn);
            ValuesList.Columns.Add(machineColumn);
            ValuesList.Columns.Add(updatetimeColumn);
            ValuesList.Columns.Add(createdatetimeColumn);
            ValuesList.Columns.Add(DBFlagColumn);
            ValuesList.Columns.Add(DBTimeColumn);
            ValuesList.Columns.Add(ReceiveOverColumn);



            dataSet.Tables.Add(ValuesList);

            //test value
            DataRow row = ValuesList.NewRow();
            row["ItemName"] = "ItemName";

            ValuesList.Rows.Add(row);
            dataGridView2.DataSource = dataSet;
            dataGridView2.DataMember = "ValuesList";
           
            
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
           
            MachineList.Rows.Add(_machine.getRow(MachineList));
            dataGridView1.DataSource = dataSet.Tables["MachineList"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.RowCount.ToString());
        }
    }
}
