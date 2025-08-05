using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;

public class cb
{
	private string gb;

	private int gc;

	private Socket gd;

	public Dictionary<string, Socket> ge;

	private Thread gf;

	private byte[] gg;

	public List<string> gh;

	public Dictionary<string, ca> gi;

	private Thread gj;

	private Thread gk;

	public bool bz { get; private set; }

	public void ng()
	{
		gc = int.Parse(GameEntry.t.bp["TcpPort"]);
		gb = GameEntry.t.bp["ConsoleIP"];
		gh = new List<string>();
		gi = new Dictionary<string, ca>();
		for (int num = 0; num < 6; num++)
		{
			gh.Add(GameEntry.t.bp[$"HostIP{num + 1}"]);
		}
		gg = new byte[256];
		ge = new Dictionary<string, Socket>();
		if (gh.Count < GameEntry.t.PlayCount)
		{
			k.ca("HostIP配置过少");
			a.k();
		}
	}

	public void nh()
	{
		if (!bz)
		{
			bz = true;
			gf = new Thread(nk);
			gf.Start();
			gj = new Thread(nj);
			gj.Start();
		}
	}

	public bool ni()
	{
		return ge.ContainsKey(gh[0]);
	}

	private void nj()
	{
		while (true)
		{
			try
			{
				nl("Check", global::d.All);
				Thread.Sleep(1000);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
		}
	}

	private void nk()
	{
		Socket socket = null;
		gd = null;
		gd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		gd.Bind(new IPEndPoint(IPAddress.Parse(gb), gc));
		gd.Listen(10);
		while (true)
		{
			try
			{
				socket = gd.Accept();
				gk = new Thread(nm);
				gk.Start();
			}
			catch (Exception ex)
			{
				Debug.Log("接受客服端连接" + ex.Message);
			}
			try
			{
				string text = socket.RemoteEndPoint.ToString().Split(':')[0];
				if (!ge.ContainsKey(text))
				{
					k.ca("客户端连接=》" + text);
					ge[text] = socket;
				}
				if (MainControl.a == null || MainControl.LobbyPanel.mGlassItemDic == null)
				{
					continue;
				}
				_ = MainControl.LobbyPanel.mGlassItemDic;
				for (int num = 0; num < gh.Count; num++)
				{
					if (text == gh[num])
					{
						Debug.Log($"后台连接{ge.Count}：{text}");
					}
				}
				Thread.Sleep(1000);
			}
			catch (Exception ex2)
			{
				k.ca("TCP:" + ex2.Message);
			}
		}
	}

	private void nl(string a, global::d b)
	{
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(a);
			if (MainControl.a == null || MonoSingle<LobbyPanel>.Instance.mGlassItemDic == null)
			{
				return;
			}
			Dictionary<int, UIGlassItem> mGlassItemDic = MonoSingle<LobbyPanel>.Instance.mGlassItemDic;
			switch (b)
			{
			case global::d.Main:
			{
				foreach (KeyValuePair<string, Socket> item in ge)
				{
					try
					{
						if (ge.ContainsKey(gh[0]))
						{
							item.Value.Send(bytes);
						}
					}
					catch (Exception)
					{
						Debug.Log("Send Failure");
						ob(item.Key);
					}
				}
				break;
			}
			case global::d.Active:
			{
				foreach (KeyValuePair<string, Socket> item2 in ge)
				{
					try
					{
						foreach (KeyValuePair<int, UIGlassItem> item3 in mGlassItemDic)
						{
							_ = item3;
						}
					}
					catch (Exception)
					{
						Debug.Log("Send Failure");
						ob(item2.Key);
					}
				}
				break;
			}
			case global::d.All:
			{
				foreach (KeyValuePair<string, Socket> item4 in ge)
				{
					for (int num = 0; num < 3; num++)
					{
						try
						{
							item4.Value.Send(bytes);
						}
						catch (Exception ex)
						{
							Debug.Log(ex.Message);
							ob(item4.Key);
						}
					}
				}
				break;
			}
			}
		}
		catch (Exception ex4)
		{
			Debug.Log(ex4.Message);
		}
	}

	private void nm()
	{
		while (true)
		{
			try
			{
				foreach (KeyValuePair<string, Socket> item in ge)
				{
					int num = item.Value.Receive(gg);
					if (num <= 0)
					{
						return;
					}
					string value = Encoding.UTF8.GetString(gg, 0, num);
					string text = item.Value.RemoteEndPoint.ToString().Split(':')[0];
					if (gh.Contains(text))
					{
						gi[text] = JsonConvert.DeserializeObject<ca>(value);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}

	public void nn(string a, bool b)
	{
		nl($"Host#TCP#{0}#{a}#{b}$", global::d.Active);
	}

	public void no()
	{
		nl($"Host#TCP#{15}$", global::d.Active);
	}

	public void np()
	{
		nl($"Host#TCP#{1}$", global::d.All);
	}

	public void nq(global::d a)
	{
		nl($"Host#TCP#{5}$", global::d.All);
	}

	public void nr(global::d a)
	{
		nl($"Host#TCP#{6}$", a);
	}

	public void ns(int a = 1000, int b = 0, int c = 0, int d = 0, int e = 0, int f = 0, int g = 0)
	{
		nl($"Host#TCP#{2}#{a}#{b}#{c}#{d}#{e}#{f}#{g}$", global::d.Main);
	}

	public void nt()
	{
		nl($"Host#TCP#{14}$", global::d.All);
	}

	public void nu(int a)
	{
		nl($"Host#TCP#{3}#{a}$", global::d.Main);
	}

	public void nv(int a)
	{
		nl($"Host#TCP#{7}#{a}$", global::d.Main);
	}

	public void nw(int a)
	{
		nl($"Host#TCP#{8}#{a}$", global::d.All);
	}

	public void nx()
	{
		nl($"Host#TCP#{4}$", global::d.All);
	}

	public void ny()
	{
		nl($"Host#TCP#{9}$", global::d.All);
	}

	public void nz(e a)
	{
		nl($"Host#TCP#{(int)a}$", global::d.All);
	}

	public void oa()
	{
		nl($"Host#TCP#{13}$", global::d.Active);
	}

	private void ob(string a)
	{
		Debug.Log("移除的ip对象:" + a);
		if (gi.ContainsKey(a))
		{
			gi.Remove(a);
		}
		if (ge.ContainsKey(a))
		{
			try
			{
				Socket socket = ge[a];
				if (socket.Connected)
				{
					socket.Shutdown(SocketShutdown.Both);
				}
				socket.Close();
				socket.Dispose();
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
			ge.Remove(a);
		}
		for (int num = 0; num < MainControl.LobbyPanel.mGlassItemDic.Count; num++)
		{
			if (a == gh[num] && MainControl.LobbyPanel.mGlassItemDic[num].cf != null)
			{
				Debug.Log($"后台退出{ge.Count}：{a}");
			}
		}
	}

	public void Close()
	{
		try
		{
			if (gj.IsAlive)
			{
				gj?.Abort();
			}
			gf?.Abort();
		}
		catch (Exception ex)
		{
			k.ca("TCP=>sendThread:" + ex.Message);
		}
		try
		{
			foreach (KeyValuePair<string, Socket> item in ge)
			{
				item.Value.Shutdown(SocketShutdown.Both);
				item.Value.Close();
				item.Value.Dispose();
			}
			if (gi != null)
			{
				ge.Clear();
				ge = null;
			}
			if (gd != null)
			{
				gd.Close();
				gd.Dispose();
				gd = null;
			}
		}
		catch (Exception ex2)
		{
			k.ca("TCP=>sendThread:" + ex2.Message);
		}
	}
}
