using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using d;

public class UiUpdateContentItem : MonoBehaviour
{
	[SerializeField]
	public Toggle m_TogSelect;

	[SerializeField]
	private Text m_LblSqlName;

	[SerializeField]
	private Text m_LblContentName;

	[SerializeField]
	private Dropdown m_DropContentType;

	[SerializeField]
	private Dropdown m_DropContentPattern;

	[SerializeField]
	private Dropdown m_DropStarNum;

	[SerializeField]
	private Toggle m_TogIsShow;

	private cg mAddInfo;

	private int ItemIndex;

	private UpdateContentPanel mPanel;

	public void rc(cg a, int b, UpdateContentPanel c)
	{
		mAddInfo = a;
		ItemIndex = b;
		mPanel = c;
		m_TogSelect.isOn = mAddInfo.gx;
		m_LblSqlName.text = mAddInfo.gy.SqlName;
		m_LblContentName.text = mAddInfo.ContentName;
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		m_DropContentType.ClearOptions();
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentTypeChildren")));
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentTypeHorrible")));
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentTypeAdventure")));
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentTypeOther")));
		m_DropContentType.AddOptions(list);
		switch (mAddInfo.gy.ContentType)
		{
		case ContentType.Children:
			m_DropContentType.value = 0;
			break;
		case ContentType.Horrible:
			m_DropContentType.value = 1;
			break;
		case ContentType.Adventure:
			m_DropContentType.value = 2;
			break;
		default:
			m_DropContentType.value = 3;
			break;
		}
		List<Dropdown.OptionData> list2 = new List<Dropdown.OptionData>();
		m_DropContentPattern.ClearOptions();
		list2.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentPatternInteraction")));
		list2.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentPatternPanorama")));
		list2.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentPatternCinema")));
		list2.Add(new Dropdown.OptionData(GameEntry.s.ib("ContentPatternOther")));
		m_DropContentPattern.AddOptions(list2);
		switch (mAddInfo.gy.ContentPattern)
		{
		case ContentPattern.Interaction:
			m_DropContentPattern.value = 0;
			break;
		case ContentPattern.Panorama:
			m_DropContentPattern.value = 1;
			break;
		case ContentPattern.Cinema:
			m_DropContentPattern.value = 2;
			break;
		default:
			m_DropContentPattern.value = 3;
			break;
		}
		m_DropStarNum.value = mAddInfo.gy.StarNum;
		m_TogIsShow.isOn = mAddInfo.gy.IsShow;
	}

	public void OnTogSelectClick()
	{
		mAddInfo.gx = m_TogSelect.isOn;
		rd();
	}

	public void OnDropContentTypeClick()
	{
		ContentType contentType = ContentType.Other;
		switch (m_DropContentType.value)
		{
		case 0:
			contentType = ContentType.Children;
			break;
		case 1:
			contentType = ContentType.Horrible;
			break;
		case 2:
			contentType = ContentType.Adventure;
			break;
		case 3:
			contentType = ContentType.Adventure;
			break;
		default:
			contentType = ContentType.Other;
			break;
		}
		mAddInfo.gy.ContentType = contentType;
		rd();
	}

	public void OnDropContentPatternClick()
	{
		ContentPattern contentPattern = ContentPattern.Interaction;
		switch (m_DropContentPattern.value)
		{
		case 0:
			contentPattern = ContentPattern.Interaction;
			break;
		case 1:
			contentPattern = ContentPattern.Panorama;
			break;
		case 2:
			contentPattern = ContentPattern.Cinema;
			break;
		default:
			contentPattern = ContentPattern.Other;
			break;
		}
		mAddInfo.gy.ContentPattern = contentPattern;
		rd();
	}

	public void OnDropStarNumClick()
	{
		mAddInfo.gy.StarNum = m_DropStarNum.value;
		rd();
	}

	public void OnTogIsShowClick()
	{
		mAddInfo.gy.IsShow = m_TogIsShow.isOn;
		rd();
	}

	private void rd()
	{
		if (mAddInfo.gx)
		{
			bn gy = mAddInfo.gy;
			mPanel.vf(mAddInfo.gw, ItemIndex, mAddInfo.gx, gy.ContentPattern, gy.ContentType, gy.StarNum, gy.IsShow);
		}
	}
}
