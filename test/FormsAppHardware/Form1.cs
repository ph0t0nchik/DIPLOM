using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;


namespace FormsAppHardware
{
    public partial class Form1 : Form
    {
        private ListViewItemComparer comparer = null;
        private System.Windows.Forms.Timer timer;
        string name, IP;
        private Thread findPC;

        public Form1()
        {
            InitializeComponent();
            GetProcess(listView2);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void GetHardWareInfo(string key, ListView list)
        {
            list.Items.Clear();
            ConnectionOptions options = new ConnectionOptions();
            options.Username = "test";
            options.Password = "1234";
            ManagementScope scope = new ManagementScope("\\\\IDEAPAD330S\\root\\CIMV2", options);
            //ManagementScope scope = new ManagementScope("\\\\localhost\\root\\CIMV2");
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT " + key);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    ListViewGroup listViewGroup;

                    try
                    {
                        listViewGroup = list.Groups.Add(obj["Name"].ToString(), obj["Name"].ToString());
                    }
                    catch (Exception) 
                    {
                        listViewGroup = list.Groups.Add(obj.ToString(), obj.ToString());
                    }
                    
                    if (obj.Properties.Count == 0)
                    {
                        MessageBox.Show("Не удалось получить информацию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    foreach (PropertyData data in obj.Properties)
                    {
                        ListViewItem item = new ListViewItem(listViewGroup);

                        if (list.Items.Count % 2 != 0)
                        {
                            item.BackColor = Color.White;
                        }
                        else
                        {
                            item.BackColor = Color.WhiteSmoke;
                        }

                        item.Text = data.Name;

                        if (data.Value != null && !string.IsNullOrEmpty(data.Value.ToString()))
                        {
                            switch (data.Value.GetType().ToString())
                            {
                                case "System.String[]":
                                    string[] stringData = data.Value as string[];
                                    
                                    string resStr1 = string.Empty;
                                    
                                    foreach (string s in  stringData)
                                    {
                                        resStr1 += $"{s}";
                                    }
                                    item.SubItems.Add(resStr1);
                                    break;
                                case "System.UInt16[]":

                                    ushort[] ushortData = data.Value as ushort[];

                                    string resStr2 = string.Empty;

                                    foreach (ushort u in ushortData)
                                    {
                                        resStr2 += $"{Convert.ToString(u)}";
                                    }
                                    item.SubItems.Add(resStr2);

                                    break;
                                default:
                                    item.SubItems.Add(data.Value.ToString());
                                    break;
                            }
                            list.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetProcess(ListView proc_list)
        {
            
            ConnectionOptions options = new ConnectionOptions();
            options.Username = "test";
            options.Password = "1234";
            ManagementScope scope = new ManagementScope("\\\\IDEAPAD330S\\root\\CIMV2", options);
            //ManagementScope scope = new ManagementScope("\\\\localhost\\root\\CIMV2");
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT Name,ProcessId FROM Win32_Process");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            proc_list.Items.Clear();
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    ListViewItem item = new ListViewItem(new string[] { obj["Name"].ToString(), Convert.ToInt32(obj["ProcessId"]).ToString() });

                    if (proc_list.Items.Count % 2 != 0)
                    {
                        item.BackColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = Color.WhiteSmoke;
                    }

                    proc_list.Items.Add(item);

                   
                    
                }
            }
            catch (Exception) { }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = string.Empty;
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "Процессор":
                    key = "* FROM Win32_Processor";
                    break;
                case "Видеокарта":
                    key = "* FROM Win32_VideoController";
                    break;
                case "Чипсет":
                    key = "* FROM Win32_IDEController";
                    break;
                case "Батарея":
                    key = "* FROM Win32_Battery";
                    break;
                case "Биос":
                    key = "* FROM Win32_BIOS";
                    break;
                case "Оперативная память":
                    key = "* FROM Win32_PhysicalMemory";
                    break;
                case "Кэш":
                    key = "* FROM Win32_CacheMemory";
                    break;
                case "USB":
                    key = "* FROM Win32_USBController";
                    break;
                case "Диск":
                    key = "* FROM Win32_DiskDrive";
                    break;
                case "Логические диски":
                    key = "* FROM Win32_LogicalDisk";
                    break;
                case "Клавиатура":
                    key = "* FROM Win32_Keyboard";
                    break;
                case "Сеть":
                    key = "* FROM Win32_NetworkAdapter";
                    break;
                case "Пользователи":
                    key = "* FROM Win32_Account";
                    break;
                case "Процессы":
                    key = "* FROM Win32_Process";
                    break;
                case "Локальная сеть":
                    key = "* FROM Win32_Group";
                    break;

            }
            GetHardWareInfo(key,listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            comparer = new ListViewItemComparer();
            comparer.ColumnIndex = 0;
        }
                
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (listView2.SelectedItems[0] != null)
                {

                    try
                    {
                        ConnectionOptions options = new ConnectionOptions();
                        options.Username = "test";
                        options.Password = "1234";
                        ManagementScope scope = new ManagementScope("\\\\IDEAPAD330S\\root\\CIMV2", options);
                        //ManagementScope scope = new ManagementScope("\\\\localhost\\root\\CIMV2");
                        scope.Connect();
                        ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE Name='" + listView2.SelectedItems[0].SubItems[0].Text + "'");
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            obj.InvokeMethod("Terminate", null);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception) { }
            GetProcess(listView2);
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView2.SelectedItems[0] != null)
            {
                toolStripLabel2.Text = listView2.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            comparer.ColumnIndex = e.Column;

            comparer.SortDirection = comparer.SortDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            listView2.ListViewItemSorter = comparer;

            listView2.Sort();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            GetProcess(listView2);
            toolStripLabel2.Text = "";
        }

        void searchPC()
        {
            bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
            if (isNetworkUp)
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        this.IP = ip.ToString();
                    }
                }
                Invoke((MethodInvoker)delegate
                {
                    label1.Text = "This Computer: " + this.IP;
                });
                string[] ipRange = IP.Split('.');
                for (int i = 0; i < 255; i++)
                {
                    Ping ping = new Ping();
                    string testIP = ipRange[0] + '.' + ipRange[1] + '.' + ipRange[2] + '.' + i.ToString();
                    if (testIP != this.IP)
                    {
                        ping.PingCompleted += new PingCompletedEventHandler(pingCompletedEvent);
                        ping.SendAsync(testIP, 100, testIP);
                    }
                }
            }
            else
            {
                MessageBox.Show("Not connected to LAN");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            try
            {
                findPC = new Thread(new ThreadStart(searchPC));
                findPC.Start();
                while (!findPC.IsAlive) ;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        //for searching online computers
        void pingCompletedEvent(object sender, PingCompletedEventArgs e)
        {
            string ip = (string)e.UserState;
            if (e.Reply.Status == IPStatus.Success)
            {
                string name;
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                    name = hostEntry.HostName;
                }
                catch (SocketException ex)
                {
                    name = ex.Message;
                }
                Invoke((MethodInvoker)delegate
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = name;
                    item.SubItems.Add(ip);
                    listView3.Items.Add(item);
                });
            }
        }
    }
}
