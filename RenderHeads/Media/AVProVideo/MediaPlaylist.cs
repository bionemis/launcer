using System;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[Serializable]
	public class MediaPlaylist
	{
		[Serializable]
		public class MediaItem
		{
			[SerializeField]
			public MediaPlayer.b fileLocation = MediaPlayer.b.RelativeToStreamingAssetsFolder;

			[SerializeField]
			public string filePath;

			[SerializeField]
			public bool loop;

			[SerializeField]
			public PlaylistMediaPlayer.c startMode;

			[SerializeField]
			public PlaylistMediaPlayer.d progressMode;

			[SerializeField]
			public float progressTimeSeconds = 0.5f;

			[SerializeField]
			public bool autoPlay = true;

			[SerializeField]
			public cx stereoPacking;

			[SerializeField]
			public cz alphaPacking;

			[SerializeField]
			public bool isOverrideTransition;

			[SerializeField]
			public PlaylistMediaPlayer.a overrideTransition;

			[SerializeField]
			public float overrideTransitionDuration = 1f;

			[SerializeField]
			public PlaylistMediaPlayer.Easing overrideTransitionEasing;
		}

		[SerializeField]
		private List<MediaItem> _items = new List<MediaItem>(8);

		public List<MediaItem> fi => _items;

		public bool bfq(int a)
		{
			if (a >= 0)
			{
				return a < _items.Count;
			}
			return false;
		}
	}
}
