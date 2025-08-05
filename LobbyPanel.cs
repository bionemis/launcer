using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoSingle<LobbyPanel>
{
	private enum a
	{
		Nome = 0,
		Loading = 1,
		Video = 2,
		Screen = 3
	}

	private sealed class b : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public LobbyPanel ba;

		public Action jf;

		private Dictionary<int, UIGlassItem>.Enumerator jg;

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
		public b(int a)
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
			LobbyPanel lobbyPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				lobbyPanel.mNoOperationTimer = 0f;
				if (lobbyPanel.cx > 0 && GameEntry.u.fz.fx)
				{
					UnityEngine.Debug.Log("打开投屏:" + GameEntry.x.dt.dx);
					jg = lobbyPanel.mGlassItemDic.GetEnumerator();
					MainControl.ProScreenPanel.uz(lobbyPanel.cx, GameEntry.x.dt);
					goto IL_00dc;
				}
				goto IL_00fc;
			case 1:
				y = -1;
				goto IL_00dc;
			case 2:
				{
					y = -1;
					jf?.Invoke();
					return false;
				}
				IL_00dc:
				if (jg.MoveNext())
				{
					jg.Current.Value.qj();
					z = new WaitForSeconds(0.4f);
					y = 1;
					return true;
				}
				lobbyPanel.displayStatus = a.Screen;
				jg = default(Dictionary<int, UIGlassItem>.Enumerator);
				goto IL_00fc;
				IL_00fc:
				z = new WaitForSeconds(0.2f);
				y = 2;
				return true;
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

	private sealed class c : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public LobbyPanel ba;

		public Action jf;

		private Dictionary<int, UIGlassItem>.Enumerator jg;

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
			LobbyPanel lobbyPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				UnityEngine.Debug.Log("关闭投屏");
				lobbyPanel.mNoOperationTimer = 0f;
				jg = lobbyPanel.mGlassItemDic.GetEnumerator();
				goto IL_0089;
			case 1:
				y = -1;
				goto IL_0089;
			case 2:
				{
					y = -1;
					GameEntry.x.im(jf);
					return false;
				}
				IL_0089:
				if (jg.MoveNext())
				{
					jg.Current.Value.qk();
					z = new WaitForSeconds(0.4f);
					y = 1;
					return true;
				}
				MainControl.ProScreenPanel.vc();
				lobbyPanel.displayStatus = a.Nome;
				z = new WaitForSeconds(0.2f);
				y = 2;
				return true;
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

	private sealed class d : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public LobbyPanel ba;

		public bool jh;

		public bool ji;

		public Action jj;

		private Dictionary<int, UIGlassItem>.Enumerator jk;

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
		public d(int a)
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
			LobbyPanel lobbyPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				jk = lobbyPanel.mGlassItemDic.GetEnumerator();
				goto IL_00c8;
			case 1:
				y = -1;
				goto IL_00c8;
			case 2:
				{
					y = -1;
					jj?.Invoke();
					return false;
				}
				IL_00c8:
				while (jk.MoveNext())
				{
					UIGlassItem value = jk.Current.Value;
					if (value.ct == f.Unconnect)
					{
						continue;
					}
					if (jh)
					{
						if (ji)
						{
							m.ShutDown(value.cf);
						}
						else
						{
							m.Restart(value.cf);
						}
					}
					else if (value.cn)
					{
						if (ji)
						{
							m.ShutDown(value.cf);
						}
						else
						{
							m.Restart(value.cf);
						}
					}
					z = new WaitForEndOfFrame();
					y = 1;
					return true;
				}
				z = new WaitForEndOfFrame();
				y = 2;
				return true;
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

	[SerializeField]
	private Transform uTsGlasses;

	[SerializeField]
	private Text uTxtCoin;

	[SerializeField]
	private Text uTxtPlayTime;

	[SerializeField]
	private Text uTxtConnectInfo;

	[SerializeField]
	private Text uTxtNoOper;

	[SerializeField]
	private Button uBtnStart;

	[SerializeField]
	private Button uBtnStop;

	[SerializeField]
	private Image uImgStartMask;

	[SerializeField]
	private Image uImgStopMask;

	private float mUpdateConnectTimer;

	[HideInInspector]
	public RectTransform mCurrGridTrans;

	public RankingListPanel uRankingList;

	public UiRankingItem mSelectGrid;

	private a displayStatus;

	private float mNoOperationTimer;

	public Dictionary<int, UIGlassItem> mGlassItemDic = new Dictionary<int, UIGlassItem>();

	private bool mIsCanClick = true;

	[SerializeField]
	private float mStartStopTimer;

	public bool IsLockContent;

	private float cw => 15f;

	public int cp { get; private set; }

	public int cx { get; set; }

	public string cy { get; private set; }

	public int cz { get; private set; }

	public int da { get; private set; }

	private float db => 2f;

	private void Start()
	{
		for (int num = 0; num < 6; num++)
		{
			int num2 = num + 1;
			UIGlassItem component = uTsGlasses.Find($"Glass{num2}").GetComponent<UIGlassItem>();
			if (num2 > GameEntry.t.PlayCount)
			{
				component.gameObject.SetActive(value: false);
				continue;
			}
			component.pt(num2, GameEntry.t.bp[$"HostIP{num2}"]);
			mGlassItemDic[num] = component;
		}
		bool flag = GameEntry.u.my(bt.IsInsertCoins);
		tc(flag);
		Image image = uImgStopMask;
		bool flag2 = (uImgStartMask.enabled = false);
		image.enabled = flag2;
		bg obj = GameEntry.w;
		obj.er = (Action)Delegate.Combine(obj.er, new Action(st));
		GameEntry.bb.cx += sv;
		GameEntry.bb.da += su;
	}

	private void Update()
	{
		if (mNoOperationTimer >= 100000f)
		{
			mNoOperationTimer = 100f;
		}
		if (mStartStopTimer >= 100000f)
		{
			mStartStopTimer = 100f;
		}
		uTxtNoOper.text = displayStatus.ToString();
		if (!mIsCanClick)
		{
			mStartStopTimer += Time.deltaTime;
			Image image = uImgStopMask;
			float fillAmount = (uImgStartMask.fillAmount = mStartStopTimer / db);
			image.fillAmount = fillAmount;
			if (mStartStopTimer >= db)
			{
				mStartStopTimer = 0f;
				mIsCanClick = true;
				if (!GameEntry.a.IsDoubleScreen)
				{
					Button button = uBtnStart;
					bool interactable = (uBtnStop.interactable = true);
					button.interactable = interactable;
					Image image2 = uImgStopMask;
					interactable = (uImgStartMask.enabled = false);
					image2.enabled = interactable;
				}
			}
		}
		if (!MainControl.IsSetting)
		{
			sp();
			sr();
		}
	}

	private void sp()
	{
		if (GameEntry.a.IsDoubleScreen)
		{
			return;
		}
		mNoOperationTimer += Time.deltaTime;
		if (MainControl.isKeyMove)
		{
			sq();
		}
		if (displayStatus != a.Nome || !(mNoOperationTimer >= cw))
		{
			return;
		}
		displayStatus = a.Loading;
		GameEntry.w.jh(2);
		if (cx >= 1)
		{
			if (GameEntry.u.fz.fx)
			{
				StartCoroutine(sx());
			}
			else
			{
				displayStatus = a.Screen;
			}
		}
		else
		{
			MainControl.ProScreenPanel.uv();
			displayStatus = a.Video;
		}
	}

	private void sq()
	{
		mNoOperationTimer = 0f;
		if (displayStatus == a.Video)
		{
			displayStatus = a.Loading;
			MainControl.ProScreenPanel.uw();
			displayStatus = a.Nome;
			GameEntry.w.jh(1);
		}
		else if (displayStatus == a.Screen)
		{
			displayStatus = a.Loading;
			GameEntry.w.jh(1);
			if (MainControl.ProScreenPanel.dd >= 1)
			{
				StartCoroutine(sy());
			}
			else
			{
				displayStatus = a.Nome;
			}
		}
	}

	private void sr()
	{
		mUpdateConnectTimer += Time.deltaTime;
		if (mUpdateConnectTimer >= 1f)
		{
			mUpdateConnectTimer = 0f;
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<string, l>.Enumerator enumerator = GameEntry.a.cc.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stringBuilder.Append("Sign:" + enumerator.Current.Value.bf.bi() + "\n");
			}
			uTxtConnectInfo.text = stringBuilder.ToString();
		}
	}

	private bool ss()
	{
		if (MainControl.IsSetting)
		{
			return false;
		}
		bool result = true;
		if (!GameEntry.a.IsDoubleScreen)
		{
			if (displayStatus != a.Nome)
			{
				result = false;
			}
			sq();
		}
		return result;
	}

	private void st()
	{
		ss();
	}

	private void su(y a)
	{
		if (ss())
		{
			switch (a)
			{
			case y.Alpha1:
				OnSelectGlassItemClick(1);
				break;
			case y.Alpha2:
				OnSelectGlassItemClick(2);
				break;
			case y.Alpha3:
				OnSelectGlassItemClick(3);
				break;
			case y.Alpha4:
				OnSelectGlassItemClick(4);
				break;
			case y.Alpha5:
				OnSelectGlassItemClick(5);
				break;
			case y.Alpha6:
				OnSelectGlassItemClick(6);
				break;
			case y.Q:
				OnBtnStartClick();
				break;
			case y.W:
				OnBtnStopClick();
				break;
			case y.Return:
				te();
				break;
			}
		}
	}

	private void sv(y a)
	{
		switch (a)
		{
		case y.LeftArrow:
			if (ss())
			{
				uRankingList.rs();
			}
			break;
		case y.RightArrow:
			if (ss())
			{
				uRankingList.rr();
			}
			break;
		}
	}

	public void sw()
	{
		StartCoroutine(sy(delegate
		{
			StartCoroutine(sx(delegate
			{
				Button button = uBtnStart;
				bool interactable = (uBtnStop.interactable = true);
				button.interactable = interactable;
				Image image = uImgStopMask;
				interactable = (uImgStartMask.enabled = false);
				image.enabled = interactable;
				if (cx <= 0)
				{
					MainControl.ProScreenPanel.uv();
					displayStatus = a.Video;
				}
			}));
		}));
	}

	[IteratorStateMachine(typeof(b))]
	private IEnumerator sx(Action a = null)
	{
		return new b(0)
		{
			ba = this,
			jf = a
		};
	}

	[IteratorStateMachine(typeof(c))]
	private IEnumerator sy(Action a = null)
	{
		return new c(0)
		{
			ba = this,
			jf = a
		};
	}

	public void OnBtnStartClick()
	{
		List<UIGlassItem> list = new List<UIGlassItem>();
		new StringBuilder();
		foreach (KeyValuePair<int, UIGlassItem> item in mGlassItemDic)
		{
			if (item.Value.mRankingItem != null && !item.Value.co)
			{
				list.Add(item.Value);
			}
		}
		int count = list.Count;
		if (count <= 0)
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("SelectMoive"));
		}
		if (count * da <= cz)
		{
			if (!sz())
			{
				return;
			}
			ta(-count * da);
			for (int num = 0; num < count; num++)
			{
				if (GameEntry.ba.gc())
				{
					list[num].Play();
				}
			}
			if (GameEntry.a.IsDoubleScreen)
			{
				sw();
			}
		}
		else
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("InsufficientBalance"));
		}
	}

	public void OnBtnStopClick()
	{
		if (!sz())
		{
			return;
		}
		List<UIGlassItem> list = new List<UIGlassItem>();
		foreach (KeyValuePair<int, UIGlassItem> item in mGlassItemDic)
		{
			UIGlassItem value = item.Value;
			if (value.cn)
			{
				list.Add(value);
			}
			value.qg();
		}
		int count = list.Count;
		if (count > 0)
		{
			for (int num = 0; num < count; num++)
			{
				if (list[num].co && list[num].mRankingItem != null)
				{
					GameEntry.u.nc(list[num].mRankingItem.xmlInfo, list[num].cq, da);
					list[num].qc();
				}
				else
				{
					list[num].qi();
				}
			}
		}
		else
		{
			foreach (KeyValuePair<int, UIGlassItem> item2 in mGlassItemDic)
			{
				if (!(item2.Value.mRankingItem == null) && item2.Value.co)
				{
					GameEntry.u.nc(item2.Value.mRankingItem.xmlInfo, item2.Value.cq, da);
					item2.Value.qc();
				}
			}
		}
		if (GameEntry.a.IsDoubleScreen)
		{
			sw();
		}
	}

	private bool sz()
	{
		if (!mIsCanClick)
		{
			MainControl.TintPanel.vk("Wait");
			return false;
		}
		mIsCanClick = false;
		Button button = uBtnStart;
		bool interactable = (uBtnStop.interactable = false);
		button.interactable = interactable;
		Image image = uImgStopMask;
		interactable = (uImgStartMask.enabled = true);
		image.enabled = interactable;
		return true;
	}

	public void ta(int a)
	{
		if (uTxtCoin.gameObject.activeSelf)
		{
			cz += a;
			uTxtCoin.text = $"{cz}/{da}";
		}
	}

	public void tb(int a)
	{
		if (uTxtCoin.gameObject.activeSelf)
		{
			cz = a;
			uTxtCoin.text = $"{cz}/{da}";
		}
	}

	public void tc(bool a)
	{
		uTxtCoin.gameObject.SetActive(a);
		cz = 0;
		if (a)
		{
			da = GameEntry.u.mz(bt.CoinEveryTime);
			MainControl.LobbyPanel.tb(0);
		}
		else
		{
			da = 0;
		}
	}

	public void td(UiRankingItem a)
	{
		if (mSelectGrid != null)
		{
			mSelectGrid?.qx(a: false);
		}
		mSelectGrid = a;
		mSelectGrid.qx(a: true);
		cy = a.xmlInfo.SqlName;
		uRankingList.mSelectIndex = a.mIndexNum;
		if (GameEntry.u.fz.ft)
		{
			cp = GameEntry.u.fz.fu * 60;
		}
		else
		{
			cp = mSelectGrid.xmlInfo.ContentTime;
		}
		uTxtPlayTime.text = th();
	}

	public void te()
	{
		new StringBuilder();
		for (int num = 0; num < mGlassItemDic.Count; num++)
		{
			mGlassItemDic[num].qh(mSelectGrid);
		}
	}

	[IteratorStateMachine(typeof(d))]
	public IEnumerator tf(bool a, bool b, Action c)
	{
		return new d(0)
		{
			ba = this,
			ji = a,
			jh = b,
			jj = c
		};
	}

	public void tg(bool a, int b)
	{
		GameEntry.u.fz.ft = a;
		GameEntry.u.fz.fu = b;
		GameEntry.u.na(bt.IsActiveFixedTime, a.ToString());
		GameEntry.u.na(bt.FixedTime, b.ToString());
		if (a)
		{
			cp = b * 60;
		}
		else if (mSelectGrid != null)
		{
			cp = mSelectGrid.xmlInfo.ContentTime;
		}
		uTxtPlayTime.text = th();
	}

	private string th()
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, cp);
		return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
	}

	public void OnBtnSettingClick()
	{
		MainControl.a.dj<SettingPanel>("SettingPanel");
	}

	public void OnSelectGlassItemClick(int a)
	{
		if (IsLockContent)
		{
			ti();
			IsLockContent = false;
		}
		mGlassItemDic[a - 1].qe();
	}

	private void ti()
	{
		foreach (KeyValuePair<int, UIGlassItem> item in mGlassItemDic)
		{
			UIGlassItem value = item.Value;
			if (value.cn)
			{
				value.qg();
			}
		}
	}

	public void OnApplicationQuit()
	{
		Dictionary<int, UIGlassItem>.Enumerator enumerator = mGlassItemDic.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.Value.qd();
		}
		GameEntry.bb.cx -= sv;
		GameEntry.bb.da -= su;
		bg obj = GameEntry.w;
		obj.er = (Action)Delegate.Remove(obj.er, new Action(st));
	}

	[CompilerGenerated]
	private void tj()
	{
		StartCoroutine(sx(delegate
		{
			Button button = uBtnStart;
			bool interactable = (uBtnStop.interactable = true);
			button.interactable = interactable;
			Image image = uImgStopMask;
			interactable = (uImgStartMask.enabled = false);
			image.enabled = interactable;
			if (cx <= 0)
			{
				MainControl.ProScreenPanel.uv();
				displayStatus = a.Video;
			}
		}));
	}

	[CompilerGenerated]
	private void tk()
	{
		Button button = uBtnStart;
		bool interactable = (uBtnStop.interactable = true);
		button.interactable = interactable;
		Image image = uImgStopMask;
		interactable = (uImgStartMask.enabled = false);
		image.enabled = interactable;
		if (cx <= 0)
		{
			MainControl.ProScreenPanel.uv();
			displayStatus = a.Video;
		}
	}
}
