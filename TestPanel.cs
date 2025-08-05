using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
	public Toggle vat1;

	public Toggle vat2;

	public Toggle vat3;

	public Toggle vat4;

	public Toggle vat5;

	public Toggle vat6;

	public Toggle special1;

	public Toggle special2;

	public Toggle special3;

	public Toggle special4;

	public Toggle special5;

	public Toggle special6;

	public Text vatText;

	public Text specailText;

	public Text vatOpenText;

	public Text VatCloseText;

	public Text specialOpenText;

	public Text specialCloseText;

	[Tooltip("旋转测试label")]
	[SerializeField]
	private Text rotation_label;

	[Tooltip("旋转测试发送text")]
	[SerializeField]
	private Text rotation_send_text;

	[Tooltip("左旋转toggle")]
	[SerializeField]
	private Toggle rotation_left;

	[Tooltip("右旋转toggle")]
	[SerializeField]
	private Toggle rotation_right;

	[Tooltip("停止旋转toggle")]
	[SerializeField]
	private Toggle rotation_stop;

	public Text qiutText;

	private void Start()
	{
		vat1.isOn = false;
		vat2.isOn = false;
		vat3.isOn = false;
		vat4.isOn = false;
		vat5.isOn = false;
		vat6.isOn = false;
		special1.isOn = false;
		special2.isOn = false;
		special3.isOn = false;
		special4.isOn = false;
		special5.isOn = false;
		special6.isOn = false;
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		rotation_label.gameObject.SetActive(value: false);
		vat1.isOn = false;
		vat2.isOn = false;
		vat3.isOn = false;
		vat4.isOn = false;
		vat5.isOn = false;
		vat6.isOn = false;
		special1.isOn = false;
		special2.isOn = false;
		special3.isOn = false;
		special4.isOn = false;
		special5.isOn = false;
		special6.isOn = false;
		vatText.text = GameEntry.s.ib("Cylinder");
		specailText.text = GameEntry.s.ib("Special");
		vat1.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("CylinderName1");
		vat2.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("CylinderName2");
		vat3.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("CylinderName3");
		vat4.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("CylinderName4");
		vat5.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("CylinderName5");
		vat6.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("CylinderName6");
		if (GameEntry.t.PlayCount == 6 || GameEntry.t.PlayCount == 4)
		{
			special1.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName1");
			special2.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName2");
			special3.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName3");
			special4.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName4");
			special5.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName5");
			special6.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName6");
			if (GameEntry.t.PlayCount == 4)
			{
				vat4.gameObject.SetActive(value: false);
				vat5.gameObject.SetActive(value: false);
				vat6.gameObject.SetActive(value: false);
			}
		}
		else if (GameEntry.t.PlayCount == 2)
		{
			special1.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("TwoSpecialName1");
			special2.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("TwoSpecialName2");
			special3.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("TwoSpecialName3");
			special4.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("TwoSpecialName4");
			special5.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("TwoSpecialName5");
			special6.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("TwoSpecialName6");
			vat4.gameObject.SetActive(value: false);
			vat5.gameObject.SetActive(value: false);
			vat6.gameObject.SetActive(value: false);
		}
		else if (GameEntry.t.PlayCount == 3)
		{
			rotation_label.text = GameEntry.s.ib("RotationLabel");
			rotation_send_text.text = GameEntry.s.ib("RotationSend");
			rotation_left.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("RotationLeft");
			rotation_right.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("RotationRight");
			rotation_stop.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("RotationStop");
			vat4.gameObject.SetActive(value: false);
			vat5.gameObject.SetActive(value: false);
			vat6.gameObject.SetActive(value: false);
			rotation_label.gameObject.SetActive(value: true);
			special1.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName1");
			special2.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName2");
			special3.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName3");
			special4.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName4");
			special5.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName5");
			special6.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName6");
		}
		else
		{
			special1.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName1");
			special2.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName2");
			special3.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName3");
			special4.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName4");
			special5.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName5");
			special6.transform.Find("Label").GetComponent<Text>().text = GameEntry.s.ib("SixSpecialName6");
		}
		vatOpenText.text = GameEntry.s.ib("CylinderBtn1");
		VatCloseText.text = GameEntry.s.ib("CylinderBtn2");
		specialOpenText.text = GameEntry.s.ib("TwoSpecialBtn1");
		specialCloseText.text = GameEntry.s.ib("TwoSpecialBtn2");
		qiutText.text = GameEntry.s.ib("Qiut");
	}

	public void QiutPanel()
	{
		base.gameObject.SetActive(value: false);
	}

	public void VatOpen()
	{
		_ = vat1.isOn;
		_ = vat2.isOn;
		_ = vat3.isOn;
		_ = vat4.isOn;
		_ = vat5.isOn;
		_ = vat6.isOn;
	}

	public void VatClose()
	{
	}

	public void OnBtnSpecialOpenClick()
	{
		if (special1.isOn)
		{
			_ = 2;
		}
		else
			_ = 0;
		int num = (special2.isOn ? 4 : 0);
		int num2 = (special3.isOn ? 8 : 0);
		int num3 = (special4.isOn ? 16 : 0);
		int num4 = (special5.isOn ? 32 : 0);
		int num5 = (special6.isOn ? 64 : 0);
	}

	public void OnBtnSpecialCloseClick()
	{
	}
}
