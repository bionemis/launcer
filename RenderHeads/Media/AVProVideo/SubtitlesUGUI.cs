using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Subtitles uGUI", 201)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class SubtitlesUGUI : MonoBehaviour
	{
		[SerializeField]
		private MediaPlayer _mediaPlayer;

		[SerializeField]
		private Text _text;

		private void Start()
		{
			bho(_mediaPlayer);
		}

		private void OnDestroy()
		{
			bho(null);
		}

		public void bho(MediaPlayer a)
		{
			if (_mediaPlayer != null)
			{
				_mediaPlayer.em.bic(bhp);
				_mediaPlayer = null;
			}
			if (a != null)
			{
				a.em.bib(bhp);
				_mediaPlayer = a;
			}
		}

		private void bhp(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			if (b == MediaPlayerEvent.a.SubtitleChange)
			{
				string subtitleText = _mediaPlayer.el.GetSubtitleText();
				subtitleText = subtitleText.Replace("<font color=", "<color=");
				subtitleText = subtitleText.Replace("</font>", "</color>");
				subtitleText = subtitleText.Replace("<u>", string.Empty);
				subtitleText = subtitleText.Replace("</u>", string.Empty);
				_text.text = subtitleText;
			}
		}
	}
}
