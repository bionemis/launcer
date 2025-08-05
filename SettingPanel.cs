using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
	[SerializeField]
	private InputField uIptPsw;

	[SerializeField]
	private InputField uIptMachineCode;

	[SerializeField]
	private Button uBtnUpdateContent;

	[SerializeField]
	private Button uBtnModifyPsw;

	[SerializeField]
	private Button uBtnAccounts;

	[SerializeField]
	private Button uBtnClearAccounts;

	[SerializeField]
	private Button uBtnFixedTime;

	[SerializeField]
	private Dropdown uDroSelectedLan;

	[SerializeField]
	private Button uBtnBaseSetting;

	[SerializeField]
	private Button uBtnCoding;

	private void Awake()
	{
		GameEntry.r.gs(1004, tw);
		GameEntry.r.gs(1002, ty);
	}

	private void Start()
	{
		List<string> list = GameEntry.u.mw();
		MainControl.a.mBossPsw = list[0];
		MainControl.a.mEmployeePsw = list[1];
		uIptMachineCode.text = "2022092400140001";
		uDroSelectedLan.value = (int)GameEntry.s.bo;
		uBtnUpdateContent.onClick.AddListener(ts);
		uBtnModifyPsw.onClick.AddListener(tt);
		uBtnAccounts.onClick.AddListener(tu);
		uBtnClearAccounts.onClick.AddListener(tv);
		uBtnFixedTime.onClick.AddListener(tz);
		uDroSelectedLan.onValueChanged.AddListener(tx);
		uBtnBaseSetting.onClick.AddListener(ua);
		uBtnCoding.onClick.AddListener(ub);
		SetBtnActive(ci.None);
		MainControl.IsSetting = true;
	}

	public void NumOnClick(int a)
	{
		uIptPsw.text += a;
	}

	public void ConfirmOnClick()
	{
		if (uIptPsw.text.Equals(MainControl.a.mBossPsw))
		{
			SetBtnActive(ci.Admin);
		}
		else if (uIptPsw.text.Equals(MainControl.a.mEmployeePsw))
		{
			SetBtnActive(ci.Employee);
		}
		else
		{
			SetBtnActive(ci.None);
			MainControl.TintPanel.vm(GameEntry.s.ib("LoginFailure"));
		}
		uIptPsw.text = "";
	}

	public void OnBtnClearNumClick()
	{
		uIptPsw.text = "";
	}

	public void OnBtnQuitClik()
	{
		uIptPsw.text = null;
		Object.Destroy(base.gameObject);
	}

	private void SetBtnActive(ci a)
	{
		switch (a)
		{
		case ci.None:
			uBtnUpdateContent.gameObject.SetActive(value: false);
			uBtnModifyPsw.gameObject.SetActive(value: false);
			uBtnAccounts.gameObject.SetActive(value: false);
			uBtnClearAccounts.gameObject.SetActive(value: false);
			uBtnFixedTime.gameObject.SetActive(value: false);
			uDroSelectedLan.gameObject.SetActive(value: false);
			uBtnBaseSetting.gameObject.SetActive(value: false);
			uBtnCoding.gameObject.SetActive(value: false);
			break;
		case ci.Employee:
			uBtnUpdateContent.gameObject.SetActive(value: false);
			uBtnModifyPsw.gameObject.SetActive(value: false);
			uBtnAccounts.gameObject.SetActive(value: true);
			uBtnClearAccounts.gameObject.SetActive(value: true);
			uBtnFixedTime.gameObject.SetActive(value: false);
			uDroSelectedLan.gameObject.SetActive(value: false);
			uBtnBaseSetting.gameObject.SetActive(value: false);
			uBtnCoding.gameObject.SetActive(value: false);
			break;
		case ci.Admin:
			uBtnUpdateContent.gameObject.SetActive(value: true);
			uBtnModifyPsw.gameObject.SetActive(value: true);
			uBtnAccounts.gameObject.SetActive(value: true);
			uBtnClearAccounts.gameObject.SetActive(value: true);
			uBtnFixedTime.gameObject.SetActive(value: true);
			uDroSelectedLan.gameObject.SetActive(value: true);
			uBtnBaseSetting.gameObject.SetActive(value: true);
			uBtnCoding.gameObject.SetActive(value: true);
			break;
		}
	}

	private void ts()
	{
		MainControl.a.dj<UpdateContentPanel>("UpdateContentPanel");
	}

	private void tt()
	{
		MainControl.a.dj<ModifyPswPanel>("ModifyPswPanel");
	}

	private void tu()
	{
		MainControl.a.dj<AccountsPanel>("AccountsPanel");
	}

	private void tv()
	{
		MainControl.a.dj<ClearAccountsPanel>("ClearAccountsPanel");
	}

	private void tw(object a)
	{
		if (GameEntry.u.nd())
		{
			MainControl.TintPanel.vm(GameEntry.s.ib("ClearPanelSucessful"));
		}
		else
		{
			MainControl.TintPanel.vm(GameEntry.s.ib("ClearPanelFailure"));
		}
	}

	private void tx(int a)
	{
		if (a != (int)GameEntry.s.bo)
		{
			MainControl.a.dj<ChangeLanguagePanel>("ChangeLanguagePanel");
		}
	}

	private void ty(object a)
	{
		if (bool.Parse(a.ToString()))
		{
			GameEntry.u.na(bt.LanguageType, uDroSelectedLan.value.ToString());
			GameEntry.s.ia();
			GameEntry.r.gy(1003);
		}
		else
		{
			uDroSelectedLan.value = (int)GameEntry.s.bo;
		}
	}

	private void tz()
	{
		MainControl.a.dj<FixedTimePanel>("FixedTimePanel");
	}

	private void ua()
	{
		MainControl.a.dj<BaseSettingPanel>("BaseSettingPanel");
	}

	public void ub()
	{
		MainControl.a.dj<CodingPanel>("CodingPanel").ui(GameEntry.z.bu.eu);
	}

	private void OnDestroy()
	{
		uIptPsw = null;
		uIptMachineCode = null;
		uBtnUpdateContent = null;
		uBtnModifyPsw = null;
		uBtnAccounts = null;
		uBtnClearAccounts = null;
		uBtnFixedTime = null;
		uDroSelectedLan = null;
		uBtnBaseSetting = null;
		uBtnCoding = null;
		GameEntry.r.gu(1004, tw);
		GameEntry.r.gu(1002, ty);
		MainControl.IsSetting = false;
	}
}
