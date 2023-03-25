using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;

namespace WmiController
{
	public static class Network
	{
		private static readonly ICollection<string> ComputersEntry = new List<string>
		{
            "CN=Computers"
		};

		private static readonly ICollection<string> DomainControllersEntry = new List<string>
		{
			"OU=Domain Controllers"
		};

		private static readonly ICollection<string> AllComputersEntry = new List<string>
		{
			"CN=Computers",
			"OU=Domain Controllers"
		};

        public static IEnumerable<string> DomainComputers
		{
			get { return GetComputers(ComputersEntry); }
		}

		public static IEnumerable<string> DomainControllers
		{
			get { return GetComputers(DomainControllersEntry); }
		}

		public static IEnumerable<string> AllDomainComputers
		{
			get { return GetComputers(AllComputersEntry); }
		}

		private static IEnumerable<string> GetComputers(ICollection<string> entries)
		{
			try
			{
				var de = GetCurrentDomainEntry();
				var items = de.Children.Cast<DirectoryEntry>()
					.Where(d => entries.Contains(d.Name))
					.SelectMany(d => d.Children.Cast<DirectoryEntry>());
				return items
					.Select(d => d.Name.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries).Last())
					.Distinct();
			}
			catch (Exception)
			{
				return Enumerable.Empty<string>();
			}
		}

		private static DirectoryEntry GetCurrentDomainEntry()
		{
			var domain = Domain.GetCurrentDomain();
			return domain.GetDirectoryEntry();
		}
	}
}


