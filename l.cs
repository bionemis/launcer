public class l
{
	public int bf;

	public bool bg;

	public bool bh;

	public int bi;

	public string bf { get; private set; }

	public string bg { get; private set; }

	public string bh { get; private set; }

	public int bi => 3;

	public bool bj { get; private set; }

	public void dt(bool a)
	{
		bj = a;
	}

	public l(string a, string b, string c)
	{
		bf = a;
		bg = b;
		bh = c;
	}
}
