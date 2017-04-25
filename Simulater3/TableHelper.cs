using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace Simulater3
{
    public static class TableHelper
    {
    
        public static DataTable GetValuesTable(string TableName=null)
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


        /// <summary>
        /// List转换为DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            Type myType = typeof(Machine);

            //PropertyInfo[] props = ModelHandler.PropCache<T>();
            PropertyInfo[] props = myType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                //Type t = ModelHandler.GetCoreType(prop.PropertyType);
                Type t = prop.PropertyType;
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
    }

}
