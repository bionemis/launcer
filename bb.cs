using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class bb
{
	private Dictionary<string, string> dj;

	public ba bo { get; private set; }

	public void hz()
	{
		ia();
	}

	public void ia()
	{
		bo = (ba)GameEntry.u.mz(bt.LanguageType);
		string text = Application.dataPath + "/StreamingAssets/Language/" + bo.ToString() + ".xml";
		dj = new Dictionary<string, string>();
		if (!ic(text))
		{
			string text2 = Application.dataPath + "/StreamingAssets/Language/Chinese.xml";
			if (!ic(text2))
			{
				k.ca("没有发现语言包");
			}
		}
		GameEntry.r.gy(1003);
	}

	public string ib(string a)
	{
		if (dj == null)
		{
			return a;
		}
		if (dj.ContainsKey(a))
		{
			return dj[a];
		}
		return a;
	}

	private bool ic(string a)
	{
		dj.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.IgnoreComments = true;
			XmlReader reader = XmlReader.Create(a, xmlReaderSettings);
			xmlDocument.Load(reader);
		}
		catch (Exception ex)
		{
			k.ca("XmlLoad:" + ex.Message);
			return false;
		}
		XmlNodeList childNodes;
		try
		{
			childNodes = xmlDocument.SelectSingleNode("Content").ChildNodes;
		}
		catch (Exception ex2)
		{
			k.ca("XmlReader:" + ex2.Message);
			return false;
		}
		try
		{
			for (int num = 0; num < childNodes.Count; num++)
			{
				dj.Add(childNodes[num].Name, childNodes[num].InnerText);
			}
		}
		catch (Exception ex3)
		{
			k.ca("Lan:" + ex3.Message);
			return false;
		}
		return true;
	}

	public void Close()
	{
		try
		{
			if (dj != null)
			{
				dj.Clear();
				dj = null;
			}
		}
		catch (Exception ex)
		{
			k.ca("Lan:" + ex.Message);
		}
	}
}
