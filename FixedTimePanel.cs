using UnityEngine;
using UnityEngine.UI;

public class FixedTimePanel : MonoBehaviour
{
	[SerializeField]
	private Text m_LblEveryTime;

	[SerializeField]
	private Text m_LblIsFixedTime;

	[SerializeField]
	private Toggle m_TogIsFixedTime;

	[SerializeField]
	private Dropdown m_DroSelectTime;

	[SerializeField]
	private GameObject mObjSelect;

	private void OnEnable()
	{
		m_LblEveryTime.text = GameEntry.s.ib("FixedTimeLabel1");
		m_LblIsFixedTime.text = GameEntry.s.ib("FixedTimeLabel2");
		m_TogIsFixedTime.isOn = GameEntry.u.fz.ft;
		mObjSelect.SetActive(m_TogIsFixedTime.isOn);
		m_DroSelectTime.options.Clear();
		uq(GameEntry.s.ib("GameTime1"));
		uq(GameEntry.s.ib("GameTime2"));
		uq(GameEntry.s.ib("GameTime3"));
		uq(GameEntry.s.ib("GameTime4"));
		uq(GameEntry.s.ib("GameTime5"));
		uq(GameEntry.s.ib("GameTime6"));
		uq(GameEntry.s.ib("GameTime7"));
		uq(GameEntry.s.ib("GameTime8"));
		uq(GameEntry.s.ib("GameTime9"));
		uq(GameEntry.s.ib("GameTime10"));
		m_DroSelectTime.value = GameEntry.u.fz.fu - 1;
		m_DroSelectTime.transform.Find("Label").GetComponent<Text>().text = m_DroSelectTime.options[GameEntry.u.fz.fu - 1].text;
	}

	public void OnTogIsFixedTime()
	{
		mObjSelect.SetActive(m_TogIsFixedTime.isOn);
	}

	private void uq(string a)
	{
		Dropdown.OptionData optionData = new Dropdown.OptionData();
		optionData.text = a;
		m_DroSelectTime.options.Add(optionData);
	}

	public void OnBtnConfirmClick()
	{
		MainControl.LobbyPanel.tg(m_TogIsFixedTime.isOn, m_DroSelectTime.value + 1);
		OnBtnCancelClick();
	}

	public void OnBtnCancelClick()
	{
		Object.Destroy(base.gameObject);
	}

	private void OnDestroy()
	{
		m_LblEveryTime = null;
		m_LblIsFixedTime = null;
		m_TogIsFixedTime = null;
		m_DroSelectTime = null;
		mObjSelect = null;
	}
}
