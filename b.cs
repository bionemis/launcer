using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public class b
{
	private static byte[] k = new byte[16]
	{
		78, 56, 61, 94, 12, 88, 56, 63, 66, 10,
		102, 77, 1, 186, 97, 45
	};

	private static byte[] m_l = new byte[16]
	{
		36, 34, 42, 122, 242, 87, 2, 90, 59, 50,
		123, 63, 72, 171, 130, 61
	};

	private static IFormatter m_m = new BinaryFormatter();

	public static bool l(object a, string b)
	{
		using (Stream stream = new FileStream(b, FileMode.Create, FileAccess.Write, FileShare.None))
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(stream, rijndaelManaged.CreateEncryptor(k, global::b.m_l), CryptoStreamMode.Write);
			global::b.m_m.Serialize(cryptoStream, a);
			cryptoStream.Close();
			stream.Close();
			return true;
		}
	}

	public static object m(string a)
	{
		using (Stream stream = new FileStream(a, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(stream, rijndaelManaged.CreateDecryptor(k, b.m_l), CryptoStreamMode.Read);
			object result = b.m_m.Deserialize(cryptoStream);
			cryptoStream.Close();
			stream.Close();
			return result;
		}
	}
}
