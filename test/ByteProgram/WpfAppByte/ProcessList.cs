using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using Microsoft.VisualBasic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppByte
{
    public class ProcessList
    {
        private List<Process> processes = null;

        private void GerProcesses()
        {
            processes.Clear();
            processes = Process.GetProcesses().ToList<Process>();

        }

        private void RefreshProcessesList()
        {
            
        }
    }
}
