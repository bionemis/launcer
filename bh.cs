using System;
using System.Runtime.InteropServices;

public class bh
{
	[DllImport("des.dll")]
	private static extern void LK_DESEncrypt_ECB(IntPtr a, int b, IntPtr c, int d, IntPtr e, IntPtr f);

	[DllImport("des.dll")]
	private static extern void LK_DESDecrypt_ECB(IntPtr a, int b, IntPtr c, int d, IntPtr e, IntPtr f);

	public static int ji(string a, byte[] b)
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

	public static int jj(string a)
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
			char c2 = Convert.ToChar(value);
			char c3 = Convert.ToChar(value2);
			if ((c2 > '9' || c2 < '0') && (c2 > 'F' || c2 < 'A'))
			{
				return 0;
			}
			if ((c3 > '9' || c3 < '0') && (c3 > 'F' || c3 < 'A'))
			{
				return 0;
			}
		}
		return length;
	}

	public static string jk(byte[] a)
	{
		Console.WriteLine(BitConverter.ToString(a));
		string text = BitConverter.ToString(a).Replace("-", "");
		Console.WriteLine(text);
		return text;
	}

	public static string jl(string a, string b)
	{
		a = a.Replace(" ", "");
		byte[] arr = new byte[256];
		byte[] arr2 = new byte[256];
		byte[] array = new byte[1];
		byte[] arr3 = new byte[256];
		int num = 0;
		int num2 = 0;
		if (a == "")
		{
			return null;
		}
		if (b == "")
		{
			return null;
		}
		num = ji(a, arr2);
		num2 = ji(b, arr);
		LK_DESEncrypt_ECB(Marshal.UnsafeAddrOfPinnedArrayElement(arr2, 0), num, Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0), num2, Marshal.UnsafeAddrOfPinnedArrayElement(arr3, 0), Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		return jk(arr3).Substring(0, array[0] * 2);
	}

	public static string jm(string a, string b)
	{
		return jk(jn(a, b));
	}

	public static byte[] jn(string a, string b)
	{
		byte[] arr = new byte[256];
		byte[] arr2 = new byte[256];
		byte[] array = new byte[1];
		byte[] array2 = new byte[256];
		int num = 0;
		int num2 = 0;
		if (a == "")
		{
			return null;
		}
		if (b == "")
		{
			return null;
		}
		num = ji(a, arr2);
		num2 = ji(b, arr);
		LK_DESDecrypt_ECB(Marshal.UnsafeAddrOfPinnedArrayElement(arr2, 0), num, Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0), num2, Marshal.UnsafeAddrOfPinnedArrayElement(array2, 0), Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		byte[] array3 = new byte[array[0]];
		Array.ConstrainedCopy(array2, 0, array3, 0, array[0]);
		return array3;
	}
}
