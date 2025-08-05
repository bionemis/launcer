using System;
using UnityEngine;

[Serializable]
public class XmlContentInfo
{
	public string SqlName;

	public string CNName;

	public string ENName;

	public ApkItem ApkItem;

	public int ContentTime;

	public Texture2D TexIcon;

	public string ContentName
	{
		get
		{
			switch (GameEntry.s.bo)
			{
			case ba.Chinese:
				return CNName;
			case ba.English:
			case ba.Other:
				return ENName;
			default:
				return CNName;
			}
		}
	}
}
