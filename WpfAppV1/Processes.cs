using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using WmiController;
using WmiController.Prototypes.Events;
using WmiController.WMI.Rx;
using WmiPresenter.Containers;
using WmiPresenter.ForeignComponents.TreeListView.Tree;
using WmiPresenter.Properties;

namespace WmiPresenter.Data
{
	public class Processes : ITreeModel, INotifyPropertyChanged, IObserver<ProcessStartTrace>, IObserver<ProcessStopTrace>
	{
		private WmiObservable<ProcessStartTrace> m_processStartTrace;
		private WmiObservable<ProcessStopTrace> m_processStopTrace;

		public Processes()
		{
			RootProcesses = new ObservableCollectionEx<Process>();
			RootProcesses.CollectionChanged += (sender, args) => OnPropertyChanged("RootProcesses");
		}

		public ObservableCollectionEx<Process> RootProcesses { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public IEnumerable GetChildren(object parent)
		{
			if (parent == null || parent.GetType() == typeof(Processes))
				return RootProcesses;
			if (parent.GetType() == typeof(Process))
				return ((Process)parent).ChildProcesses;
			return Enumerable.Empty<object>();
		}

		public bool HasChildren(object parent)
		{
			if (parent == null || parent.GetType() == typeof(Processes))
				return RootProcesses.Any();
			if (parent.GetType() == typeof(Process))
			{
				var status = ((Process)parent);
				return status.ChildProcesses != null && status.ChildProcesses.Any();
			}
			return false;
		}

		public void Load(Computer computer)
		{
			var processes = computer.Context.Source<WmiController.Prototypes.Process>()
				.OrderBy(p => p.ProcessId)
				.ToDictionary(p => p.ProcessId, v => new { PId = v.ParentProcessId, Proc = ConvertProcess(v) });
			var attached = true;
			while (processes.Any() && attached)
			{
				attached = false;
				foreach (var process in processes.ToList())
				{
					if (process.Value.PId == process.Value.Proc.Id)
						continue;
					if (!processes.ContainsKey(process.Value.PId))
						continue;
					var parent = processes[process.Value.PId];
					if (parent.Proc.ChildProcesses == null)
						parent.Proc.ChildProcesses = new ObservableCollectionEx<Process>();
					if (parent.Proc.ChildProcesses.Contains(process.Value.Proc))
						continue;
					parent.Proc.ChildProcesses.Add(process.Value.Proc);
					attached = true;
				}
			}
			var proc = processes.Values.Select(v => v.Proc).ToList();
			foreach (var process in proc.ToList())
			{
				if (Process.FindProcess(proc.Except(new[] { process }), process.Id) != null)
					proc.Remove(process);
			}
			RootProcesses.Clear();
			RootProcesses.AddRange(proc);
			if (m_processStartTrace != null)
				m_processStartTrace.Dispose();
			if (m_processStopTrace != null)
				m_processStopTrace.Dispose();
			m_processStartTrace = computer.Context.Events<ProcessStartTrace>();
			m_processStopTrace = computer.Context.Events<ProcessStopTrace>();
			m_processStartTrace.Subscribe(this);
			m_processStopTrace.Subscribe(this);
		}

		public void OnNext(ProcessStartTrace value)
		{
			var parent = Process.FindProcess(RootProcesses, (int)value.ParentProcessID);
			var newProcess = new Process
			{
				Id = (int)value.ProcessID,
				Name = value.ProcessName
			};
			if (parent == null)
				RootProcesses.Add(newProcess);
			else
			{
				parent.ChildProcesses = parent.ChildProcesses ?? new ObservableCollectionEx<Process>();
				parent.ChildProcesses.Add(newProcess);
			}
		}

		public void OnNext(ProcessStopTrace value)
		{
			Process.RemoveProcess(RootProcesses, (int)value.ProcessID);
		}

		public void OnError(Exception error)
		{
		}

		public void OnCompleted()
		{
		}

		private Process ConvertProcess(WmiController.Prototypes.Process p)
		{
			return new Process
			{
				Id = (int)p.ProcessId,
				Name = p.Name
			};
		}
	}
}
