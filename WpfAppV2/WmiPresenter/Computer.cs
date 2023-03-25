using System;
using System.Linq;
using WmiController.Prototypes;
using WmiContext = WmiController.WMI.WmiContext;

namespace WmiController
{
	public class Computer : IDisposable
	{
		private static readonly Computer current = new Computer(".");

		static Computer()
		{
			AppDomain.CurrentDomain.DomainUnload += Dispose;
		}

		public Computer(string name)
		{
			Context = new WmiContext(string.Format(@"\\{0}\root\cimv2", name));
		}

		public ComputerSystem SystemInfo
		{
			get { return GetSystemData<ComputerSystem>(); }
		}

		public PerfFormattedDataPerfOsSystem SystemData
		{
			get { return GetSystemData<PerfFormattedDataPerfOsSystem>(); }
		}

		public PerfFormattedDataPerfOsMemory SystemMemory
		{
			get { return GetSystemData<PerfFormattedDataPerfOsMemory>(); }
		}

		public T GetSystemData<T>()
		{
			return Context.Source<T>().Single();
		}

		public WmiContext Context { get; private set; }

		public static Computer Current
		{
			get { return current; }
		}

		public void Dispose()
		{
			Context.Dispose();
		}

		private static void Dispose(object sender, EventArgs args)
		{
			current.Dispose();
		}
	}
}

