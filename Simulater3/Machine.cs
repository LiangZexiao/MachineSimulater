using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Timers;

namespace Simulater3
{
    public class Machine
    {
        //机器ID
        public int id { get; set; }
        //机器名
        public string Name { get; set; }
        //ip地址
        public string IpAddress { get; set; }
        //生产周期，以秒为单位
        public int ProductionCycle { get; set; }
        //运行状态
        public bool isRun {get;set;}
        //出模数量
        public int ProductQT = 0;
        
        //参数列表
        public ArrayList ParameterNames;
        //参数值
        public ArrayList ParameterValues;

        //生成数据表
        public DataTable ValuesTable;

        //timer
        public Timer timer;

        public Machine()
        {
            ParameterNames = new ArrayList();

            ValuesTable = TableHelper.GetValuesTable("ValuesTable");
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(Tick);
        }

        //机器启动
        public void Run()
        {
         
            //timer.Interval = ProductionCycle/10;
            timer.Interval = ProductionCycle * 100;
            timer.Start();
            isRun = true;
        }
        public void Stop()
        {
            timer.Stop();
            isRun = false;
        }
         private void Tick(object sender, ElapsedEventArgs e)
        {
             if(ValuesTable.Rows.Count==10*ParameterNames.Count)
             {
                 Console.WriteLine(Name+" upload data\n");
                 Upload();

             }
             Console.WriteLine(Name + " create data\n");
             CreateValues();
        }

        //产生数据
        private void CreateValues()
        {
            ParameterValues = new ArrayList(ParameterNames.Count);
            Random r = new Random();
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                double randomValue = 0.01 * r.Next(0, 1000);
                ParameterValues.Add(randomValue);

                DataRow row = ValuesTable.NewRow();
                row["ItemName"] = ParameterNames[i];
                row["ItemValue"] = ParameterValues[i];
                row["Machine"] = Name;
                row["ProductQT"] = ProductQT;
                row["Createtime"] = DateTime.Now.ToLocalTime().ToString();
                ValuesTable.Rows.Add(row);
            }
            
        }
        //上传数据
        public void Upload()
        {
            ProductQT++;
            //保存文件
            string xmlString = XMLHelper.ConvertDataTableToXML(ValuesTable);
            XMLHelper.XMLToFile(xmlString, AppDomain.CurrentDomain.BaseDirectory+@"data\"+ Name + ".xml");
            Console.WriteLine("upload function");
            ValuesTable.Clear();

            //post发送数据
            /*bool success;
            string errormsg;
            Send.Post(this, "www.baidu.com", out success, out errormsg);
            Console.Write(success.ToString());
            Console.Write(" : " + errormsg);*/

        }
        public override string ToString()
        {
            StringBuilder values = new StringBuilder();
            string str = "ID:" + id 
                + "\nName:" + Name 
                + "\nIPAddress" + IpAddress 
                + "\nProductCycle:" + ProductionCycle 
                + "\nisRun:" + isRun.ToString() + "\n";

            if (ParameterValues==null)
            {
                for (int i = 0; i < ParameterNames.Count; i++)
                {
                    values.Append(ParameterNames[i]);
                    values.Append(":");
                    values.Append("未生成\n");
                }
            }
            else
            {
                for (int i = 0; i < ParameterNames.Count; i++)
                {
                    values.Append(ParameterNames[i]);
                    values.Append(":");
                    values.Append(i<ParameterValues.Count? ParameterValues[i]:"未生成");
                    values.Append("\n");
                }
            }
            return str + values.ToString(); ;
        }

    }
}
