using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using d;

public class UpdateContentPanel : MonoBehaviour
{
	[SerializeField]
	private Transform m_Content;

	[SerializeField]
	private Dropdown m_DropAddContentType;

	[SerializeField]
	private Toggle m_TogIsAllSelect;

	[SerializeField]
	private Color m_ItemColor1;

	[SerializeField]
	private Color m_ItemColor2;

	private bool isChange;

	private Dictionary<bq, List<cg>> AddContentInfoDic;

	private Dictionary<bq, List<UiUpdateContentItem>> AddAddContentItemDic;

	private Transform mResItem;

	[SerializeField]
	private Transform uItemPool;

	private Queue<Transform> mQueItem = new Queue<Transform>();

	private void Awake()
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		m_DropAddContentType.ClearOptions();
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("AddContentTypeAdded")));
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("AddContentTypeNotAdded")));
		list.Add(new Dropdown.OptionData(GameEntry.s.ib("AddContentTypeAddFailed")));
		m_DropAddContentType.AddOptions(list);
		m_DropAddContentType.value = 0;
		m_TogIsAllSelect.isOn = false;
		isChange = false;
		mResItem = Resources.Load<Transform>("Item/UpdateContentItem");
		OnBtnReloadClick();
	}

	public void OnBtnSaveClick()
	{
		bool flag = false;
		foreach (cg item in AddContentInfoDic[bq.Added])
		{
			if (item.gx)
			{
				bn gy = item.gy;
				flag = GameEntry.u.mt(gy.SqlName, gy.ContentPattern, gy.ContentType, gy.StarNum, gy.IsShow);
			}
		}
		foreach (cg item2 in AddContentInfoDic[bq.NeedAdd])
		{
			if (item2.gx)
			{
				bn gy2 = item2.gy;
				flag = GameEntry.u.mr(gy2.SqlName, gy2.ContentPattern, gy2.ContentType, gy2.StarNum, gy2.IsShow);
			}
		}
		foreach (cg item3 in AddContentInfoDic[bq.AddFailed])
		{
			if (item3.gx)
			{
				flag = GameEntry.u.ms(item3.gy.SqlName);
			}
		}
		if (flag)
		{
			MainControl.TintPanel.vm(GameEntry.s.ib("ChangeContentSuccess"));
		}
		else
		{
			MainControl.TintPanel.vm(GameEntry.s.ib("ChangeContentFailure"));
		}
		isChange = true;
		OnBtnReloadClick();
	}

	public void OnBtnReturnClick()
	{
		if (isChange)
		{
			MainControl.LobbyPanel.uRankingList.rk(delegate
			{
				vh(a: true);
			});
		}
		else
		{
			vh(a: true);
		}
	}

	public void OnBtnReloadClick()
	{
		vd();
		ve();
		m_TogIsAllSelect.isOn = false;
	}

	public void OnDropAddContentTypeClick()
	{
		ve();
		m_TogIsAllSelect.isOn = false;
	}

	public void OnTogIsAllSelectClick()
	{
		bq value = (bq)m_DropAddContentType.value;
		int count = AddContentInfoDic[value].Count;
		if (count != 0)
		{
			for (int num = 0; num < count; num++)
			{
				AddContentInfoDic[value][num].gx = m_TogIsAllSelect.isOn;
				AddAddContentItemDic[value][num].m_TogSelect.isOn = m_TogIsAllSelect.isOn;
			}
		}
	}

	private void vd()
	{
		if (AddContentInfoDic != null)
		{
			AddContentInfoDic.Clear();
		}
		else
		{
			AddContentInfoDic = new Dictionary<bq, List<cg>>();
		}
		AddContentInfoDic[bq.NeedAdd] = new List<cg>();
		AddContentInfoDic[bq.Added] = new List<cg>();
		AddContentInfoDic[bq.AddFailed] = new List<cg>();
		List<bn> list = GameEntry.u.mq(bp.All);
		List<bn> list2 = list;
		if (Directory.Exists(h.e))
		{
			DirectoryInfo[] directories = new DirectoryInfo(h.e).GetDirectories();
			foreach (DirectoryInfo directoryInfo in directories)
			{
				bool flag = false;
				foreach (bn item in list)
				{
					if (directoryInfo.Name.Equals(item.SqlName))
					{
						cg cg2 = new cg(bq.Added, b: false, item);
						cf cf2 = new cf(directoryInfo.FullName + "/Setting.xml");
						if (cf2.og())
						{
							cg2.ContentName = cf2.ContentName;
							cf2.Close();
						}
						AddContentInfoDic[bq.Added].Add(cg2);
						list2.Remove(item);
						flag = true;
						break;
					}
				}
				if (flag)
				{
					continue;
				}
				cf cf3 = new cf(directoryInfo.FullName + "/Setting.xml");
				if (cf3.og())
				{
					string contentName = cf3.ContentName;
					cf3.Close();
					if (File.Exists(directoryInfo.FullName + "/1.png"))
					{
						bn bn2 = new bn(directoryInfo.Name, 0, ContentPattern.Cinema, ContentType.Children, 0, f: true, DateTime.Now);
						cg cg3 = new cg(bq.NeedAdd, b: false, bn2);
						cg3.ContentName = contentName;
						AddContentInfoDic[bq.NeedAdd].Add(cg3);
					}
				}
			}
			{
				foreach (bn item2 in list2)
				{
					cg cg4 = new cg(bq.AddFailed, b: false, item2);
					cg4.ContentName = "XXX";
					AddContentInfoDic[bq.AddFailed].Add(cg4);
				}
				return;
			}
		}
		MainControl.TintPanel.vk("Content path error !");
	}

	private void ve()
	{
		bq value = (bq)m_DropAddContentType.value;
		if (AddAddContentItemDic != null)
		{
			AddAddContentItemDic.Clear();
		}
		else
		{
			AddAddContentItemDic = new Dictionary<bq, List<UiUpdateContentItem>>();
		}
		AddAddContentItemDic[value] = new List<UiUpdateContentItem>();
		vh();
		int num = 0;
		List<cg> list = AddContentInfoDic[value];
		if (list.Count == 0)
		{
			return;
		}
		for (int num2 = 0; num2 < list.Count; num2++)
		{
			Transform obj = vg();
			obj.SetParent(m_Content);
			obj.localScale = Vector3.one;
			obj.localPosition = Vector3.zero;
			obj.gameObject.SetActive(value: true);
			Transform transform = obj.Find("Bg");
			if (num % 2 == 1)
			{
				transform.GetComponent<Image>().color = m_ItemColor1;
			}
			else
			{
				transform.GetComponent<Image>().color = m_ItemColor2;
			}
			UiUpdateContentItem component = obj.GetComponent<UiUpdateContentItem>();
			component.rc(list[num2], num, this);
			AddAddContentItemDic[value].Add(component);
			num++;
		}
	}

	public void vf(bq a, int b, bool c, ContentPattern d, ContentType e, int f, bool g)
	{
		if (b >= 0 || b <= AddContentInfoDic[a].Count - 1)
		{
			bn gy = AddContentInfoDic[a][b].gy;
			bn bn2 = new bn(gy.SqlName, gy.PlayCount, d, e, f, g, gy.CreateTime);
			cg value = new cg(a, c, bn2);
			AddContentInfoDic[a][b] = value;
		}
	}

	private Transform vg()
	{
		if (mQueItem.Count > 0)
		{
			return mQueItem.Dequeue();
		}
		return UnityEngine.Object.Instantiate(mResItem);
	}

	private void vh(bool a = false)
	{
		UiUpdateContentItem[] componentsInChildren = m_Content.GetComponentsInChildren<UiUpdateContentItem>();
		int num = componentsInChildren.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			if (a)
			{
				UnityEngine.Object.Destroy(m_Content.GetChild(num2).gameObject);
				continue;
			}
			Transform transform = componentsInChildren[num2].transform;
			transform.SetParent(uItemPool);
			mQueItem.Enqueue(transform);
		}
		if (a)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		try
		{
			AddContentInfoDic.Clear();
			AddContentInfoDic = null;
			AddAddContentItemDic.Clear();
			AddAddContentItemDic = null;
		}
		catch (Exception)
		{
		}
	}

	[CompilerGenerated]
	private void vi()
	{
		vh(a: true);
	}
}
