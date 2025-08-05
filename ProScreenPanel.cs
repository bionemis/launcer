using System.IO;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

public class ProScreenPanel : MonoBehaviour
{
	[SerializeField]
	private GridLayoutGroup uScreenBg;

	[SerializeField]
	private GameObject[] uScreenItems;

	[SerializeField]
	private MediaPlayer mediaPlayer;

	[SerializeField]
	private Camera mCamera;

	private float frame;

	public bool dc { get; private set; }

	public int dd { get; private set; }

	private void Awake()
	{
		frame = GameEntry.x.bq;
		uScreenBg.spacing = new Vector2(0f - frame, 0f - frame);
	}

	private void Start()
	{
		if (GameEntry.a.IsDoubleScreen)
		{
			base.gameObject.GetComponent<Canvas>().targetDisplay = 1;
			mCamera.targetDisplay = 1;
			uv();
		}
		vc();
	}

	public void uv()
	{
		if (!dc && GameEntry.u.fz.fv)
		{
			dc = true;
			string path = h.j;
			if (File.Exists(path))
			{
				mediaPlayer.gameObject.SetActive(value: true);
				mediaPlayer.bdu(MediaPlayer.b.AbsolutePathOrURL, path);
				mediaPlayer.Play();
			}
		}
	}

	public void uw()
	{
		if (dc)
		{
			dc = false;
			mediaPlayer.gameObject.SetActive(value: false);
			mediaPlayer.bem();
		}
	}

	public void uz(int a, be b)
	{
		dd = a;
		uw();
		ushort num;
		be be2;
		switch (a)
		{
		default:
			num = 1;
			be2 = b;
			break;
		case 2:
			num = 2;
			uScreenBg.constraintCount = 2;
			be2 = new be(va(b.dx, 2), b.dy);
			break;
		case 3:
		case 4:
			num = 4;
			uScreenBg.constraintCount = 2;
			be2 = new be(va(b.dx, 2), va(b.dy, 2));
			break;
		case 5:
		case 6:
			num = 6;
			uScreenBg.constraintCount = 3;
			be2 = new be(va(b.dx, 3), va(b.dy, 2));
			break;
		}
		if (!GameEntry.a.IsDoubleScreen)
		{
			uScreenBg.GetComponent<Image>().enabled = true;
			mCamera.enabled = true;
		}
		MonoBehaviour.print("playCount:" + a + "  x:" + be2.dx + "  y:" + be2.dy);
		for (int num2 = 0; num2 < num; num2++)
		{
			uScreenItems[num2].SetActive(value: true);
		}
		uScreenBg.cellSize = new Vector2(be2.dx, be2.dy);
	}

	private float va(float a, int b)
	{
		if (b <= 1)
		{
			return a;
		}
		float num = (float)(b - 1) * frame;
		return (a + num) / (float)b;
	}

	public void vb(int a, int b)
	{
		uScreenItems[a].transform.Find("Text").GetComponent<Text>().text = b.ToString();
	}

	public void vc()
	{
		dd = 0;
		if (!GameEntry.a.IsDoubleScreen)
		{
			uScreenBg.GetComponent<Image>().enabled = false;
			mCamera.enabled = false;
		}
		for (int num = 0; num < uScreenItems.Length; num++)
		{
			uScreenItems[num].transform.Find("Text").GetComponent<Text>().text = "";
			uScreenItems[num].SetActive(value: false);
		}
	}
}
