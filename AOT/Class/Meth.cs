using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace AOT.Classes
{
	class Methods
	{

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		[DllImport("user32.dll")]
		static extern bool SetWindowText(IntPtr hWnd, string text);

        const int SWP_NOMOVE = 0x0002;
		const int SWP_NOSIZE = 0x0001;
        const int HWND_TOPMOST = -1;
		const int HWND_NOTOPMOST = -2;

		public static void AoT_on(string title) // AoT_on()
		{
			Process[] processes = Process.GetProcesses(".");
			foreach (var process in processes)
			{
				string mWinTitle = process.MainWindowTitle.ToString();
				if (mWinTitle == title)
				{
					IntPtr handle = process.MainWindowHandle;
					if (handle != IntPtr.Zero)
					{
						SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
						string newTitle = title + " - AoT";
						SetWindowText(handle, newTitle);
					}
				}
			}
		}

		public static void AoT_off(string title) // AoT_off()
		{
			Process[] processes = Process.GetProcesses(".");
			foreach (var process in processes)
			{
				string mWinTitle = process.MainWindowTitle.ToString();
				if (mWinTitle == title)
				{
					IntPtr handle = process.MainWindowHandle;
					if (handle != IntPtr.Zero)
					{
						SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
						string newTitle = title.Substring(0, title.Length - 6);
						SetWindowText(handle, newTitle);
					}
				}
			}
		}
	}
}
