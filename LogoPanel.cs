using System;
using System.IO;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

public class LogoPanel : MonoSingle<LogoPanel>
{
	[SerializeField]
	private Image m_ImgLogo;

	[SerializeField]
	private Image m_ImgQr;

	[SerializeField]
	private Image m_ImgIntroduction;

	[SerializeField]
	private GameObject m_CompanyLogo;

	[SerializeField]
	private Text m_TxtVersion;

	[SerializeField]
	private string uVersion;

	private MediaPlayer m_MediaPlayer;

	private void Start()
	{
		m_TxtVersion.text = uVersion;
		if (GameEntry.a.IsBig)
		{
			Sprite sprite = tn(h.h, 235, 400);
			if (sprite != null)
			{
				m_ImgLogo.sprite = sprite;
				m_CompanyLogo.SetActive(value: false);
			}
			if (m_ImgQr != null)
			{
				Sprite sprite2 = tn(h.i, 200, 200);
				if (sprite2 != null)
				{
					m_ImgQr.sprite = sprite2;
				}
			}
			if (m_ImgIntroduction != null)
			{
				Sprite sprite3 = tn(h.f + "Introduction.png", 1050, 370);
				if (sprite3 != null)
				{
					m_ImgIntroduction.sprite = sprite3;
				}
			}
			m_MediaPlayer = base.transform.Find("VideoBg").GetComponent<MediaPlayer>();
			tl();
		}
		else
		{
			Sprite sprite4 = tn(h.h, 364, 179);
			if (sprite4 != null)
			{
				m_ImgLogo.sprite = sprite4;
				m_CompanyLogo.SetActive(value: false);
			}
		}
	}

	public void tl()
	{
		string path = h.f + "1.mp4";
		if (GameEntry.a.IsBig && File.Exists(path))
		{
			m_MediaPlayer?.bdu(MediaPlayer.b.AbsolutePathOrURL, path);
			m_MediaPlayer?.ej.SetLooping(a: true);
		}
	}

	public void tm(string a)
	{
		if (GameEntry.a.IsBig && File.Exists(a))
		{
			m_MediaPlayer?.bdu(MediaPlayer.b.AbsolutePathOrURL, a);
			m_MediaPlayer?.ej.SetLooping(a: false);
		}
	}

	private Sprite tn(string a, int b, int c)
	{
		try
		{
			FileStream fileStream = new FileStream(a, FileMode.Open, FileAccess.Read);
			fileStream.Seek(0L, SeekOrigin.Begin);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, (int)fileStream.Length);
			fileStream.Close();
			fileStream.Dispose();
			fileStream = null;
			Texture2D texture2D = new Texture2D(b, c);
			texture2D.LoadImage(array);
			return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
		}
		catch (Exception ex)
		{
			MonoBehaviour.print("加载图片" + ex.Message);
			return null;
		}
	}
}
