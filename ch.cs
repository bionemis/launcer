using UnityEngine;

public class ch
{
	public bn ContentInfo;

	public Sprite ca { get; }

	public Sprite cb { get; }

	public Sprite cc { get; }

	public Sprite cd { get; }

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

	public string CNName { get; }

	public string ENName { get; }

	public float ContentTime { get; }

	public string bl { get; }

	public string ce { get; }

	public ch(Sprite a, Sprite b, Sprite c, Sprite d, string e, string f, float g, string h, string i, bn j)
	{
		_003Cgz_003Ek__BackingField = a;
		_003Cha_003Ek__BackingField = b;
		_003Chb_003Ek__BackingField = c;
		_003Chc_003Ek__BackingField = d;
		_003Cgq_003Ek__BackingField = e;
		_003Cgr_003Ek__BackingField = f;
		_003Cgv_003Ek__BackingField = g;
		_003Chd_003Ek__BackingField = h;
		_003Che_003Ek__BackingField = i;
		ContentInfo = j;
	}
}
