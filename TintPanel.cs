using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TintPanel : MonoSingle<TintPanel>
{
	private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public float jw;

		public TintPanel ba;

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
			TintPanel tintPanel = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				z = new WaitForSeconds(jw);
				y = 1;
				return true;
			case 1:
				y = -1;
				tintPanel.uContreTintPanel.SetActive(value: false);
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
	private GameObject uContreTintPanel;

	private Text m_LblContreTint;

	[SerializeField]
	private GameObject uTopTintPanel;

	private Text m_LblTopTint;

	private float topShowTimer;

	[SerializeField]
	private GameObject uConfirmPanel;

	private Text m_CfmTxtTitle;

	private Text m_CfmTxtContent;

	private GameObject m_BtnConfirm;

	private GameObject m_BtnCancel;

	[SerializeField]
	private GameObject uErrorPanel;

	private Text m_ErrTxtTitle;

	private Text m_ErrTxtContent;

	private Action OnClickConfirm;

	private bool isForver;

	private void Awake()
	{
		topShowTimer = 0f;
		isForver = false;
		m_LblContreTint = uContreTintPanel.transform.Find("Text").GetComponent<Text>();
		m_LblTopTint = uTopTintPanel.transform.Find("bg/Text").GetComponent<Text>();
		m_CfmTxtTitle = uConfirmPanel.transform.Find("TxtTitle").GetComponent<Text>();
		m_CfmTxtContent = uConfirmPanel.transform.Find("TxtContent").GetComponent<Text>();
		m_BtnConfirm = uConfirmPanel.transform.Find("ObjBtns/BtnConfirm").gameObject;
		m_BtnCancel = uConfirmPanel.transform.Find("ObjBtns/BtnCancel").gameObject;
		m_ErrTxtTitle = uErrorPanel.transform.Find("TxtTitle").GetComponent<Text>();
		m_ErrTxtContent = uErrorPanel.transform.Find("TxtContent").GetComponent<Text>();
	}

	private void Update()
	{
		topShowTimer -= Time.deltaTime;
		if (topShowTimer <= 0f && !isForver)
		{
			uTopTintPanel.SetActive(value: false);
		}
	}

	public void vk(string a, float b = 1.5f)
	{
		m_LblContreTint.text = a;
		uContreTintPanel.SetActive(value: true);
		StartCoroutine(vl(b));
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator vl(float a)
	{
		return new a(0)
		{
			ba = this,
			jw = a
		};
	}

	public void vm(string a, float b = 1.5f)
	{
		m_LblTopTint.text = a;
		uTopTintPanel.SetActive(value: true);
		topShowTimer = b;
	}

	public void vn(string a)
	{
		m_LblTopTint.text = a;
		uTopTintPanel.SetActive(value: true);
		isForver = true;
	}

	public void vo()
	{
		uTopTintPanel.SetActive(value: false);
		isForver = false;
	}

	public void vp(string a, string b, Action c = null, bool d = false)
	{
		uConfirmPanel.SetActive(value: true);
		m_CfmTxtTitle.text = a;
		m_CfmTxtContent.text = b;
		m_BtnCancel.SetActive(d);
		if (c == null)
		{
			m_BtnConfirm.SetActive(value: false);
			m_BtnCancel.SetActive(value: true);
			OnClickConfirm = null;
		}
		else
		{
			m_BtnConfirm.SetActive(value: true);
			OnClickConfirm = c;
		}
	}

	public void BtnConfirmClick()
	{
		OnClickConfirm?.Invoke();
		BtnCancelClick();
	}

	public void BtnCancelClick()
	{
		uConfirmPanel.SetActive(value: false);
	}

	public void vq(string a, string b)
	{
		uErrorPanel.SetActive(value: true);
		m_ErrTxtTitle.text = a;
		m_ErrTxtContent.text = b;
	}

	public void vr()
	{
		uErrorPanel.SetActive(value: false);
		m_ErrTxtTitle.text = "";
		m_ErrTxtContent.text = "";
	}
}
