using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RankingListPanel : MonoBehaviour
{
	private sealed class a
	{
		public int jb;

		public RankingListPanel ba;

		internal void rg(bool a)
		{
			ba.rm(a, jb);
		}
	}

	private sealed class c : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public RankingListPanel ba;

		public Action jc;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return z;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return z;
			}
		}

		[DebuggerHidden]
		public c(int a)
		{
			y = a;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = y;
			RankingListPanel rankingListPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				z = null;
				y = 1;
				return true;
			case 1:
				y = -1;
				Resources.UnloadUnusedAssets();
				GC.Collect();
				if (rankingListPanel.mTogArr[0].isOn)
				{
					rankingListPanel.rn(bp.All);
				}
				else
				{
					rankingListPanel.mTogArr[0].isOn = true;
				}
				MainControl.TintPanel.vo();
				if (jc != null)
				{
					jc();
				}
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Dictionary<string, ApkItem> mApkItemDic;

	private Dictionary<string, UiRankingItem> mRankingItemPoolDic = new Dictionary<string, UiRankingItem>();

	private List<UiRankingItem> uiRankings = new List<UiRankingItem>();

	private Dictionary<string, Texture2D> mTexture2dDic = new Dictionary<string, Texture2D>();

	[SerializeField]
	private RectTransform uRectContent;

	[SerializeField]
	private RectTransform uRectAbandon;

	[SerializeField]
	private bp mRankingType;

	[SerializeField]
	private Text m_TxtRightPage;

	[SerializeField]
	private Text m_TxtLeftPage;

	private Toggle[] mTogArr;

	public int mMaxCount;

	private int mCurrPage;

	public int mSelectIndex;

	public int mMaxPage;

	public float mMaxContentWidth;

	public float mFlipOverGridWidth;

	public Action OnLoadApkComplete;

	public float cv
	{
		get
		{
			if (GameEntry.a.IsBig)
			{
				return 720f;
			}
			return 1050f;
		}
	}

	private void Awake()
	{
		Transform transform = base.transform.Find("TogGroup");
		mTogArr = transform.GetComponentsInChildren<Toggle>();
		for (int num = 0; num < mTogArr.Length; num++)
		{
			a a2 = new a();
			a2.ba = this;
			a2.jb = num;
			mTogArr[a2.jb].onValueChanged.AddListener(a2.rg);
		}
	}

	private void Start()
	{
		mApkItemDic = new Dictionary<string, ApkItem>();
		rk(delegate
		{
			GameEntry.a.fp();
		});
	}

	public void rk(Action a = null)
	{
		MainControl.TintPanel.vn(GameEntry.s.ib("TipsUpdateData"));
		StartCoroutine(rl(a));
	}

	[IteratorStateMachine(typeof(c))]
	private IEnumerator rl(Action a)
	{
		return new c(0)
		{
			ba = this,
			jc = a
		};
	}

	private void rm(bool a, int b)
	{
		if (a)
		{
			rn((bp)b);
		}
	}

	private void rn(bp a)
	{
		UnityEngine.Debug.Log("SelectRankingType type:" + a);
		mRankingType = a;
		mCurrPage = 1;
		mMaxCount = 0;
		mSelectIndex = 0;
		UiRankingItem[] componentsInChildren = uRectContent.GetComponentsInChildren<UiRankingItem>();
		int num = componentsInChildren.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			string sqlName = componentsInChildren[num2].xmlInfo.SqlName;
			if (!mRankingItemPoolDic.ContainsKey(sqlName))
			{
				componentsInChildren[num2].transform.SetParent(uRectAbandon);
				mRankingItemPoolDic.Add(sqlName, componentsInChildren[num2]);
			}
		}
		uiRankings.Clear();
		if (GameEntry.a.IsBig)
		{
			ro(rw());
		}
		else
		{
			rp(rw());
		}
		rt(0);
	}

	public void ro(List<XmlContentInfo> a)
	{
		uRectContent.sizeDelta = new Vector2(720f, 800f);
		List<bn> list = new List<bn>();
		list = GameEntry.u.mq(mRankingType);
		if (a == null && list.Count <= 0)
		{
			return;
		}
		int count = list.Count;
		int count2 = a.Count;
		for (int num = 0; num < count; num++)
		{
			bn bn2 = list[num];
			if (!bn2.IsShow)
			{
				continue;
			}
			for (int num2 = 0; num2 < count2; num2++)
			{
				XmlContentInfo xmlContentInfo = a[num2];
				if (bn2.SqlName.Equals(xmlContentInfo.SqlName))
				{
					uiRankings.Add(rv(xmlContentInfo, bn2, mMaxCount));
					mMaxCount++;
				}
			}
		}
		mMaxPage = (int)Mathf.Ceil((float)mMaxCount / 6f);
		GetComponent<GridLayoutGroup>();
		mMaxContentWidth = (float)mMaxPage * cv;
		mFlipOverGridWidth = mMaxContentWidth - 720f;
		if (mMaxContentWidth <= uRectContent.rect.width)
		{
			mMaxContentWidth = 720f;
		}
		uRectContent.sizeDelta = new Vector2(mMaxContentWidth, 800f);
		uRectContent.localPosition = new Vector3(mFlipOverGridWidth / 2f, 0f, 0f);
	}

	private void rp(List<XmlContentInfo> a)
	{
		uRectContent.sizeDelta = new Vector2(900f, 360f);
		List<bn> list = new List<bn>();
		list = GameEntry.u.mq(mRankingType);
		if (a != null || list.Count > 0)
		{
			rq(a, list, c: true);
			rq(a, list, c: false);
			mMaxPage = (int)Mathf.Ceil((float)mMaxCount / 5f);
			GetComponent<GridLayoutGroup>();
			mMaxContentWidth = (float)mMaxPage * cv;
			mFlipOverGridWidth = mMaxContentWidth - cv;
			if (mMaxContentWidth <= uRectContent.rect.width)
			{
				mMaxContentWidth = cv;
			}
			uRectContent.localPosition = new Vector3(mFlipOverGridWidth / 2f, 0f, 0f);
			uRectContent.sizeDelta = new Vector2(mMaxContentWidth, 330f);
		}
	}

	private void rq(List<XmlContentInfo> a, List<bn> b, bool c)
	{
		int count = b.Count;
		int count2 = a.Count;
		for (int num = 0; num < count; num++)
		{
			bn bn2 = b[num];
			if (!bn2.IsShow || (DateTime.Now - bn2.CreateTime).Days <= 1 != c)
			{
				continue;
			}
			for (int num2 = 0; num2 < count2; num2++)
			{
				XmlContentInfo xmlContentInfo = a[num2];
				if (bn2.SqlName.Equals(xmlContentInfo.SqlName))
				{
					uiRankings.Add(ru(xmlContentInfo, bn2, mMaxCount, c));
					mMaxCount++;
				}
			}
		}
	}

	public void rr()
	{
		if (rt(mSelectIndex + 1) && (mSelectIndex + 1) % 5 == 1 && mSelectIndex >= 5)
		{
			OnBtnRightPageClick();
		}
	}

	public void rs()
	{
		if (rt(mSelectIndex - 1) && (mSelectIndex + 1) % 5 == 0)
		{
			OnBtnLeftPageClick();
		}
	}

	private bool rt(int a)
	{
		if (uiRankings.Count > a && a >= 0)
		{
			mSelectIndex = a;
			MainControl.LobbyPanel.td(uiRankings[a]);
			return true;
		}
		return false;
	}

	public void OnBtnLeftPageClick()
	{
		float num = uRectContent.anchoredPosition3D.x + cv;
		float num2 = mFlipOverGridWidth / 2f;
		if (num > num2)
		{
			num = num2;
		}
		uRectContent.anchoredPosition3D = new Vector3(num, uRectContent.anchoredPosition3D.y, 0f);
		UpdatePageNum();
	}

	public void OnBtnRightPageClick()
	{
		float num = uRectContent.anchoredPosition3D.x - cv;
		float num2 = mFlipOverGridWidth / 2f;
		if (num < 0f - num2)
		{
			num = 0f - num2;
		}
		uRectContent.anchoredPosition3D = new Vector3(num, uRectContent.anchoredPosition3D.y, 0f);
		UpdatePageNum();
	}

	public void UpdatePageNum()
	{
		if (uRectContent == null)
		{
			return;
		}
		float num = Mathf.Abs(uRectContent.anchoredPosition3D.x - mFlipOverGridWidth / 2f);
		int num2 = 0;
		for (int num3 = 0; num3 < mMaxPage; num3++)
		{
			num2 = num3;
			if ((float)num3 * cv <= num && num < (float)(num3 + 1) * cv)
			{
				break;
			}
		}
		Mathf.Ceil(Mathf.Abs(mFlipOverGridWidth - mFlipOverGridWidth / 2f) / 144f);
		mCurrPage = num2 + 1;
		if (mCurrPage > mMaxPage)
		{
			mCurrPage = mMaxPage;
		}
		m_TxtRightPage.text = mCurrPage + "/" + mMaxPage;
		m_TxtLeftPage.text = mCurrPage + "/" + mMaxPage;
	}

	private UiRankingItem ru(XmlContentInfo a, bn b, int c, bool d)
	{
		UiRankingItem uiRankingItem = null;
		try
		{
			Transform transform = null;
			if (mRankingItemPoolDic.ContainsKey(b.SqlName))
			{
				uiRankingItem = mRankingItemPoolDic[b.SqlName];
				transform = uiRankingItem.transform;
				mRankingItemPoolDic.Remove(b.SqlName);
			}
			else
			{
				transform = UnityEngine.Object.Instantiate(Resources.Load<Transform>("Item/UiRankingItem"));
				uiRankingItem = transform.GetComponent<UiRankingItem>();
			}
			transform.transform.SetParent(uRectContent);
			transform.localScale = Vector3.one;
			transform.localPosition = Vector3.zero;
			uiRankingItem.mIndexNum = c;
			uiRankingItem.xmlInfo.CNName = a.CNName;
			uiRankingItem.xmlInfo.ENName = a.ENName;
			uiRankingItem.xmlInfo.TexIcon = a.TexIcon;
			uiRankingItem.xmlInfo.ContentTime = a.ContentTime;
			uiRankingItem.StarNum = b.StarNum;
			uiRankingItem.xmlInfo.ApkItem = a.ApkItem;
			uiRankingItem.xmlInfo.SqlName = a.SqlName;
			uiRankingItem.PlayCount = b.PlayCount;
			uiRankingItem.qw(d);
		}
		catch (Exception ex)
		{
			k.ca("ContentXml:" + ex.Message);
		}
		return uiRankingItem;
	}

	private UiBigRankingItem rv(XmlContentInfo a, bn b, int c)
	{
		UiBigRankingItem uiBigRankingItem = null;
		try
		{
			if (mRankingItemPoolDic.ContainsKey(b.SqlName))
			{
				uiBigRankingItem = mRankingItemPoolDic[b.SqlName] as UiBigRankingItem;
				uiBigRankingItem.gameObject.SetActive(value: true);
				uiBigRankingItem.mIndexNum = c;
				uiBigRankingItem.xmlInfo = a;
				uiBigRankingItem.PlayCount = b.PlayCount;
				uiBigRankingItem.StarNum = b.StarNum;
				uiBigRankingItem.ContentPattern = b.ContentPattern;
				uiBigRankingItem.qo(uiBigRankingItem.xmlInfo.TexIcon, uiBigRankingItem.ContentPattern, uiBigRankingItem.ContentType, uiBigRankingItem.PlayCount, uiBigRankingItem.StarNum);
				mRankingItemPoolDic.Remove(b.SqlName);
			}
			else
			{
				Transform obj = UnityEngine.Object.Instantiate(Resources.Load<Transform>("Item/UiBigRankingItem"));
				obj.transform.SetParent(base.transform);
				obj.localScale = Vector3.one;
				obj.localPosition = Vector3.zero;
				UiBigRankingItem component = obj.GetComponent<UiBigRankingItem>();
				component.mIndexNum = c;
				component.xmlInfo = a;
				component.PlayCount = b.PlayCount;
				component.StarNum = b.StarNum;
				component.ContentPattern = b.ContentPattern;
				component.qo(component.xmlInfo.TexIcon, component.ContentPattern, component.ContentType, component.PlayCount, component.StarNum);
				uiBigRankingItem = component;
			}
		}
		catch (Exception ex)
		{
			k.ca("ContentXml:" + ex.Message);
		}
		return uiBigRankingItem;
	}

	private List<XmlContentInfo> rw()
	{
		if (mApkItemDic.Count > 0)
		{
			mApkItemDic.Clear();
		}
		ApkItem apkItem = new ApkItem("QuestMainLobby.apk", h.e, "com.LCDZ.QuestMainLobby", "com.unity3d.player.UnityPlayerActivity");
		mApkItemDic.Add(apkItem.BackgeName, apkItem);
		DirectoryInfo[] directories = new DirectoryInfo(h.e).GetDirectories();
		int num = directories.Length;
		List<XmlContentInfo> list = new List<XmlContentInfo>();
		for (int num2 = 0; num2 < num; num2++)
		{
			XmlContentInfo xmlContentInfo = new XmlContentInfo();
			cf cf2 = new cf(directories[num2].FullName + "/Setting.xml");
			if (!cf2.og() || !File.Exists(directories[num2].FullName + "/" + cf2.ApkName))
			{
				continue;
			}
			string text = directories[num2].Name;
			if (mTexture2dDic.ContainsKey(text))
			{
				xmlContentInfo.TexIcon = mTexture2dDic[text];
			}
			else
			{
				xmlContentInfo.TexIcon = rx(directories[num2].FullName + "/1.png");
				if (!(xmlContentInfo.TexIcon != null))
				{
					UnityEngine.Debug.Log("No found Image:" + directories[num2].FullName);
					continue;
				}
				mTexture2dDic[text] = xmlContentInfo.TexIcon;
			}
			xmlContentInfo.SqlName = text;
			xmlContentInfo.CNName = cf2.CNName;
			xmlContentInfo.ENName = cf2.ENName;
			xmlContentInfo.ContentTime = cf2.ContentTime;
			ApkItem apkItem2 = (xmlContentInfo.ApkItem = new ApkItem(cf2.ApkName, directories[num2].FullName, cf2.BackgeName, cf2.ClassName));
			if (!mApkItemDic.ContainsKey(apkItem2.BackgeName))
			{
				mApkItemDic.Add(apkItem2.BackgeName, apkItem2);
			}
			list.Add(xmlContentInfo);
			cf2.Close();
		}
		OnLoadApkComplete();
		return list;
	}

	private Texture2D rx(string a)
	{
		try
		{
			FileStream fileStream = new FileStream(a, FileMode.Open, FileAccess.Read);
			fileStream.Seek(0L, SeekOrigin.Begin);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, (int)fileStream.Length);
			fileStream.Close();
			fileStream.Dispose();
			fileStream = null;
			Texture2D texture2D = new Texture2D(200, 208);
			texture2D.LoadImage(array);
			return texture2D;
		}
		catch (Exception ex)
		{
			k.ca("加载图片" + ex.Message);
			return null;
		}
	}
}
