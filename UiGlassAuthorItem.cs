using UnityEngine;
using UnityEngine.UI;

public class UiGlassAuthorItem : MonoBehaviour
{
	[SerializeField]
	private Button uBtnCoding;

	[SerializeField]
	private InputField uIptGlassImei;

	public string cu { get; private set; }

	public void qv(bool a, string b)
	{
		uBtnCoding.interactable = !a;
		cu = b;
		uIptGlassImei.text = cu;
	}

	public void OnBtnCodingClick()
	{
		GameEntry.r.gx(1013, uIptGlassImei.text);
	}

	public void Close()
	{
		cu = null;
		uBtnCoding = null;
		uIptGlassImei = null;
		Object.Destroy(base.gameObject);
	}
}
