using System;
using System.ServiceModel;
using System.Threading;
using HookContracts;

namespace HookClientExample
{
	class Program
	{
		static void Main()
		{
			var instance = new Callback();
			var context = new InstanceContext(instance);
			var binding = new NetTcpBinding { MaxReceivedMessageSize = 200000, ReaderQuotas = { MaxArrayLength = 200000 } };

			var factory = new DuplexChannelFactory<IHookControl>(
				context,
				binding,
				new EndpointAddress("net.tcp://localhost:7887/HookControl"));


			var gate = factory.CreateChannel();

			gate.Start();

			while (!Console.KeyAvailable)
			{
				Thread.Sleep(1000);
			}

			gate.Stop();
		}
	}
}

