using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LockPanel : MonoBehaviour
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
				z = new WaitForSeconds(1f);
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

	[SerializeField]
	private InputField psw_input;

	[SerializeField]
	private Text psw_label;

	[SerializeField]
	private GameObject num_panel;

	private string boss_psw;

	[SerializeField]
	private Text tint_text;

	private void Start()
	{
		boss_psw = GameEntry.u.mw()[0];
	}

	private void Update()
	{
		psw_label.text = GameEntry.s.ib("PSW");
	}

	private void OnEnable()
	{
		psw_input.text = "";
		num_panel.SetActive(value: false);
	}

	private void OnDisable()
	{
		psw_input.text = "";
		num_panel.SetActive(value: false);
	}

	public void enable_lockPanel()
	{
		base.gameObject.SetActive(value: true);
	}

	public void disenable_lockPanel()
	{
		num_panel.SetActive(value: true);
	}

	public void num_click(int a)
	{
		if (a >= 0 && a <= 9)
		{
			psw_input.text += a;
			return;
		}
		switch (a)
		{
		case 10:
			num_panel.SetActive(value: false);
			psw_input.text = "";
			break;
		case 11:
			if (psw_input.text.Equals(boss_psw))
			{
				base.gameObject.SetActive(value: false);
				psw_input.text = "";
				break;
			}
			psw_input.text = "";
			tint_text.text = GameEntry.s.ib("LoginFailure");
			tint_text.transform.parent.gameObject.SetActive(value: true);
			StartCoroutine(rf(tint_text.transform.parent.gameObject));
			break;
		}
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator rf(GameObject a)
	{
		return new a(0)
		{
			ja = a
		};
	}
}
