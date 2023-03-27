using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace FormsAppHardware
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                    key = "Caption FROM Win32_Process";
                    break;

            }
            GetHardWareInfo(key,listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
        }
    }
}
