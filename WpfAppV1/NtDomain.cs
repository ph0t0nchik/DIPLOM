using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_NTDomain")]
	public class NtDomain
	{
		/// <summary>
		/// Represents the property Caption
		/// </summary>
		public virtual string Caption { get; set; }

		/// <summary>
		/// Represents the property ClientSiteName
		/// </summary>
		public virtual string ClientSiteName { get; set; }

		/// <summary>
		/// Represents the property CreationClassName
		/// </summary>
		public virtual string CreationClassName { get; set; }

		/// <summary>
		/// Represents the property DcSiteName
		/// </summary>
		public virtual string DcSiteName { get; set; }

		/// <summary>
		/// Represents the property Description
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Represents the property DnsForestName
		/// </summary>
		public virtual string DnsForestName { get; set; }

		/// <summary>
		/// Represents the property DomainControllerAddress
		/// </summary>
		public virtual string DomainControllerAddress { get; set; }

		/// <summary>
		/// Represents the property DomainControllerAddressType
		/// </summary>
		public virtual int DomainControllerAddressType { get; set; }

		/// <summary>
		/// Represents the property DomainControllerName
		/// </summary>
		public virtual string DomainControllerName { get; set; }

		/// <summary>
		/// Represents the property DomainGuid
		/// </summary>
		public virtual string DomainGuid { get; set; }

		/// <summary>
		/// Represents the property DomainName
		/// </summary>
		public virtual string DomainName { get; set; }

		/// <summary>
		/// Represents the property DSDirectoryServiceFlag
		/// </summary>
		public virtual bool DSDirectoryServiceFlag { get; set; }

		/// <summary>
		/// Represents the property DSDnsControllerFlag
		/// </summary>
		public virtual bool DSDnsControllerFlag { get; set; }

		/// <summary>
		/// Represents the property DSDnsDomainFlag
		/// </summary>
		public virtual bool DSDnsDomainFlag { get; set; }

		/// <summary>
		/// Represents the property DSDnsForestFlag
		/// </summary>
		public virtual bool DSDnsForestFlag { get; set; }

		/// <summary>
		/// Represents the property DSGlobalCatalogFlag
		/// </summary>
		public virtual bool DSGlobalCatalogFlag { get; set; }

		/// <summary>
		/// Represents the property DSKerberosDistributionCenterFlag
		/// </summary>
		public virtual bool DSKerberosDistributionCenterFlag { get; set; }

		/// <summary>
		/// Represents the property DSPrimaryDomainControllerFlag
		/// </summary>
		public virtual bool DSPrimaryDomainControllerFlag { get; set; }

		/// <summary>
		/// Represents the property DSTimeServiceFlag
		/// </summary>
		public virtual bool DSTimeServiceFlag { get; set; }

		/// <summary>
		/// Represents the property DSWritableFlag
		/// </summary>
		public virtual bool DSWritableFlag { get; set; }

		/// <summary>
		/// Represents the property InstallDate
		/// </summary>
		public virtual DateTime InstallDate { get; set; }

		/// <summary>
		/// Represents the property Name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Represents the property NameFormat
		/// </summary>
		public virtual string NameFormat { get; set; }

		/// <summary>
		/// Represents the property PrimaryOwnerContact
		/// </summary>
		public virtual string PrimaryOwnerContact { get; set; }

		/// <summary>
		/// Represents the property PrimaryOwnerName
		/// </summary>
		public virtual string PrimaryOwnerName { get; set; }

		/// <summary>
		/// Represents the property Roles
		/// </summary>
		public virtual string[] Roles { get; set; }

		/// <summary>
		/// Represents the property Status
		/// </summary>
		public virtual string Status { get; set; }
	}
}

