using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Simulater2
{
    class MyTableHelper
    {
        public static DataTable GetMachineListTable(string TableName=null)
        {
            DataTable MachineList = new DataTable(TableName);
            DataColumn column;

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

            return MachineList;
        }
        public static DataTable GetValuesListTable(string TableName=null)
        {
            DataTable ValuesList = new DataTable(TableName);
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
            return ValuesList;
        }

        public static void AddMachine(DataTable MachineList, Machine newMachine)
        {
            DataRow row = MachineList.NewRow();
            //row["id"] = newMachine.id;
            row["Name"] = newMachine.Name;
            row["IpAddress"] = newMachine.IpAddress;
            row["ProductionCycle"] = newMachine.ProductionCycle;
            row["ParameterList"] = newMachine.ParameterList;

            MachineList.Rows.Add(row);
        }
    }
}
