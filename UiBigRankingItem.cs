using UnityEngine;
using UnityEngine.UI;
using d;

public class UiBigRankingItem : UiRankingItem
{
	private Text ih;

	private Text ii;

	private Image ij;

	private Text ik;

	private Text il;

	public ContentType ContentType;

	public ContentPattern ContentPattern;

	private void Awake()
	{
		ih = base.transform.Find("LblContentName").GetComponent<Text>();
		ii = base.transform.Find("LblStar").GetComponent<Text>();
		ij = base.transform.Find("ImgStarNum").GetComponent<Image>();
		ik = base.transform.Find("TxtPlayCount").GetComponent<Text>();
		il = base.transform.Find("TxtContentType").GetComponent<Text>();
		GameEntry.r.gs(1003, qn);
	}

	private void qn(object a)
	{
		il.text = qr(ContentType);
	}

	public void qo(Texture2D a, ContentPattern b, ContentType c, int d, int e)
	{
		io.texture = a;
		il.text = qr(c);
		ik.text = d.ToString();
		Color color = new Color(0f, 255f, 0f, 255f);
		switch (b)
		{
		case ContentPattern.Interaction:
			color = new Color(1f, 0.85490197f, 0f, 1f);
			break;
		case ContentPattern.Panorama:
			color = new Color(66f / 85f, 1f, 0f, 1f);
			break;
		default:
			color = new Color(0f, 1f, 0f, 1f);
			break;
		}
		ij.color = color;
		ij.sprite = qq(e);
	}

	public void qp()
	{
		PlayCount++;
		ik.text = PlayCount.ToString();
		GameEntry.u.mu(xmlInfo.SqlName, PlayCount);
	}

	private Sprite qq(int a)
	{
		switch (a)
		{
		default:
			return Resources.Load<Sprite>("Stars/Star1");
		case 1:
			return Resources.Load<Sprite>("Stars/Star2");
		case 2:
			return Resources.Load<Sprite>("Stars/Star3");
		case 3:
			return Resources.Load<Sprite>("Stars/Star4");
		case 4:
			return Resources.Load<Sprite>("Stars/Star5");
		}
	}

	private string qr(ContentType a)
	{
		switch (a)
		{
		case ContentType.Children:
			return GameEntry.s.ib("ContentTypeChildren");
		case ContentType.Horrible:
			return GameEntry.s.ib("ContentTypeHorrible");
		case ContentType.Adventure:
			return GameEntry.s.ib("ContentTypeAdventure");
		default:
			return GameEntry.s.ib("ContentTypeOther");
		}
	}

	protected override void qs()
	{
		base.qs();
		GameEntry.r.gs(1003, qn);
	}
}
