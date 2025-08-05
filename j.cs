using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class j
{
	public static bool bg(this byte a, byte b)
	{
		return (a & b) == b;
	}

	public static string bh(this string a)
	{
		return a + ":5555";
	}

	public static string bi(this string a)
	{
		if (a.Contains(":5555"))
		{
			return a.Replace(":5555", "");
		}
		return a;
	}

	public static string bj(this string a)
	{
		return a.Split(':')[1];
	}

	public static string bk(this string a)
	{
		byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(a);
		string text = string.Empty;
		for (int num = 0; num < bytes.Length; num++)
		{
			text += Convert.ToString(bytes[num], 16).ToUpper();
		}
		return text;
	}

	public static string bl(this string a)
	{
		byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(a);
		string text = string.Empty;
		for (int num = 0; num < 8; num++)
		{
			text += Convert.ToString(bytes[num], 16);
		}
		return text;
	}

	public static string bm(string a)
	{
		List<byte> list = new List<byte>();
		while (a != "" && a != " ")
		{
			if (a.Substring(0, 1) == " ")
			{
				a = a.Substring(1, a.Length - 1);
			}
			int num = a.IndexOf(" ");
			list.Add(Convert.ToByte(a.Substring(0, num), 16));
			int length = a.Length;
			a = a.Substring(num, length - num);
		}
		byte[] bytes = list.ToArray();
		return Encoding.Default.GetString(bytes);
	}

	public static string bn(this string a)
	{
		string text = "";
		a = a.Replace(" ", "");
		int num = a.Length / 2;
		for (int num2 = 0; num2 < num; num2++)
		{
			text += int.Parse(a[num2 * 2].ToString() + a[num2 + 2]).ToString("X2");
		}
		return text;
	}

	public static byte[] bo(this string a)
	{
		a = a.Replace(" ", "");
		if (a.Length % 2 != 0)
		{
			a += " ";
		}
		byte[] array = new byte[a.Length / 2];
		for (int num = 0; num < array.Length; num++)
		{
			array[num] = Convert.ToByte(a.Substring(num * 2, 2), 16);
		}
		return array;
	}

	public static byte[] bp(this string a)
	{
		try
		{
			string[] array = a.Trim().ToUpper().Replace(',', ' ')
				.Replace('，', ' ')
				.Split(' ');
			int num = array.Length;
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				if (array[num2] == "")
				{
					num--;
				}
			}
			byte[] array2 = new byte[num];
			int num3 = 0;
			for (int num4 = 0; num4 < array.Length; num4++)
			{
				int num5 = 0;
				if (!(array[num4] == ""))
				{
					num5 = Convert.ToInt32(array[num4], 16);
					try
					{
						array2[num3] = Convert.ToByte(num5);
					}
					catch (Exception ex)
					{
						Debug.Log("超出字节数 " + ex.ToString());
						return null;
					}
					num3++;
				}
			}
			return array2;
		}
		catch (Exception ex2)
		{
			Debug.LogError("StringToByte:" + ex2.Message);
			return null;
		}
	}

	public static string bq(this byte[] a)
	{
		if (a == null || a.Length == 0)
		{
			return null;
		}
		string text = a[0].ToString("X2");
		for (int num = 1; num < a.Length; num++)
		{
			text = text + " " + a[num].ToString("X2");
		}
		return text;
	}

	public static string br(this string a)
	{
		StringBuilder stringBuilder = new StringBuilder();
		a = a.Replace(" ", "");
		if (a.Length % 2 != 0)
		{
			return null;
		}
		for (int num = 0; num < a.Length; num++)
		{
			stringBuilder.Append(a[num]);
			if ((num + 1) % 2 == 0 && num != a.Length - 1)
			{
				stringBuilder.Append(" ");
			}
		}
		return stringBuilder.ToString();
	}

	public static string bs(this string a, ushort b = 8)
	{
		StringBuilder stringBuilder = new StringBuilder();
		a = a.Replace(" ", "");
		if (a.Length % b != 0)
		{
			return null;
		}
		for (int num = 0; num < a.Length; num++)
		{
			stringBuilder.Append(a[num]);
			if ((num + 1) % b == 0 && num != a.Length - 1)
			{
				stringBuilder.Append("-");
			}
		}
		return stringBuilder.ToString();
	}

	private static string bt(string a)
	{
		return (a.Replace(" ", "").Length / 2 + 1).ToString("X2");
	}

	public static int bu()
	{
		return (DateTime.Now - new DateTime(1900, 1, 1)).Days + 2;
	}

	public static string bv(int a)
	{
		double value = a - 2;
		return new DateTime(1900, 1, 1, 0, 0, 0, 0).AddDays(value).ToLocalTime().ToString("yyyy-MM-dd");
	}

	public static string bw()
	{
		return bu().ToString("X2");
	}

	public static string[] bx()
	{
		return bu().ToString("X2").br().Split(' ');
	}
}
