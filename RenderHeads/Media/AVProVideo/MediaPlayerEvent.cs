using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace RenderHeads.Media.AVProVideo
{
	[Serializable]
	public class MediaPlayerEvent : UnityEvent<MediaPlayer, MediaPlayerEvent.a, da>
	{
		public enum a
		{
			MetaDataReady = 0,
			ReadyToPlay = 1,
			Started = 2,
			FirstFrameReady = 3,
			FinishedPlaying = 4,
			Closing = 5,
			Error = 6,
			SubtitleChange = 7,
			Stalled = 8,
			Unstalled = 9,
			ResolutionChanged = 10,
			StartedSeeking = 11,
			FinishedSeeking = 12,
			StartedBuffering = 13,
			FinishedBuffering = 14,
			PropertiesChanged = 15,
			PlaylistItemChanged = 16,
			PlaylistFinished = 17
		}

		private List<UnityAction<MediaPlayer, a, da>> _listeners = new List<UnityAction<MediaPlayer, a, da>>(4);

		public bool bia()
		{
			if (_listeners.Count <= 0)
			{
				return GetPersistentEventCount() > 0;
			}
			return true;
		}

		public void bib(UnityAction<MediaPlayer, a, da> a)
		{
			if (!_listeners.Contains(a))
			{
				_listeners.Add(a);
				AddListener(a);
			}
		}

		public void bic(UnityAction<MediaPlayer, a, da> a)
		{
			int num = _listeners.IndexOf(a);
			if (num >= 0)
			{
				_listeners.RemoveAt(num);
				RemoveListener(a);
			}
		}

		public void bid()
		{
			_listeners.Clear();
			RemoveAllListeners();
		}
	}
}
