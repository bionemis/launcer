using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[RequireComponent(typeof(AudioSource))]
	[AddComponentMenu("AVPro Video/Audio Output", 400)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class AudioOutput : MonoBehaviour
	{
		public enum a
		{
			Single = 0,
			Multiple = 1
		}

		public a _audioOutputMode = a.Multiple;

		[SerializeField]
		private MediaPlayer _mediaPlayer;

		private AudioSource _audioSource;

		[HideInInspector]
		public int _channelMask = -1;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			baw(_mediaPlayer);
		}

		private void OnDestroy()
		{
			baw(null);
		}

		private void Update()
		{
			if (_mediaPlayer != null && _mediaPlayer.ej != null && _mediaPlayer.ej.IsPlaying())
			{
				bay(_mediaPlayer, _audioSource);
			}
		}

		public void baw(MediaPlayer a)
		{
			if (_mediaPlayer != null)
			{
				_mediaPlayer.em.bic(bax);
				_mediaPlayer = null;
			}
			_mediaPlayer = a;
			if (_mediaPlayer != null)
			{
				_mediaPlayer.em.bib(bax);
			}
		}

		private void bax(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			switch (b)
			{
			case MediaPlayerEvent.a.Closing:
				_audioSource.Stop();
				break;
			case MediaPlayerEvent.a.Started:
				bay(_mediaPlayer, _audioSource);
				_audioSource.Play();
				break;
			}
		}

		private static void bay(MediaPlayer a, AudioSource b)
		{
			if (a != null && a.ej != null)
			{
				float volume = a.ej.GetVolume();
				bool mute = a.ej.IsMuted();
				float playbackRate = a.ej.GetPlaybackRate();
				b.volume = volume;
				b.mute = mute;
				b.pitch = playbackRate;
			}
		}

		private void OnAudioFilterRead(float[] a, int b)
		{
			cp.a.bhu(this, _mediaPlayer, a, _channelMask, b, _audioOutputMode);
		}
	}
}
