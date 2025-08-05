using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AccountsPanel : MonoBehaviour
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
				z = new WaitForSeconds(3f);
				y = 1;
				return true;
			case 1:
				y = -1;
				ja.SetActive(value: false);
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

	public Dropdown firstYearDrop;

	public Dropdown firstMonthDrop;

	public Dropdown firstDayDrop;

	public Dropdown secondYearDrop;

	public Dropdown secondMonthDrop;

	public Dropdown secondDayDrop;

	public Transform parent;

	public Text totalTimesText;

	public Text totalPopuText;

	private int jm;

	private int jn;

	private int jo;

	private int jp;

	private GameObject jq;

	private void Awake()
	{
		jq = Resources.Load<GameObject>("Item/SqlContentLog");
		firstYearDrop.options.Clear();
		for (int num = 2017; num <= 2030; num++)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			optionData.text = num.ToString();
			firstYearDrop.options.Add(optionData);
		}
		firstYearDrop.transform.Find("Label").GetComponent<Text>().text = firstYearDrop.options[0].text;
		firstMonthDrop.options.Clear();
		for (int num2 = 1; num2 <= 12; num2++)
		{
			Dropdown.OptionData optionData2 = new Dropdown.OptionData();
			optionData2.text = num2.ToString();
			firstMonthDrop.options.Add(optionData2);
		}
		firstMonthDrop.transform.Find("Label").GetComponent<Text>().text = firstMonthDrop.options[0].text;
		jo = DateTime.DaysInMonth(Convert.ToInt32(firstYearDrop.options[firstYearDrop.value].text), Convert.ToInt32(firstMonthDrop.options[firstMonthDrop.value].text));
		firstDayDrop.options.Clear();
		for (int num3 = 1; num3 <= jo; num3++)
		{
			Dropdown.OptionData optionData3 = new Dropdown.OptionData();
			optionData3.text = num3.ToString();
			firstDayDrop.options.Add(optionData3);
		}
		firstDayDrop.transform.Find("Label").GetComponent<Text>().text = firstDayDrop.options[0].text;
		secondYearDrop.options.Clear();
		for (int num4 = 2017; num4 <= 2030; num4++)
		{
			Dropdown.OptionData optionData4 = new Dropdown.OptionData();
			optionData4.text = num4.ToString();
			secondYearDrop.options.Add(optionData4);
		}
		secondYearDrop.transform.Find("Label").GetComponent<Text>().text = secondYearDrop.options[0].text;
		secondMonthDrop.options.Clear();
		for (int num5 = 1; num5 <= 12; num5++)
		{
			Dropdown.OptionData optionData5 = new Dropdown.OptionData();
			optionData5.text = num5.ToString();
			secondMonthDrop.options.Add(optionData5);
		}
		secondMonthDrop.transform.Find("Label").GetComponent<Text>().text = secondMonthDrop.options[0].text;
		jp = DateTime.DaysInMonth(Convert.ToInt32(secondYearDrop.options[secondYearDrop.value].text), Convert.ToInt32(secondMonthDrop.options[secondMonthDrop.value].text));
		secondDayDrop.options.Clear();
		for (int num6 = 1; num6 <= jp; num6++)
		{
			Dropdown.OptionData optionData6 = new Dropdown.OptionData();
			optionData6.text = num6.ToString();
			secondDayDrop.options.Add(optionData6);
		}
		secondDayDrop.transform.Find("Label").GetComponent<Text>().text = secondDayDrop.options[0].text;
	}

	private void Start()
	{
		jm = 0;
		jn = 0;
		int year = DateTime.Now.Year;
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		firstYearDrop.value = year - 2017;
		firstMonthDrop.value = month - 1;
		firstDayDrop.value = day - 1;
		secondYearDrop.value = year - 2017;
		secondMonthDrop.value = month - 1;
		secondDayDrop.value = day - 1;
		string text = $"{firstYearDrop.options[firstYearDrop.value].text}/{firstMonthDrop.options[firstMonthDrop.value].text},{firstDayDrop.options[firstDayDrop.value].text} 00:00:00";
		string text2 = $"{secondYearDrop.options[secondYearDrop.value].text}/{secondMonthDrop.options[secondMonthDrop.value].text},{secondDayDrop.options[secondDayDrop.value].text} 23:59:00";
		Check(text, text2);
	}

	public void FirstOnValueChange(int a)
	{
		jo = DateTime.DaysInMonth(Convert.ToInt32(firstYearDrop.options[firstYearDrop.value].text), Convert.ToInt32(firstMonthDrop.options[firstMonthDrop.value].text));
		firstDayDrop.options.Clear();
		for (int num = 1; num <= jo; num++)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			optionData.text = num.ToString();
			firstDayDrop.options.Add(optionData);
		}
		firstDayDrop.value = 0;
	}

	public void SecondOnValueChange(int a)
	{
		jp = DateTime.DaysInMonth(Convert.ToInt32(secondYearDrop.options[secondYearDrop.value].text), Convert.ToInt32(secondMonthDrop.options[secondMonthDrop.value].text));
		secondDayDrop.options.Clear();
		for (int num = 1; num <= jp; num++)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			optionData.text = num.ToString();
			secondDayDrop.options.Add(optionData);
		}
		secondDayDrop.value = 0;
	}

	public void StatisticsOnClick()
	{
		string text = $"{firstYearDrop.options[firstYearDrop.value].text}/{firstMonthDrop.options[firstMonthDrop.value].text},{firstDayDrop.options[firstDayDrop.value].text} 00:00:00";
		string text2 = $"{secondYearDrop.options[secondYearDrop.value].text}/{secondMonthDrop.options[secondMonthDrop.value].text},{secondDayDrop.options[secondDayDrop.value].text} 23:59:00";
		Check(text, text2);
	}

	public void Check(string a, string b)
	{
		jm = 0;
		jn = 0;
		for (int num = 0; num < parent.childCount; num++)
		{
			UnityEngine.Object.Destroy(parent.GetChild(num).gameObject);
		}
		List<bo> list = new List<bo>();
		list = GameEntry.u.nb(a, b);
		int count = list.Count;
		for (int num2 = 0; num2 < count; num2++)
		{
			bo bo2 = list[num2];
			Transform obj = UnityEngine.Object.Instantiate(jq).transform;
			obj.transform.SetParent(parent);
			obj.localPosition = Vector3.zero;
			obj.localScale = Vector3.one;
			obj.transform.Find("ID").GetComponent<Text>().text = (num2 + 1).ToString();
			obj.transform.Find("PlayerCount").GetComponent<Text>().text = bo2.PlayerCount.ToString();
			obj.transform.Find("ContentName").GetComponent<Text>().text = bo2.ContentName;
			TimeSpan timeSpan = new TimeSpan(0, 0, bo2.PlayTime);
			obj.transform.Find("PlayTime").GetComponent<Text>().text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
			obj.transform.Find("CreateTime").GetComponent<Text>().text = bo2.CreateTime.ToString();
			jm += bo2.PlayerCount;
			jn++;
		}
		totalPopuText.text = jm.ToString();
		totalTimesText.text = jn.ToString();
		if (count > 10)
		{
			parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, count * 50);
		}
		else
		{
			parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, 450f);
		}
	}

	private static byte[] ud(string a)
	{
		a = a.Replace(" ", "");
		if (a.Length % 2 != 0)
		{
			a += " ";
		}
		byte[] array = new byte[a.Length / 2];
		for (int num = 0; num < array.Length; num++)
		{
			array[num] = Convert.ToByte(a.Substring(num * 2, 2), 16);
		}
		return array;
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator ue(GameObject a)
	{
		return new a(0)
		{
			ja = a
		};
	}

	private bool uf(string a)
	{
		byte b2 = 0;
		byte[] array = ud(a);
		for (int num = 0; num < array.Length - 1; num++)
		{
			b2 += array[num];
		}
		if (array[array.Length - 1] == b2)
		{
			return true;
		}
		return false;
	}

	public void OnBtnQuitClik()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
