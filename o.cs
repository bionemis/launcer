using System.Collections.Generic;
using System.Text;

public class o
{
	public bool bv;

	public int bw;

	public string bx;

	public Dictionary<int, string> by;

	public string fn()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("Success:{0}\nExitCode:{1}\nOutputString:{2}\nMoreOutputString:\n", bv, bw, bx);
		if (by != null)
		{
			foreach (KeyValuePair<int, string> item in by)
			{
				stringBuilder.AppendFormat("{0}:{1}\n", item.Key, item.Value.Replace("\r", "\\Ⓡ").Replace("\n", "\\Ⓝ"));
			}
		}
		return stringBuilder.ToString();
	}
}
