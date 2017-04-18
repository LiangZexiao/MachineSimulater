using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace MachineSimulater
{
    public class Machine
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string IpAddress { get; set; }
        public int ProductionCycle { get; set; }
        public string ParameterList { get; set; }

        public void Show() 
        {
            MessageBox.Show("Machine Name:" + Name + '\n' + "Machine Number:" + Number + '\n' + "Machine IpAddress:"+IpAddress+'\n'+"Machine ProductionCycle:"+ProductionCycle.ToString());
        }

    }
}
