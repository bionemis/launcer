using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
	[SerializeField]
	private RectTransform uObjSettingPanel;

	public string mBossPsw;

	public string mEmployeePsw;

	public static bool IsSetting;

	public static bool isKeyMove;

	private Vector2 currVecMouse;

	private Vector2 lastVecMouse;

	private float mouseTimer;

	public static MainControl a { get; private set; }

	public static LogoPanel LogoPanel { get; private set; }

	public static LobbyPanel LobbyPanel { get; private set; }

	public static TintPanel TintPanel { get; private set; }

	public static ProScreenPanel ProScreenPanel { get; private set; }

	public static RectTransform be { get; private set; }

	private void Awake()
	{
		a = this;
		List<string> list = GameEntry.u.mw();
		mBossPsw = list[0];
		mEmployeePsw = list[1];
		LogoPanel = base.transform.Find("LogoPanel").GetComponent<LogoPanel>();
		LobbyPanel = base.transform.Find("LobbyPanel").GetComponent<LobbyPanel>();
		TintPanel = base.transform.Find("TintPanel").GetComponent<TintPanel>();
		be = base.transform.Find("UiItemPool").GetComponent<RectTransform>();
	}

	private void Start()
	{
		ProScreenPanel = dj<ProScreenPanel>("ProScreenPanel", GameEntry.a.transform);
	}

	private void FixedUpdate()
	{
		currVecMouse = Input.mousePosition;
		mouseTimer += Time.deltaTime;
		if (mouseTimer >= 0.2f)
		{
			mouseTimer = 0f;
			lastVecMouse = currVecMouse;
		}
		isKeyMove = Vector3.Distance(currVecMouse, lastVecMouse) > 10f || Input.GetMouseButtonDown(0);
	}

	public a dj<a>(string a, Transform b = null) where a : MonoBehaviour
	{
		RectTransform rectTransform = Object.Instantiate(Resources.Load<RectTransform>("Panel/" + a));
		if (b == null)
		{
			rectTransform.SetParent(uObjSettingPanel);
		}
		else
		{
			rectTransform.SetParent(b);
		}
		rectTransform.localPosition = Vector3.zero;
		rectTransform.localEulerAngles = Vector3.zero;
		rectTransform.localScale = Vector3.one;
		rectTransform.sizeDelta = Vector2.zero;
		return rectTransform.GetComponent<a>();
	}
}
