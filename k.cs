using System;
using System.IO;
using UnityEngine;

public class k : IDisposable
{
	private static readonly object v = new object();

	private static FileStream w = null;

	private static bool x;

	private static string q => h.c + "播放器日志.txt";

	private static void bz()
	{
		try
		{
			if (new FileInfo(q).Length > 10240 && w == null)
			{
				w = new FileStream(q, FileMode.Create);
				w.Close();
				w.Dispose();
				w = null;
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Read\u00a0Log\u00a0Size" + ex.Message);
		}
	}

	public static void ca(string a)
	{
		lock (v)
		{
			if (!x)
			{
				x = true;
				bz();
			}
			try
			{
				StreamWriter streamWriter = File.AppendText(q);
				Debug.Log("logPath: " + a);
				streamWriter.WriteLine(DateTime.Now.ToString() + ":\u00a0\u00a0\u00a0" + a);
				streamWriter.Close();
				streamWriter.Dispose();
			}
			catch (Exception ex)
			{
				Debug.Log("LogError:" + ex.Message);
			}
		}
	}

	public void Dispose()
	{
		try
		{
			w.Close();
			w.Dispose();
			w = null;
		}
		catch (Exception)
		{
		}
	}
}
