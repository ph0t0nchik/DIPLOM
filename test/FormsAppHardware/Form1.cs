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
        ManagementScope scope;

        Dictionary<string, HashSet<KeyRecord>> enteredKeys = new Dictionary<string, HashSet<KeyRecord>>();

        public Form1()
        {
            InitializeComponent();
            //timer = new System.Windows.Forms.Timer();
            //timer.Interval = 5000;
            //timer.Tick += new EventHandler(Timer_Tick);
            //timer.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //tsComboBoxСharacteristic.SelectedIndex = 0;
            comparer = new ListViewItemComparer();
            comparer.ColumnIndex = 0;
        }



        private void GetHardWareInfo(string key, ListView list)
        {
            list.Items.Clear();
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

                                    foreach (string s in stringData)
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

        private void tsComboBoxСharacteristic_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = string.Empty;
            switch (tsComboBoxСharacteristic.SelectedItem.ToString())
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
            GetHardWareInfo(key, listViewСharacteristic);
        }

        private void GetProcess(ListView proc_list)
        {
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

        private void listViewProcess_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            comparer.ColumnIndex = e.Column;

            comparer.SortDirection = comparer.SortDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            listViewProcess.ListViewItemSorter = comparer;

            listViewProcess.Sort();
        }

        private void listViewProcess_MouseClick(object sender, MouseEventArgs e)
        {
            if (listViewProcess.SelectedItems[0] != null)
            {
                toolStripLabel2.Text = listViewProcess.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void tsButtonUpdateProcess_Click(object sender, EventArgs e)
        {
            GetProcess(listViewProcess);
            toolStripLabel2.Text = "";
        }

        private void tsButtonDeleteProcess_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (listViewProcess.SelectedItems[0] != null)
                {

                    try
                    {
                        ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE Name='" + listViewProcess.SelectedItems[0].SubItems[0].Text + "'");
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
            GetProcess(listViewProcess);
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
                    ListViewItem item = new ListViewItem();
                    item.Text = "Этот компьютер";
                    item.SubItems.Add(this.IP);
                    listViewConnect.Items.Add(item);
                });

                string[] ipRange = IP.Split('.');
                for (int i = 2; i < 255; i++)
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

        private void buttonUpdateConnect_Click(object sender, EventArgs e)
        {
            listViewConnect.Items.Clear();
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
                    listViewConnect.Items.Add(item);
                });
            }
        }

        private void listViewConnect_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxLogin.Clear();
            textBoxPas.Clear();
        }

        private void textBoxPC_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPC.Text == "Этот компьютер")
            {
                buttonConnect.Enabled = true;
                textBoxLogin.Enabled = false;
                textBoxPas.Enabled = false;
            }
            else
            {
                textBoxLogin.Enabled = true;
                textBoxPas.Enabled = true;
                buttonConnect.Enabled = false;
            }
        }

        private void listViewConnect_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxPC.Text = listViewConnect.SelectedItems[0].SubItems[0].Text;
            enteredKeys.Clear();

            updateKeysInfo(null, null);
            timer1.Start();
        }

        private void textBoxLoginPas_TextChanged(object sender, EventArgs e)
        {
            buttonConnect.Enabled = textBoxLogin.Text.Length > 0 && textBoxPas.Text.Length > 0;
        }

        private void updateKeysInfo(object sender, EventArgs e)
        {
            string computerName = textBoxPC.Text;
            string path = $"../../../../{computerName}/log.txt";
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    {
                        string[] tokens = line.Split(new char[] { '|' }, 3);
                        var time = Convert.ToDateTime(tokens[0]);
                        if (enteredKeys.ContainsKey(tokens[1]))
                            enteredKeys[tokens[1]].Add(new KeyRecord(tokens[2], time));
                        else
                            enteredKeys.Add(tokens[1], new HashSet<KeyRecord>() { new KeyRecord(tokens[2], time) });
                    }
                }

                textBox1.Text = "";
                StringBuilder sb = new StringBuilder();

                foreach (var kvp in enteredKeys)
                {
                    sb.Append($"{kvp.Key}:\n\t>>>>> ");
                    foreach (var pressed in kvp.Value.OrderBy(k => k.Time))
                        sb.Append(pressed.Key);
                    sb.AppendLine("\n\n\n");
                }
                textBox1.Text = sb.ToString();
                textBox1.ForeColor = Color.White;
            }
            else
            {
                textBox1.Text = "Компьютер не найден или для него не было создано логов.";
                textBox1.ForeColor = Color.Red;
            }

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionOptions options = new ConnectionOptions();
                if (textBoxPC.Text == "Этот компьютер")
                {
                    scope = new ManagementScope("\\\\localhost\\root\\CIMV2");
                    scope.Connect();
                }
                else
                {
                    options.Username = textBoxLogin.Text;
                    options.Password = textBoxPas.Text;
                    scope = new ManagementScope($"\\\\{textBoxPC.Text}\\root\\CIMV2", options);
                    scope.Connect();
                }

                labelStatusConnect.ForeColor = Color.Green;
                labelStatusConnect.Text = "Подключение установлено";
                listViewСharacteristic.Items.Clear();
                GetProcess(listViewProcess);
            }
            catch (Exception)
            {
                labelStatusConnect.ForeColor = Color.Red;
                labelStatusConnect.Text = "Не удалось подключиться";
            }
        }


    }

    internal class KeyRecord
    {
        public string Key { get; set; }
        public DateTime Time { get; set; }

        public override int GetHashCode()
        {
            return Time.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is KeyRecord r && r.Key == Key && r.Time == Time;
        }

        public KeyRecord(string key, DateTime time)
        {
            Key = key;
            Time = time;
        }
    }
}
