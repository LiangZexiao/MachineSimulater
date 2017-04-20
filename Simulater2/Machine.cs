using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Simulater2
{
    class Machine
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int ProductionCycle { get; set; }
        public string ParameterList { get; set; }

        public void Show()
        {
            MessageBox.Show( "Machine id:" + id+'\n'
                +"Machine Name:" + Name + '\n' 
                + "Machine IpAddress:" + IpAddress + '\n' 
                + "Machine ProductionCycle:" + ProductionCycle.ToString());
        }

        public DataRow getRow(DataTable table)
        {
            DataRow row = table.NewRow();
            row["id"] = this.id;
            row["Name"] = this.Name;
            row["IpAddress"] = this.IpAddress;
            row["ProductionCycle"] = this.ProductionCycle;
            row["ParameterList"] = this.ParameterList;
            return row;
        }
    }
}
