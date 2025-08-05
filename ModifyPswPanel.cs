using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ModifyPswPanel : MonoBehaviour
{
	private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public GameObject ja;

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
			switch (y)
			{
			default:
				return false;
			case 0:
				y = -1;
				z = new WaitForSeconds(1.2f);
				y = 1;
				return true;
			case 1:
			{
				y = -1;
				List<string> list = GameEntry.u.mw();
				MainControl.a.mBossPsw = list[0];
				MainControl.a.mEmployeePsw = list[1];
				ja.gameObject.SetActive(value: false);
				return false;
			}
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

	public Text m_TxtTips;

	public InputField pswInput;

	public Text QiutText;

	private bool jr;

	private string js = "";

	private string jt = "";

	private ci ju;

	private cj jv;

	private void Awake()
	{
		jv = cj.Step1;
		ju = ci.None;
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		m_TxtTips.text = GameEntry.s.ib("ModifyPSWPanelShowString1");
		QiutText.text = GameEntry.s.ib("Qiut");
		pswInput.text = "";
		js = "";
		jt = "";
		jv = cj.Step1;
		ju = ci.None;
	}

	public void NumOnClick(int a)
	{
		js += a;
	}

	public void CancelOnClick()
	{
		js = "";
	}

	public void OkOnClick()
	{
		if (jv.Equals(cj.Step1))
		{
			if (js.Equals(MainControl.a.mBossPsw))
			{
				ju = ci.Admin;
				jv = cj.Step2;
				jr = true;
				m_TxtTips.text = GameEntry.s.ib("ModifyPSWPanelShowString2");
			}
			else if (js.Equals(MainControl.a.mEmployeePsw))
			{
				ju = ci.Employee;
				jv = cj.Step2;
				jr = false;
				m_TxtTips.text = GameEntry.s.ib("ModifyPSWPanelShowString2");
			}
			else
			{
				ju = ci.None;
				jv = cj.Step1;
				MainControl.TintPanel.vm(GameEntry.s.ib("ModifyPSWFailure"));
			}
		}
		else if (jv.Equals(cj.Step2))
		{
			if (js.Length != 6)
			{
				MainControl.TintPanel.vm(GameEntry.s.ib("ModifyPSWLen"));
			}
			else
			{
				jt = js;
				m_TxtTips.text = GameEntry.s.ib("ModifyPSWAgain");
				jv = cj.Step3;
			}
		}
		else if (jv.Equals(cj.Step3))
		{
			if (jt.Equals(js))
			{
				if (jr)
				{
					GameEntry.u.mv(a: true, js);
				}
				else
				{
					GameEntry.u.mv(a: false, js);
				}
				MainControl.TintPanel.vm(GameEntry.s.ib("ModifyPSWSuccessful"));
				StartCoroutine(us(base.gameObject));
			}
			else
			{
				MainControl.TintPanel.vm(GameEntry.s.ib("ModifyPSWDifferent"));
			}
		}
		js = "";
	}

	public void OnBtnQuitClick()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator us(GameObject a)
	{
		return new a(0)
		{
			ja = a
		};
	}

	private void Update()
	{
		pswInput.text = js;
	}
}
