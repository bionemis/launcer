using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class r
{
	public Dictionary<string, l> cc;

	private Thread cd;

	private float ce;

	public void fo()
	{
		cc = new Dictionary<string, l>();
		m.dw();
		m.dy();
	}

	public void Start()
	{
	}

	public void fp()
	{
		cd = new Thread((ThreadStart)delegate
		{
			while (true)
			{
				Thread.Sleep(500);
				fq();
			}
		});
		cd.Start();
	}

	public void Update()
	{
		ce += Time.deltaTime;
		if (!(ce >= 0.5f))
		{
			return;
		}
		ce = 0f;
		List<string> fc = GameEntry.z.fc;
		Dictionary<string, l>.Enumerator enumerator = cc.GetEnumerator();
		while (enumerator.MoveNext())
		{
			l value = enumerator.Current.Value;
			for (int num = 0; num < fc.Count; num++)
			{
				if (value.bi == 1 && fc[num].Contains(value.bh.bk()))
				{
					value.dt(a: true);
				}
			}
		}
	}

	public void Close()
	{
		cd?.Abort();
		m.dx();
	}

	public void fq()
	{
		List<string> list = new List<string>();
		string[] array = m.ec();
		Dictionary<int, UIGlassItem> mGlassItemDic = MainControl.LobbyPanel.mGlassItemDic;
		int num = array.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			string text = array[num2];
			string text2 = m.ed(text);
			string text3 = m.fb(text);
			if (cc.ContainsKey(text))
			{
				l l2 = cc[text];
				l2.bf = 0;
				if (string.IsNullOrEmpty(text2))
				{
					if (text.Contains("192.168") && l2.bf.Equals(text))
					{
						if (l2.bi == 2)
						{
							l2.bi = 3;
							list.Add(text);
							Debug.Log("wifi断开 主动去掉假连接=》" + text);
							m.eb(text);
						}
						continue;
					}
					if (l2.bi == 1 && l2.bg)
					{
						l2.bg = false;
					}
				}
				else if (text.Contains(text2))
				{
					if (text3.Contains("error") || text3.Equals("adb.exe: device offline"))
					{
						Debug.LogError("获取Imei失败！这里说明就是ip假连接.");
					}
					else if (l2.bi == 3 && l2.bj)
					{
						l2.bi = 2;
						fs(text2, text3);
					}
					if (!l2.bh && l2.bj)
					{
						l2.bh = true;
						l2.bi = 2;
						l2.bg = true;
						fs(text2, text3);
					}
				}
				else if (text2.Contains("192.168") && l2.bi == 1 && !l2.bg && !fr(text2) && l2.bj)
				{
					m.dz(text3);
					l2.bg = true;
					if (!m.ea(text3, text2).Contains("connected to 192.168"))
					{
						k.ca("电脑未网络错误：" + text2);
					}
				}
			}
			else if (!text3.Contains("device unauthorized") && !text3.Contains("adb.exe"))
			{
				l l3 = new l(text, text2, text3);
				l3.bi = 1;
				Debug.Log("新建的连接:" + l3.bf);
				cc.Add(text, l3);
			}
			Thread.Sleep(50);
		}
		foreach (KeyValuePair<string, l> item in cc)
		{
			item.Value.bf++;
			if (item.Value.bf >= item.Value.bi)
			{
				Debug.Log("连接超时:" + item.Value.bf);
				list.Add(item.Key);
			}
		}
		for (int num3 = 0; num3 < list.Count; num3++)
		{
			for (int num4 = 0; num4 < mGlassItemDic.Count; num4++)
			{
				l l4 = cc[list[num3]];
				if (l4.bh && l4.bf.Contains(mGlassItemDic[num4].cf))
				{
					Debug.Log("断开连接:" + l4.bf);
					mGlassItemDic[num4].qa();
				}
			}
			cc.Remove(list[num3]);
		}
	}

	private bool fr(string a)
	{
		Dictionary<string, l>.Enumerator enumerator = cc.GetEnumerator();
		while (enumerator.MoveNext())
		{
			l value = enumerator.Current.Value;
			if (value.bf.Contains(a) && value.bg)
			{
				return true;
			}
		}
		return false;
	}

	private void fs(string a, string b)
	{
		Dictionary<int, UIGlassItem> mGlassItemDic = MainControl.LobbyPanel.mGlassItemDic;
		for (int num = 0; num < mGlassItemDic.Count; num++)
		{
			if (mGlassItemDic[num].cf.Equals(a))
			{
				string[] array = m.en(a, p.RboThird);
				mGlassItemDic[num].pw(b, array);
			}
		}
	}

	[CompilerGenerated]
	private void ft()
	{
		while (true)
		{
			Thread.Sleep(500);
			fq();
		}
	}
}
