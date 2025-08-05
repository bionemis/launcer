using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NumKeyboardPanel : MonoBehaviour
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
				z = new WaitForSeconds(0.5f);
				y = 1;
				return true;
			case 1:
				y = -1;
				ja.gameObject.SetActive(value: false);
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

		public NumKeyboardPanel ba;

		public string jl;

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
			NumKeyboardPanel numKeyboardPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				numKeyboardPanel.tint_go.transform.GetChild(0).GetComponent<Text>().text = jl;
				numKeyboardPanel.tint_go.SetActive(value: true);
				z = new WaitForSeconds(1f);
				y = 1;
				return true;
			case 1:
				y = -1;
				numKeyboardPanel.tint_go.SetActive(value: false);
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

	[Tooltip("show the tinit string")]
	[SerializeField]
	private GameObject tint_go;

	[Tooltip("show the password")]
	[SerializeField]
	private InputField psw_input;

	private string psw_str;

	private string boss_psw;

	private string employee_psw;

	[Tooltip("confirm shutdown or restarted button")]
	[SerializeField]
	private Button sure_button;

	private GameObject num1Selected;

	private GameObject num2Selected;

	private GameObject num3Selected;

	private GameObject num4Selected;

	private GameObject num5Selected;

	private GameObject num6Selected;

	private GameObject num7Selected;

	private GameObject num8Selected;

	private GameObject num9Selected;

	private GameObject num0Selected;

	private GameObject cancelSelected;

	private GameObject OkSelected;

	private void Awake()
	{
		num1Selected = base.transform.Find("Num1").Find("Selected").gameObject;
		num2Selected = base.transform.Find("Num2").Find("Selected").gameObject;
		num3Selected = base.transform.Find("Num3").Find("Selected").gameObject;
		num4Selected = base.transform.Find("Num4").Find("Selected").gameObject;
		num5Selected = base.transform.Find("Num5").Find("Selected").gameObject;
		num6Selected = base.transform.Find("Num6").Find("Selected").gameObject;
		num7Selected = base.transform.Find("Num7").Find("Selected").gameObject;
		num8Selected = base.transform.Find("Num8").Find("Selected").gameObject;
		num9Selected = base.transform.Find("Num9").Find("Selected").gameObject;
		num0Selected = base.transform.Find("Num0").Find("Selected").gameObject;
		cancelSelected = base.transform.Find("Cancel").Find("Selected").gameObject;
		OkSelected = base.transform.Find("Ok").Find("Selected").gameObject;
	}

	private void Start()
	{
	}

	private void Update()
	{
		psw_input.text = psw_str;
	}

	private void OnEnable()
	{
		tint_go.SetActive(value: false);
		num1Selected.SetActive(value: false);
		num2Selected.SetActive(value: false);
		num3Selected.SetActive(value: false);
		num4Selected.SetActive(value: false);
		num5Selected.SetActive(value: false);
		num6Selected.SetActive(value: false);
		num7Selected.SetActive(value: false);
		num8Selected.SetActive(value: false);
		num9Selected.SetActive(value: false);
		num0Selected.SetActive(value: false);
		cancelSelected.SetActive(value: false);
		OkSelected.SetActive(value: false);
		sure_button.enabled = false;
		List<string> list = GameEntry.u.mw();
		boss_psw = list[0];
		employee_psw = list[1];
		psw_str = "";
	}

	public void Num1OnClick()
	{
		num1Selected.SetActive(value: true);
		StartCoroutine(tq(num1Selected));
		psw_str += "1";
	}

	public void Num2OnClick()
	{
		num2Selected.SetActive(value: true);
		StartCoroutine(tq(num2Selected));
		psw_str += "2";
	}

	public void Num3OnClick()
	{
		num3Selected.SetActive(value: true);
		StartCoroutine(tq(num3Selected));
		psw_str += "3";
	}

	public void Num4OnClick()
	{
		num4Selected.SetActive(value: true);
		StartCoroutine(tq(num4Selected));
		psw_str += "4";
	}

	public void Num5OnClick()
	{
		num5Selected.SetActive(value: true);
		StartCoroutine(tq(num5Selected));
		psw_str += "5";
	}

	public void Num6OnClick()
	{
		num6Selected.SetActive(value: true);
		StartCoroutine(tq(num6Selected));
		psw_str += "6";
	}

	public void Num7OnClick()
	{
		num7Selected.SetActive(value: true);
		StartCoroutine(tq(num7Selected));
		psw_str += "7";
	}

	public void Num8OnClick()
	{
		num8Selected.SetActive(value: true);
		StartCoroutine(tq(num8Selected));
		psw_str += "8";
	}

	public void Num9OnClick()
	{
		num9Selected.SetActive(value: true);
		StartCoroutine(tq(num9Selected));
		psw_str += "9";
	}

	public void Num0OnClick()
	{
		num0Selected.SetActive(value: true);
		StartCoroutine(tq(num0Selected));
		psw_str += "0";
	}

	public void CancelOnClick()
	{
		cancelSelected.SetActive(value: true);
		StartCoroutine(tq(cancelSelected));
		psw_str = "";
	}

	public void OkOnClick()
	{
		OkSelected.SetActive(value: true);
		StartCoroutine(tq(OkSelected));
		if (psw_str.Equals(boss_psw))
		{
			sure_button.enabled = true;
			StartCoroutine(tr(GameEntry.s.ib("ConfirmThePasswordSuccessfully")));
			psw_str = "";
		}
		else if (psw_str.Equals(employee_psw))
		{
			sure_button.enabled = true;
			StartCoroutine(tr(GameEntry.s.ib("ConfirmThePasswordSuccessfully")));
			psw_str = "";
		}
		else
		{
			StartCoroutine(tr(GameEntry.s.ib("LoginFailure")));
			psw_str = "";
		}
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator tq(GameObject a)
	{
		return new a(0)
		{
			ja = a
		};
	}

	[IteratorStateMachine(typeof(b))]
	private IEnumerator tr(string a)
	{
		return new b(0)
		{
			ba = this,
			jl = a
		};
	}
}
