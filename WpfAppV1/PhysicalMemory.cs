using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_PhysicalMemory")]
	public class PhysicalMemory
	{
		/// <summary>
		/// Represents the property BankLabel
		/// </summary>
		public virtual string BankLabel { get; set; }

		/// <summary>
		/// Represents the property Capacity
		/// </summary>
		public virtual ulong Capacity { get; set; }

		/// <summary>
		/// Represents the property Caption
		/// </summary>
		public virtual string Caption { get; set; }

		/// <summary>
		/// Represents the property CreationClassName
		/// </summary>
		public virtual string CreationClassName { get; set; }

		/// <summary>
		/// Represents the property DataWidth
		/// </summary>
		public virtual ushort DataWidth { get; set; }

		/// <summary>
		/// Represents the property Description
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Represents the property DeviceLocator
		/// </summary>
		public virtual string DeviceLocator { get; set; }

		/// <summary>
		/// Represents the property FormFactor
		/// </summary>
		public virtual ushort FormFactor { get; set; }

		/// <summary>
		/// Represents the property HotSwappable
		/// </summary>
		public virtual bool HotSwappable { get; set; }

		/// <summary>
		/// Represents the property InstallDate
		/// </summary>
		public virtual DateTime InstallDate { get; set; }

		/// <summary>
		/// Represents the property InterleaveDataDepth
		/// </summary>
		public virtual ushort InterleaveDataDepth { get; set; }

		/// <summary>
		/// Represents the property InterleavePosition
		/// </summary>
		public virtual uint InterleavePosition { get; set; }

		/// <summary>
		/// Represents the property Manufacturer
		/// </summary>
		public virtual string Manufacturer { get; set; }

		/// <summary>
		/// Represents the property MemoryType
		/// </summary>
		public virtual ushort MemoryType { get; set; }

		/// <summary>
		/// Represents the property Model
		/// </summary>
		public virtual string Model { get; set; }

		/// <summary>
		/// Represents the property Name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Represents the property OtherIdentifyingInfo
		/// </summary>
		public virtual string OtherIdentifyingInfo { get; set; }

		/// <summary>
		/// Represents the property PartNumber
		/// </summary>
		public virtual string PartNumber { get; set; }

		/// <summary>
		/// Represents the property PositionInRow
		/// </summary>
		public virtual uint PositionInRow { get; set; }

		/// <summary>
		/// Represents the property PoweredOn
		/// </summary>
		public virtual bool PoweredOn { get; set; }

		/// <summary>
		/// Represents the property Removable
		/// </summary>
		public virtual bool Removable { get; set; }

		/// <summary>
		/// Represents the property Replaceable
		/// </summary>
		public virtual bool Replaceable { get; set; }

		/// <summary>
		/// Represents the property SerialNumber
		/// </summary>
		public virtual string SerialNumber { get; set; }

		/// <summary>
		/// Represents the property SKU
		/// </summary>
		public virtual string SKU { get; set; }

		/// <summary>
		/// Represents the property Speed
		/// </summary>
		public virtual uint Speed { get; set; }

		/// <summary>
		/// Represents the property Status
		/// </summary>
		public virtual string Status { get; set; }

		/// <summary>
		/// Represents the property Tag
		/// </summary>
		public virtual string Tag { get; set; }

		/// <summary>
		/// Represents the property TotalWidth
		/// </summary>
		public virtual ushort TotalWidth { get; set; }

		/// <summary>
		/// Represents the property TypeDetail
		/// </summary>
		public virtual ushort TypeDetail { get; set; }

		/// <summary>
		/// Represents the property Version
		/// </summary>
		public virtual string Version { get; set; }
	}
}

