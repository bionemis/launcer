using System.Collections.Generic;
using UnityEngine;

public class ADBTest : MonoBehaviour
{
	private r a;

	public Dictionary<string, l> deMobaDir;

	private void Start()
	{
		a = new r();
		a.fo();
		deMobaDir = a.cc;
	}

	private void Update()
	{
		a?.Update();
	}

	private void OnApplicationQuit()
	{
		a.Close();
	}
}
