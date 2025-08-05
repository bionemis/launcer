using UnityEngine;

public class ClearAccountsPanel : MonoBehaviour
{
	public void OnBtnSureClick()
	{
		GameEntry.r.gy(1004);
		OnBtnCancelClick();
	}

	public void OnBtnCancelClick()
	{
		Object.Destroy(base.gameObject);
	}
}
