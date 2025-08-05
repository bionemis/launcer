using System;
using System.Xml;
using UnityEngine;

public class cf
{
	private string gp;

	private XmlDocument dk;

	private XmlNodeList dl;

	public string ContentName
	{
		get
		{
			switch (GameEntry.s.bo)
			{
			case ba.Chinese:
				return CNName;
			default:
				return ENName;
			}
		}
	}

	public string CNName { get; private set; }

	public string ENName { get; private set; }

	public string ApkName { get; private set; }

	public string BackgeName { get; private set; }

	public string ClassName { get; private set; }

	public int ContentTime { get; private set; }

	public cf(string a)
	{
		gp = a;
	}

	public bool og()
	{
		dk = new XmlDocument();
		try
		{
			dk.Load(gp);
		}
		catch (Exception ex)
		{
			Debug.LogError("ContentXml:" + ex.Message);
			return false;
		}
		try
		{
			dl = dk.SelectSingleNode("Content").ChildNodes;
			oh();
		}
		catch (Exception ex2)
		{
			k.ca("ContentXml:" + ex2.Message);
			return false;
		}
		return true;
	}

	private void oh()
	{
		for (int num = 0; num < dl.Count; num++)
		{
			switch (dl[num].Name)
			{
			case "Name":
				_ = string.Empty;
				foreach (XmlElement childNode in dl[num].ChildNodes)
				{
					string name = childNode.Name;
					if (!(name == "EN"))
					{
						if (name == "CN")
						{
							CNName = childNode.InnerText;
						}
					}
					else
					{
						ENName = childNode.InnerText;
					}
				}
				break;
			case "ApkName":
				ApkName = dl[num].InnerText;
				break;
			case "BackgeName":
				BackgeName = dl[num].InnerText;
				break;
			case "ClassName":
				ClassName = dl[num].InnerText;
				break;
			case "ContentTime":
				ContentTime = Convert.ToInt32(dl[num].InnerText);
				break;
			}
		}
	}

	public void Close()
	{
		dk.Clone();
	}
}
