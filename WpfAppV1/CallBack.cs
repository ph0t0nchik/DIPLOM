using System;
using System.Windows.Forms;
using HookContracts;

namespace HookClientExample
{
	internal class Callback : IHookCallback
	{
		public void OnKeyDown(Keys keys)
		{
			Console.WriteLine("KeyDown: [{0}]", keys);
		}

		public void OnKeyUp(Keys keys)
		{
			Console.WriteLine("KeyUp: [{0}]", keys);
		}
	}
}

