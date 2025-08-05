using UnityEngine;
using UnityEngine.UI;

public class VolumePanel : MonoBehaviour
{
	public Slider volumeSlider;

	public GameObject volumeBorder;

	private int jd;

	private bool je;

	public static bool isFirst = true;

	private void Start()
	{
		jd = (int)(volumeSlider.value * 100f);
	}

	private void Update()
	{
		if (jd != (int)(volumeSlider.value * 100f))
		{
			jd = (int)(volumeSlider.value * 100f);
		}
		if (isFirst)
		{
			isFirst = false;
			volumeSlider.gameObject.SetActive(value: false);
			volumeBorder.SetActive(value: false);
			je = false;
		}
	}

	public void VolumeButton()
	{
		if (je)
		{
			volumeSlider.gameObject.SetActive(value: false);
			volumeBorder.SetActive(value: false);
			je = false;
		}
		else
		{
			volumeSlider.gameObject.SetActive(value: true);
			volumeBorder.SetActive(value: true);
			je = true;
		}
	}
}
