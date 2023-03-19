using System;
using System.Threading;
using System.Windows.Forms;

namespace HookServiceApp
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool createdNew;
			var m = new Mutex(true, "SomeNameHere", out createdNew);

			if (!createdNew)
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());

			// Keep the mutex reference alive until the termination of the program.
			GC.KeepAlive(m);
		}
	}
}

