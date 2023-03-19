using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_NetworkConnection")]
	public class NetworkConnection
	{
		/// <summary>
		/// Represents the property AccessMask
		/// </summary>
		public virtual uint AccessMask { get; set; }

		/// <summary>
		/// Represents the property Caption
		/// </summary>
		public virtual string Caption { get; set; }

		/// <summary>
		/// Represents the property Comment
		/// </summary>
		public virtual string Comment { get; set; }

		/// <summary>
		/// Represents the property ConnectionState
		/// </summary>
		public virtual string ConnectionState { get; set; }

		/// <summary>
		/// Represents the property ConnectionType
		/// </summary>
		public virtual string ConnectionType { get; set; }

		/// <summary>
		/// Represents the property Description
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Represents the property DisplayType
		/// </summary>
		public virtual string DisplayType { get; set; }

		/// <summary>
		/// Represents the property InstallDate
		/// </summary>
		public virtual DateTime InstallDate { get; set; }

		/// <summary>
		/// Represents the property LocalName
		/// </summary>
		public virtual string LocalName { get; set; }

		/// <summary>
		/// Represents the property Name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Represents the property Persistent
		/// </summary>
		public virtual bool Persistent { get; set; }

		/// <summary>
		/// Represents the property ProviderName
		/// </summary>
		public virtual string ProviderName { get; set; }

		/// <summary>
		/// Represents the property RemoteName
		/// </summary>
		public virtual string RemoteName { get; set; }

		/// <summary>
		/// Represents the property RemotePath
		/// </summary>
		public virtual string RemotePath { get; set; }

		/// <summary>
		/// Represents the property ResourceType
		/// </summary>
		public virtual string ResourceType { get; set; }

		/// <summary>
		/// Represents the property Status
		/// </summary>
		public virtual string Status { get; set; }

		/// <summary>
		/// Represents the property UserName
		/// </summary>
		public virtual string UserName { get; set; }
	}
}

