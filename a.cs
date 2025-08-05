using System;
using System.Diagnostics;
using UnityEngine;

public class a
{
	public static string i = "Scenes/Little/Main";

	public static string j = "V2_1.0.7";

	public static Process j(string a)
	{
		Process[] processes = Process.GetProcesses();
		for (int num = 0; num < processes.Length; num++)
		{
			if (processes[num].ProcessName.Equals(a))
			{
				return processes[num];
			}
		}
		return null;
	}

	public static void k()
	{
		GameEntry.a?.Close();
		Application.Quit();
		Process.GetCurrentProcess().Kill();
		Environment.Exit(0);
	}
}
