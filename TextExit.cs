using UnityEngine;
using UnityEngine.UI;

public class TextExit : Text
{
	[HideInInspector]
	public string m_LocalizationName;

	[HideInInspector]
	public bool m_IsAddMaoHao;

	private float m_Lucency = 1f;

	private Color m_ColorLucency;

	public float p
	{
		get
		{
			m_ColorLucency = GetComponent<TextExit>().color;
			return m_ColorLucency.a;
		}
		set
		{
			m_Lucency = value;
			m_ColorLucency = GetComponent<TextExit>().color;
			GetComponent<TextExit>().color = new Color(m_ColorLucency.r, m_ColorLucency.g, m_ColorLucency.b, m_Lucency);
		}
	}

	protected override void Start()
	{
		base.Start();
		if (GameEntry.s != null)
		{
			text = GameEntry.s.ib(m_LocalizationName);
			if (m_IsAddMaoHao)
			{
				text += ":";
			}
			GameEntry.r.gs(1003, bf);
		}
	}

	private void bf(object a)
	{
		text = GameEntry.s.ib(m_LocalizationName);
		if (m_IsAddMaoHao)
		{
			text += ":";
		}
	}

	protected override void OnDestroy()
	{
		GameEntry.r?.gu(1003, bf);
	}
}
