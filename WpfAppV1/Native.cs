using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HookServiceApp
{
	internal static class Native
	{
		private delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

		public delegate void KeyboardProc(Keys keys);

		public struct KeyboardHookStruct
		{
			public int vkCode;
			public int scanCode;
			public int flags;
			public int time;
			public int dwExtraInfo;
		}

		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x100;
		private const int WM_KEYUP = 0x101;
		private const int WM_SYSKEYDOWN = 0x104;
		private const int WM_SYSKEYUP = 0x105;

		/// <summary>
		/// Handle to the hook, need this to unhook and call the next hook
		/// </summary>
		private static IntPtr _hhook = IntPtr.Zero;

		public static event KeyboardProc KeyDown;

		public static event KeyboardProc KeyUp;

		private static readonly KeyboardHookProc s_hook = HookProc;

		public static void Hook()
		{
			var hInstance = LoadLibrary("User32");
			_hhook = SetWindowsHookEx(WH_KEYBOARD_LL, s_hook, hInstance, 0);
		}

		public static void Unhook()
		{
			UnhookWindowsHookEx(_hhook);
		}

		/// <summary>
		/// The callback for the keyboard hook
		/// </summary>
		/// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anyting</param>
		/// <param name="wParam">The event type</param>
		/// <param name="lParam">The keyhook event information</param>
		/// <returns></returns>
		public static int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
		{
			if (code >= 0)
			{
				var key = (Keys) lParam.vkCode;
				if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
				{
					KeyDown(key);
				}
				else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
				{
					KeyUp(key);
				}
			}
			return CallNextHookEx(_hhook, code, wParam, ref lParam);
		}

		/// <summary>
		/// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
		/// </summary>
		/// <param name="idHook">The id of the event you want to hook</param>
		/// <param name="callback">The callback.</param>
		/// <param name="hInstance">The handle you want to attach the event to, can be null</param>
		/// <param name="threadId">The thread you want to attach the event to, can be null</param>
		/// <returns>a handle to the desired hook</returns>
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

		/// <summary>
		/// Unhooks the windows hook.
		/// </summary>
		/// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
		/// <returns>True if successful, false otherwise</returns>
		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		/// <summary>
		/// Calls the next hook.
		/// </summary>
		/// <param name="idHook">The hook id</param>
		/// <param name="nCode">The hook code</param>
		/// <param name="wParam">The wparam.</param>
		/// <param name="lParam">The lparam.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		private static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

		/// <summary>
		/// Loads the library.
		/// </summary>
		/// <param name="lpFileName">Name of the library</param>
		/// <returns>A handle to the library</returns>
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string lpFileName);
	}
}

