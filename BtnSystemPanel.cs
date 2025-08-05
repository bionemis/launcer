using UnityEngine;

public class BtnSystemPanel : MonoBehaviour
{
	private bool @is;

	private GameObject it;

	private GameObject iu;

	private GameObject iv;

	private Animator iw;

	private Animator ix;

	private Animator iy;

	private float iz;

	public ReShConfirmPanel mReShConfirmPanel;

	private void Awake()
	{
		it = base.transform.Find("BtnQiut").gameObject;
		iu = base.transform.Find("BtnRestart").gameObject;
		iv = base.transform.Find("BtnShutDown").gameObject;
		iw = it.GetComponent<Animator>();
		ix = iu.GetComponent<Animator>();
		iy = iv.GetComponent<Animator>();
	}

	private void Start()
	{
		SystemOnClick();
	}

	private void Update()
	{
		iz += Time.deltaTime;
		if (iz > 10000f)
		{
			iz = 10f;
		}
	}

	public void SystemOnClick()
	{
		if (!(iz <= 0.5f))
		{
			iz = 0f;
			if (@is)
			{
				@is = false;
				iw.Play("Qiut_Stop");
				ix.Play("Restart_Stop");
				iy.Play("ShutDown_Stop");
				it.SetActive(value: false);
				iu.SetActive(value: false);
				iv.SetActive(value: false);
			}
			else
			{
				@is = true;
				it.SetActive(value: true);
				iu.SetActive(value: true);
				iv.SetActive(value: true);
				iw.Play("Qiut_Start");
				ix.Play("Restart_Start");
				iy.Play("ShutDown_Start");
			}
		}
	}

	public void OnBtnQiutClick()
	{
		if (!(iz <= 0.5f))
		{
			a.k();
		}
	}

	public void OnBtnRestartClick()
	{
		if (iz >= 0.5f)
		{
			mReShConfirmPanel.ry(a: false);
		}
	}

	public void OnBtnShutDownClick()
	{
		if (iz >= 0.5f)
		{
			mReShConfirmPanel.ry(a: true);
		}
	}
}
