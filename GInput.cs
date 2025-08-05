using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class GInput : MonoBehaviour
{
	private enum a
	{
		WH_JOURNALRECORD = 0,
		WH_JOURNALPLAYBACK = 1,
		WH_KEYBOARD = 2,
		WH_GETMESSAGE = 3,
		WH_CALLWNDPROC = 4,
		WH_CBT = 5,
		WH_SYSMSGFILTER = 6,
		WH_MOUSE = 7,
		WH_HARDWARE = 8,
		WH_DEBUG = 9,
		WH_SHELL = 10,
		WH_FOREGROUNDIDLE = 11,
		WH_CALLWNDPROCRET = 12,
		WH_KEYBOARD_LL = 13,
		WH_MOUSE_LL = 14
	}

	private delegate IntPtr b(int a, IntPtr b, IntPtr c);

	private static GInput m_b;

	private static bool m_c;

	public static bool[] keyStates;

	public static bool[] keyDownStates;

	public static bool[] keyUpStates;

	private const int m_d = 256;

	private const int m_e = 257;

	private static IntPtr m_f;

	private static b m_g;

	private static IntPtr m_h;

	public static GInput a
	{
		get
		{
			if (GInput.m_b == null)
			{
				GInput.m_b = UnityEngine.Object.FindObjectOfType<GInput>();
				if (GInput.m_b == null)
				{
					GInput.m_b = new GameObject("GlobalInputManager").AddComponent<GInput>();
				}
			}
			return GInput.m_b;
		}
	}

	public static bool b
	{
		get
		{
			if (GInput.m_c)
			{
				GInput.m_c = false;
				return true;
			}
			return false;
		}
	}

	private static bool b()
	{
		if (GInput.m_g == null)
		{
			GInput.m_g = d;
		}
		if (GInput.m_h != IntPtr.Zero)
		{
			c();
		}
		GInput.m_f = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
		GInput.m_h = SetWindowsHookEx(13, GInput.m_g, GInput.m_f, 0u);
		if (GInput.m_h == IntPtr.Zero)
		{
			UnityEngine.Debug.Log("Failed to hook");
			return false;
		}
		UnityEngine.Debug.Log("Hooked successfully");
		return true;
	}

	private static void c()
	{
		UnhookWindowsHookEx(GInput.m_h);
		GInput.m_h = IntPtr.Zero;
	}

	private static IntPtr d(int a, IntPtr b, IntPtr c)
	{
		if (a >= 0)
		{
			int num = Marshal.ReadInt32(c);
			if (b == (IntPtr)256)
			{
				e(num, b: true);
			}
			if (b == (IntPtr)257)
			{
				e(num, b: false);
			}
		}
		return CallNextHookEx(GInput.m_h, a, b, c);
	}

	private static void e(int a, bool b)
	{
		if (!keyStates[a])
		{
			GInput.m_c = true;
			keyDownStates[a] = b;
		}
		keyStates[a] = b;
		keyUpStates[a] = !b;
	}

	public static bool f(y a)
	{
		return keyStates[(int)a];
	}

	public static bool g(y a)
	{
		if (keyDownStates[(int)a])
		{
			keyDownStates[(int)a] = false;
			return true;
		}
		return false;
	}

	public static bool h(y a)
	{
		if (keyUpStates[(int)a])
		{
			keyUpStates[(int)a] = false;
			return true;
		}
		return false;
	}

	static GInput()
	{
		GInput.m_c = false;
		keyStates = new bool[255];
		keyDownStates = new bool[255];
		keyUpStates = new bool[255];
		GInput.m_h = IntPtr.Zero;
		b();
		GInput.m_b = new GameObject("GlobalInputManager").AddComponent<GInput>();
		UnityEngine.Object.DontDestroyOnLoad(GInput.m_b);
	}

	private void OnDisable()
	{
		UnityEngine.Debug.Log("Application ending after " + Time.time + " seconds");
		UnityEngine.Debug.Log("Uninstall hook");
		c();
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr SetWindowsHookEx(int a, b b, IntPtr c, uint d);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool UnhookWindowsHookEx(IntPtr a);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr CallNextHookEx(IntPtr a, int b, IntPtr c, IntPtr d);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr GetModuleHandle(string a);

	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();
}
