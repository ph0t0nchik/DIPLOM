using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmiPresenter.WMI.Common;


namespace WmiPresenter.Prototypes
{
	[WmiMap("Win32_LogicalDisk")]
	public class LogicalDisk
	{
		// Represents the property Access
		public virtual ushort Access { get; set; }

		// Represents the property Availability
		public virtual ushort Availability { get; set; }

		// Represents the property BlockSize
		public virtual ulong BlockSize { get; set; }

		// Represents the property Caption
		public virtual string Caption { get; set; }

		// Represents the property Compressed
		public virtual bool Compressed { get; set; }

		// Represents the property ConfigManagerErrorCode
		public virtual uint ConfigManagerErrorCode { get; set; }

		// Represents the property ConfigManagerUserConfig
		public virtual bool ConfigManagerUserConfig { get; set; }

		// Represents the property CreationClassName
		public virtual string CreationClassName { get; set; }

		// Represents the property Description
		public virtual string Description { get; set; }

		// Represents the property DeviceID
		public virtual string DeviceID { get; set; }

		// Represents the property DriveType
		public virtual uint DriveType { get; set; }

		// Represents the property ErrorCleared
		public virtual bool ErrorCleared { get; set; }

		// Represents the property ErrorDescription
		public virtual string ErrorDescription { get; set; }

		// Represents the property ErrorMethodology
		public virtual string ErrorMethodology { get; set; }

		// Represents the property FileSystem
		public virtual string FileSystem { get; set; }

		// Represents the property FreeSpace
		public virtual ulong FreeSpace { get; set; }

		// Represents the property InstallDate
		public virtual DateTime InstallDate { get; set; }

		// Represents the property LastErrorCode
		public virtual uint LastErrorCode { get; set; }

		// Represents the property MaximumComponentLength
		public virtual uint MaximumComponentLength { get; set; }

		// Represents the property MediaType
		public virtual uint MediaType { get; set; }

		// Represents the property Name
		public virtual string Name { get; set; }

		// Represents the property NumberOfBlocks
		public virtual ulong NumberOfBlocks { get; set; }

		// Represents the property PNPDeviceID
		public virtual string PNPDeviceID { get; set; }

		// Represents the property PowerManagementCapabilities
		public virtual ushort[] PowerManagementCapabilities { get; set; }

		// Represents the property PowerManagementSupported
		public virtual bool PowerManagementSupported { get; set; }

		// Represents the property ProviderName
		public virtual string ProviderName { get; set; }

		// Represents the property Purpose
		public virtual string Purpose { get; set; }

		// Represents the property QuotasDisabled
		public virtual bool QuotasDisabled { get; set; }

		// Represents the property QuotasIncomplete
		public virtual bool QuotasIncomplete { get; set; }

		// Represents the property QuotasRebuilding
		public virtual bool QuotasRebuilding { get; set; }

		// Represents the property Size
		public virtual ulong Size { get; set; }

		// Represents the property Status
		public virtual string Status { get; set; }

		// Represents the property StatusInfo
		public virtual ushort StatusInfo { get; set; }

		// Represents the property SupportsDiskQuotas
		public virtual bool SupportsDiskQuotas { get; set; }

		// Represents the property SupportsFileBasedCompression
		public virtual bool SupportsFileBasedCompression { get; set; }

		// Represents the property SystemCreationClassName
		public virtual string SystemCreationClassName { get; set; }

		// Represents the property SystemName
		public virtual string SystemName { get; set; }

		// Represents the property VolumeDirty
		public virtual bool VolumeDirty { get; set; }

		// Represents the property VolumeName
		public virtual string VolumeName { get; set; }

		// Represents the property VolumeSerialNumber
		public virtual string VolumeSerialNumber { get; set; }
	}
}

