using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BatTest : MonoBehaviour
{
	[SerializeField]
	private InputField uIptAccount;

	[SerializeField]
	private InputField uIptPassword;

	private string codingPath = Path.Combine(Application.streamingAssetsPath, "Data/Coding.dat");

	private void Start()
	{
		if (!File.Exists(codingPath))
		{
			File.Create(codingPath).Dispose();
		}
	}

	private void Update()
	{
	}

	public void OnBtnLoadClick()
	{
		try
		{
			new Hashtable();
			Hashtable obj = b.m(codingPath) as Hashtable;
			string text = obj["ZH"].ToString();
			string text2 = obj["MM"].ToString();
			Debug.Log("ZH:" + text + "  MM:" + text2);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
	}

	public void OnBtnSaveClick()
	{
		b.l(new Hashtable
		{
			{ "ZH", uIptAccount.text },
			{ "MM", uIptPassword.text }
		}, codingPath);
	}
}
