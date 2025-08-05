using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class x
{
	public delegate bool a(IntPtr a, uint b);

	private sealed class b
	{
		public IntPtr cs;

		internal bool gz(IntPtr a, uint b)
		{
			uint num = 0u;
			if (GetParent(a) == IntPtr.Zero)
			{
				GetWindowThreadProcessId(a, ref num);
				if (num == b)
				{
					cs = a;
					SetLastError(0u);
					return false;
				}
			}
			return true;
		}
	}

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool EnumWindows(a a, uint b);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr GetParent(IntPtr a);

	[DllImport("user32.dll")]
	public static extern uint GetWindowThreadProcessId(IntPtr a, ref uint b);

	[DllImport("kernel32.dll")]
	public static extern void SetLastError(uint a);

	public static IntPtr ha(Process a)
	{
		b b2 = new b();
		b2.cs = IntPtr.Zero;
		uint id = (uint)a.Id;
		if (EnumWindows(b2.gz, id) || Marshal.GetLastWin32Error() != 0)
		{
			return IntPtr.Zero;
		}
		return b2.cs;
	}
}
