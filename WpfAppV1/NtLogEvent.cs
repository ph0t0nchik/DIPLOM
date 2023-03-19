using System;
using WmiController.WMI.Common;

namespace WmiController.Prototypes
{
	[WmiMap("Win32_NTLogEvent")]
	public class NtLogEvent
	{
		// Represents the property Category
		public virtual ushort Category { get; set; }

		// Represents the property CategoryString
		public virtual string CategoryString { get; set; }

		// Represents the property ComputerName
		public virtual string ComputerName { get; set; }

		// Represents the property Data
		public virtual byte[] Data { get; set; }

		// Represents the property EventCode
		public virtual ushort EventCode { get; set; }

		// Represents the property EventIdentifier
		public virtual uint EventIdentifier { get; set; }

		// Represents the property EventType
		public virtual byte EventType { get; set; }

		// Represents the property InsertionStrings
		public virtual string[] InsertionStrings { get; set; }

		// Represents the property Logfile
		public virtual string Logfile { get; set; }

		// Represents the property Message
		public virtual string Message { get; set; }

		// Represents the property RecordNumber
		public virtual uint RecordNumber { get; set; }

		// Represents the property SourceName
		public virtual string SourceName { get; set; }

		// Represents the property TimeGenerated
		public virtual DateTime TimeGenerated { get; set; }

		// Represents the property TimeWritten
		public virtual DateTime TimeWritten { get; set; }

		// Represents the property Type
		public virtual string Type { get; set; }

		// Represents the property User
		public virtual string User { get; set; }
	}
}

