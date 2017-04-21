using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Simulater2
{
    public class Machine
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int ProductionCycle { get; set; }
        public string ParameterList { get; set; }

        public DataTable ValuesList;
       
        public Machine()
        {
            ValuesList = MyTableHelper.GetValuesListTable();
        }
        public void Show()
        {
            MessageBox.Show( "Machine id:" + id+'\n'
                +"Machine Name:" + Name + '\n' 
                + "Machine IpAddress:" + IpAddress + '\n' 
                + "Machine ProductionCycle:" + ProductionCycle.ToString());
        }
        public void updateValues()
        {
            string[] paramer = ParameterList.Split(',');
            string[] values = new string[paramer.Count()];
            for (int i = 0; i < paramer.Count(); i++)
            {
                values[i] = "第" + i + "个参数值";
            }

            for (int i = 0; i < paramer.Count(); i++)
            {
                DataRow row = ValuesList.NewRow();
                row["ItemName"] = paramer[i];//参数名字
                row["ItemValue"] = values[i];//参数值
                row["Machine"] = Name; //机器名称
                row["Updatetime"] = DateTime.Now.ToString("G");//参数获取时间
                row["DBFlag"] = false;//写入数据成功
                row["ReceiveOverFlag"] = false;//一模数据接受完成
                ValuesList.Rows.Add(row);
            }

        }


    }
}
