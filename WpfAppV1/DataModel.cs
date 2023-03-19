using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Threading;
using WmiController;
using WmiController.Prototypes;
using WmiPresenter.Containers;
using WmiPresenter.Data;

namespace WmiPresenter
{
	internal class DataModel
	{
		private volatile Computer m_computer;
		private readonly Timer m_timer;
		private readonly Dispatcher m_dispatcher;

		public DataModel(Dispatcher dispatcher)
		{
			m_dispatcher = dispatcher;
			Computers = new ObservableCollectionEx<Comp> { new Comp { Name = "." } };
			Computers.AddRange(Network.AllDomainComputers.Select(c => new Comp { Name = c }));
			Processes = new Processes();
			Statistics = new ObservableCollectionEx<StatisticsItem>();
			m_timer = new Timer(5000);
			m_timer.Elapsed += (sender, args) => CalculateStatistics();
			m_timer.Start();
		}

		public ObservableCollectionEx<Comp> Computers { get; private set; }

		public Processes Processes { get; private set; }

		public ObservableCollectionEx<StatisticsItem> Statistics { get; private set; }

		public void ComputerChanged(Comp computer)
		{
			if (m_computer != null)
				m_computer.Dispose();
			m_computer = new Computer(computer.Name);
			Processes.Load(m_computer);
			CalculateStatistics();
		}

		private void CalculateStatistics()
		{
			if (m_computer == null)
				return;
			if (m_dispatcher == null)
				return;
			var list = new List StatisticsItem();
			var system = m_computer.SystemInfo;
			list.Add(StatisticsItem.Create("Имя компьютера", system.Name));
			list.Add(StatisticsItem.Create("Пользователь", system.UserName));
			list.Add(StatisticsItem.Create("Количество процессоров", system.NumberOfProcessors));
			list.Add(StatisticsItem.Create("Количество памяти в байтах", system.TotalPhysicalMemory));
			list.Add(StatisticsItem.Create("Количество памяти в мегабайтах", system.TotalPhysicalMemory / (1024 * 1024)));
			var systemData = m_computer.SystemData;
			list.Add(StatisticsItem.Create("Время работы системы от последней (пере)загрузки", TimeSpan.FromSeconds(systemData.SystemUpTime)));
			list.Add(StatisticsItem.Create("Количество запущенных процессов", systemData.Processes));
			list.Add(StatisticsItem.Create("Количество потоков", systemData.Threads));
			list.Add(StatisticsItem.Create("Количество системных вызовов в секунду", systemData.SystemCallsPersec));
			list.Add(StatisticsItem.Create("Количество переключений контекста в секунду", systemData.ContextSwitchesPersec));
			list.Add(StatisticsItem.Create("Количество читаемых из файлов байтов в секунду", systemData.FileReadBytesPersec));
			list.Add(StatisticsItem.Create("Количество зуписываемых в файлы байтов в секунду", systemData.FileWriteBytesPersec));
			var procInfo = m_computer.Context.Source<Processor>().ToList();
			for (var i = 0; i < procInfo.Count; i++)
			{
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - текущая частота в мегагерцах", i), procInfo[i].CurrentClockSpeed));
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - текущая частота от внешнего источника в мегагерцах", i), procInfo[i].ExtClock));
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - максимальная частота в мегагерцах", i), procInfo[i].MaxClockSpeed));
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - разрядность", i), procInfo[i].AddressWidth));
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - количество ядер", i), procInfo[i].NumberOfCores));
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - количество логических процессоров", i), procInfo[i].NumberOfLogicalProcessors));
				list.Add(StatisticsItem.Create(string.Format("Процессор [{0}] - загрузка в процентах", i), procInfo[i].LoadPercentage));
				list.Add(StatisticsItem.CreateScale(string.Format("Процессор [{0}] - загрузка", i), procInfo[i].LoadPercentage));
			}
			//var memInfo = m_computer.Context

			lock (Statistics)
			{
				m_dispatcher.BeginInvoke(new Action(() =>
				{
					Statistics.Clear();
					Statistics.AddRange(list);
				}));
			}
		}
	}
}
