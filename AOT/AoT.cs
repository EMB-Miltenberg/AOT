using AOT.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AOT
{


    public partial class AoT : Form
    {
        public AoT()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			if(comboBox1.Text != "Aktives Fenster Auswählen")
            {
                Methods.AoT_on(comboBox1.Text.ToString());
            }
            else
            {
                ErrorNoWindow();
            }
            ListUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Aktives Fenster Auswählen")
            {
                Methods.AoT_off(comboBox1.Text.ToString());
            }
            else
            {
                ErrorNoWindow();
            }
            ListUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListUpdate();
        }

        private void ListUpdate()
        {
            comboBox1.Items.Clear();
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    comboBox1.Items.Add(process.MainWindowTitle.ToString());
                }
            }
        }

        private void ErrorNoWindow()
        {
            MessageBox.Show("Bitte Fenster Auswählen");
        }
    }
}
