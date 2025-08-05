using System;
using d;

public class bn
{
	public string SqlName;

	public int PlayCount;

	public ContentPattern ContentPattern;

	public ContentType ContentType;

	public int StarNum;

	public bool IsShow;

	public DateTime CreateTime;

	public bn(string a, int b, ContentPattern c, ContentType d, int e, bool f, DateTime g)
	{
		SqlName = a;
		PlayCount = b;
		ContentPattern = c;
		ContentType = d;
		StarNum = e;
		IsShow = f;
		CreateTime = g;
	}
}
