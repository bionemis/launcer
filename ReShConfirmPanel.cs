using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ReShConfirmPanel : MonoBehaviour
{
	[SerializeField]
	private Text mTxtTitle;

	[SerializeField]
	private GameObject mObjConfirm;

	[SerializeField]
	private InputField mIptPsw;

	[SerializeField]
	private Toggle mTogAll;

	[SerializeField]
	private bool mIsShutDown;

	private void Start()
	{
		mObjConfirm.SetActive(value: false);
	}

	private void Update()
	{
	}

	public void ry(bool a)
	{
		mObjConfirm.SetActive(value: false);
		base.gameObject.SetActive(value: true);
		mIsShutDown = a;
		if (a)
		{
			mTxtTitle.text = GameEntry.s.ib("ShutDown");
		}
		else
		{
			mTxtTitle.text = GameEntry.s.ib("Restart");
		}
	}

	public void NumOnClick(int a)
	{
		mIptPsw.text += a;
	}

	public void OnBtnOkClick()
	{
		if (mIptPsw.text.Equals(MainControl.a.mBossPsw))
		{
			mObjConfirm.SetActive(value: true);
		}
		else
		{
			MainControl.TintPanel.vm(GameEntry.s.ib("LoginFailure"));
		}
		mIptPsw.text = "";
	}

	public void OnBtnClearClick()
	{
		mIptPsw.text = "";
	}

	public void CancelOnClick()
	{
		mIptPsw.text = "";
		base.gameObject.SetActive(value: false);
	}

	public void OnBtnConfirmClick()
	{
		if (!mObjConfirm.activeSelf)
		{
			return;
		}
		mObjConfirm.SetActive(value: false);
		StartCoroutine(MainControl.LobbyPanel.tf(mIsShutDown, mTogAll.isOn, delegate
		{
			if (mTogAll.isOn)
			{
				if (mIsShutDown)
				{
					Process.Start("shutdown.exe", "-s -t 3");
				}
				else
				{
					Process.Start("shutdown.exe", "-r -t 3");
				}
			}
		}));
	}

	public void Close()
	{
		CancelOnClick();
	}

	[CompilerGenerated]
	private void rz()
	{
		if (mTogAll.isOn)
		{
			if (mIsShutDown)
			{
				Process.Start("shutdown.exe", "-s -t 3");
			}
			else
			{
				Process.Start("shutdown.exe", "-r -t 3");
			}
		}
	}
}
