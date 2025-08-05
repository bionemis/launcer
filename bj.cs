using System;
using System.Collections.Generic;
using UnityEngine;

public class bj
{
	public struct a
	{
		public string eu;

		public string ev;
	}

	private bi ew;

	private string ex = "80080000";

	private string ey = "4DFCF725";

	public List<string> fc = new List<string>();

	private const string fh = "D6 A3 3B 96 3E 10 AD 6A";

	private const string fi = "A6 19 5C 7E D5 B8 36 57";

	public string bs { get; private set; }

	public string bt { get; private set; }

	public a bu { get; private set; }

	public int bv { get; private set; }

	public int bw { get; private set; }

	public bool bx { get; private set; }

	public bool by { get; set; }

	public void ka()
	{
		ew = new bi();
	}

	public void Start(bool a = false)
	{
		if (ew.jo() != 1)
		{
			kb();
			kc();
			bu = default(a);
			bu = kf();
			ko(a: true, a);
		}
		else
		{
			k.ca("nessun dongle rilevato");
			global::a.k();
		}
	}

	private void kb()
	{
		byte[] sourceArray = new byte[256];
		int num = 0;
		if (ew.jo() == 1)
		{
			num = ew.jt(sourceArray);
			if (num != 0)
			{
				byte[] destinationArray = new byte[num];
				Array.Copy(sourceArray, 0, destinationArray, 0, num);
				ew.jq(destinationArray);
			}
		}
	}

	public void kc()
	{
		if (kd() == null)
		{
			k.ca("获取密钥失败");
			global::a.k();
			return;
		}
		if (string.IsNullOrEmpty(GameEntry.t.License))
		{
			k.ca("许可证不存在或者不正确");
			global::a.k();
			return;
		}
		string text = "";
		if (ke(GameEntry.t.License, out text) == null || text == null)
		{
			k.ca("校验许可失败");
			global::a.k();
		}
		else
		{
			kz();
			lb();
		}
	}

	private byte[] kd()
	{
		byte[] array = lg(ex + " 04 11 00 00 00");
		bs = lh(17, array);
		bt = array[array.Length - 4].ToString("X2");
		return array;
	}

	private byte[] ke(string a, out string b)
	{
		string text = ex + " 0A 13 " + a + " 00";
		byte[] array = lg(text);
		if (array == null)
		{
			k.ca("证许可错误！");
			global::a.k();
		}
		b = lh(19, array);
		return array;
	}

	public a kf()
	{
		byte[] array = lg(ex + " 04 06 00 00 00");
		if (array == null)
		{
			k.ca("获取机器编码错误");
			global::a.k();
		}
		return li(6, array);
	}

	public void ko(bool a, bool b = false)
	{
		string[] array = j.bx();
		string text = array[0] + " 00 " + array[1] + " 00 00 00 00 " + bt;
		text = bh.jl(text, bs);
		string text2 = ex + " 0A 17 " + text + " 00";
		byte[] array2 = lg(text2);
		byte[] array3 = bh.jm(lh(23, array2), bs).bo();
		byte b2 = array3[7];
		switch (b2)
		{
		case 81:
			k.ca("任意授权数据错误");
			break;
		case 82:
			k.ca("周年授权数据错误");
			break;
		case 83:
			k.ca("永久授权数据错误");
			break;
		case 84:
			k.ca("授权模式错误");
			break;
		case 85:
		{
			s s2 = GameEntry.z.ku();
			k.ca("日期倒走或系统被锁定时，拒绝请求，建议系统时间修改为：" + j.bv(s2.cg));
			break;
		}
		}
		if (b2 >= 81 && b2 < 85)
		{
			by = true;
			global::a.k();
			return;
		}
		if (b2 == 85)
		{
			by = true;
			if (a)
			{
				MainControl.TintPanel.vp(GameEntry.s.ib("TipsTitle"), GameEntry.s.ib("DateTooSmall"), ks);
			}
			return;
		}
		byte b3 = array3[4];
		bw = array3[0] * 256 + array3[1];
		bv = array3[2] * 256 + array3[3];
		int num = bv - GameEntry.ba.bn.PlayTimes;
		switch (b3)
		{
		case 1:
		case 2:
			Debug.Log("任意/周年授权");
			bx = false;
			if (a)
			{
				kp(num);
			}
			break;
		case 3:
			bx = true;
			Debug.Log("永久授权");
			break;
		default:
			k.ca("授权类型错误");
			global::a.k();
			return;
		}
		if (!b)
		{
			kq(a);
		}
	}

	private void kp(int a)
	{
		if (a <= 10 || bw < 3)
		{
			Debug.Log("即将到期");
			string text = GameEntry.s.ib("TipsAboutToExpire");
			string text2 = string.Format("{0}:{1}\n{2}:{3}", GameEntry.s.ib("TipsRemainingDays"), bw, GameEntry.s.ib("TipsRemainingTimes"), a);
			if (a <= 0 || bw <= 0)
			{
				text = GameEntry.s.ib("TipsContactAdministrator");
				MainControl.TintPanel.vp(text, text2, ks);
			}
			else
			{
				text2 = text2 + "\n" + GameEntry.s.ib("TipsIsToCoding");
				MainControl.TintPanel.vp(text, text2, ks, d: true);
			}
		}
	}

	private void kq(bool a)
	{
		s s2 = GameEntry.z.ku();
		s s3 = GameEntry.ba.bn;
		if (s3.cf > s2.cf)
		{
			s2.cf = s3.cf;
		}
		else
		{
			s3.cf = s2.cf;
		}
		if (s3.PlayTimes > s2.PlayTimes)
		{
			s2.PlayTimes = s3.PlayTimes;
		}
		else
		{
			s3.PlayTimes = s2.PlayTimes;
		}
		if (s3.cg > s2.cg)
		{
			s2.cg = s3.cg;
		}
		else
		{
			s3.cg = s2.cg;
		}
		int num = j.bu();
		if (num >= s2.cg)
		{
			s2.cg = (s3.cg = num);
			kr(s2);
			return;
		}
		k.ca("手动修改了日期");
		by = true;
		if (a)
		{
			MainControl.TintPanel.vp(GameEntry.s.ib("TipsTitle"), GameEntry.s.ib("DateTooSmall"), ks);
		}
	}

	public void kr(s a)
	{
		if (!by)
		{
			kt(a);
			GameEntry.ba.gb();
		}
	}

	private void ks()
	{
		MainControl.a.dj<SettingPanel>("SettingPanel").ub();
	}

	public void kt(s a)
	{
		string text = a.cf.ToString("X8") + a.PlayTimes.ToString("X4") + a.cg.ToString("X4");
		text = bh.jl(text, GameEntry.z.bs);
		string text2 = ex + " 11 09 " + ey + " 0C00 " + text + " " + bt + " 00";
		byte[] array = lg(text2);
		bool flag;
		if (array[0] == 9)
		{
			ew.jq(array);
			flag = array[1] == 102;
		}
		else
		{
			flag = false;
		}
		if (!flag)
		{
			k.ca("写入数据失败");
			global::a.k();
		}
	}

	public s ku()
	{
		string text = ex + " 0A 14 " + ey + " 0C00 08 " + bt + " 00";
		byte[] array = lg(text);
		string value = lh(20, array);
		if (string.IsNullOrEmpty(value))
		{
			global::a.k();
			return null;
		}
		value = bh.jm(value, bs);
		array = value.bo();
		s s2 = new s();
		s2.cf = kv(array[0], array[1], array[2], array[3]);
		if (s2.cf >= 65535)
		{
			s2.cf = 0;
		}
		s2.PlayTimes = kv(array[4], array[5]);
		if (s2.PlayTimes >= 65535)
		{
			s2.PlayTimes = 0;
		}
		s2.cg = kv(array[6], array[7]);
		if (s2.cg >= 65535)
		{
			s2.cg = j.bu();
		}
		return s2;
	}

	private int kv(params byte[] bt)
	{
		int num = 0;
		int num2 = bt.Length;
		for (int num3 = 0; num3 < num2; num3++)
		{
			int num4 = (int)Math.Pow(256.0, num2 - num3 - 1);
			num += bt[num3] * num4;
		}
		return num;
	}

	public string kw(string a)
	{
		byte[] array = GameEntry.t.License.bo();
		byte[] array2 = "D6 A3 3B 96 3E 10 AD 6A".bo();
		int num = array.Length;
		byte[] array3 = new byte[num];
		for (int num2 = 0; num2 < num; num2++)
		{
			array3[num2] = (byte)(array[num2] ^ array2[num2]);
		}
		string text = GameEntry.ba.gd();
		Debug.Log("明文:" + GameEntry.t.License + "|常量:" + "D6 A3 3B 96 3E 10 AD 6A".Replace(" ", "") + "|异或之后:" + ew.jq(array3));
		string text2 = a + ew.jq(array3);
		string text3 = bh.jl(text, text2);
		Debug.Log("账目明文:" + text + "|8字节请求码+8字节结果:" + text2 + "|结果:" + text3);
		return text3;
	}

	public bool kx(string a, string b)
	{
		string text = b + "A6 19 5C 7E D5 B8 36 57".Replace(" ", "");
		string text2 = bh.jm(a, text);
		string text3 = GameEntry.ba.gd();
		Debug.Log("accDesKey:" + a + "|desKey:" + text + "|加密狗:" + text2 + "|本地:" + text3);
		string text4 = string.Empty;
		for (int num = 2; num < text3.Length - 2; num++)
		{
			text4 += text3[num];
		}
		string text5 = string.Empty;
		for (int num2 = 2; num2 < text2.Length - 2; num2++)
		{
			text5 += text2[num2];
		}
		if (!text4.Equals(text5))
		{
			k.ca("激活码和明文对比错误");
			return false;
		}
		return true;
	}

	[Obsolete("弃用,请使用=》VerifyAuthorCodeExit()")]
	public int ky(string a, string b)
	{
		string text = b + "A6 19 5C 7E D5 B8 36 57".Replace(" ", "");
		string text2 = bh.jm(a, text);
		string text3 = GameEntry.ba.gd();
		Debug.Log("accDesKey:" + a + "|desKey:" + text + "|加密狗:" + text2 + "|本地:" + text3);
		if (!text3.Equals(text2))
		{
			k.ca("激活码和明文对比错误");
			return -1;
		}
		byte[] array = text3.bo();
		int num = array[0] * 256 + array[7];
		byte[] array2 = text2.bo();
		int num2 = array2[0] * 256 + array2[7];
		Debug.Log("lcoal:" + num + " | dog:" + num2);
		if (num != num2)
		{
			return num2;
		}
		return num;
	}

	public int kz()
	{
		byte[] array = lg(ex + " 0A 14 " + ey.br() + " 00 20 08 " + bt + " 00");
		return bh.jn(lh(20, array), bs)[0];
	}

	private List<int> la()
	{
		byte[] array = lg(ex + " 0A 14 " + ey.br() + " 00 30 20 " + bt + " 00");
		if (string.IsNullOrEmpty(lh(20, array)))
		{
			return null;
		}
		byte[] array2 = bh.jn(lh(20, array), bs);
		List<int> list = new List<int>();
		for (int num = 0; num < array2.Length; num++)
		{
			if (array2[num] != byte.MaxValue)
			{
				list.Add(array2[num]);
			}
		}
		return list;
	}

	private void lb()
	{
		fc.Clear();
		List<int> list = la();
		for (int num = 0; num < list.Count; num++)
		{
			int num2 = Convert.ToInt32(256) + num * 32;
			int num3 = num2 % 256;
			num2 = (num2 - num3) / 256;
			byte[] array = lg($"{ex} 0A 14 {ey.br()} {num2:X2} {num3:X2} 20 {bt} 00");
			string value = lh(20, array);
			if (!string.IsNullOrEmpty(value))
			{
				string item = bh.jm(value, bs);
				fc.Add(item);
			}
		}
	}

	public string lc(string a)
	{
		byte[] array = lg(ex + " 0A 16 " + a + " 00");
		Debug.Log(ex + " 0A 16 " + a + " 00");
		Debug.Log("激活机器码：" + ew.jq(array));
		return lh(22, array);
	}

	public string ld()
	{
		string text = j.bu().ToString("X2").br();
		byte[] array = lg(ex + " 06 15 " + text + " 00 00 00");
		return lh(21, array);
	}

	public byte[] le(string a, out string b, out string c)
	{
		string text = ex + " 24 18 " + a + " 00";
		byte[] array = lg(text);
		b = array[1].ToString("X2");
		Debug.LogError("添加内容长度：" + b);
		c = array[2].ToString("X2");
		return array;
	}

	private string lf(string a)
	{
		return ew.jq(lg(a));
	}

	private byte[] lg(string a)
	{
		if (string.IsNullOrEmpty(a))
		{
			return null;
		}
		string text = a.Replace(" ", "");
		for (int num = 0; num < a.Length; num++)
		{
			if (a[num] != ' ')
			{
				text += a[num];
			}
		}
		byte[] sourceArray = new byte[256];
		int length = ew.jp(text, sourceArray);
		byte[] destinationArray = new byte[256];
		Array.Copy(sourceArray, 0, destinationArray, 0, length);
		int[] array = new int[2];
		byte[] sourceArray2 = new byte[256];
		if (ew.js(length, destinationArray, sourceArray2, array) == 0)
		{
			byte[] array2 = new byte[array[0] + array[1]];
			Array.Copy(sourceArray2, 0, array2, 0, array[0] + array[1]);
			return array2;
		}
		return null;
	}

	private string lh(byte a, byte[] b)
	{
		if (b[0] == a && b[b.Length - 2] == 144 && b[b.Length - 1] == 0)
		{
			byte[] array = new byte[b.Length - 4];
			int num = 0;
			for (int num2 = 1; num2 < b.Length - 3; num2++)
			{
				array[num] = b[num2];
				num++;
			}
			return ew.jq(array);
		}
		Debug.LogError($"包头=》{a} 校验失败");
		return null;
	}

	private a li(byte a, byte[] b)
	{
		a result = default(a);
		if (b[0] == a && b[b.Length - 2] == 144 && b[b.Length - 1] == 0)
		{
			byte[] array = new byte[b.Length - 4];
			int num = 0;
			for (int num2 = 1; num2 < b.Length - 3; num2++)
			{
				array[num] = b[num2];
				result.eu += b[num2].ToString("00");
				num++;
			}
			result.ev = ew.jq(array);
			return result;
		}
		Debug.LogError($"包头=》{a} 校验失败");
		return result;
	}

	public void Close()
	{
		ew?.jr();
		ex = string.Empty;
	}
}
