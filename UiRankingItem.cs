using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRankingItem : MonoBehaviour
{
	public class a
	{
		public Image im;

		public Text @in;
	}

	public int mIndexNum;

	public bool mIsSelect;

	protected RawImage io;

	protected GameObject ip;

	protected Text hv;

	protected Image iq;

	protected GameObject ir;

	public int StarNum;

	public int PlayCount;

	public XmlContentInfo xmlInfo;

	public List<UIGlassItem> m_MountGlassList;

	public List<a> mGlassBindingList;

	private void Awake()
	{
		io = base.transform.Find("ImgMask/RawIcon").GetComponent<RawImage>();
		ip = base.transform.Find("ImgSelect").gameObject;
		ip.SetActive(value: false);
		ir = base.transform.Find("ImgNew").gameObject;
		hv = base.transform.Find("ImgTextBack/TxtContentName").GetComponent<Text>();
		iq = base.transform.Find("StarBg/ImgStar").GetComponent<Image>();
		m_MountGlassList = new List<UIGlassItem>();
		mGlassBindingList = new List<a>();
		for (int num = 0; num < 6; num++)
		{
			a a2 = new a();
			a2.im = base.transform.Find($"GlassBinding/ImgBinding{num + 1}").GetComponent<Image>();
			a2.@in = base.transform.Find($"GlassBinding/ImgBinding{num + 1}/TxtNum").GetComponent<Text>();
			a2.@in.enabled = false;
			mGlassBindingList.Add(a2);
		}
	}

	public void qw(bool a)
	{
		ir.SetActive(a);
		io.texture = xmlInfo.TexIcon;
		iq.sprite = qy(StarNum);
	}

	private void Update()
	{
		hv.text = xmlInfo.ContentName;
	}

	public void SelectedThis()
	{
		MainControl.LobbyPanel.td(this);
		MainControl.LobbyPanel.te();
	}

	public void qx(bool a)
	{
		mIsSelect = a;
		ip.gameObject.SetActive(mIsSelect);
	}

	private Sprite qy(int a)
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

	public void qz(int a)
	{
		mGlassBindingList[a].im.color = new Color(1f, 1f, 1f, 0.2f);
		mGlassBindingList[a].@in.color = new Color(1f, 1f, 1f, 0.2f);
	}

	public void ra(UIGlassItem a)
	{
		int index = a.mGlassNum - 1;
		if (!m_MountGlassList.Contains(a))
		{
			m_MountGlassList.Add(a);
		}
		mGlassBindingList[index].im.color = Color.white;
		mGlassBindingList[index].@in.enabled = true;
		mGlassBindingList[index].@in.color = Color.white;
	}

	public void rb(UIGlassItem a)
	{
		int index = a.mGlassNum - 1;
		if (m_MountGlassList.Contains(a))
		{
			m_MountGlassList.Remove(a);
		}
		mGlassBindingList[index].im.color = Color.white;
		mGlassBindingList[index].@in.enabled = false;
	}

	public void Close()
	{
		if (io.texture != null)
		{
			io.texture = null;
		}
		qs();
	}

	protected virtual void qs()
	{
	}
}
