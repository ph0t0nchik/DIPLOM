using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Media;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\cimv2", "select * from win32_videocontroller");
            ManagementObjectCollection moc = mos.Get();
            foreach (ManagementObject mo in moc)
                label1.Text = mo.GetPropertyValue("Name").ToString();
        }
    }
}
