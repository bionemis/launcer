using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class SampleApp_Multiple : MonoBehaviour
	{
		[SerializeField]
		private string m_videoPath = "BigBuckBunny_360p30.mp4";

		[SerializeField]
		private MediaPlayer.b m_videoLocation = MediaPlayer.b.RelativeToStreamingAssetsFolder;

		private int m_NumVideosAdded;

		private List<DisplayUGUI> m_aAddedVideos = new List<DisplayUGUI>();

		private void Update()
		{
			foreach (DisplayUGUI aAddedVideo in m_aAddedVideos)
			{
				if (aAddedVideo.gameObject != null && !aAddedVideo.gameObject.activeSelf && aAddedVideo._mediaPlayer != null && aAddedVideo._mediaPlayer.ej != null && aAddedVideo._mediaPlayer.ek.GetTexture() != null)
				{
					aAddedVideo.gameObject.SetActive(value: true);
				}
			}
		}

		private void bkp()
		{
			GameObject gameObject = GameObject.Find("Canvas/Panel");
			RectTransform rectTransform = (gameObject ? gameObject.GetComponent<RectTransform>() : null);
			if ((bool)rectTransform)
			{
				Vector2 sizeDelta = rectTransform.sizeDelta;
				Vector2 vector = new Vector2(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f);
				int count = m_aAddedVideos.Count;
				int num = Mathf.CeilToInt(Mathf.Sqrt(count));
				float num2 = 1f / (float)num * sizeDelta.x;
				float num3 = 1f / (float)num * sizeDelta.y;
				for (int i = 0; i < count; i++)
				{
					DisplayUGUI displayUGUI = m_aAddedVideos[i];
					int num4 = i % num;
					int num5 = i / num;
					displayUGUI.rectTransform.anchoredPosition = new Vector2(num2 * (float)num4 - vector.x, num3 * (float)(-num5) + vector.y);
					displayUGUI.rectTransform.sizeDelta = new Vector2(num2, num3);
				}
			}
		}

		public void AddVideoClicked()
		{
			m_NumVideosAdded++;
			MediaPlayer mediaPlayer = new GameObject("AVPro MediaPlayer " + m_NumVideosAdded).AddComponent<MediaPlayer>();
			mediaPlayer.m_Loop = true;
			mediaPlayer.bdu(m_videoLocation, m_videoPath);
			GameObject gameObject = GameObject.Find("Canvas/Panel");
			if (gameObject != null)
			{
				GameObject obj = new GameObject("AVPro Video uGUI " + m_NumVideosAdded);
				obj.transform.parent = gameObject.transform;
				obj.SetActive(value: false);
				obj.AddComponent<RectTransform>();
				obj.AddComponent<CanvasRenderer>();
				DisplayUGUI displayUGUI = obj.AddComponent<DisplayUGUI>();
				displayUGUI._mediaPlayer = mediaPlayer;
				displayUGUI._scaleMode = ScaleMode.StretchToFill;
				displayUGUI.rectTransform.localScale = Vector3.one;
				displayUGUI.rectTransform.pivot = new Vector2(0f, 1f);
				m_aAddedVideos.Add(displayUGUI);
				bkp();
			}
		}

		public void RemoveVideoClicked()
		{
			if (m_aAddedVideos.Count > 0)
			{
				int index = Random.Range(0, m_aAddedVideos.Count);
				DisplayUGUI displayUGUI = m_aAddedVideos[index];
				if (displayUGUI._mediaPlayer != null)
				{
					displayUGUI._mediaPlayer.bel();
					Object.Destroy(displayUGUI._mediaPlayer.gameObject);
					displayUGUI._mediaPlayer = null;
				}
				Object.Destroy(displayUGUI.gameObject);
				m_aAddedVideos.RemoveAt(index);
				m_NumVideosAdded--;
			}
		}

		private void OnDestroy()
		{
			foreach (DisplayUGUI aAddedVideo in m_aAddedVideos)
			{
				if ((bool)aAddedVideo._mediaPlayer)
				{
					aAddedVideo._mediaPlayer = null;
				}
			}
			m_aAddedVideos.Clear();
		}
	}
}
