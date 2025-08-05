using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class VCR : MonoBehaviour
	{
		public MediaPlayer _mediaPlayer;

		public MediaPlayer _mediaPlayerB;

		public DisplayUGUI _mediaDisplay;

		public RectTransform _bufferedSliderRect;

		public Slider _videoSeekSlider;

		private float yi;

		private bool yj;

		public Slider _audioVolumeSlider;

		private float yk;

		public Toggle _AutoStartToggle;

		public Toggle _MuteToggle;

		public MediaPlayer.b _location = MediaPlayer.b.RelativeToStreamingAssetsFolder;

		public string _folder = "AVProVideoDemos/";

		public string[] _videoFiles = new string[2] { "BigBuckBunny_720p30.mp4", "SampleSphere.mp4" };

		private int yl;

		private Image ym;

		private MediaPlayer yn;

		public MediaPlayer gg
		{
			get
			{
				if (gh == _mediaPlayer)
				{
					return _mediaPlayerB;
				}
				return _mediaPlayer;
			}
		}

		public MediaPlayer gh => yn;

		private void blk()
		{
			gg.ej.Pause();
			if (gh == _mediaPlayer)
			{
				yn = _mediaPlayerB;
			}
			else
			{
				yn = _mediaPlayer;
			}
			_mediaDisplay.ec = gg;
		}

		public void OnOpenVideoFile()
		{
			gh.m_VideoPath = Path.Combine(_folder, _videoFiles[yl]);
			yl = (yl + 1) % _videoFiles.Length;
			if (string.IsNullOrEmpty(gh.m_VideoPath))
			{
				gh.bel();
				yl = 0;
			}
			else
			{
				gh.bdu(_location, gh.m_VideoPath, _AutoStartToggle.isOn);
			}
			if (_bufferedSliderRect != null)
			{
				ym = _bufferedSliderRect.GetComponent<Image>();
			}
		}

		public void OnAutoStartChange()
		{
			if ((bool)gg && (bool)_AutoStartToggle && _AutoStartToggle.enabled && gg.m_AutoStart != _AutoStartToggle.isOn)
			{
				gg.m_AutoStart = _AutoStartToggle.isOn;
			}
			if ((bool)gh && (bool)_AutoStartToggle && _AutoStartToggle.enabled && gh.m_AutoStart != _AutoStartToggle.isOn)
			{
				gh.m_AutoStart = _AutoStartToggle.isOn;
			}
		}

		public void OnMuteChange()
		{
			if ((bool)gg)
			{
				gg.ej.MuteAudio(_MuteToggle.isOn);
			}
			if ((bool)gh)
			{
				gh.ej.MuteAudio(_MuteToggle.isOn);
			}
		}

		public void OnPlayButton()
		{
			if ((bool)gg)
			{
				gg.ej.Play();
			}
		}

		public void OnPauseButton()
		{
			if ((bool)gg)
			{
				gg.ej.Pause();
			}
		}

		public void OnVideoSeekSlider()
		{
			if ((bool)gg && (bool)_videoSeekSlider && _videoSeekSlider.value != yi)
			{
				gg.ej.Seek(_videoSeekSlider.value * gg.ei.GetDurationMs());
			}
		}

		public void OnVideoSliderDown()
		{
			if ((bool)gg)
			{
				yj = gg.ej.IsPlaying();
				if (yj)
				{
					gg.ej.Pause();
				}
				OnVideoSeekSlider();
			}
		}

		public void OnVideoSliderUp()
		{
			if ((bool)gg && yj)
			{
				gg.ej.Play();
				yj = false;
			}
		}

		public void OnAudioVolumeSlider()
		{
			if ((bool)gg && (bool)_audioVolumeSlider && _audioVolumeSlider.value != yk)
			{
				gg.ej.SetVolume(_audioVolumeSlider.value);
			}
			if ((bool)gh && (bool)_audioVolumeSlider && _audioVolumeSlider.value != yk)
			{
				gh.ej.SetVolume(_audioVolumeSlider.value);
			}
		}

		public void OnRewindButton()
		{
			if ((bool)gg)
			{
				gg.ej.Rewind();
			}
		}

		private void Awake()
		{
			yn = _mediaPlayerB;
		}

		private void Start()
		{
			if ((bool)gg)
			{
				gg.em.bib(bll);
				if ((bool)gh)
				{
					gh.em.bib(bll);
				}
				if ((bool)_audioVolumeSlider && gg.ej != null)
				{
					float value = (yk = gg.ej.GetVolume());
					_audioVolumeSlider.value = value;
				}
				_AutoStartToggle.isOn = gg.m_AutoStart;
				_ = gg.m_AutoOpen;
				OnOpenVideoFile();
			}
		}

		private void OnDestroy()
		{
			if ((bool)gh)
			{
				gh.em.bic(bll);
			}
			if ((bool)gg)
			{
				gg.em.bic(bll);
			}
		}

		private void Update()
		{
			if (!gg || gg.ei == null || !(gg.ei.GetDurationMs() > 0f))
			{
				return;
			}
			float currentTimeMs = gg.ej.GetCurrentTimeMs();
			float durationMs = gg.ei.GetDurationMs();
			float num = (yi = Mathf.Clamp(currentTimeMs / durationMs, 0f, 1f));
			_videoSeekSlider.value = num;
			if (_bufferedSliderRect != null && gg.ej.IsBuffering())
			{
				float b = 0f;
				float c = gg.ej.GetBufferingProgress();
				if (c <= 0f && gg.ej.GetBufferedTimeRangeCount() > 0)
				{
					gg.ej.GetBufferedTimeRange(0, ref b, ref c);
					b /= gg.ei.GetDurationMs();
					c /= gg.ei.GetDurationMs();
				}
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				if (ym != null && ym.type == Image.Type.Filled)
				{
					ym.fillAmount = num;
				}
				else
				{
					zero[0] = b;
					one[0] = c;
				}
				_bufferedSliderRect.anchorMin = zero;
				_bufferedSliderRect.anchorMax = one;
			}
		}

		public void bll(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			switch (b)
			{
			case MediaPlayerEvent.a.FirstFrameReady:
				blk();
				break;
			}
			Debug.Log("Event: " + b);
		}
	}
}
