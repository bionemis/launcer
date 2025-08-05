using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class bd
{
	private static int dq = 0;

	private static int dr = 1;

	private be ds = new be(1280f, 1024f);

	public be dt = new be(1600f, 900f);

	private string du;

	public Dictionary<string, Process> dv = new Dictionary<string, Process>();

	private int dw;

	public float bq => 20f;

	[DllImport("user32.dll", SetLastError = true)]
	private static extern int GetSystemMetrics(int a);

	public void ij()
	{
		ds.dx = GetSystemMetrics(dq);
		ds.dy = GetSystemMetrics(dr);
		Screen.SetResolution((int)ds.dx, (int)ds.dy, fullscreen: true);
		du = h.m;
	}

	public void Start()
	{
		if (GameEntry.a.IsDoubleScreen)
		{
			if (Display.displays.Length > 1)
			{
				Display.displays[1].Activate();
				dt.dx = Display.displays[1].systemWidth;
				dt.dy = Display.displays[1].systemHeight;
			}
			else
			{
				MainControl.TintPanel.vk("No Found 2 Screen！");
			}
		}
		else
		{
			dt.dx = ds.dx;
			dt.dy = ds.dy;
		}
	}

	public void Update()
	{
	}

	public void ik(string a, int b)
	{
		try
		{
			int num = MainControl.LobbyPanel.cx;
			if (dw + 1 > num)
			{
				MainControl.TintPanel.vk("Out of range");
				return;
			}
			MainControl.ProScreenPanel.vb(dw, b);
			be be2 = default(be);
			be be3 = default(be);
			@in(dw, num, out be2, out be3);
			string text = "--always-on-top --window-borderless";
			string text2 = $"{text} --window-x {(int)be3.dx} --window-y {(int)be3.dy} --window-width {(int)be2.dx} --window-height {(int)be2.dy}";
			float num2 = be2.dx / be2.dy;
			be be4 = new be(1500f, 900f);
			float num3 = be4.dx / be4.dy;
			if (num2 >= num3)
			{
				be4.dy = (int)Math.Round(be4.dx / num2);
			}
			else
			{
				be4.dx = (int)Math.Round(be4.dy * num2);
			}
			string text3 = $"--crop {(int)be4.dx}:{(int)be4.dy}:2017:460";
			dw++;
			if (dv.ContainsKey(a))
			{
				dv[a].Start();
				return;
			}
			Process value = n.Start(du, "-s " + a + " " + text2 + " " + text3);
			dv.Add(a, value);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
	}

	public void il(string a)
	{
		if (dv.ContainsKey(a))
		{
			try
			{
				dv[a]?.Kill();
				dv[a]?.Close();
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError(a + "  " + ex.Message);
			}
			dv.Remove(a);
		}
	}

	public void im(Action a)
	{
		dw = 0;
		if (dv.Count > 0)
		{
			Dictionary<string, Process>.Enumerator enumerator = dv.GetEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					Process value = enumerator.Current.Value;
					value?.Kill();
					value?.Close();
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError(enumerator.Current.Key + "  " + ex.Message);
				}
			}
			dv.Clear();
		}
		a?.Invoke();
	}

	private void @in(int a, int b, out be c, out be d)
	{
		float dx = dt.dx;
		float num = dt.dy;
		float num2 = 0f;
		if (GameEntry.a.IsDoubleScreen)
		{
			num2 = ds.dx;
		}
		float num3 = bq;
		switch (b)
		{
		case 1:
			dx = io(dt.dx, 1);
			num = io(dt.dy, 1);
			num2 += ip(dx, a);
			break;
		case 2:
			dx = io(dt.dx, 2);
			num = io(dt.dy, 1);
			num2 += ip(dx, a);
			break;
		case 3:
		case 4:
			dx = io(dt.dx, 2);
			num = io(dt.dy, 2);
			if (a < 2)
			{
				num2 += ip(dx, a);
				break;
			}
			num2 += ip(dx, a - 2);
			num3 += num + bq;
			break;
		case 5:
		case 6:
			dx = io(dt.dx, 3);
			num = io(dt.dy, 2);
			if (a < 3)
			{
				num2 += ip(dx, a);
				break;
			}
			num2 += ip(dx, a - 3);
			num3 += num + bq;
			break;
		}
		c = default(be);
		c.dx = dx;
		c.dy = num;
		d = default(be);
		d.dx = num2;
		d.dy = num3;
	}

	private float io(float a, int b)
	{
		if (b <= 0)
		{
			return a;
		}
		return (a - (float)(b + 1) * bq) / (float)b;
	}

	private float ip(float a, int b)
	{
		return a * (float)b + bq * (float)(b + 1);
	}

	public void Close()
	{
		if (dv != null)
		{
			dv.Clear();
			dv = null;
		}
	}
}
