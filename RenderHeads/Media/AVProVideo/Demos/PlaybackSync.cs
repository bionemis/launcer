using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class PlaybackSync : MonoBehaviour
	{
		private enum a
		{
			Loading = 0,
			Playing = 1,
			Finished = 2
		}

		public MediaPlayer _masterPlayer;

		public MediaPlayer[] _slavePlayers;

		public float _toleranceMs = 30f;

		public bool _matchVideo = true;

		public bool _muteSlaves = true;

		private a ya;

		private void Start()
		{
			for (int i = 0; i < _slavePlayers.Length; i++)
			{
				_slavePlayers[i].m_Muted = true;
				if (_matchVideo)
				{
					_slavePlayers[i].bdu(_masterPlayer.m_VideoLocation, _masterPlayer.m_VideoPath, c: false);
				}
			}
		}

		private void LateUpdate()
		{
			if (ya == a.Loading && bkt())
			{
				_masterPlayer.Play();
				for (int i = 0; i < _slavePlayers.Length; i++)
				{
					_slavePlayers[i].Play();
				}
				ya = a.Playing;
			}
			if (ya == a.Finished)
			{
				Debug.Log("Do Something");
			}
			else
			{
				if (ya != a.Playing)
				{
					return;
				}
				if (_masterPlayer.ej.IsPlaying())
				{
					float currentTimeMs = _masterPlayer.ej.GetCurrentTimeMs();
					for (int j = 0; j < _slavePlayers.Length; j++)
					{
						MediaPlayer mediaPlayer = _slavePlayers[j];
						float currentTimeMs2 = mediaPlayer.ej.GetCurrentTimeMs();
						if (Mathf.Abs(currentTimeMs - currentTimeMs2) > _toleranceMs)
						{
							mediaPlayer.ej.SeekFast(currentTimeMs + _toleranceMs * 0.5f);
							if (mediaPlayer.ej.IsPaused())
							{
								mediaPlayer.Play();
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < _slavePlayers.Length; k++)
					{
						_slavePlayers[k].Pause();
					}
				}
				if (bkv(_masterPlayer))
				{
					ya = a.Finished;
				}
			}
		}

		private bool bkt()
		{
			bool result = false;
			if (bku(_masterPlayer))
			{
				result = true;
				for (int i = 0; i < _slavePlayers.Length; i++)
				{
					if (!bku(_slavePlayers[i]))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		private static bool bku(MediaPlayer a)
		{
			if (a != null && a.ej != null && a.ej.HasMetaData() && a.ej.CanPlay())
			{
				return a.ek.GetTextureFrameCount() > 0;
			}
			return false;
		}

		private static bool bkv(MediaPlayer a)
		{
			bool result = false;
			if (a != null && a.ej != null && a.ej.IsFinished())
			{
				result = true;
			}
			return result;
		}
	}
}
