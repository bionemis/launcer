using System;
using UnityEngine.Events;

namespace RenderHeads.Media.AVProVideo
{
	[Serializable]
	public class StreamParserEvent : UnityEvent<StreamParser, StreamParserEvent.a>
	{
		public enum a
		{
			Success = 0,
			Failed = 1
		}
	}
}
