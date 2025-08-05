using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class t
{
	private string ch;

	public int bm { get; private set; }

	public s bn { get; private set; }

	public void fz()
	{
		bn = new s();
		string text = Application.dataPath + "/../Data";
		ch = Path.Combine(text, "Coding.dat");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		if (!File.Exists(ch))
		{
			File.Create(ch).Dispose();
		}
		if (!GameEntry.ba.ga())
		{
			bn.cf = 0;
			bn.PlayTimes = 0;
			bn.cg = j.bu();
		}
	}

	private bool ga()
	{
		try
		{
			Hashtable hashtable = b.m(ch) as Hashtable;
			bn.cf = int.Parse(hashtable["HistoryPlayDays"].ToString());
			bn.PlayTimes = int.Parse(hashtable["PlayTimes"].ToString());
			bn.cg = int.Parse(hashtable["DayCoefficient"].ToString());
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			return false;
		}
	}

	public bool gb()
	{
		try
		{
			b.l(new Hashtable
			{
				{ "HistoryPlayDays", bn.cf },
				{ "PlayTimes", bn.PlayTimes },
				{ "DayCoefficient", bn.cg }
			}, ch);
			return true;
		}
		catch (Exception ex)
		{
			k.ca(ex.Message);
			a.k();
			return false;
		}
	}

	public bool gc()
	{
		try
		{
			if (GameEntry.z.by)
			{
				MainControl.TintPanel.vk(GameEntry.s.ib("SoftdogLock"));
				return false;
			}
			int num = GameEntry.z.bv - bn.PlayTimes;
			if (num <= 0 || GameEntry.z.bw <= 0)
			{
				string text = GameEntry.s.ib("TipsContactAdministrator");
				string text2 = string.Format("{0}:{1}\n", GameEntry.s.ib("TipsRemainingDays"), GameEntry.z.bw) + string.Format("{0}:{1}", GameEntry.s.ib("TipsRemainingTimes"), num);
				MainControl.TintPanel.vp(text, text2, delegate
				{
					MainControl.a.dj<SettingPanel>("SettingPanel").ub();
				});
				return false;
			}
			bn.PlayTimes++;
			bn.cf++;
			bm++;
			if (bm >= 5)
			{
				bm = 0;
				GameEntry.ba.bn.cg = j.bu();
				GameEntry.z.ko(a: true);
			}
			else
			{
				gb();
			}
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			return false;
		}
	}

	public string gd()
	{
		string[] array = j.bx();
		return array[0] + GameEntry.ba.bn.cf.ToString("X8") + GameEntry.ba.bn.PlayTimes.ToString("X4") + array[1];
	}
}
