using UnityEngine;
using UnityEngine.UI;

public class BaseSettingPanel : MonoSingle<BaseSettingPanel>
{
	[Header("是否开启投币")]
	[SerializeField]
	private BaseSettingItem mObjIsInsertCoins;

	[Header("播放宣传视频")]
	[SerializeField]
	private BaseSettingItem mObjIsPlayPublicityVideo;

	[Header("播放内容视频")]
	[SerializeField]
	private BaseSettingItem mObjIsPlayContentVideo;

	[SerializeField]
	[Header("投屏")]
	private Toggle mTogProjectionScreen;

	private void Start()
	{
		mObjIsInsertCoins.Toggle.isOn = GameEntry.u.fz.fp;
		mObjIsPlayPublicityVideo.Toggle.isOn = GameEntry.u.fz.fv;
		if (GameEntry.a.IsBig)
		{
			mObjIsPlayContentVideo.Obj.SetActive(value: false);
		}
		else
		{
			mObjIsPlayContentVideo.Toggle.isOn = GameEntry.u.fz.fw;
		}
	}

	public void Close()
	{
		bool isOn = mObjIsInsertCoins.Toggle.isOn;
		GameEntry.u.fz.fp = isOn;
		GameEntry.u.na(bt.IsInsertCoins, GameEntry.u.fz.fp.ToString());
		GameEntry.u.na(bt.CoinNum, GameEntry.u.fz.fq.ToString());
		MainControl.LobbyPanel.tc(isOn);
		GameEntry.u.fz.fx = mTogProjectionScreen.isOn;
		GameEntry.u.na(bt.IsProjectionScreen, mTogProjectionScreen.isOn.ToString());
		bool isOn2 = mObjIsPlayPublicityVideo.Toggle.isOn;
		GameEntry.u.fz.fv = isOn2;
		GameEntry.u.na(bt.IsPlayPublicityVideo, isOn2.ToString());
		if (!isOn2)
		{
			MainControl.ProScreenPanel.uw();
		}
		GameEntry.u.fz.fw = mObjIsPlayContentVideo.Toggle.isOn;
		GameEntry.u.na(bt.IsPlayContentVideo, GameEntry.u.fz.fw.ToString());
		Object.Destroy(base.gameObject);
	}
}
