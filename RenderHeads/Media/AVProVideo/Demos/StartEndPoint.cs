using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class StartEndPoint : MonoBehaviour
	{
		public MediaPlayer _mediaPlayer;

		public float _startPointSeconds;

		public float _endPointSeconds;

		public bool _loop;

		[Tooltip("If looping is enabled, this is the time the video rewinds to when it reaches the end point")]
		public float _startLoopSeconds;

		private bool yb;

		private void OnEnable()
		{
			yb = true;
		}

		private void Update()
		{
			if (bkw(_mediaPlayer))
			{
				if (yb)
				{
					bkx(_mediaPlayer, _startPointSeconds);
					yb = false;
				}
				else if (!_loop)
				{
					bky(_mediaPlayer, _endPointSeconds);
				}
				else
				{
					bkz(_mediaPlayer, _endPointSeconds, _startLoopSeconds);
				}
			}
		}

		private static bool bkw(MediaPlayer a)
		{
			if (a != null && a.ej != null)
			{
				return a.ej.HasMetaData();
			}
			return false;
		}

		private static void bkx(MediaPlayer a, float b)
		{
			a.ej.Seek(b * 1000f);
			a.Play();
		}

		private static void bky(MediaPlayer a, float b)
		{
			if (a.ej.IsPlaying() && a.ej.GetCurrentTimeMs() >= b * 1000f)
			{
				a.Pause();
			}
		}

		private static void bkz(MediaPlayer a, float b, float c)
		{
			if (a.ej.IsPlaying() && a.ej.GetCurrentTimeMs() >= b * 1000f)
			{
				a.ej.Seek(c * 1000f);
			}
		}
	}
}
