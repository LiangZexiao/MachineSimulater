﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineSimulater
{

    public partial class SetupUrl : Form
    {
        public SetupUrl()
        {
            InitializeComponent();
            textBox1.Text = Form1.URL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.URL = textBox1.Text;
            this.Close();
        }
    }
}
