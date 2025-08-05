using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class cq : RenderHeads.Media.AVProVideo.Stream
{
	private const string ue = "BANDWIDTH=";

	private const string uf = "RESOLUTION=";

	private const string ug = "#EXTINF";

	private const string uh = "#EXT-X-STREAM-INF";

	private List<RenderHeads.Media.AVProVideo.Stream> ui;

	private List<Chunk> ua;

	private string uj;

	private int uk;

	private int ul;

	private int um;

	public override int Width => uk;

	public override int Height => ul;

	public override int Bandwidth => um;

	public override string URL => uj;

	public override List<Chunk> GetAllChunks()
	{
		List<Chunk> list = new List<Chunk>();
		for (int num = 0; num < ui.Count; num++)
		{
			List<Chunk> allChunks = ui[num].GetAllChunks();
			list.AddRange(allChunks);
		}
		list.AddRange(ua);
		return list;
	}

	public override List<Chunk> GetChunks()
	{
		return ua;
	}

	public override List<RenderHeads.Media.AVProVideo.Stream> GetAllStreams()
	{
		List<RenderHeads.Media.AVProVideo.Stream> list = new List<RenderHeads.Media.AVProVideo.Stream>();
		for (int num = 0; num < ui.Count; num++)
		{
			List<RenderHeads.Media.AVProVideo.Stream> allStreams = ui[num].GetAllStreams();
			list.AddRange(allStreams);
		}
		list.AddRange(ui);
		return list;
	}

	public override List<RenderHeads.Media.AVProVideo.Stream> GetStreams()
	{
		return ui;
	}

	private bool bhw(string a, ref int b, ref int c, ref int d)
	{
		if (a.StartsWith("#EXT-X-STREAM-INF"))
		{
			int num = a.IndexOf("BANDWIDTH=");
			if (num >= 0)
			{
				int num2 = a.IndexOf(',', num + "BANDWIDTH=".Length);
				if (num2 < 0)
				{
					num2 = a.Length;
				}
				if (num2 >= 0 && num2 - "BANDWIDTH=".Length > num)
				{
					int length = num2 - num - "BANDWIDTH=".Length;
					if (!int.TryParse(a.Substring(num + "BANDWIDTH=".Length, length), out d))
					{
						d = 0;
					}
				}
			}
			else
			{
				d = 0;
			}
			int num3 = a.IndexOf("RESOLUTION=");
			if (num3 >= 0)
			{
				int num4 = a.IndexOf(',', num3 + "RESOLUTION=".Length);
				if (num4 < 0)
				{
					num4 = a.Length;
				}
				if (num4 >= 0 && num4 - "RESOLUTION=".Length > num3)
				{
					int length2 = num4 - num3 - "RESOLUTION=".Length;
					string text = a.Substring(num3 + "RESOLUTION=".Length, length2);
					int num5 = text.IndexOf('x');
					if (num5 < 0 || !int.TryParse(text.Substring(0, num5), out b) || !int.TryParse(text.Substring(num5 + 1, text.Length - (num5 + 1)), out c))
					{
						b = (c = 0);
					}
				}
			}
			else
			{
				b = (c = 0);
			}
			return true;
		}
		return false;
	}

	private static bool bhx(string a)
	{
		return a.StartsWith("#EXTINF");
	}

	private void bhy(string[] a, string b)
	{
		bool flag = false;
		bool flag2 = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		Chunk item = default(Chunk);
		for (int num4 = 0; num4 < a.Length; num4++)
		{
			if (bhw(a[num4], ref num, ref num2, ref num3))
			{
				flag2 = true;
				flag = false;
			}
			else if (bhx(a[num4]))
			{
				flag = true;
				flag2 = false;
			}
			else if (flag)
			{
				item.name = b + a[num4];
				ua.Add(item);
				flag = false;
				flag2 = false;
			}
			else if (flag2)
			{
				try
				{
					cq item2 = new cq((a[num4].IndexOf("://") < 0) ? (b + a[num4]) : a[num4], num, num2, num3);
					ui.Add(item2);
				}
				catch (Exception ex)
				{
					Debug.LogError("[AVProVideo]HLSParser cannot parse stream " + b + a[num4] + ", " + ex.Message);
				}
				flag = false;
				flag2 = false;
			}
			else
			{
				flag = false;
				flag2 = false;
			}
		}
	}

	public cq(string a, int b = 0, int c = 0, int d = 0)
	{
		ui = new List<RenderHeads.Media.AVProVideo.Stream>();
		ua = new List<Chunk>();
		uk = b;
		ul = c;
		um = d;
		uj = a;
		try
		{
			string[] array = null;
			if (a.ToLower().StartsWith("http://") || a.ToLower().StartsWith("https://"))
			{
				if (a.ToLower().StartsWith("https://"))
				{
					ServicePointManager.ServerCertificateValidationCallback = bhz;
				}
				using (WebClient webClient = new WebClient())
				{
					array = webClient.DownloadString(a).Split('\n');
				}
			}
			else
			{
				array = File.ReadAllLines(a);
			}
			int num = a.LastIndexOf('/');
			if (num < 0)
			{
				num = a.LastIndexOf('\\');
			}
			string text = uj.Substring(0, num + 1);
			bhy(array, text);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private bool bhz(object a, X509Certificate b, X509Chain c, SslPolicyErrors d)
	{
		bool result = true;
		if (d != SslPolicyErrors.None)
		{
			for (int num = 0; num < c.ChainStatus.Length; num++)
			{
				if (c.ChainStatus[num].Status != X509ChainStatusFlags.RevocationStatusUnknown)
				{
					c.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
					c.ChainPolicy.RevocationMode = X509RevocationMode.Online;
					c.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
					c.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
					if (!c.Build((X509Certificate2)b))
					{
						result = false;
						break;
					}
				}
			}
		}
		return result;
	}
}
