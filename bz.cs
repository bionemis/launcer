using System;
using System.Collections.Generic;
using d;

public class bz
{
	public br fy;

	public bs fz;

	public void mo()
	{
		fy = new br();
		fy.lk();
		fz = new bs();
		fz.mn();
	}

	public List<string> mp(bp a)
	{
		return fy.me(a);
	}

	public List<bn> mq(bp a)
	{
		return fy.lx(a);
	}

	public bool mr(string a, ContentPattern b, ContentType c, int d, bool e)
	{
		return fy.lz(a, b, c, d, e);
	}

	public bool ms(string a)
	{
		return fy.ma(a);
	}

	public bool mt(string a, ContentPattern b, ContentType c, int d, bool e)
	{
		return fy.mc(a, b, c, d, e);
	}

	public bool mu(string a, int b)
	{
		return fy.md(a, b);
	}

	public void mv(bool a, string b)
	{
		fy.lq(a, b);
	}

	internal List<string> mw()
	{
		return fy.lp();
	}

	public string mx(bt a)
	{
		return fy.mk(a);
	}

	public bool my(bt a)
	{
		return bool.Parse(fy.mk(a));
	}

	public int mz(bt a)
	{
		return int.Parse(fy.mk(a));
	}

	public void na(bt a, string b)
	{
		fy.ml(a, b);
	}

	public List<bo> nb(string a, string b)
	{
		return fy.ls(a, b);
	}

	public void nc(XmlContentInfo a, DateTime b, int c)
	{
		bo bo2 = new bo();
		bo2.SqlName = a.SqlName;
		bo2.CNName = a.CNName;
		bo2.ENName = a.ENName;
		bo2.PlayTime = (int)(DateTime.Now - b).TotalSeconds;
		bo2.PlayerCount = 1;
		bo2.Income = c;
		bo2.CreateTime = b;
		fy.lu(bo2);
	}

	public bool nd()
	{
		return fy.mi();
	}

	public void Close()
	{
		try
		{
			fy.Close();
			fz = null;
		}
		catch (Exception ex)
		{
			k.ca("SQL:" + ex.Message);
		}
	}
}
