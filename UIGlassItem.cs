using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIGlassItem : MonoBehaviour
{
	private sealed class a
	{
		public Dictionary<string, ApkItem> hf;

		public i hg;

		public UIGlassItem ba;

		internal void oo()
		{
			Dictionary<string, ApkItem>.Enumerator enumerator = hf.GetEnumerator();
			while (enumerator.MoveNext() && !hg.o.IsCancellationRequested)
			{
				ApkItem value = enumerator.Current.Value;
				if (!value.IsInstall)
				{
					Debug.Log("正在安装=》" + value.ApkName);
					ba.cm = value.ApkName;
					string text = m.el(ba.cf, value.bl + "/" + value.ApkName);
					if (text.Contains("failed to install"))
					{
						k.ca("Install:" + ba.cf + "=>" + value.ApkName + " msg:" + text);
					}
					ba.cl += 1;
					value.IsInstall = true;
				}
				ba.cm = "";
			}
		}

		internal void op()
		{
			m.eh(ba.cf.bh());
			Thread.Sleep(100);
			while (!ba.@if.o.IsCancellationRequested)
			{
				try
				{
					string value = m.ee(ba.cf.bh());
					if (!string.IsNullOrEmpty(value))
					{
						ba.ch = float.Parse(value);
					}
					Thread.Sleep(1000);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message);
					return;
				}
			}
			Debug.Log("Task is Cancelled.");
		}
	}

	[Header("眼镜号码")]
	public int mGlassNum;

	private List<Sprite> ht;

	private Transform hu;

	private Text hv;

	private Text hw;

	private Image hx;

	private Text hy;

	private Text hz;

	private Transform ia;

	private Image ib;

	private Text ic;

	public UiRankingItem mRankingItem;

	private float ie;

	private i @if;

	private Dictionary<string, ApkItem> ig;

	public string cf { get; private set; }

	public string cg { get; private set; }

	public float ch { get; private set; }

	public bool ci { get; private set; }

	public string cj { get; private set; }

	public int ck { get; private set; }

	public int cl { get; private set; }

	public string cm { get; private set; }

	public bool cn { get; private set; }

	public bool co { get; private set; }

	public float cp { get; private set; }

	public DateTime cq { get; private set; }

	private Image cr => GetComponent<Image>();

	private Button cs => GetComponent<Button>();

	public f ct { get; private set; }

	public void ps(f a)
	{
		ct = a;
	}

	private void Start()
	{
		cn = false;
		co = false;
		ps(f.Unconnect);
		cs.interactable = false;
		hu = base.transform.Find("ImgContentBg");
		hu.gameObject.SetActive(value: false);
		hv = hu.Find("TxtContentName").GetComponent<Text>();
		hw = hu.Find("TxtPlayTimer").GetComponent<Text>();
		hw.enabled = false;
		hx = base.transform.Find("FilledInstall").GetComponent<Image>();
		hx.gameObject.SetActive(value: false);
		hy = hx.transform.Find("TxtProgress").GetComponent<Text>();
		hz = hx.transform.Find("TxtInsName").GetComponent<Text>();
		hx.fillAmount = 0f;
		hy.text = "0%";
		ia = base.transform.Find("ObjBattery");
		ia.gameObject.SetActive(value: false);
		ib = ia.Find("FilledBattery").GetComponent<Image>();
		ic = ia.Find("TxtBattery").GetComponent<Text>();
		ig = new Dictionary<string, ApkItem>();
		RankingListPanel uRankingList = MainControl.LobbyPanel.uRankingList;
		uRankingList.OnLoadApkComplete = (Action)Delegate.Combine(uRankingList.OnLoadApkComplete, new Action(pu));
	}

	private void Update()
	{
		pv();
		if (co)
		{
			TimeSpan timeSpan = DateTime.Now - cq;
			TimeSpan timeSpan2 = new TimeSpan(0, 0, (int)cp);
			hw.text = $"({timeSpan.Minutes:00}:{timeSpan.Seconds:00}/<color=#FFFF00>{timeSpan2.Minutes:00}:{timeSpan2.Seconds:00}</color>)";
			if (timeSpan.TotalSeconds >= (double)cp)
			{
				cq = DateTime.Now;
				qd();
			}
		}
	}

	private void OnApplicationQuit()
	{
		Close();
	}

	public void pt(int a, string b)
	{
		mGlassNum = a;
		cf = b;
		ht = new List<Sprite>();
		ht.Add(Resources.Load<Sprite>("Glasses/未连接-VR" + mGlassNum));
		ht.Add(Resources.Load<Sprite>("Glasses/无状态"));
		ht.Add(Resources.Load<Sprite>("Glasses/未选择2-VR" + mGlassNum));
		ht.Add(Resources.Load<Sprite>("Glasses/已选择-VR" + mGlassNum));
		cr.sprite = ht[0];
	}

	public void Close()
	{
		@if?.Close();
		if (ig != null)
		{
			ig.Clear();
			ig = null;
		}
	}

	private void pu()
	{
		ig = MainControl.LobbyPanel.uRankingList.mApkItemDic;
	}

	private void pv()
	{
		ie += Time.deltaTime;
		if (!(ie >= 0.5f))
		{
			return;
		}
		ie = 0f;
		switch (ct)
		{
		case f.Unconnect:
			cs.interactable = false;
			cn = false;
			ia.gameObject.SetActive(value: false);
			hx.gameObject.SetActive(value: false);
			cr.sprite = ht[0];
			qd();
			break;
		case f.Install:
			hx.gameObject.SetActive(value: true);
			cr.sprite = ht[1];
			hz.text = "Ins:" + cm;
			break;
		case f.Unselected:
			cr.sprite = ht[2];
			break;
		}
		if (ct != f.Unconnect)
		{
			cs.interactable = true;
			ic.text = $"{ch}%";
			ib.fillAmount = ch / 100f;
			if (ct != f.Install)
			{
				ia.gameObject.SetActive(value: true);
				hx.gameObject.SetActive(value: false);
			}
		}
		if (cl < ck)
		{
			float num = (float)cl / (float)ck;
			hx.fillAmount = num;
			hy.text = $"{num * 100f:0.0}%";
		}
	}

	public void pw(string a, string[] b)
	{
		if (!string.IsNullOrEmpty(cj))
		{
			m.ej(a, cj);
		}
		cg = a;
		ps(f.Install);
		px(b);
	}

	private void px(string[] a)
	{
		if (ig != null && ig.Count <= 0)
		{
			Debug.LogError("内容没有初始化");
			return;
		}
		ck = ig.Count;
		for (int num = 0; num < a.Length; num++)
		{
			string key = a[num].bj();
			if (ig.ContainsKey(key))
			{
				cl++;
				ig[key].IsInstall = true;
			}
		}
		Task.Run(delegate
		{
			try
			{
				foreach (KeyValuePair<string, ApkItem> item in ig)
				{
					ApkItem value = item.Value;
					if (!value.IsInstall)
					{
						Debug.Log("正在安装=》" + value.ApkName);
						cm = value.ApkName;
						string text = m.el(cf, value.bl + "/" + value.ApkName);
						if (text.Contains("failed to install"))
						{
							k.ca("Install:" + cf + "=>" + value.ApkName + " msg:" + text);
						}
						cl++;
						value.IsInstall = true;
					}
					cm = "";
					if (py())
					{
						break;
					}
				}
			}
			catch (Exception ex)
			{
				k.ca("获取apk信息失败， 是否没有权限:" + ex.Message);
				GameEntry.a.cc.Remove(cf);
				qa();
			}
		});
		Debug.Log("更新内容完成");
	}

	private bool py()
	{
		if (cl == ck)
		{
			@if = new i();
			Debug.Log("校验完成：" + cf);
			ps(f.Unselected);
			@if.s = Task.Run(delegate
			{
				m.eh(cf.bh());
				Thread.Sleep(100);
				while (!@if.o.IsCancellationRequested)
				{
					try
					{
						string value = m.ee(cf.bh());
						if (!string.IsNullOrEmpty(value))
						{
							ch = float.Parse(value);
						}
						Thread.Sleep(1000);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.Message);
						return;
					}
				}
				Debug.Log("Task is Cancelled.");
			});
			return true;
		}
		return false;
	}

	private void pz(string[] a)
	{
		a a2 = new a();
		a2.ba = this;
		a2.hf = MainControl.LobbyPanel.uRankingList.mApkItemDic;
		ck = a2.hf.Count;
		for (int num = 0; num < a.Length; num++)
		{
			string key = a[num].bj();
			if (a2.hf.ContainsKey(key))
			{
				cl++;
				a2.hf[key].IsInstall = true;
			}
		}
		a2.hg = new i();
		a2.hg.s = Task.Run((Action)a2.oo);
		a2.hg.s.Wait();
		a2.hg.Close();
		@if = new i();
		if (cl == ck)
		{
			ps(f.Unselected);
			@if.s = Task.Run((Action)a2.op);
		}
		else
		{
			k.ca("获取apk信息失败， 是否没有权限");
			GameEntry.a.cc.Remove(cf);
			qa();
		}
		Debug.Log("初始完成");
	}

	public void qa()
	{
		Debug.LogError("断开连接");
		@if.Close();
		cg = null;
		ps(f.Unconnect);
		ck = 0;
		cl = 0;
		qb();
	}

	private void qb()
	{
		Dictionary<string, ApkItem>.Enumerator enumerator = ig.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.Value.IsInstall = false;
		}
	}

	public void Play()
	{
		if (!co && mRankingItem != null)
		{
			co = true;
			qg();
			cp = MainControl.LobbyPanel.cp;
			MainControl.LobbyPanel.cx++;
			ApkItem apkItem = mRankingItem.xmlInfo.ApkItem;
			mRankingItem.qz(mGlassNum - 1);
			cj = apkItem.BackgeName;
			m.ei(cf.bh(), apkItem.BackgeName, apkItem.ClassName);
			cq = DateTime.Now;
			hw.enabled = true;
		}
	}

	public void qc()
	{
		hw.enabled = false;
		hu.gameObject.SetActive(value: false);
		if (co)
		{
			co = false;
			cp = 0f;
			MainControl.LobbyPanel.cx--;
			hv.text = "XXX";
			cj = "";
			ApkItem apkItem = mRankingItem.xmlInfo.ApkItem;
			m.ej(cf.bh(), apkItem.BackgeName);
			m.eh(cf.bh());
		}
		qi();
	}

	public void qd()
	{
		qc();
		if (ci)
		{
			ci = false;
			MainControl.LobbyPanel.sw();
		}
	}

	public void qe()
	{
		if (cn)
		{
			qg();
		}
		else
		{
			qf();
		}
	}

	private void qf()
	{
		if (ct == f.Unselected)
		{
			cn = true;
			ps(f.Selected);
			cr.sprite = ht[3];
		}
	}

	public void qg()
	{
		if (ct == f.Selected)
		{
			cn = false;
			ps(f.Unselected);
			cr.sprite = ht[2];
		}
	}

	public void qh(UiRankingItem a)
	{
		if (!cn || co)
		{
			return;
		}
		MainControl.LobbyPanel.IsLockContent = true;
		hu.gameObject.SetActive(value: false);
		if (mRankingItem != null)
		{
			if (mRankingItem.Equals(a))
			{
				mRankingItem.rb(this);
				mRankingItem = null;
				return;
			}
			mRankingItem.rb(this);
		}
		mRankingItem = a;
		hu.gameObject.SetActive(value: true);
		hv.text = mRankingItem.xmlInfo.ContentName;
		a.ra(this);
	}

	public void qi()
	{
		if (mRankingItem != null)
		{
			hw.enabled = false;
			hu.gameObject.SetActive(value: false);
			mRankingItem.rb(this);
			mRankingItem = null;
		}
	}

	public void qj()
	{
		if (co && !ci)
		{
			ci = true;
			GameEntry.x.ik(cf, mGlassNum);
		}
	}

	public void qk()
	{
		if (ci)
		{
			ci = false;
			GameEntry.x.il(cf);
		}
	}

	[CompilerGenerated]
	private void ql()
	{
		try
		{
			foreach (KeyValuePair<string, ApkItem> item in ig)
			{
				ApkItem value = item.Value;
				if (!value.IsInstall)
				{
					Debug.Log("正在安装=》" + value.ApkName);
					cm = value.ApkName;
					string text = m.el(cf, value.bl + "/" + value.ApkName);
					if (text.Contains("failed to install"))
					{
						k.ca("Install:" + cf + "=>" + value.ApkName + " msg:" + text);
					}
					cl++;
					value.IsInstall = true;
				}
				cm = "";
				if (py())
				{
					break;
				}
			}
		}
		catch (Exception ex)
		{
			k.ca("获取apk信息失败， 是否没有权限:" + ex.Message);
			GameEntry.a.cc.Remove(cf);
			qa();
		}
	}

	[CompilerGenerated]
	private void qm()
	{
		m.eh(cf.bh());
		Thread.Sleep(100);
		while (!@if.o.IsCancellationRequested)
		{
			try
			{
				string value = m.ee(cf.bh());
				if (!string.IsNullOrEmpty(value))
				{
					ch = float.Parse(value);
				}
				Thread.Sleep(1000);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				return;
			}
		}
		Debug.Log("Task is Cancelled.");
	}
}
