using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class z
{
	public delegate void a(y a);

	public delegate void b();

	private delegate IntPtr c(int a, IntPtr b, IntPtr c);

	private const int ct = 13;

	private const int d = 256;

	private const int e = 257;

	private c cu;

	private IntPtr cv = IntPtr.Zero;

	private IntPtr cw = IntPtr.Zero;

	private static bool m_c = false;

	public static bool[] db = new bool[255];

	private const uint dc = 1u;

	private const uint dd = 2u;

	private const uint de = 4u;

	private const uint df = 32u;

	private const uint dg = 64u;

	private float dh;

	public event a cx;

	public event a cy;

	public event a cz;

	public event a da;

	public event b OnAnyKeyDown;

	public event b OnAnyKeyUp;

	public event b OnAnyKey;

	public void hj()
	{
		cw = x.ha(Process.GetCurrentProcess());
		cu = hr;
		cv = hq(cu);
	}

	public void Start()
	{
		bg obj = GameEntry.w;
		obj.en = (Action<byte>)Delegate.Combine(obj.en, new Action<byte>(hs));
		bg obj2 = GameEntry.w;
		obj2.ep = (Action<byte>)Delegate.Combine(obj2.ep, new Action<byte>(ht));
	}

	public void Update()
	{
		dh += Time.deltaTime;
		if (dh >= 2f)
		{
			dh = 0f;
			hw();
		}
	}

	public void Close()
	{
		bg obj = GameEntry.w;
		obj.en = (Action<byte>)Delegate.Remove(obj.en, new Action<byte>(hs));
		bg obj2 = GameEntry.w;
		obj2.ep = (Action<byte>)Delegate.Remove(obj2.ep, new Action<byte>(ht));
		UnhookWindowsHookEx(cv);
	}

	private void hk()
	{
		UnityEngine.Debug.LogError("OnAnyKey");
	}

	private void hl()
	{
		UnityEngine.Debug.LogError("OnAnyKeyUp");
	}

	private void hm()
	{
		UnityEngine.Debug.LogError("OnAnyKeyDown");
	}

	private void hn(y a)
	{
		UnityEngine.Debug.LogError("OnKey:" + a);
	}

	private void ho(y a)
	{
		UnityEngine.Debug.LogError("OnKeyUp:" + a);
	}

	private void hp(y a)
	{
		UnityEngine.Debug.LogError("OnKeyDown:" + a);
	}

	private IntPtr hq(c a)
	{
		using (Process process = Process.GetCurrentProcess())
		{
			using (ProcessModule processModule = process.MainModule)
			{
				return SetWindowsHookEx(13, a, GetModuleHandle(processModule.ModuleName), 0u);
			}
		}
	}

	private IntPtr hr(int a, IntPtr b, IntPtr c)
	{
		try
		{
			if (a >= 0)
			{
				int num = Marshal.ReadInt32(c);
				if (b == (IntPtr)256)
				{
					hv(num, b: true);
				}
				if (b == (IntPtr)257)
				{
					hv(num, b: false);
				}
			}
			return CallNextHookEx(cv, a, b, c);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			return (IntPtr)0;
		}
	}

	private void hs(byte a)
	{
		hu(a, 1, y.LeftArrow);
		hu(a, 2, y.RightArrow);
		hu(a, 4, y.Return);
		hu(a, 8, y.Q);
		hu(a, 16, y.W);
	}

	private void ht(byte a)
	{
		hu(a, 1, y.Alpha1);
		hu(a, 2, y.Alpha2);
		hu(a, 4, y.Alpha3);
		hu(a, 8, y.Alpha4);
		hu(a, 16, y.Alpha5);
		hu(a, 32, y.Alpha6);
	}

	private void hu(byte a, byte b, y c)
	{
		if (a.bg(b))
		{
			hv((int)c, b: true);
		}
		else if (db[(int)c])
		{
			hv((int)c, b: false);
		}
	}

	private void hv(int a, bool b)
	{
		if (b)
		{
			this.cx?.Invoke((y)a);
			this.OnAnyKeyDown?.Invoke();
			if (!db[a])
			{
				this.da?.Invoke((y)a);
			}
		}
		else
		{
			this.cy?.Invoke((y)a);
			this.OnAnyKeyUp?.Invoke();
		}
		this.cz?.Invoke((y)a);
		this.OnAnyKey?.Invoke();
		db[a] = b;
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr SetWindowsHookEx(int a, c b, IntPtr c, uint d);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool UnhookWindowsHookEx(IntPtr a);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr CallNextHookEx(IntPtr a, int b, IntPtr c, IntPtr d);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr GetModuleHandle(string a);

	[DllImport("user32.dll")]
	public static extern void SwitchToThisWindow(IntPtr a, bool b);

	[DllImport("User32.dll")]
	public static extern bool SetForegroundWindow(IntPtr a);

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(IntPtr a, IntPtr b, int c, int d, int e, int f, uint g);

	[DllImport("kernel32.dll")]
	public static extern uint GetCurrentThreadId();

	[DllImport("user32.dll")]
	public static extern bool AttachThreadInput(uint a, uint b, bool c);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern uint GetWindowThreadProcessId(IntPtr a, out uint b);

	public void hw(bool a = true)
	{
		if (a)
		{
			if (!(GetForegroundWindow() == cw))
			{
				SetWindowPos(cw, new IntPtr(0), 0, 0, 0, 0, 99u);
				SwitchToThisWindow(cw, b: true);
				IntPtr foregroundWindow = GetForegroundWindow();
				uint currentThreadId = GetCurrentThreadId();
				uint num;
				uint windowThreadProcessId = GetWindowThreadProcessId(foregroundWindow, out num);
				AttachThreadInput(currentThreadId, windowThreadProcessId, c: true);
				SetForegroundWindow(cw);
				AttachThreadInput(currentThreadId, windowThreadProcessId, c: false);
			}
		}
		else
		{
			SwitchToThisWindow(cw, b: false);
		}
	}
}
