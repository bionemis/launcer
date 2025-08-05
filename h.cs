using System.IO;
using UnityEngine;

public class h
{
	public static string c
	{
		get
		{
			string text = "D:\\QuickFlashingFile\\";
			if (Directory.Exists(text))
			{
				return text;
			}
			return "D:\\";
		}
	}

	public static string d => Path.Combine(c, "File\\");

	public static string e => Path.Combine(c, "Host\\");

	public static string f => Path.Combine(c, "Logo\\");

	public static string g => u("1");

	public static string h => u("name0");

	public static string i => u("qr");

	public static string j => Path.Combine(f, "1.mp4");

	public static string k => Path.Combine(c, "Setting.xml");

	public static string l => Path.Combine(Application.streamingAssetsPath, "AdbLib\\adb.exe");

	public static string m => Path.Combine(Application.streamingAssetsPath, "AdbLib\\scrcpy.exe");

	private static string u(string a)
	{
		string text = Path.Combine(f, a + ".jpg");
		if (File.Exists(text))
		{
			return text;
		}
		return Path.Combine(f, a + ".png");
	}
}
