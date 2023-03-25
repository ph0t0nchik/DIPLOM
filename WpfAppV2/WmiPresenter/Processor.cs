using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_Processor")]
	public class Processor
	{
		/// <summary>
		/// Represents the property AddressWidth
		/// </summary>
		public virtual ushort AddressWidth { get; set; }

		/// <summary>
		/// Represents the property Architecture
		/// </summary>
		public virtual ushort Architecture { get; set; }

		/// <summary>
		/// Represents the property Availability
		/// </summary>
		public virtual ushort Availability { get; set; }

		/// <summary>
		/// Represents the property Caption
		/// </summary>
		public virtual string Caption { get; set; }

		/// <summary>
		/// Represents the property ConfigManagerErrorCode
		/// </summary>
		public virtual uint ConfigManagerErrorCode { get; set; }

		/// <summary>
		/// Represents the property ConfigManagerUserConfig
		/// </summary>
		public virtual bool ConfigManagerUserConfig { get; set; }

		/// <summary>
		/// Represents the property CpuStatus
		/// </summary>
		public virtual ushort CpuStatus { get; set; }

		/// <summary>
		/// Represents the property CreationClassName
		/// </summary>
		public virtual string CreationClassName { get; set; }

		/// <summary>
		/// Represents the property CurrentClockSpeed
		/// </summary>
		public virtual uint CurrentClockSpeed { get; set; }

		/// <summary>
		/// Represents the property CurrentVoltage
		/// </summary>
		public virtual ushort CurrentVoltage { get; set; }

		/// <summary>
		/// Represents the property DataWidth
		/// </summary>
		public virtual ushort DataWidth { get; set; }

		/// <summary>
		/// Represents the property Description
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Represents the property DeviceID
		/// </summary>
		public virtual string DeviceID { get; set; }

		/// <summary>
		/// Represents the property ErrorCleared
		/// </summary>
		public virtual bool ErrorCleared { get; set; }

		/// <summary>
		/// Represents the property ErrorDescription
		/// </summary>
		public virtual string ErrorDescription { get; set; }

		/// <summary>
		/// Represents the property ExtClock
		/// </summary>
		public virtual uint ExtClock { get; set; }

		/// <summary>
		/// Represents the property Family
		/// </summary>
		public virtual ushort Family { get; set; }

		/// <summary>
		/// Represents the property InstallDate
		/// </summary>
		public virtual DateTime InstallDate { get; set; }

		/// <summary>
		/// Represents the property L2CacheSize
		/// </summary>
		public virtual uint L2CacheSize { get; set; }

		/// <summary>
		/// Represents the property L2CacheSpeed
		/// </summary>
		public virtual uint L2CacheSpeed { get; set; }

		/// <summary>
		/// Represents the property L3CacheSize
		/// </summary>
		public virtual uint L3CacheSize { get; set; }

		/// <summary>
		/// Represents the property L3CacheSpeed
		/// </summary>
		public virtual uint L3CacheSpeed { get; set; }

		/// <summary>
		/// Represents the property LastErrorCode
		/// </summary>
		public virtual uint LastErrorCode { get; set; }

		/// <summary>
		/// Represents the property Level
		/// </summary>
		public virtual ushort Level { get; set; }

		/// <summary>
		/// Represents the property LoadPercentage
		/// </summary>
		public virtual ushort LoadPercentage { get; set; }

		/// <summary>
		/// Represents the property Manufacturer
		/// </summary>
		public virtual string Manufacturer { get; set; }

		/// <summary>
		/// Represents the property MaxClockSpeed
		/// </summary>
		public virtual uint MaxClockSpeed { get; set; }

		/// <summary>
		/// Represents the property Name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Represents the property NumberOfCores
		/// </summary>
		public virtual uint NumberOfCores { get; set; }

		/// <summary>
		/// Represents the property NumberOfLogicalProcessors
		/// </summary>
		public virtual uint NumberOfLogicalProcessors { get; set; }

		/// <summary>
		/// Represents the property OtherFamilyDescription
		/// </summary>
		public virtual string OtherFamilyDescription { get; set; }

		/// <summary>
		/// Represents the property PNPDeviceID
		/// </summary>
		public virtual string PNPDeviceID { get; set; }

		/// <summary>
		/// Represents the property PowerManagementCapabilities
		/// </summary>
		public virtual ushort[] PowerManagementCapabilities { get; set; }

		/// <summary>
		/// Represents the property PowerManagementSupported
		/// </summary>
		public virtual bool PowerManagementSupported { get; set; }

		/// <summary>
		/// Represents the property ProcessorId
		/// </summary>
		public virtual string ProcessorId { get; set; }

		/// <summary>
		/// Represents the property ProcessorType
		/// </summary>
		public virtual ushort ProcessorType { get; set; }

		/// <summary>
		/// Represents the property Revision
		/// </summary>
		public virtual ushort Revision { get; set; }

		/// <summary>
		/// Represents the property Role
		/// </summary>
		public virtual string Role { get; set; }

		/// <summary>
		/// Represents the property SecondLevelAddressTranslationExtensions
		/// </summary>
		public virtual bool SecondLevelAddressTranslationExtensions { get; set; }

		/// <summary>
		/// Represents the property SocketDesignation
		/// </summary>
		public virtual string SocketDesignation { get; set; }

		/// <summary>
		/// Represents the property Status
		/// </summary>
		public virtual string Status { get; set; }

		/// <summary>
		/// Represents the property StatusInfo
		/// </summary>
		public virtual ushort StatusInfo { get; set; }

		/// <summary>
		/// Represents the property Stepping
		/// </summary>
		public virtual string Stepping { get; set; }

		/// <summary>
		/// Represents the property SystemCreationClassName
		/// </summary>
		public virtual string SystemCreationClassName { get; set; }

		/// <summary>
		/// Represents the property SystemName
		/// </summary>
		public virtual string SystemName { get; set; }

		/// <summary>
		/// Represents the property UniqueId
		/// </summary>
		public virtual string UniqueId { get; set; }

		/// <summary>
		/// Represents the property UpgradeMethod
		/// </summary>
		public virtual ushort UpgradeMethod { get; set; }

		/// <summary>
		/// Represents the property Version
		/// </summary>
		public virtual string Version { get; set; }

		/// <summary>
		/// Represents the property VirtualizationFirmwareEnabled
		/// </summary>
		public virtual bool VirtualizationFirmwareEnabled { get; set; }

		/// <summary>
		/// Represents the property VMMonitorModeExtensions
		/// </summary>
		public virtual bool VMMonitorModeExtensions { get; set; }

		/// <summary>
		/// Represents the property VoltageCaps
		/// </summary>
		public virtual uint VoltageCaps { get; set; }
	}
}

