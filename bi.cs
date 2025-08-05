using System;
using System.Runtime.InteropServices;

public class bi
{
	private static int et;

	[DllImport("LKT5103AHID_H_64.dll")]
	private static extern int EK_Open(int a);

	[DllImport("LKT5103AHID_H_64.dll")]
	private static extern int EK_Close();

	[DllImport("LKT5103AHID_H_64.dll")]
	private static extern int EK_Reset(out int a, IntPtr b);

	[DllImport("LKT5103AHID_H_64.dll")]
	private static extern int EK_Exchange_APDU(int a, IntPtr b, IntPtr c, IntPtr d);

	[DllImport("LKT5103AHID_H_64.dll")]
	private static extern void LK_DesSetKey(byte a, byte[] b);

	[DllImport("LKT5103AHID_H_64.dll")]
	private static extern int LK_DesEncode(string a, string b);

	public int jo()
	{
		return EK_Open(1);
	}

	public int jp(string a, byte[] b)
	{
		int length = a.Length;
		if (length % 2 != 0 || length < 10)
		{
			return 0;
		}
		length >>= 1;
		for (int num = 0; num < length; num++)
		{
			string value = a.Substring(2 * num, 1);
			string value2 = a.Substring(2 * num + 1, 1);
			int num2 = 16;
			int num3 = 16;
			char c2 = Convert.ToChar(value);
			char c3 = Convert.ToChar(value2);
			if (c2 <= '9' && c2 >= '0')
			{
				num2 = c2 - 48;
			}
			else
			{
				if (c2 > 'F' || c2 < 'A')
				{
					return 0;
				}
				num2 = c2 - 65 + 10;
			}
			if (c3 <= '9' && c3 >= '0')
			{
				num3 = c3 - 48;
			}
			else
			{
				if (c3 > 'F' || c3 < 'A')
				{
					return 0;
				}
				num3 = c3 - 65 + 10;
			}
			b[num] = Convert.ToByte((num2 << 4) + num3);
		}
		return length;
	}

	public string jq(byte[] a)
	{
		return BitConverter.ToString(a).Replace("-", "");
	}

	public bool jr()
	{
		if (EK_Close() != 0)
		{
			return true;
		}
		return false;
	}

	public int js(int a, byte[] b, byte[] c, int[] d)
	{
		int num = EK_Exchange_APDU(a, Marshal.UnsafeAddrOfPinnedArrayElement(b, 0), Marshal.UnsafeAddrOfPinnedArrayElement(d, 0), Marshal.UnsafeAddrOfPinnedArrayElement(c, 0));
		if (num != 0)
		{
			return num;
		}
		return 0;
	}

	public int jt(byte[] a)
	{
		int num = EK_Reset(out et, Marshal.UnsafeAddrOfPinnedArrayElement(a, 0));
		if (num != 0)
		{
			return num;
		}
		return et;
	}
}
