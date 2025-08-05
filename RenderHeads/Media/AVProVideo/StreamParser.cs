using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	public class StreamParser : MonoBehaviour
	{
		public enum a
		{
			HLS = 0
		}

		public string _url;

		public a _streamType;

		public bool _autoLoad = true;

		private Stream tx;

		private bool ty;

		private List<Stream> tz;

		private List<Stream.Chunk> ua;

		private StreamParserEvent ub;

		public StreamParserEvent em
		{
			get
			{
				if (ub == null)
				{
					ub = new StreamParserEvent();
				}
				return ub;
			}
		}

		public bool fq => ty;

		public Stream fr
		{
			get
			{
				if (!ty)
				{
					return null;
				}
				return tx;
			}
		}

		public List<Stream> fs
		{
			get
			{
				if (!ty)
				{
					return null;
				}
				return tz;
			}
		}

		public List<Stream.Chunk> ft
		{
			get
			{
				if (!ty)
				{
					return null;
				}
				return ua;
			}
		}

		private void bhi()
		{
			try
			{
				if (_streamType == a.HLS)
				{
					tx = new cq(_url);
				}
				else
				{
					tx = new cq(_url);
				}
				tz = tx.GetAllStreams();
				ua = tx.GetAllChunks();
				ty = true;
				Debug.Log("[AVProVideo] Stream parser completed parsing stream file " + _url);
				if (ub != null)
				{
					ub.Invoke(this, StreamParserEvent.a.Success);
				}
			}
			catch (Exception ex)
			{
				ty = false;
				Debug.LogError("[AVProVideo] Parser unable to read stream " + ex.Message);
				if (ub != null)
				{
					ub.Invoke(this, StreamParserEvent.a.Failed);
				}
			}
		}

		public void bhn()
		{
			new Thread(bhi).Start();
		}

		private void Start()
		{
			if (_autoLoad)
			{
				bhn();
			}
		}
	}
}
