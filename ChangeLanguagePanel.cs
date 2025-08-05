using UnityEngine;

public class ChangeLanguagePanel : MonoBehaviour
{
	public void BtnSureChangeLanClik()
	{
		GameEntry.r.gx(1002, true);
		Object.Destroy(base.gameObject);
	}

	public void BtnClosePanelClik()
	{
		GameEntry.r.gx(1002, false);
		Object.Destroy(base.gameObject);
	}
}
