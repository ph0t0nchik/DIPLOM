using System;
using System.ServiceModel;
using System.Windows.Forms;
using HookContracts;

namespace HookServiceApp
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	internal class HookControl : IHookControl, IDisposable
	{
		private readonly IHookCallback m_callback;

		public HookControl()
		{
			m_callback = OperationContext.Current.GetCallbackChannel<IHookCallback>();
		}

		public void Start()
		{
			Native.KeyDown += NativeOnKeyDownEvent;
			Native.KeyUp += NativeOnKeyUpEvent;
		}

		public void Stop()
		{
			Native.KeyDown -= NativeOnKeyDownEvent;
			Native.KeyUp -= NativeOnKeyUpEvent;
		}

		public void Dispose()
		{
			Stop();
		}

		private void NativeOnKeyDownEvent(Keys keys)
		{
			m_callback.OnKeyDown(keys);
		}

		private void NativeOnKeyUpEvent(Keys keys)
		{
			m_callback.OnKeyUp(keys);
		}
	}
}

