using System;

public class bo
{
	public string SqlName;

	public string CNName;

	public string ENName;

	public int PlayTime;

	public int Income;

	public int PlayerCount;

	public DateTime CreateTime;

	public string ContentName
	{
		get
		{
			if (GameEntry.a != null)
			{
				if (GameEntry.s.bo != ba.Chinese)
				{
					return ENName;
				}
				return CNName;
			}
			return CNName;
		}
	}
}
