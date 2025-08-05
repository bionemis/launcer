using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CodingPanel : MonoBehaviour
{
	private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public CodingPanel ba;

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
		public a(int a)
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
			CodingPanel codingPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				z = new WaitForSeconds(1f);
				y = 1;
				return true;
			case 1:
				y = -1;
				GameEntry.z.kc();
				z = new WaitForSeconds(2f);
				y = 2;
				return true;
			case 2:
				y = -1;
				codingPanel.UpdateGlassAuthorLst();
				codingPanel.ul();
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

	private sealed class b : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public CodingPanel ba;

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
			CodingPanel codingPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				MainControl.TintPanel.vk(GameEntry.s.ib("DataReset"), 2f);
				GameEntry.z.Start(a: true);
				z = new WaitForSeconds(1f);
				y = 1;
				return true;
			case 1:
				y = -1;
				GameEntry.ba.bn.cg = j.bu();
				UnityEngine.Debug.Log(GameEntry.ba.bn.cg);
				GameEntry.ba.bn.PlayTimes = 0;
				GameEntry.z.kr(GameEntry.ba.bn);
				z = new WaitForSeconds(0.5f);
				y = 2;
				return true;
			case 2:
				y = -1;
				codingPanel.OnBtnQuitClick();
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

	[SerializeField]
	private Transform uObjGlassesGrid;

	[Header("眼镜打码")]
	[SerializeField]
	private GameObject uObjGlassCoding;

	[SerializeField]
	private Button uBtnGetRequestCode;

	[SerializeField]
	private Button uBtnAuthorizationCode;

	[SerializeField]
	private InputField uGlass_IptMachineCode;

	[SerializeField]
	private InputField uGlass_IptGlassesCode;

	[SerializeField]
	private InputField uGlass_IptRequestCode;

	[SerializeField]
	private InputField uGlass_IptAuthorizationCode;

	[SerializeField]
	private Toggle uTogAuthor;

	[SerializeField]
	private Toggle uTogNoAuthor;

	[Header("播放器打码")]
	[SerializeField]
	private GameObject uObjPlayAuthor;

	[SerializeField]
	private InputField uPlay_IptMachineCode;

	[SerializeField]
	private InputField uPlay_IptRequestCode;

	[SerializeField]
	private InputField uPlay_IptAuthorizationCode1;

	[SerializeField]
	private InputField uPlay_IptAuthorizationCode2;

	[SerializeField]
	private InputField uPlay_IptAuthorizationCode3;

	[SerializeField]
	private InputField uPlay_IptAuthorizationCode4;

	[SerializeField]
	private Text uPlay_TxtHistoryPlayTimes;

	[SerializeField]
	private Text uPlay_TxtTotalPlayTimes;

	[SerializeField]
	private Text uPlay_TxtPlayTimes;

	[SerializeField]
	private Text uPlay_TxtRemainingDays;

	private List<UiGlassAuthorItem> authorItems = new List<UiGlassAuthorItem>();

	private string reuestCode;

	private void Awake()
	{
		GameEntry.r.gs(1013, um);
	}

	public void ui(object a = null)
	{
		uObjGlassCoding.SetActive(value: false);
		uPlay_IptMachineCode.text = "2022092400140001";
		uTogAuthor.isOn = false;
		UpdateGlassAuthorLst();
		GameEntry.z.ko(a: false);
		uk();
	}

	public void UpdateGlassAuthorLst()
	{
		uTogNoAuthor.interactable = uTogAuthor.isOn;
		uTogAuthor.interactable = !uTogAuthor.isOn;
		for (int num = 0; num < authorItems.Count; num++)
		{
			authorItems[num].Close();
		}
		authorItems.Clear();
		Dictionary<string, l>.Enumerator enumerator = GameEntry.a.cc.GetEnumerator();
		while (enumerator.MoveNext())
		{
			l value = enumerator.Current.Value;
			if (value.bj == uTogAuthor.isOn && value.bi == 1 && !uj(value.bh.bk()))
			{
				Transform obj = UnityEngine.Object.Instantiate(Resources.Load<Transform>("Item/UiGlassAuthorItem"));
				obj.SetParent(uObjGlassesGrid);
				obj.localScale = Vector3.one;
				obj.localPosition = Vector3.zero;
				UiGlassAuthorItem component = obj.GetComponent<UiGlassAuthorItem>();
				component.qv(value.bj, value.bh.bk());
				authorItems.Add(component);
			}
		}
	}

	private bool uj(string a)
	{
		for (int num = 0; num < authorItems.Count; num++)
		{
			if (authorItems[num].cu.Equals(a))
			{
				return true;
			}
		}
		return false;
	}

	private void uk()
	{
		uPlay_TxtHistoryPlayTimes.text = GameEntry.ba.bn.cf.ToString();
		uPlay_TxtTotalPlayTimes.text = (GameEntry.z.bx ? "∞" : GameEntry.z.bv.ToString());
		uPlay_TxtPlayTimes.text = GameEntry.ba.bn.PlayTimes.ToString();
		uPlay_TxtRemainingDays.text = (GameEntry.z.bx ? "∞" : GameEntry.z.bw.ToString());
	}

	public void ul()
	{
		uObjGlassCoding.SetActive(value: false);
		uBtnGetRequestCode.interactable = true;
		uBtnAuthorizationCode.interactable = true;
		uGlass_IptRequestCode.text = "0C2942D4A14C85F7";
		uGlass_IptAuthorizationCode.text = "";
		uGlass_IptGlassesCode.text = "";
	}

	private void um(object a)
	{
		uObjGlassCoding.SetActive(value: true);
		uGlass_IptGlassesCode.text = "2022092400140001";
	}

	public void OnBtnGetRequestCodeClick()
	{
		uGlass_IptRequestCode.text = GameEntry.z.ld();
		uBtnGetRequestCode.interactable = false;
	}

	public void OnBtnAuthorizationClick()
	{
		uBtnAuthorizationCode.interactable = false;
		string text = uGlass_IptAuthorizationCode.text;
		if (string.IsNullOrEmpty(text))
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("InputContentNotNull"));
			return;
		}
		string text2 = "";
		string text3 = "";
		UnityEngine.Debug.Log("授权眼镜：" + text);
		GameEntry.z.le(text, out text2, out text3);
		switch (text3)
		{
		default:
			MainControl.TintPanel.vk(GameEntry.s.ib("ContentIdError"), 5f);
			break;
		case "22":
			MainControl.TintPanel.vk(GameEntry.s.ib("ContentRegistrationFull"), 5f);
			break;
		case "33":
			MainControl.TintPanel.vk(GameEntry.s.ib("ContentHasBeenRegistered"), 5f);
			break;
		case "44":
			MainControl.TintPanel.vk(GameEntry.s.ib("LockOrCodeError"), 5f);
			break;
		case "66":
			MainControl.TintPanel.vk(GameEntry.s.ib("AddContentComplete"), 5f);
			break;
		}
		StartCoroutine(un());
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator un()
	{
		return new a(0)
		{
			ba = this
		};
	}

	public void OnPlay_BtnGetRequestCodeClick()
	{
		reuestCode = GameEntry.z.ld();
		string text = reuestCode + GameEntry.z.kw(reuestCode);
		uPlay_IptRequestCode.text = text.bs(8);
	}

	public void OnPlay_BtnAuthorizationClick()
	{
		string value = uPlay_IptAuthorizationCode1.text + uPlay_IptAuthorizationCode2.text;
		if (string.IsNullOrEmpty(value))
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("TipsInputCannotBeNull"));
			return;
		}
		string value2 = uPlay_IptAuthorizationCode3.text + uPlay_IptAuthorizationCode4.text;
		if (string.IsNullOrEmpty(value2))
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("TipsInputCannotBeNull"));
		}
		if (!GameEntry.z.kx(value2, reuestCode))
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("TipsCheckFailure"));
			return;
		}
		string text = GameEntry.z.lc(value);
		if (string.IsNullOrEmpty(text))
		{
			MainControl.TintPanel.vk(GameEntry.s.ib("TipsCheckFailure"));
			return;
		}
		byte[] array = text.bo();
		UnityEngine.Debug.Log("天数:" + array[2]);
		UnityEngine.Debug.Log("次数:" + (array[3] * 256 + array[4]));
		UnityEngine.Debug.Log("授权:" + array[5]);
		UnityEngine.Debug.LogError("activate:" + text);
		GameEntry.z.by = false;
		MainControl.TintPanel.vp(GameEntry.s.ib("TipsTitle"), GameEntry.s.ib("TipsCodeCompletion"), delegate
		{
			StartCoroutine(uo());
		});
	}

	[IteratorStateMachine(typeof(b))]
	private IEnumerator uo()
	{
		return new b(0)
		{
			ba = this
		};
	}

	public void OnBtnQuitClick()
	{
		if (uObjGlassCoding.activeSelf)
		{
			if (!uBtnGetRequestCode.interactable)
			{
				GameEntry.z.kc();
				UpdateGlassAuthorLst();
			}
			ul();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		reuestCode = null;
		GameEntry.r.gu(1013, um);
	}

	[CompilerGenerated]
	private void up()
	{
		StartCoroutine(uo());
	}
}
