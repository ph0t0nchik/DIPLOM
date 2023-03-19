using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_Process")]
	public class Process
	{
		// Represents the property Caption
		public virtual string Caption { get; set; }

		// Represents the property CommandLine
		public virtual string CommandLine { get; set; }

		// Represents the property CreationClassName
		public virtual string CreationClassName { get; set; }

		// Represents the property CreationDate
		public virtual DateTime CreationDate { get; set; }

		// Represents the property CSCreationClassName
		public virtual string CSCreationClassName { get; set; }

		// Represents the property CSName
		public virtual string CSName { get; set; }

		// Represents the property Description
		public virtual string Description { get; set; }

		// Represents the property ExecutablePath
		public virtual string ExecutablePath { get; set; }

		// Represents the property ExecutionState
		public virtual ushort ExecutionState { get; set; }

		// Represents the property Handle
		public virtual string Handle { get; set; }

		// Represents the property HandleCount
		public virtual uint HandleCount { get; set; }

		// Represents the property InstallDate
		public virtual DateTime InstallDate { get; set; }

		// Represents the property KernelModeTime
		public virtual ulong KernelModeTime { get; set; }

		// Represents the property MaximumWorkingSetSize
		public virtual uint MaximumWorkingSetSize { get; set; }

		// Represents the property MinimumWorkingSetSize
		public virtual uint MinimumWorkingSetSize { get; set; }

		// Represents the property Name
		public virtual string Name { get; set; }

		// Represents the property OSCreationClassName
		public virtual string OSCreationClassName { get; set; }

		// Represents the property OSName
		public virtual string OSName { get; set; }

		// Represents the property OtherOperationCount
		public virtual ulong OtherOperationCount { get; set; }

		// Represents the property OtherTransferCount
		public virtual ulong OtherTransferCount { get; set; }

		// Represents the property PageFaults
		public virtual uint PageFaults { get; set; }

		// Represents the property PageFileUsage
		public virtual uint PageFileUsage { get; set; }

		// Represents the property ParentProcessId
		public virtual uint ParentProcessId { get; set; }

		// Represents the property PeakPageFileUsage
		public virtual uint PeakPageFileUsage { get; set; }

		// Represents the property PeakVirtualSize
		public virtual ulong PeakVirtualSize { get; set; }

		// Represents the property PeakWorkingSetSize
		public virtual uint PeakWorkingSetSize { get; set; }

		// Represents the property Priority
		public virtual uint Priority { get; set; }

		// Represents the property PrivatePageCount
		public virtual ulong PrivatePageCount { get; set; }

		// Represents the property ProcessId
		public virtual uint ProcessId { get; set; }

		// Represents the property QuotaNonPagedPoolUsage
		public virtual uint QuotaNonPagedPoolUsage { get; set; }

		// Represents the property QuotaPagedPoolUsage
		public virtual uint QuotaPagedPoolUsage { get; set; }

		// Represents the property QuotaPeakNonPagedPoolUsage
		public virtual uint QuotaPeakNonPagedPoolUsage { get; set; }

		// Represents the property QuotaPeakPagedPoolUsage
		public virtual uint QuotaPeakPagedPoolUsage { get; set; }

		// Represents the property ReadOperationCount
		public virtual ulong ReadOperationCount { get; set; }

		// Represents the property ReadTransferCount
		public virtual ulong ReadTransferCount { get; set; }

		// Represents the property SessionId
		public virtual uint SessionId { get; set; }

		// Represents the property Status
		public virtual string Status { get; set; }

		// Represents the property TerminationDate
		public virtual DateTime TerminationDate { get; set; }

		// Represents the property ThreadCount
		public virtual uint ThreadCount { get; set; }

		// Represents the property UserModeTime
		public virtual ulong UserModeTime { get; set; }

		// Represents the property VirtualSize
		public virtual ulong VirtualSize { get; set; }

		// Represents the property WindowsVersion
		public virtual string WindowsVersion { get; set; }

		// Represents the property WorkingSetSize
		public virtual ulong WorkingSetSize { get; set; }

		// Represents the property WriteOperationCount
		public virtual ulong WriteOperationCount { get; set; }

		// Represents the property WriteTransferCount
		public virtual ulong WriteTransferCount { get; set; }
	}
}

