using System;

[Serializable]
public class ApkItem
{
	public bool IsInstall;

	public string ApkName { get; private set; }

	public string bl { get; private set; }

	public string BackgeName { get; private set; }

	public string ClassName { get; private set; }

	public ApkItem(string a, string b, string c, string d)
	{
		IsInstall = false;
		ApkName = a;
		bl = b;
		BackgeName = c;
		ClassName = d;
	}
}
