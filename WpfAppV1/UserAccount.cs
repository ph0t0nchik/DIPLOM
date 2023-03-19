using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_UserAccount")]
	public class UserAccount
	{
		// Represents the property AccountType
		public virtual uint AccountType { get; set; }

		// Represents the property Caption
		public virtual string Caption { get; set; }

		// Represents the property Description
		public virtual string Description { get; set; }

		// Represents the property Disabled
		public virtual bool Disabled { get; set; }

		// Represents the property Domain
		public virtual string Domain { get; set; }

		// Represents the property FullName
		public virtual string FullName { get; set; }

		// Represents the property InstallDate
		public virtual DateTime InstallDate { get; set; }

		// Represents the property LocalAccount
		public virtual bool LocalAccount { get; set; }

		// Represents the property Lockout
		public virtual bool Lockout { get; set; }

		// Represents the property Name
		public virtual string Name { get; set; }

		// Represents the property PasswordChangeable
		public virtual bool PasswordChangeable { get; set; }

		// Represents the property PasswordExpires
		public virtual bool PasswordExpires { get; set; }

		// Represents the property PasswordRequired
		public virtual bool PasswordRequired { get; set; }

		// Represents the property SID
		public virtual string SID { get; set; }

		// Represents the property SIDType
		public virtual byte SIDType { get; set; }

		// Represents the property Status
		public virtual string Status { get; set; }
	}
}


