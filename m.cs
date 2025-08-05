using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

public static class m
{
	private sealed class a
	{
		public string bk;

		public string bl;

		internal void du()
		{
			string text = $"-s {bk} shell {bl}";
			n.fh(m.bk, text);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	private struct b : IAsyncStateMachine
	{
		public int y;

		public AsyncTaskMethodBuilder<string> bm;

		public string bk;

		public string bn;

		private TaskAwaiter<o> bo;

		private void MoveNext()
		{
			int num = y;
			string result2;
			try
			{
				TaskAwaiter<o> awaiter;
				if (num != 0)
				{
					string text = "/storage/emulated/0/Android/obb/";
					UnityEngine.Debug.Log("-s " + bk + " push \"" + bn + "\" \"" + text + "\"");
					awaiter = n.fj(m.bk, "push -s " + bk + " \"" + bn + "\" \"" + text + "\"").GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (y = 0);
						bo = awaiter;
						bm.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = bo;
					bo = default(TaskAwaiter<o>);
					num = (y = -1);
				}
				o result = awaiter.GetResult();
				result2 = ((!string.IsNullOrEmpty(result.bx)) ? result.bx.Trim() : null);
			}
			catch (Exception exception)
			{
				y = -2;
				bm.SetException(exception);
				return;
			}
			y = -2;
			bm.SetResult(result2);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine a)
		{
			bm.SetStateMachine(a);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine a)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(a);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	private struct c : IAsyncStateMachine
	{
		public int y;

		public AsyncVoidMethodBuilder bm;

		public string bk;

		public string bl;

		public Action<string> bp;

		private TaskAwaiter<o> bo;

		private void MoveNext()
		{
			int num = y;
			try
			{
				TaskAwaiter<o> awaiter;
				if (num != 0)
				{
					string text = $"-s {bk} shell getprop {bl}";
					awaiter = n.fj(m.bk, text).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (y = 0);
						bo = awaiter;
						bm.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = bo;
					bo = default(TaskAwaiter<o>);
					num = (y = -1);
				}
				o result = awaiter.GetResult();
				if (string.IsNullOrEmpty(result.bx))
				{
					bp?.Invoke(null);
				}
				else
				{
					bp?.Invoke(result.bx.Trim());
				}
			}
			catch (Exception exception)
			{
				y = -2;
				bm.SetException(exception);
				return;
			}
			y = -2;
			bm.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine a)
		{
			bm.SetStateMachine(a);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine a)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(a);
		}
	}

	private static string bk => h.l;

	public static void dw()
	{
		n.fh(bk, "start-server");
	}

	public static void dx()
	{
		n.fh(bk, "kill-server");
	}

	public static void dy()
	{
		n.fh(bk, "version");
	}

	public static void Restart(string a)
	{
		n.fg(bk, "-s " + a + " reboot");
	}

	public static void ShutDown(string a)
	{
		string text = $"-s {a} shell reboot -p";
		n.fg(bk, text);
	}

	public static string dz(string a)
	{
		return n.fi(bk, "-s " + a + " tcpip 5555").bx;
	}

	public static string ea(string a, string b)
	{
		return n.fi(h.l, "-s " + a + " connect " + b).bx;
	}

	public static void eb(string a)
	{
		n.fh(h.l, "disconnect " + a);
	}

	public static string[] ec()
	{
		string[] array = n.fi(bk, "devices").bx.Split(new string[4] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		List<string> list = new List<string>();
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text.Trim().IndexOf("\t") != -1)
			{
				string[] array3 = text.Split('\t');
				list.Add(array3[0]);
			}
		}
		list.Sort();
		return list.ToArray();
	}

	public static string ed(string a)
	{
		string text = ef(a, "ip -f inet addr show wlan0");
		if (string.IsNullOrEmpty(text))
		{
			return "";
		}
		string[] array = text.Split(new string[2] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		string result = null;
		for (int num = 0; num < array.Length; num++)
		{
			if (array[num].Contains("inet 192.168"))
			{
				result = array[num].Replace("inet ", "").Trim().Split('/')[0];
			}
		}
		return result;
	}

	public static string ee(string a)
	{
		string text = ef(a, "dumpsys battery");
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		string[] array = text.Split(new string[2] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		string result = null;
		for (int num = 0; num < array.Length; num++)
		{
			if (array[num].Contains("level:"))
			{
				result = array[num].Split(':')[1];
			}
		}
		return result;
	}

	public static string ef(string a, string b)
	{
		string text = $"-s {a} shell {b}";
		Task<o> task = n.fj(bk, text);
		if (string.IsNullOrEmpty(task.Result.bx))
		{
			return null;
		}
		return task.Result.bx.Trim();
	}

	private static void eg(string a, string b)
	{
		Task.Run((Action)new a
		{
			bk = a,
			bl = b
		}.du);
	}

	public static void eh(string a)
	{
		ei(a, "com.LCDZ.QuestMainLobby", "com.unity3d.player.UnityPlayerActivity");
	}

	public static void ei(string a, string b, string c)
	{
		eg(a, "am start -n \"" + b + "/" + c + "\" ");
	}

	public static void ej(string a, string b)
	{
		eg(a, "am force-stop \"" + b + "\" ");
	}

	public static string ek(string a)
	{
		return n.fi(bk, "uninstall \"" + a + "\" ").bx;
	}

	public static string el(string a, string b)
	{
		UnityEngine.Debug.Log("-s " + a + " install -r \"" + b + "\" ");
		Task<o> task = n.fj(bk, "-s " + a + " install -r \"" + b + "\" ");
		if (string.IsNullOrEmpty(task.Result.bx))
		{
			return null;
		}
		return task.Result.bx.Trim();
	}

	public static async Task<string> em(string a, string b)
	{
		string text = "/storage/emulated/0/Android/obb/";
		UnityEngine.Debug.Log("-s " + a + " push \"" + b + "\" \"" + text + "\"");
		o o2 = await n.fj(bk, "push -s " + a + " \"" + b + "\" \"" + text + "\"");
		if (string.IsNullOrEmpty(o2.bx))
		{
			return null;
		}
		return o2.bx.Trim();
	}

	public static string[] en(string a, p b)
	{
		string[] result = null;
		switch (b)
		{
		case p.RboAll:
			result = eo("-s " + a + " shell pm list packages");
			break;
		case p.RboSystem:
			result = eo("-s " + a + " shell pm list packages -s");
			break;
		case p.RboThird:
			result = eo("-s " + a + " shell pm list packages -3");
			break;
		}
		return result;
	}

	private static string[] eo(string a)
	{
		return n.fj(bk, a).Result.bx.Split(new string[4] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
	}

	public static string ep(string a)
	{
		return ef(a, "dumpsys activity activities");
	}

	public static bool eq(string a, string b)
	{
		string text = ef(a, "ps | findstr " + b);
		UnityEngine.Debug.Log(text);
		return !string.IsNullOrEmpty(text);
	}

	private static string er(string a, string b)
	{
		return ef(a, "getprop " + b);
	}

	private static string es(string a, string b)
	{
		string text = $"-s {a} shell getprop {b}";
		string text2 = n.fj(bk, text).Result.bx;
		if (string.IsNullOrEmpty(text2))
		{
			return "";
		}
		return text2.Trim();
	}

	private static async void et(string a, string b, Action<string> c)
	{
		string text = $"-s {a} shell getprop {b}";
		o o2 = await n.fj(bk, text);
		if (string.IsNullOrEmpty(o2.bx))
		{
			c?.Invoke(null);
		}
		else
		{
			c?.Invoke(o2.bx.Trim());
		}
	}

	public static string eu(string a)
	{
		return er(a, "ro.product.model");
	}

	public static string ev(string a)
	{
		return er(a, "ro.product.brand");
	}

	public static string ew(string a)
	{
		return er(a, "ro.build.fingerprint");
	}

	public static string ex(string a)
	{
		return er(a, "ro.build.version.release");
	}

	public static string ey(string a)
	{
		return er(a, "ro.build.version.sdk");
	}

	public static string ez(string a)
	{
		return er(a, "ro.serialno");
	}

	public static void fa(string a, Action<string> b)
	{
		et(a, "ro.serialno", b);
	}

	public static string fb(string a)
	{
		return es(a, "ro.serialno");
	}
}
