using System;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Playlist Media Player (BETA)", -100)]
	public class PlaylistMediaPlayer : MediaPlayer, cv
	{
		public enum a
		{
			None = 0,
			Fade = 1,
			Black = 2,
			White = 3,
			Transparent = 4,
			Horiz = 5,
			Vert = 6,
			Diag = 7,
			MirrorH = 8,
			MirrorV = 9,
			MirrorD = 10,
			ScrollV = 11,
			ScrollH = 12,
			Circle = 13,
			Diamond = 14,
			Blinds = 15,
			Arrows = 16,
			SlideH = 17,
			SlideV = 18,
			Zoom = 19,
			RectV = 20,
			Random = 21
		}

		public new enum b
		{
			None = 0,
			Loop = 1
		}

		public enum c
		{
			Immediate = 0,
			Manual = 1
		}

		public enum d
		{
			OnFinish = 0,
			BeforeFinish = 1,
			Manual = 2
		}

		[Serializable]
		public class Easing
		{
			public enum a
			{
				Step = 0,
				Linear = 1,
				InQuad = 2,
				OutQuad = 3,
				InOutQuad = 4,
				InCubic = 5,
				OutCubic = 6,
				InOutCubic = 7,
				InQuint = 8,
				OutQuint = 9,
				InOutQuint = 10,
				InQuart = 11,
				OutQuart = 12,
				InOutQuart = 13,
				InExpo = 14,
				OutExpo = 15,
				InOutExpo = 16,
				Random = 17,
				RandomNotStep = 18
			}

			public a preset = a.Linear;

			public static Func<float, float> bfr(a a)
			{
				Func<float, float> result = null;
				switch (a)
				{
				case a.Step:
					result = bfv;
					break;
				case a.Linear:
					result = bfw;
					break;
				case a.InQuad:
					result = bfx;
					break;
				case a.OutQuad:
					result = bfy;
					break;
				case a.InOutQuad:
					result = bfz;
					break;
				case a.InCubic:
					result = bga;
					break;
				case a.OutCubic:
					result = bgb;
					break;
				case a.InOutCubic:
					result = bgc;
					break;
				case a.InQuint:
					result = bgg;
					break;
				case a.OutQuint:
					result = bgh;
					break;
				case a.InOutQuint:
					result = bgi;
					break;
				case a.InQuart:
					result = bgd;
					break;
				case a.OutQuart:
					result = bge;
					break;
				case a.InOutQuart:
					result = bgf;
					break;
				case a.InExpo:
					result = bgj;
					break;
				case a.OutExpo:
					result = bgk;
					break;
				case a.InOutExpo:
					result = bgl;
					break;
				case a.Random:
					result = bfr((a)UnityEngine.Random.Range(0, 17));
					break;
				case a.RandomNotStep:
					result = bfr((a)UnityEngine.Random.Range(1, 17));
					break;
				}
				return result;
			}

			public static float bfs(float a, float b)
			{
				return Mathf.Pow(a, b);
			}

			public static float bft(float a, float b)
			{
				return 1f - Mathf.Abs(Mathf.Pow(a - 1f, b));
			}

			public static float bfu(float a, float b)
			{
				if (a < 0.5f)
				{
					return bfs(a * 2f, b) / 2f;
				}
				return bft(a * 2f - 1f, b) / 2f + 0.5f;
			}

			public static float bfv(float a)
			{
				float result = 0f;
				if (a >= 0.5f)
				{
					result = 1f;
				}
				return result;
			}

			public static float bfw(float a)
			{
				return a;
			}

			public static float bfx(float a)
			{
				return bfs(a, 2f);
			}

			public static float bfy(float a)
			{
				return bft(a, 2f);
			}

			public static float bfz(float a)
			{
				return bfu(a, 2f);
			}

			public static float bga(float a)
			{
				return bfs(a, 3f);
			}

			public static float bgb(float a)
			{
				return bft(a, 3f);
			}

			public static float bgc(float a)
			{
				return bfu(a, 3f);
			}

			public static float bgd(float a)
			{
				return bfs(a, 4f);
			}

			public static float bge(float a)
			{
				return bft(a, 4f);
			}

			public static float bgf(float a)
			{
				return bfu(a, 4f);
			}

			public static float bgg(float a)
			{
				return bfs(a, 5f);
			}

			public static float bgh(float a)
			{
				return bft(a, 5f);
			}

			public static float bgi(float a)
			{
				return bfu(a, 5f);
			}

			public static float bgj(float a)
			{
				float result = 0f;
				if (a != 0f)
				{
					result = Mathf.Pow(2f, 10f * (a - 1f));
				}
				return result;
			}

			public static float bgk(float a)
			{
				float result = 1f;
				if (a != 1f)
				{
					result = 0f - Mathf.Pow(2f, -10f * a) + 1f;
				}
				return result;
			}

			public static float bgl(float a)
			{
				float result = 0f;
				if (a > 0f)
				{
					result = 1f;
					if (a < 1f)
					{
						a *= 2f;
						if (a < 1f)
						{
							result = 0.5f * Mathf.Pow(2f, 10f * (a - 1f));
						}
						else
						{
							a -= 1f;
							result = 0.5f * (0f - Mathf.Pow(2f, -10f * a) + 2f);
						}
					}
				}
				return result;
			}
		}

		[SerializeField]
		private MediaPlayer _playerA;

		[SerializeField]
		private MediaPlayer _playerB;

		[SerializeField]
		private bool _playlistAutoProgress = true;

		[SerializeField]
		[Tooltip("Close the video on the other MediaPlayer when it is not visible any more.  This is useful for freeing up memory and GPU decoding resources.")]
		private bool _autoCloseVideo = true;

		[SerializeField]
		private b _playlistLoopMode;

		[SerializeField]
		private MediaPlaylist _playlist = new MediaPlaylist();

		[SerializeField]
		[Tooltip("Pause the previously playing video. This is useful for systems that will struggle to play 2 videos at once")]
		private bool _pausePreviousOnTransition = true;

		[SerializeField]
		private a _nextTransition;

		[SerializeField]
		private float _transitionDuration = 1f;

		[SerializeField]
		private Easing _transitionEasing;

		private static int _propFromTex;

		private static int _propT;

		private int _playlistIndex;

		private MediaPlayer _nextPlayer;

		private Shader _shader;

		private Material _material;

		private a _currentTransition;

		private string _currentTransitionName = "LERP_NONE";

		private float _currentTransitionDuration = 1f;

		private Easing.a _currentTransitionEasing;

		private float _textureTimer;

		private float _transitionTimer;

		private Func<float, float> _easeFunc;

		private RenderTexture _rt;

		private MediaPlaylist.MediaItem _currentItem;

		private MediaPlaylist.MediaItem _nextItem;

		public MediaPlayer fj
		{
			get
			{
				if (fk == _playerA)
				{
					return _playerB;
				}
				return _playerA;
			}
		}

		public MediaPlayer fk => _nextPlayer;

		public MediaPlaylist fl => _playlist;

		public int fm => _playlistIndex;

		public MediaPlaylist.MediaItem fn
		{
			get
			{
				if (_playlist.bfq(_playlistIndex))
				{
					return _playlist.fi[_playlistIndex];
				}
				return null;
			}
		}

		public b fo
		{
			get
			{
				return _playlistLoopMode;
			}
			set
			{
				_playlistLoopMode = value;
			}
		}

		public bool fp
		{
			get
			{
				return _playlistAutoProgress;
			}
			set
			{
				_playlistAutoProgress = value;
			}
		}

		public override cu ei
		{
			get
			{
				if (fj != null)
				{
					return fj.ei;
				}
				return null;
			}
		}

		public override ct ej
		{
			get
			{
				if (fj != null)
				{
					return fj.ej;
				}
				return null;
			}
		}

		public override cv ek
		{
			get
			{
				if (fj != null)
				{
					if (bhe())
					{
						return this;
					}
					return fj.ek;
				}
				return null;
			}
		}

		private void bgv()
		{
			if (_pausePreviousOnTransition)
			{
				fj.Pause();
			}
			base.em.Invoke(this, MediaPlayerEvent.a.PlaylistItemChanged, da.None);
			if (_currentTransition != a.None)
			{
				Texture texture = bgw();
				Texture texture2 = bgx();
				if (texture != null && texture2 != null)
				{
					int num = Mathf.Max(texture2.width, texture.width);
					int num2 = Mathf.Max(texture2.height, texture.height);
					if (_rt != null && (_rt.width != num || _rt.height != num2))
					{
						RenderTexture.ReleaseTemporary(_rt = null);
					}
					if (_rt == null)
					{
						_rt = RenderTexture.GetTemporary(num, num2, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 1);
					}
					Graphics.Blit(texture, _rt);
					_material.SetTexture(_propFromTex, texture);
					_easeFunc = Easing.bfr(_currentTransitionEasing);
					_transitionTimer = 0f;
				}
				else
				{
					_transitionTimer = _currentTransitionDuration;
					if (_autoCloseVideo)
					{
						fj.bel();
					}
				}
			}
			if (fk == _playerA)
			{
				_nextPlayer = _playerB;
			}
			else
			{
				_nextPlayer = _playerA;
			}
			_currentItem = _nextItem;
			_nextItem = null;
		}

		private Texture bgw()
		{
			if (fj != null && fj.ek != null)
			{
				return fj.ek.GetTexture();
			}
			return null;
		}

		private Texture bgx()
		{
			if (_nextPlayer != null && _nextPlayer.ek != null)
			{
				return _nextPlayer.ek.GetTexture();
			}
			return null;
		}

		private void Awake()
		{
			_nextPlayer = _playerA;
			_shader = Shader.Find("AVProVideo/Helper/Transition");
			_material = new Material(_shader);
			_propFromTex = Shader.PropertyToID("_FromTex");
			_propT = Shader.PropertyToID("_Fade");
			_easeFunc = Easing.bfr(_transitionEasing.preset);
		}

		protected override void OnDestroy()
		{
			if (_rt != null)
			{
				RenderTexture.ReleaseTemporary(_rt);
				_rt = null;
			}
			if (_material != null)
			{
				UnityEngine.Object.Destroy(_material);
				_material = null;
			}
			base.OnDestroy();
		}

		private void Start()
		{
			if ((bool)fj)
			{
				fj.em.bib(bgy);
				if ((bool)fk)
				{
					fk.em.bib(bgy);
				}
			}
			bhc(0);
		}

		public void bgy(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			if (a == fj)
			{
				base.em.Invoke(a, b, c);
			}
			switch (b)
			{
			case MediaPlayerEvent.a.FirstFrameReady:
				if (a == fk)
				{
					bgv();
					base.em.Invoke(a, b, c);
				}
				break;
			case MediaPlayerEvent.a.FinishedPlaying:
				if (_playlistAutoProgress && a == fj && _currentItem.progressMode == d.OnFinish)
				{
					bha();
				}
				break;
			}
		}

		public bool bgz()
		{
			return bhc(_playlistIndex - 1);
		}

		public bool bha()
		{
			bool num = bhc(_playlistIndex + 1);
			if (!num)
			{
				base.em.Invoke(this, MediaPlayerEvent.a.PlaylistFinished, da.None);
			}
			return num;
		}

		public bool bhb(int a)
		{
			if (_playlistLoopMode == b.Loop && _playlist.fi.Count > 0)
			{
				a %= _playlist.fi.Count;
				if (a < 0)
				{
					a += _playlist.fi.Count;
				}
			}
			return _playlist.bfq(a);
		}

		public bool bhc(int a)
		{
			if (_playlistLoopMode == b.Loop && _playlist.fi.Count > 0)
			{
				a %= _playlist.fi.Count;
				if (a < 0)
				{
					a += _playlist.fi.Count;
				}
			}
			if (_playlist.bfq(a))
			{
				_playlistIndex = a;
				_nextItem = _playlist.fi[_playlistIndex];
				bhd(_nextItem);
				return true;
			}
			return false;
		}

		public void bhd(MediaPlaylist.MediaItem a)
		{
			bool flag = false;
			if (fk.m_VideoPath == a.filePath && fk.m_VideoLocation == a.fileLocation)
			{
				flag = true;
			}
			if (a.isOverrideTransition)
			{
				bhf(a.overrideTransition, a.overrideTransitionDuration, a.overrideTransitionEasing.preset);
			}
			else
			{
				bhf(_nextTransition, _transitionDuration, _transitionEasing.preset);
			}
			m_Loop = (fk.m_Loop = a.loop);
			m_AutoStart = (fk.m_AutoStart = a.autoPlay);
			m_VideoLocation = (fk.m_VideoLocation = a.fileLocation);
			m_VideoPath = (fk.m_VideoPath = a.filePath);
			m_StereoPacking = (fk.m_StereoPacking = a.stereoPacking);
			m_AlphaPacking = (fk.m_AlphaPacking = a.alphaPacking);
			if (flag)
			{
				fk.ben(a: false);
				if (_nextItem.startMode == c.Immediate)
				{
					fk.Play();
				}
				bgv();
			}
			else if (string.IsNullOrEmpty(fk.m_VideoPath))
			{
				fk.bel();
			}
			else
			{
				fk.bdu(fk.m_VideoLocation, fk.m_VideoPath, _nextItem.startMode == c.Immediate);
			}
		}

		private bool bhe()
		{
			if (_rt != null && _transitionTimer < _currentTransitionDuration && _currentTransition != a.None)
			{
				return true;
			}
			return false;
		}

		private void bhf(a a, float b, Easing.a c)
		{
			if (a == a.Random)
			{
				a = (a)UnityEngine.Random.Range(0, 21);
			}
			if (a != _currentTransition)
			{
				if (!string.IsNullOrEmpty(_currentTransitionName))
				{
					_material.DisableKeyword(_currentTransitionName);
				}
				_currentTransition = a;
				_currentTransitionName = bhg(a);
				_material.EnableKeyword(_currentTransitionName);
			}
			_currentTransitionDuration = b;
			_currentTransitionEasing = c;
		}

		protected override void Update()
		{
			if (bhe())
			{
				_transitionTimer += Time.deltaTime;
				float num = _easeFunc(Mathf.Clamp01(_transitionTimer / _currentTransitionDuration));
				fk.ej.SetVolume(1f - num);
				fj.ej.SetVolume(num);
				_material.SetFloat(_propT, num);
				_rt.DiscardContents();
				Graphics.Blit(bgw(), _rt, _material);
				if (!bhe())
				{
					if (_autoCloseVideo)
					{
						if (fk != null)
						{
							fk.m_VideoPath = string.Empty;
							fk.bel();
						}
					}
					else if (!_pausePreviousOnTransition && fk != null && fk.ej.IsPlaying())
					{
						fk.Pause();
					}
				}
			}
			else if (_playlistAutoProgress && _nextItem == null && _currentItem != null && _currentItem.progressMode == d.BeforeFinish && ej != null && ej.GetCurrentTimeMs() >= ei.GetDurationMs() - _currentItem.progressTimeSeconds * 1000f)
			{
				bha();
			}
			base.Update();
		}

		public Texture GetTexture(int a = 0)
		{
			return _rt;
		}

		public int GetTextureCount()
		{
			return fj.ek.GetTextureCount();
		}

		public int GetTextureFrameCount()
		{
			return fj.ek.GetTextureFrameCount();
		}

		public bool SupportsTextureFrameCount()
		{
			return fj.ek.SupportsTextureFrameCount();
		}

		public long GetTextureTimeStamp()
		{
			return fj.ek.GetTextureTimeStamp();
		}

		public bool RequiresVerticalFlip()
		{
			return fj.ek.RequiresVerticalFlip();
		}

		public Matrix4x4 GetYpCbCrTransform()
		{
			return fj.ek.GetYpCbCrTransform();
		}

		private static string bhg(a a)
		{
			switch (a)
			{
			case a.None:
				return "LERP_NONE";
			case a.Fade:
				return "LERP_FADE";
			case a.Black:
				return "LERP_BLACK";
			case a.White:
				return "LERP_WHITE";
			case a.Transparent:
				return "LERP_TRANSP";
			case a.Horiz:
				return "LERP_HORIZ";
			case a.Vert:
				return "LERP_VERT";
			case a.Diag:
				return "LERP_DIAG";
			case a.MirrorH:
				return "LERP_HORIZ_MIRROR";
			case a.MirrorV:
				return "LERP_VERT_MIRROR";
			case a.MirrorD:
				return "LERP_DIAG_MIRROR";
			case a.ScrollV:
				return "LERP_SCROLL_VERT";
			case a.ScrollH:
				return "LERP_SCROLL_HORIZ";
			case a.Circle:
				return "LERP_CIRCLE";
			case a.Diamond:
				return "LERP_DIAMOND";
			case a.Blinds:
				return "LERP_BLINDS";
			case a.Arrows:
				return "LERP_ARROW";
			case a.SlideH:
				return "LERP_SLIDE_HORIZ";
			case a.SlideV:
				return "LERP_SLIDE_VERT";
			case a.Zoom:
				return "LERP_ZOOM_FADE";
			case a.RectV:
				return "LERP_RECTS_VERT";
			default:
				return string.Empty;
			}
		}
	}
}
