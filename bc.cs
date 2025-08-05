using System;
using System.Collections.Generic;
using System.Xml;

public class bc
{
	private XmlDocument dk;

	private XmlNodeList dl;

	public int PlayCount { get; private set; }

	public string License { get; private set; }

	public bool IsDoubleScreen { get; private set; }

	public Dictionary<string, string> bp { get; private set; }

	public void @if()
	{
		ig();
		ih();
	}

	public void ig()
	{
		try
		{
			dk = new XmlDocument();
			dk.Load(h.k);
			dl = dk.SelectSingleNode("Content").ChildNodes;
		}
		catch (Exception ex)
		{
			k.ca("MainXml:" + ex.Message);
			a.k();
		}
	}

	public void ih()
	{
		try
		{
			bp = new Dictionary<string, string>();
			for (int num = 0; num < dl.Count; num++)
			{
				if (dl[num].Name.Equals("IP"))
				{
					XmlNodeList childNodes = dl[num].ChildNodes;
					for (int num2 = 0; num2 < childNodes.Count; num2++)
					{
						bp.Add(childNodes[num2].Name, childNodes[num2].InnerText);
					}
				}
				else if (dl[num].Name.Equals("PlayCount"))
				{
					PlayCount = int.Parse(dl[num].InnerText);
					if (PlayCount > 6)
					{
						PlayCount = 6;
					}
					if (PlayCount < 0)
					{
						PlayCount = 1;
					}
				}
				else if (dl[num].Name.Equals("License"))
				{
					License = dl[num].InnerText;
				}
				else if (dl[num].Name.Equals("IsDoubleScreen"))
				{
					IsDoubleScreen = bool.Parse(dl[num].InnerText);
				}
			}
		}
		catch (Exception ex)
		{
			k.ca("MainXml:" + ex.Message);
			a.k();
		}
	}

	public void Close()
	{
		try
		{
			dk = null;
		}
		catch (Exception ex)
		{
			k.ca("XML:" + ex.Message);
		}
	}
}
