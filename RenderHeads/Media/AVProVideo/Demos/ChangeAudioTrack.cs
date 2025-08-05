using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class ChangeAudioTrack : MonoBehaviour
	{
		public MediaPlayer _mediaPlayer;

		public int _trackIndex;

		private bool xz;

		private void OnEnable()
		{
			xz = true;
		}

		private void Update()
		{
			if (xz && bkq())
			{
				bkr(_mediaPlayer, _trackIndex);
				xz = false;
			}
		}

		private bool bkq()
		{
			if (_mediaPlayer != null && _mediaPlayer.ei != null)
			{
				return _mediaPlayer.ej.HasMetaData();
			}
			return false;
		}

		private static bool bkr(MediaPlayer a, int b)
		{
			bool result = false;
			int audioTrackCount = a.ei.GetAudioTrackCount();
			if (b >= 0 && b < audioTrackCount)
			{
				a.ej.SetAudioTrack(b);
				result = true;
			}
			else
			{
				Debug.LogWarning("[AVProVideo] Audio track index is out of range: " + b);
			}
			return result;
		}
	}
}
