using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Media Player", -100)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class MediaPlayer : MonoBehaviour
	{
		[Serializable]
		public class Setup
		{
			public bool persistent;
		}

		public enum b
		{
			AbsolutePathOrURL = 0,
			RelativeToProjectFolder = 1,
			RelativeToStreamingAssetsFolder = 2,
			RelativeToDataFolder = 3,
			RelativeToPersistentDataFolder = 4
		}

		[Serializable]
		public class PlatformOptions
		{
			[Serializable]
			public struct HTTPHeader
			{
				public string header;

				public string value;

				public HTTPHeader(string a, string b)
				{
					header = a;
					value = b;
				}
			}

			private enum b
			{
				Begin = 0,
				FindKey = 1,
				ReadKey = 2,
				FindColon = 3,
				FindValue = 4,
				ReadValue = 5,
				FindComma = 6,
				Finished = 7,
				Failed = 8
			}

			public bool overridePath;

			public MediaPlayer.b pathLocation = MediaPlayer.b.RelativeToStreamingAssetsFolder;

			public string path;

			public virtual bool bbx()
			{
				return overridePath;
			}

			public virtual string bby()
			{
				return null;
			}

			public virtual string bbz()
			{
				return null;
			}

			public virtual string bca()
			{
				return null;
			}

			protected static string bcb(string a)
			{
				StringBuilder stringBuilder = null;
				for (int i = 0; i < a.Length; i++)
				{
					switch (a[i])
					{
					case '"':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\\"");
						break;
					case '\\':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\\\");
						break;
					case '/':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\/");
						break;
					case '\b':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\b");
						break;
					case '\f':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\f");
						break;
					case '\n':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\n");
						break;
					case '\r':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\r");
						break;
					case '\t':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(a.Substring(0, i));
						}
						stringBuilder.Append("\\t");
						break;
					default:
						stringBuilder?.Append(a[i]);
						break;
					}
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return a;
			}

			protected static List<HTTPHeader> bcc(string a)
			{
				b b = b.Begin;
				int num = 0;
				string a2 = null;
				string text = null;
				List<HTTPHeader> list = new List<HTTPHeader>();
				char c = '\0';
				char c2 = c;
				for (int i = 0; i < a.Length; i++)
				{
					if (b == b.Finished)
					{
						break;
					}
					if (b == b.Failed)
					{
						break;
					}
					c2 = c;
					c = a[i];
					switch (b)
					{
					case b.Begin:
						if (!char.IsWhiteSpace(c))
						{
							b = ((c == '{') ? b.FindKey : b.Failed);
						}
						break;
					case b.FindKey:
						if (!char.IsWhiteSpace(c))
						{
							switch (c)
							{
							case '"':
								b = b.ReadKey;
								num = i + 1;
								break;
							case '}':
								b = b.Finished;
								break;
							default:
								b = b.Failed;
								break;
							}
						}
						break;
					case b.ReadKey:
						if (c == '"' && c2 != '\\')
						{
							a2 = a.Substring(num, i - num);
							b = b.FindColon;
						}
						break;
					case b.FindColon:
						if (!char.IsWhiteSpace(c))
						{
							b = ((c != ':') ? b.Failed : b.FindValue);
						}
						break;
					case b.FindValue:
						if (!char.IsWhiteSpace(c))
						{
							if (c == '"')
							{
								b = b.ReadValue;
								num = i + 1;
							}
							else
							{
								b = b.Failed;
							}
						}
						break;
					case b.ReadValue:
						if (c == '"' && c2 != '\\')
						{
							text = a.Substring(num, i - num);
							list.Add(new HTTPHeader(a2, text));
							b = b.FindComma;
						}
						break;
					case b.FindComma:
						if (!char.IsWhiteSpace(c))
						{
							switch (c)
							{
							case ',':
								b = b.FindKey;
								break;
							case '}':
								b = b.Finished;
								break;
							}
						}
						break;
					}
				}
				if (b == b.Finished)
				{
					return list;
				}
				UnityEngine.Debug.LogWarning("Failed to convert HTTP headers from Json, you will need to do this manually.");
				return null;
			}
		}

		[Serializable]
		public class OptionsWindows : PlatformOptions
		{
			public df.a videoApi;

			public bool useHardwareDecoding = true;

			public bool useUnityAudio;

			public bool forceAudioResample = true;

			public bool useTextureMips;

			public bool hintAlphaChannel;

			public bool useLowLatency;

			public string forceAudioOutputDeviceName = string.Empty;

			public List<string> preferredFilters = new List<string>();

			public bool enableAudio360;

			public di audio360ChannelMode;

			public override bool bbx()
			{
				if (!base.bbx() && useHardwareDecoding && !useTextureMips && !hintAlphaChannel && !useLowLatency && !useUnityAudio && videoApi == df.a.MediaFoundation && forceAudioResample && !enableAudio360 && audio360ChannelMode == di.TBE_8_2 && string.IsNullOrEmpty(forceAudioOutputDeviceName))
				{
					return preferredFilters.Count != 0;
				}
				return true;
			}
		}

		[Serializable]
		public class OptionsApple : PlatformOptions, ISerializationCallbackReceiver
		{
			public enum a
			{
				SystemDirect = 0,
				Unity = 1
			}

			public a audioMode;

			public List<HTTPHeader> httpHeaders = new List<HTTPHeader>();

			[SerializeField]
			[Multiline]
			private string httpHeaderJson;

			public string keyServerURLOverride;

			public string keyServerAuthToken;

			[Multiline]
			public string base64EncodedKeyBlob;

			public string bcd()
			{
				if (httpHeaders.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					int index = 0;
					stringBuilder.Append("{");
					stringBuilder.AppendFormat("\"{0}\":\"{1}\"", PlatformOptions.bcb(httpHeaders[index].header), PlatformOptions.bcb(httpHeaders[index].value));
					for (index = 1; index < httpHeaders.Count; index++)
					{
						stringBuilder.AppendFormat(",\"{0}\":\"{1}\"", PlatformOptions.bcb(httpHeaders[index].header), PlatformOptions.bcb(httpHeaders[index].value));
					}
					stringBuilder.Append("}");
					return stringBuilder.ToString();
				}
				return httpHeaderJson;
			}

			public override bool bbx()
			{
				if (!base.bbx() && audioMode == a.SystemDirect && (httpHeaders == null || httpHeaders.Count <= 0) && string.IsNullOrEmpty(httpHeaderJson) && string.IsNullOrEmpty(keyServerURLOverride) && string.IsNullOrEmpty(keyServerAuthToken))
				{
					return !string.IsNullOrEmpty(base64EncodedKeyBlob);
				}
				return true;
			}

			public override string bby()
			{
				return keyServerURLOverride;
			}

			public override string bbz()
			{
				return keyServerAuthToken;
			}

			public override string bca()
			{
				return base64EncodedKeyBlob;
			}

			public void OnBeforeSerialize()
			{
				if (httpHeaders != null && httpHeaders.Count > 0 && httpHeaderJson.Length > 0)
				{
					httpHeaderJson = null;
				}
			}

			public void OnAfterDeserialize()
			{
				if (httpHeaderJson != null && httpHeaderJson.Length != 0)
				{
					httpHeaders = PlatformOptions.bcc(httpHeaderJson);
					if (httpHeaders != null)
					{
						httpHeaderJson = null;
					}
				}
			}
		}

		[Serializable]
		public class OptionsMacOSX : OptionsApple
		{
		}

		[Serializable]
		public class OptionsIOS : OptionsApple
		{
			public bool useYpCbCr420Textures = true;

			public bool resumePlaybackOnAudioSessionRouteChange;

			public override bool bbx()
			{
				if (!base.bbx() && useYpCbCr420Textures)
				{
					return resumePlaybackOnAudioSessionRouteChange;
				}
				return true;
			}
		}

		[Serializable]
		public class OptionsTVOS : OptionsIOS
		{
		}

		[Serializable]
		public class OptionsAndroid : PlatformOptions, ISerializationCallbackReceiver
		{
			public dg.a videoApi = dg.a.ExoPlayer;

			public bool useFastOesPath;

			public bool showPosterFrame;

			public bool enableAudio360;

			public di audio360ChannelMode;

			public bool preferSoftwareDecoder;

			public List<HTTPHeader> httpHeaders = new List<HTTPHeader>();

			[SerializeField]
			[Multiline]
			private string httpHeaderJson;

			[SerializeField]
			[Tooltip("Byte offset into the file where the media file is located.  This is useful when hiding or packing media files within another file.")]
			public int fileOffset;

			public string bce()
			{
				if (httpHeaders.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					int index = 0;
					stringBuilder.Append("{");
					stringBuilder.AppendFormat("\"{0}\":\"{1}\"", PlatformOptions.bcb(httpHeaders[index].header), PlatformOptions.bcb(httpHeaders[index].value));
					for (index = 1; index < httpHeaders.Count; index++)
					{
						stringBuilder.AppendFormat(",\"{0}\":\"{1}\"", PlatformOptions.bcb(httpHeaders[index].header), PlatformOptions.bcb(httpHeaders[index].value));
					}
					stringBuilder.Append("}");
					return stringBuilder.ToString();
				}
				return httpHeaderJson;
			}

			public override bool bbx()
			{
				if (!base.bbx() && fileOffset == 0 && !useFastOesPath && !showPosterFrame && videoApi == dg.a.ExoPlayer && (httpHeaders == null || httpHeaders.Count <= 0) && !enableAudio360 && audio360ChannelMode == di.TBE_8_2)
				{
					return preferSoftwareDecoder;
				}
				return true;
			}

			public void OnBeforeSerialize()
			{
				if (httpHeaders != null && httpHeaders.Count > 0 && httpHeaderJson.Length > 0)
				{
					httpHeaderJson = null;
				}
			}

			public void OnAfterDeserialize()
			{
				if (httpHeaderJson != null && httpHeaderJson.Length != 0)
				{
					httpHeaders = PlatformOptions.bcc(httpHeaderJson);
					if (httpHeaders != null)
					{
						httpHeaderJson = null;
					}
				}
			}
		}

		[Serializable]
		public class OptionsWindowsPhone : PlatformOptions
		{
			public bool useHardwareDecoding = true;

			public bool useUnityAudio;

			public bool forceAudioResample = true;

			public bool useTextureMips;

			public bool useLowLatency;

			public override bool bbx()
			{
				if (!base.bbx() && useHardwareDecoding && !useTextureMips && !useLowLatency && !useUnityAudio)
				{
					return !forceAudioResample;
				}
				return true;
			}
		}

		[Serializable]
		public class OptionsWindowsUWP : PlatformOptions
		{
			public bool useHardwareDecoding = true;

			public bool useUnityAudio;

			public bool forceAudioResample = true;

			public bool useTextureMips;

			public bool useLowLatency;

			public override bool bbx()
			{
				if (!base.bbx() && useHardwareDecoding && !useTextureMips && !useLowLatency && !useUnityAudio)
				{
					return !forceAudioResample;
				}
				return true;
			}
		}

		[Serializable]
		public class OptionsWebGL : PlatformOptions
		{
			public dh.a externalLibrary;

			public bool useTextureMips;

			public override bool bbx()
			{
				if (!base.bbx() && externalLibrary == dh.a.None)
				{
					return useTextureMips;
				}
				return true;
			}
		}

		[Serializable]
		public class OptionsPS4 : PlatformOptions
		{
		}

		public delegate void n(Texture2D a);

		private sealed class o : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public string tk;

			public MediaPlayer ba;

			public b tl;

			public string tm;

			private UnityWebRequest tn;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return z;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return z;
				}
			}

			[DebuggerHidden]
			public o(int a)
			{
				y = a;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int num = y;
				MediaPlayer mediaPlayer = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					tn = UnityWebRequest.Get(tk);
					z = tn.SendWebRequest();
					y = 1;
					return true;
				case 1:
				{
					y = -1;
					string a = string.Empty;
					if (!tn.isNetworkError)
					{
						a = tn.downloadHandler.text;
					}
					else
					{
						UnityEngine.Debug.LogError("[AVProVideo] Error loading subtitles '" + tn.error + "' from " + tk);
					}
					if (mediaPlayer.m_Subtitles.LoadSubtitlesSRT(a))
					{
						mediaPlayer.m_SubtitleLocation = tl;
						mediaPlayer.m_SubtitlePath = tm;
						mediaPlayer.m_LoadSubtitles = false;
					}
					else
					{
						UnityEngine.Debug.LogError("[AVProVideo] Failed to load subtitles" + tk, mediaPlayer);
					}
					mediaPlayer.m_loadSubtitlesRoutine = null;
					tn.Dispose();
					return false;
				}
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class p : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public MediaPlayer ba;

			private YieldInstruction to;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return z;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return z;
				}
			}

			[DebuggerHidden]
			public p(int a)
			{
				y = a;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int num = y;
				MediaPlayer mediaPlayer = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					to = new WaitForEndOfFrame();
					break;
				case 1:
					y = -1;
					if (mediaPlayer.enabled && mediaPlayer.m_Player != null)
					{
						mediaPlayer.m_Player.Render();
					}
					break;
				}
				if (Application.isPlaying)
				{
					z = to;
					y = 1;
					return true;
				}
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class q : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public Texture2D tp;

			public MediaPlayer ba;

			public float tq;

			public int tr;

			public bool ts;

			public n tt;

			private Texture2D bs;

			private int tu;

			private int tv;

			private int tw;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return z;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return z;
				}
			}

			[DebuggerHidden]
			public q(int a)
			{
				y = a;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int num = y;
				MediaPlayer mediaPlayer = ba;
				Texture texture;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					bs = tp;
					texture = null;
					if (mediaPlayer.m_Control != null)
					{
						if (tq >= 0f)
						{
							mediaPlayer.Pause();
							float num2 = tq * 1000f;
							if (!(mediaPlayer.ek.GetTexture() != null) || !(Mathf.Abs(mediaPlayer.m_Control.GetCurrentTimeMs() - num2) < (float)tr))
							{
								int textureFrameCount = mediaPlayer.m_Texture.GetTextureFrameCount();
								if (ts)
								{
									mediaPlayer.m_Control.Seek(num2);
								}
								else
								{
									mediaPlayer.m_Control.SeekFast(num2);
								}
								if (!mediaPlayer.m_Control.WaitForNextFrame(bfk(), textureFrameCount))
								{
									tu = mediaPlayer.ek.GetTextureFrameCount();
									tv = 0;
									tw = 50;
									goto IL_0122;
								}
								goto IL_0153;
							}
							texture = mediaPlayer.ek.GetTexture();
						}
						else
						{
							texture = mediaPlayer.ek.GetTexture();
						}
					}
					goto IL_016f;
				case 1:
					y = -1;
					goto IL_0122;
				case 2:
					{
						y = -1;
						return false;
					}
					IL_016f:
					if (texture != null)
					{
						bs = dl.bix(texture, mediaPlayer.ek.RequiresVerticalFlip(), dl.bil(mediaPlayer.ei.GetTextureTransform()), tp);
					}
					tt(bs);
					z = null;
					y = 2;
					return true;
					IL_0153:
					texture = mediaPlayer.ek.GetTexture();
					goto IL_016f;
					IL_0122:
					if (tu + 1 >= mediaPlayer.ek.GetTextureFrameCount() && tv++ < tw)
					{
						z = null;
						y = 1;
						return true;
					}
					goto IL_0153;
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		public b m_VideoLocation = b.RelativeToStreamingAssetsFolder;

		public string m_VideoPath;

		public bool m_AutoOpen = true;

		public bool m_AutoStart = true;

		public bool m_Loop;

		[Range(0f, 1f)]
		public float m_Volume = 1f;

		[SerializeField]
		[Range(-1f, 1f)]
		private float m_Balance;

		public bool m_Muted;

		[SerializeField]
		[Range(-4f, 4f)]
		public float m_PlaybackRate = 1f;

		public bool m_Resample;

		public dn.b m_ResampleMode;

		[Range(3f, 10f)]
		public int m_ResampleBufferSize = 5;

		private dn m_Resampler;

		[SerializeField]
		private bool m_Persistent;

		[SerializeField]
		private dc m_videoMapping;

		public cx m_StereoPacking;

		public cz m_AlphaPacking;

		public bool m_DisplayDebugStereoColorTint;

		public FilterMode m_FilterMode = FilterMode.Bilinear;

		public TextureWrapMode m_WrapMode = TextureWrapMode.Clamp;

		[Range(0f, 16f)]
		public int m_AnisoLevel;

		[SerializeField]
		private bool m_LoadSubtitles;

		[SerializeField]
		private b m_SubtitleLocation = b.RelativeToStreamingAssetsFolder;

		private b m_queueSubtitleLocation;

		[SerializeField]
		private string m_SubtitlePath;

		private string m_queueSubtitlePath;

		private Coroutine m_loadSubtitlesRoutine;

		[SerializeField]
		private Transform m_AudioHeadTransform;

		[SerializeField]
		private bool m_AudioFocusEnabled;

		[SerializeField]
		private Transform m_AudioFocusTransform;

		[SerializeField]
		[Range(40f, 120f)]
		private float m_AudioFocusWidthDegrees = 90f;

		[SerializeField]
		[Range(-24f, 0f)]
		private float m_AudioFocusOffLevelDB;

		[SerializeField]
		private MediaPlayerEvent m_events;

		[SerializeField]
		private int m_eventMask = -1;

		[SerializeField]
		private de m_forceFileFormat;

		[SerializeField]
		private bool _pauseMediaOnAppPause = true;

		[SerializeField]
		private bool _playMediaOnAppUnpause = true;

		private ct m_Control;

		private cv m_Texture;

		private cu m_Info;

		private cr m_Player;

		private cs m_Subtitles;

		private IDisposable m_Dispose;

		private bool m_VideoOpened;

		private bool m_AutoStartTriggered;

		private bool m_WasPlayingOnExternalPause;

		private Coroutine _renderingCoroutine;

		private static bool s_GlobalStartup;

		private bool m_EventFired_ReadyToPlay;

		private bool m_EventFired_Started;

		private bool m_EventFired_FirstFrameReady;

		private bool m_EventFired_FinishedPlaying;

		private bool m_EventFired_MetaDataReady;

		private bool m_EventState_PlaybackStalled;

		private bool m_EventState_PlaybackBuffering;

		private bool m_EventState_PlaybackSeeking;

		private int m_EventState_PreviousWidth;

		private int m_EventState_PreviousHeight;

		private int m_previousSubtitleIndex = -1;

		private static Camera m_DummyCamera;

		private bool m_FinishedFrameOpenCheck;

		[SerializeField]
		private uint m_sourceSampleRate;

		[SerializeField]
		private uint m_sourceChannels;

		[SerializeField]
		private bool m_manuallySetAudioSourceProperties;

		[SerializeField]
		private OptionsWindows _optionsWindows = new OptionsWindows();

		[SerializeField]
		private OptionsMacOSX _optionsMacOSX = new OptionsMacOSX();

		[SerializeField]
		private OptionsIOS _optionsIOS = new OptionsIOS();

		[SerializeField]
		private OptionsTVOS _optionsTVOS = new OptionsTVOS();

		[SerializeField]
		private OptionsAndroid _optionsAndroid = new OptionsAndroid();

		[SerializeField]
		private OptionsWindowsPhone _optionsWindowsPhone = new OptionsWindowsPhone();

		[SerializeField]
		private OptionsWindowsUWP _optionsWindowsUWP = new OptionsWindowsUWP();

		[SerializeField]
		private OptionsWebGL _optionsWebGL = new OptionsWebGL();

		[SerializeField]
		private OptionsPS4 _optionsPS4 = new OptionsPS4();

		public dn ef => m_Resampler;

		public bool eg
		{
			get
			{
				return m_Persistent;
			}
			set
			{
				m_Persistent = value;
			}
		}

		public dc eh
		{
			get
			{
				return m_videoMapping;
			}
			set
			{
				m_videoMapping = value;
			}
		}

		public virtual cu ei => m_Info;

		public virtual ct ej => m_Control;

		public virtual cr dt => m_Player;

		public virtual cv ek => m_Texture;

		public virtual cs el => m_Subtitles;

		public MediaPlayerEvent em
		{
			get
			{
				if (m_events == null)
				{
					m_events = new MediaPlayerEvent();
				}
				return m_events;
			}
		}

		public bool en => m_VideoOpened;

		public bool eo
		{
			get
			{
				return _pauseMediaOnAppPause;
			}
			set
			{
				_pauseMediaOnAppPause = value;
			}
		}

		public bool ep
		{
			get
			{
				return _playMediaOnAppUnpause;
			}
			set
			{
				_playMediaOnAppUnpause = value;
			}
		}

		public de eq
		{
			get
			{
				return m_forceFileFormat;
			}
			set
			{
				m_forceFileFormat = value;
			}
		}

		public Transform er
		{
			get
			{
				return m_AudioHeadTransform;
			}
			set
			{
				m_AudioHeadTransform = value;
			}
		}

		public bool es
		{
			get
			{
				return m_AudioFocusEnabled;
			}
			set
			{
				m_AudioFocusEnabled = value;
			}
		}

		public float et
		{
			get
			{
				return m_AudioFocusOffLevelDB;
			}
			set
			{
				m_AudioFocusOffLevelDB = value;
			}
		}

		public float eu
		{
			get
			{
				return m_AudioFocusWidthDegrees;
			}
			set
			{
				m_AudioFocusWidthDegrees = value;
			}
		}

		public Transform ev
		{
			get
			{
				return m_AudioFocusTransform;
			}
			set
			{
				m_AudioFocusTransform = value;
			}
		}

		public OptionsWindows ew => _optionsWindows;

		public OptionsMacOSX ex => _optionsMacOSX;

		public OptionsIOS ey => _optionsIOS;

		public OptionsTVOS ez => _optionsTVOS;

		public OptionsAndroid fa => _optionsAndroid;

		public OptionsWindowsPhone fb => _optionsWindowsPhone;

		public OptionsWindowsUWP fc => _optionsWindowsUWP;

		public OptionsWebGL fd => _optionsWebGL;

		public OptionsPS4 fe => _optionsPS4;

		public bool ff => m_LoadSubtitles;

		public string fg => m_SubtitlePath;

		public b fh => m_SubtitleLocation;

		private void Awake()
		{
			if (m_Persistent)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		protected void bdt()
		{
			BaseMediaPlayer baseMediaPlayer = bfa();
			if (baseMediaPlayer != null)
			{
				m_Control = baseMediaPlayer;
				m_Texture = baseMediaPlayer;
				m_Info = baseMediaPlayer;
				m_Player = baseMediaPlayer;
				m_Subtitles = baseMediaPlayer;
				m_Dispose = baseMediaPlayer;
				if (!s_GlobalStartup)
				{
					dl.bij(string.Format("Initialising AVPro Video (script v{0} plugin v{1}) on {2}/{3} (MT {4}) on {5} {6}", "1.11.7", baseMediaPlayer.GetVersion(), SystemInfo.graphicsDeviceName, SystemInfo.graphicsDeviceVersion, SystemInfo.graphicsMultiThreaded, Application.platform, SystemInfo.operatingSystem));
					s_GlobalStartup = true;
				}
			}
		}

		private void Start()
		{
			if (m_Control == null)
			{
				bdt();
			}
			if (m_Control == null)
			{
				return;
			}
			if (m_AutoOpen)
			{
				bej();
				if (m_LoadSubtitles && m_Subtitles != null && !string.IsNullOrEmpty(m_SubtitlePath))
				{
					bec(m_SubtitleLocation, m_SubtitlePath);
				}
			}
			bep();
		}

		public bool bdu(b a, string b, bool c = true)
		{
			m_VideoLocation = a;
			m_VideoPath = b;
			m_AutoStart = c;
			if (m_Control == null)
			{
				m_AutoOpen = false;
				bdt();
			}
			return bej();
		}

		public bool bdv(byte[] a, bool b = true)
		{
			m_VideoLocation = MediaPlayer.b.AbsolutePathOrURL;
			m_VideoPath = "buffer";
			m_AutoStart = b;
			if (m_Control == null)
			{
				bdt();
			}
			return bef(a);
		}

		public bool bdw(ulong a, bool b = true)
		{
			m_VideoLocation = MediaPlayer.b.AbsolutePathOrURL;
			m_VideoPath = "buffer";
			m_AutoStart = b;
			if (m_Control == null)
			{
				bdt();
			}
			return beg(a);
		}

		public bool bdx(byte[] a, ulong b, ulong c)
		{
			return beh(a, b, c);
		}

		public bool bdy()
		{
			return bei();
		}

		public bool bec(b a, string b)
		{
			bool result = false;
			if (m_Subtitles != null)
			{
				if (!string.IsNullOrEmpty(b))
				{
					string text = bez(bes(), ref b, ref a);
					bool flag = true;
					if (text.Contains("://"))
					{
						flag = false;
					}
					if (flag && !File.Exists(text))
					{
						UnityEngine.Debug.LogError("[AVProVideo] Subtitle file not found: " + text, this);
					}
					else
					{
						dl.bij("Opening subtitles " + text, this);
						m_previousSubtitleIndex = -1;
						try
						{
							if (text.Contains("://"))
							{
								if (m_loadSubtitlesRoutine != null)
								{
									StopCoroutine(m_loadSubtitlesRoutine);
									m_loadSubtitlesRoutine = null;
								}
								m_loadSubtitlesRoutine = StartCoroutine(bed(text, a, b));
							}
							else
							{
								string a2 = File.ReadAllText(text);
								if (m_Subtitles.LoadSubtitlesSRT(a2))
								{
									m_SubtitleLocation = a;
									m_SubtitlePath = b;
									m_LoadSubtitles = false;
									result = true;
								}
								else
								{
									UnityEngine.Debug.LogError("[AVProVideo] Failed to load subtitles" + text, this);
								}
							}
						}
						catch (Exception exception)
						{
							UnityEngine.Debug.LogError("[AVProVideo] Failed to load subtitles " + text, this);
							UnityEngine.Debug.LogException(exception, this);
						}
					}
				}
				else
				{
					UnityEngine.Debug.LogError("[AVProVideo] No subtitle file path specified", this);
				}
			}
			else
			{
				m_queueSubtitleLocation = a;
				m_queueSubtitlePath = b;
			}
			return result;
		}

		[IteratorStateMachine(typeof(o))]
		private IEnumerator bed(string a, b b, string c)
		{
			return new o(0)
			{
				ba = this,
				tk = a,
				tl = b,
				tm = c
			};
		}

		public void bee()
		{
			if (m_loadSubtitlesRoutine != null)
			{
				StopCoroutine(m_loadSubtitlesRoutine);
				m_loadSubtitlesRoutine = null;
			}
			if (m_Subtitles != null)
			{
				m_previousSubtitleIndex = -1;
				m_LoadSubtitles = false;
				m_Subtitles.LoadSubtitlesSRT(string.Empty);
			}
			else
			{
				m_queueSubtitlePath = string.Empty;
			}
		}

		private bool bef(byte[] a)
		{
			bool result = false;
			if (m_Control != null)
			{
				bel();
				m_VideoOpened = true;
				m_AutoStartTriggered = !m_AutoStart;
				dl.bij("Opening buffer of length " + a.Length, this);
				if (!m_Control.OpenVideoFromBuffer(a))
				{
					UnityEngine.Debug.LogError("[AVProVideo] Failed to open buffer", this);
					if (bet() != ew || ew.videoApi != df.a.DirectShow)
					{
						UnityEngine.Debug.LogError("[AVProVideo] Loading from buffer is currently only supported in Windows when using the DirectShow API");
					}
				}
				else
				{
					bek();
					result = true;
					bep();
				}
			}
			return result;
		}

		private bool beg(ulong a)
		{
			bool result = false;
			if (m_Control != null)
			{
				bel();
				m_VideoOpened = true;
				m_AutoStartTriggered = !m_AutoStart;
				dl.bij("Starting Opening buffer of length " + a, this);
				if (!m_Control.StartOpenVideoFromBuffer(a))
				{
					UnityEngine.Debug.LogError("[AVProVideo] Failed to start open video from buffer", this);
					if (bet() != ew || ew.videoApi != df.a.DirectShow)
					{
						UnityEngine.Debug.LogError("[AVProVideo] Loading from buffer is currently only supported in Windows when using the DirectShow API");
					}
				}
				else
				{
					bek();
					result = true;
					bep();
				}
			}
			return result;
		}

		private bool beh(byte[] a, ulong b, ulong c)
		{
			if (ej != null)
			{
				return ej.AddChunkToVideoBuffer(a, b, c);
			}
			return false;
		}

		private bool bei()
		{
			if (ej != null)
			{
				return ej.EndOpenVideoFromBuffer();
			}
			return false;
		}

		private bool bej()
		{
			bool result = false;
			if (m_Control != null)
			{
				bel();
				m_VideoOpened = true;
				m_AutoStartTriggered = !m_AutoStart;
				m_FinishedFrameOpenCheck = true;
				long num = bex();
				string text = bez(bes(), ref m_VideoPath, ref m_VideoLocation);
				if (!string.IsNullOrEmpty(m_VideoPath))
				{
					string c = null;
					bool flag = true;
					if (text.Contains("://"))
					{
						flag = false;
						c = bey();
					}
					if (flag && !File.Exists(text))
					{
						UnityEngine.Debug.LogError("[AVProVideo] File not found: " + text, this);
					}
					else
					{
						dl.bij($"Opening {text} (offset {num}) with API {bew()}", this);
						if (_optionsWindows.enableAudio360)
						{
							m_Control.SetAudioChannelMode(_optionsWindows.audio360ChannelMode);
						}
						else
						{
							m_Control.SetAudioChannelMode(di.INVALID);
						}
						if (!m_Control.OpenVideoFromFile(text, num, c, m_manuallySetAudioSourceProperties ? m_sourceSampleRate : 0u, m_manuallySetAudioSourceProperties ? m_sourceChannels : 0u, (int)m_forceFileFormat))
						{
							UnityEngine.Debug.LogError("[AVProVideo] Failed to open " + text, this);
						}
						else
						{
							bek();
							result = true;
							bep();
						}
					}
				}
				else
				{
					UnityEngine.Debug.LogError("[AVProVideo] No file path specified", this);
				}
			}
			return result;
		}

		private void bek()
		{
			if (m_Control != null)
			{
				m_Control.SetLooping(m_Loop);
				m_Control.SetPlaybackRate(m_PlaybackRate);
				m_Control.SetVolume(m_Volume);
				m_Control.SetBalance(m_Balance);
				m_Control.MuteAudio(m_Muted);
				m_Control.SetTextureProperties(m_FilterMode, m_WrapMode, m_AnisoLevel);
				PlatformOptions platformOptions = bet();
				if (platformOptions != null)
				{
					m_Control.SetKeyServerURL(platformOptions.bby());
					m_Control.SetKeyServerAuthToken(platformOptions.bbz());
					m_Control.SetDecryptionKeyBase64(platformOptions.bca());
				}
			}
		}

		public void bel()
		{
			if (m_Control != null)
			{
				if (m_events != null && m_VideoOpened && m_events.bia() && bfg(MediaPlayerEvent.a.Closing))
				{
					m_events.Invoke(this, MediaPlayerEvent.a.Closing, da.None);
				}
				m_AutoStartTriggered = false;
				m_VideoOpened = false;
				m_EventFired_MetaDataReady = false;
				m_EventFired_ReadyToPlay = false;
				m_EventFired_Started = false;
				m_EventFired_FirstFrameReady = false;
				m_EventFired_FinishedPlaying = false;
				m_EventState_PlaybackBuffering = false;
				m_EventState_PlaybackSeeking = false;
				m_EventState_PlaybackStalled = false;
				m_EventState_PreviousWidth = 0;
				m_EventState_PreviousHeight = 0;
				if (m_loadSubtitlesRoutine != null)
				{
					StopCoroutine(m_loadSubtitlesRoutine);
					m_loadSubtitlesRoutine = null;
				}
				m_previousSubtitleIndex = -1;
				m_Control.CloseVideo();
			}
			if (m_Resampler != null)
			{
				m_Resampler.Reset();
			}
			beq();
		}

		public void Play()
		{
			if (m_Control != null && m_Control.CanPlay())
			{
				m_Control.Play();
				m_EventFired_ReadyToPlay = true;
			}
			else
			{
				m_AutoStart = true;
				m_AutoStartTriggered = false;
			}
		}

		public void Pause()
		{
			if (m_Control != null && m_Control.IsPlaying())
			{
				m_Control.Pause();
			}
			m_WasPlayingOnExternalPause = false;
		}

		public void bem()
		{
			if (m_Control != null)
			{
				m_Control.Stop();
			}
		}

		public void ben(bool a)
		{
			if (m_Control != null)
			{
				if (a)
				{
					Pause();
				}
				m_Control.Rewind();
			}
		}

		protected virtual void Update()
		{
			if (m_Control != null)
			{
				if (m_VideoOpened && m_AutoStart && !m_AutoStartTriggered && m_Control.CanPlay())
				{
					m_AutoStartTriggered = true;
					Play();
				}
				if (_renderingCoroutine == null && m_Control.CanPlay())
				{
					bep();
				}
				if (m_Subtitles != null && !string.IsNullOrEmpty(m_queueSubtitlePath))
				{
					bec(m_queueSubtitleLocation, m_queueSubtitlePath);
					m_queueSubtitlePath = string.Empty;
				}
				bfd();
				bfc();
				m_Player.Update();
				bfe();
				bff();
				m_Control.ResetEventState();
			}
		}

		private void LateUpdate()
		{
			beo();
		}

		private void beo()
		{
			if (m_Resample && m_Resampler == null)
			{
				m_Resampler = new dn(this, base.gameObject.name, m_ResampleBufferSize, m_ResampleMode);
			}
			if (m_Resampler != null)
			{
				m_Resampler.Update();
				m_Resampler.bjy();
			}
		}

		private void OnEnable()
		{
			if (m_Control != null && m_WasPlayingOnExternalPause)
			{
				m_AutoStart = true;
				m_AutoStartTriggered = false;
				m_WasPlayingOnExternalPause = false;
			}
			if (m_Player != null)
			{
				m_Player.OnEnable();
			}
			bep();
		}

		private void OnDisable()
		{
			if (m_Control != null && m_Control.IsPlaying())
			{
				m_WasPlayingOnExternalPause = true;
				Pause();
			}
			beq();
		}

		protected virtual void OnDestroy()
		{
			bel();
			if (m_Dispose != null)
			{
				m_Dispose.Dispose();
				m_Dispose = null;
			}
			m_Control = null;
			m_Texture = null;
			m_Info = null;
			m_Player = null;
			if (m_Resampler != null)
			{
				m_Resampler.bjm();
				m_Resampler = null;
			}
		}

		private void OnApplicationQuit()
		{
			if (!s_GlobalStartup)
			{
				return;
			}
			dl.bij("Shutdown");
			MediaPlayer[] array = Resources.FindObjectsOfTypeAll<MediaPlayer>();
			if (array != null && array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].bel();
					array[i].OnDestroy();
				}
			}
			@do.bka();
			s_GlobalStartup = false;
		}

		private void bep()
		{
			if (_renderingCoroutine == null)
			{
				_renderingCoroutine = StartCoroutine(ber());
			}
		}

		private void beq()
		{
			if (_renderingCoroutine != null)
			{
				StopCoroutine(_renderingCoroutine);
				_renderingCoroutine = null;
			}
		}

		[IteratorStateMachine(typeof(p))]
		private IEnumerator ber()
		{
			return new p(0)
			{
				ba = this
			};
		}

		public static cw bes()
		{
			return cw.Windows;
		}

		public PlatformOptions bet()
		{
			return _optionsWindows;
		}

		public static string beu(b a)
		{
			string result = string.Empty;
			switch (a)
			{
			case b.RelativeToDataFolder:
				result = Application.dataPath;
				break;
			case b.RelativeToPersistentDataFolder:
				result = Application.persistentDataPath;
				break;
			case b.RelativeToProjectFolder:
			{
				string path = "..";
				result = Path.GetFullPath(Path.Combine(Application.dataPath, path));
				result = result.Replace('\\', '/');
				break;
			}
			case b.RelativeToStreamingAssetsFolder:
				result = Application.streamingAssetsPath;
				break;
			}
			return result;
		}

		public static string bev(string a, b b)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(a))
			{
				switch (b)
				{
				case b.AbsolutePathOrURL:
					result = a;
					break;
				case b.RelativeToProjectFolder:
				case b.RelativeToStreamingAssetsFolder:
				case b.RelativeToDataFolder:
				case b.RelativeToPersistentDataFolder:
					result = Path.Combine(beu(b), a);
					break;
				}
			}
			return result;
		}

		private string bew()
		{
			_ = string.Empty;
			return _optionsWindows.videoApi.ToString();
		}

		private long bex()
		{
			return 0L;
		}

		private string bey()
		{
			string text = null;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			return text;
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetShortPathNameW", SetLastError = true)]
		private static extern int GetShortPathName([MarshalAs(UnmanagedType.LPWStr)] string a, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder b, int c);

		private string bez(cw a, ref string b, ref b c)
		{
			string empty = string.Empty;
			if (a != cw.Unknown)
			{
				PlatformOptions platformOptions = bet();
				if (platformOptions != null && platformOptions.overridePath)
				{
					b = platformOptions.path;
					c = platformOptions.pathLocation;
				}
			}
			empty = bev(b, c);
			if (empty.Length > 200 && !empty.Contains("://"))
			{
				empty = "\\\\?\\" + empty.Replace("/", "\\");
				int shortPathName = GetShortPathName(empty, null, 0);
				if (shortPathName > 0)
				{
					StringBuilder stringBuilder = new StringBuilder(shortPathName);
					if (GetShortPathName(empty, stringBuilder, shortPathName) != 0)
					{
						empty = stringBuilder.ToString().Replace("\\\\?\\", "");
						UnityEngine.Debug.LogWarning("[AVProVideo] Long path detected. Changing to DOS 8.3 format");
					}
				}
			}
			return empty;
		}

		public virtual BaseMediaPlayer bfa()
		{
			BaseMediaPlayer baseMediaPlayer = null;
			if (@do.bjz())
			{
				baseMediaPlayer = new @do(_optionsWindows.videoApi, _optionsWindows.useHardwareDecoding, _optionsWindows.useTextureMips, _optionsWindows.hintAlphaChannel, _optionsWindows.useLowLatency, _optionsWindows.forceAudioOutputDeviceName, _optionsWindows.useUnityAudio, _optionsWindows.forceAudioResample, _optionsWindows.preferredFilters);
			}
			if (baseMediaPlayer == null)
			{
				UnityEngine.Debug.LogError($"[AVProVideo] Not supported on this platform {Application.platform} {SystemInfo.deviceModel} {SystemInfo.processorType} {SystemInfo.operatingSystem}.  Using null media player!");
				baseMediaPlayer = new dm();
			}
			return baseMediaPlayer;
		}

		private bool bfb(int a, float b)
		{
			bool result = false;
			DateTime now = DateTime.Now;
			int num = 0;
			while (ej != null && (DateTime.Now - now).TotalMilliseconds < (double)b)
			{
				m_Player.Update();
				if (a != ek.GetTextureFrameCount())
				{
					result = true;
					break;
				}
				num++;
			}
			m_Player.Render();
			return result;
		}

		private void bfc()
		{
			m_Control.SetAudioFocusEnabled(m_AudioFocusEnabled);
			m_Control.SetAudioFocusProperties(m_AudioFocusOffLevelDB, m_AudioFocusWidthDegrees);
			m_Control.SetAudioFocusRotation((m_AudioFocusTransform == null) ? Quaternion.identity : m_AudioFocusTransform.rotation);
		}

		private void bfd()
		{
			if (m_AudioHeadTransform != null)
			{
				m_Control.SetAudioHeadRotation(m_AudioHeadTransform.rotation);
			}
			else
			{
				m_Control.ResetAudioHeadRotation();
			}
		}

		private void bfe()
		{
			da lastError = m_Control.GetLastError();
			if (lastError != da.None)
			{
				UnityEngine.Debug.LogError("[AVProVideo] Error: " + dl.bih(lastError));
				_ = 100;
				if (m_events != null && m_events.bia() && bfg(MediaPlayerEvent.a.Error))
				{
					m_events.Invoke(this, MediaPlayerEvent.a.Error, lastError);
				}
			}
		}

		private void bff()
		{
			if (m_events == null || m_Control == null || !m_events.bia())
			{
				return;
			}
			m_FinishedFrameOpenCheck = false;
			if (bfg(MediaPlayerEvent.a.FinishedPlaying) && bfh(MediaPlayerEvent.a.FinishedPlaying, m_EventFired_FinishedPlaying))
			{
				m_EventFired_FinishedPlaying = !m_FinishedFrameOpenCheck;
			}
			if (m_EventFired_Started && bfg(MediaPlayerEvent.a.Started) && m_Control != null && !m_Control.IsPlaying() && !m_Control.IsSeeking())
			{
				m_EventFired_Started = false;
			}
			if (m_EventFired_FinishedPlaying && bfg(MediaPlayerEvent.a.FinishedPlaying) && m_Control != null && m_Control.IsPlaying() && !m_Control.IsFinished())
			{
				bool flag = true;
				flag = false;
				if (m_Info.HasVideo())
				{
					float num = 1000f / m_Info.GetVideoFrameRate();
					if (m_Info.GetDurationMs() - m_Control.GetCurrentTimeMs() > num)
					{
						flag = true;
					}
				}
				else if (m_Control.GetCurrentTimeMs() < m_Info.GetDurationMs())
				{
					flag = true;
				}
				if (flag)
				{
					m_EventFired_FinishedPlaying = false;
				}
			}
			m_EventFired_MetaDataReady = bfh(MediaPlayerEvent.a.MetaDataReady, m_EventFired_MetaDataReady);
			m_EventFired_ReadyToPlay = bfh(MediaPlayerEvent.a.ReadyToPlay, m_EventFired_ReadyToPlay);
			m_EventFired_Started = bfh(MediaPlayerEvent.a.Started, m_EventFired_Started);
			m_EventFired_FirstFrameReady = bfh(MediaPlayerEvent.a.FirstFrameReady, m_EventFired_FirstFrameReady);
			if (bfh(MediaPlayerEvent.a.SubtitleChange, b: false))
			{
				m_previousSubtitleIndex = m_Subtitles.GetSubtitleIndex();
			}
			if (bfh(MediaPlayerEvent.a.ResolutionChanged, b: false))
			{
				m_EventState_PreviousWidth = m_Info.GetVideoWidth();
				m_EventState_PreviousHeight = m_Info.GetVideoHeight();
			}
			if (bfg(MediaPlayerEvent.a.Stalled))
			{
				bool flag2 = m_Info.IsPlaybackStalled();
				if (flag2 != m_EventState_PlaybackStalled)
				{
					m_EventState_PlaybackStalled = flag2;
					MediaPlayerEvent.a a = (m_EventState_PlaybackStalled ? MediaPlayerEvent.a.Stalled : MediaPlayerEvent.a.Unstalled);
					bfh(a, b: false);
				}
			}
			if (bfg(MediaPlayerEvent.a.StartedSeeking))
			{
				bool flag3 = m_Control.IsSeekingStarted();
				if (flag3 != m_EventState_PlaybackSeeking)
				{
					m_EventState_PlaybackSeeking = flag3;
					MediaPlayerEvent.a a2 = (m_EventState_PlaybackSeeking ? MediaPlayerEvent.a.StartedSeeking : MediaPlayerEvent.a.FinishedSeeking);
					bfh(a2, b: false);
				}
			}
			if (bfg(MediaPlayerEvent.a.StartedBuffering))
			{
				bool flag4 = m_Control.IsBuffering();
				if (flag4 != m_EventState_PlaybackBuffering)
				{
					m_EventState_PlaybackBuffering = flag4;
					MediaPlayerEvent.a a3 = (m_EventState_PlaybackBuffering ? MediaPlayerEvent.a.StartedBuffering : MediaPlayerEvent.a.FinishedBuffering);
					bfh(a3, b: false);
				}
			}
		}

		protected bool bfg(MediaPlayerEvent.a a)
		{
			return ((uint)m_eventMask & (1 << (int)a)) != 0;
		}

		private bool bfh(MediaPlayerEvent.a a, bool b)
		{
			if (bfi(a, b))
			{
				b = true;
				m_events.Invoke(this, a, da.None);
			}
			return b;
		}

		private bool bfi(MediaPlayerEvent.a a, bool b)
		{
			bool result = false;
			if (m_events != null && m_Control != null && !b && bfg(a))
			{
				switch (a)
				{
				case MediaPlayerEvent.a.FinishedPlaying:
					result = (!m_Control.IsLooping() && m_Control.CanPlay() && m_Control.IsFinished()) || (m_Control.GetCurrentTimeMs() > m_Info.GetDurationMs() && !m_Control.IsLooping());
					break;
				case MediaPlayerEvent.a.MetaDataReady:
					result = m_Control.HasMetaData();
					break;
				case MediaPlayerEvent.a.FirstFrameReady:
					result = m_Texture != null && m_Control.CanPlay() && m_Control.HasMetaData() && m_Texture.GetTextureFrameCount() > 0;
					break;
				case MediaPlayerEvent.a.ReadyToPlay:
					result = !m_Control.IsPlaying() && m_Control.CanPlay() && !m_AutoStart;
					break;
				case MediaPlayerEvent.a.Started:
					result = m_Control.IsPlaying();
					break;
				case MediaPlayerEvent.a.SubtitleChange:
					result = m_previousSubtitleIndex != m_Subtitles.GetSubtitleIndex();
					break;
				case MediaPlayerEvent.a.Stalled:
					result = m_Info.IsPlaybackStalled();
					break;
				case MediaPlayerEvent.a.Unstalled:
					result = !m_Info.IsPlaybackStalled();
					break;
				case MediaPlayerEvent.a.StartedSeeking:
					result = m_Control.IsSeekingStarted();
					break;
				case MediaPlayerEvent.a.FinishedSeeking:
					result = !m_Control.IsSeeking();
					break;
				case MediaPlayerEvent.a.StartedBuffering:
					result = m_Control.IsBuffering();
					break;
				case MediaPlayerEvent.a.FinishedBuffering:
					result = !m_Control.IsBuffering();
					break;
				case MediaPlayerEvent.a.ResolutionChanged:
					result = m_Info != null && (m_EventState_PreviousWidth != m_Info.GetVideoWidth() || m_EventState_PreviousHeight != m_Info.GetVideoHeight());
					break;
				default:
					UnityEngine.Debug.LogWarning("[AVProVideo] Unhandled event type");
					break;
				}
			}
			return result;
		}

		private void OnApplicationFocus(bool a)
		{
		}

		private void OnApplicationPause(bool a)
		{
		}

		[ContextMenu("Save Frame To PNG")]
		public void bfj()
		{
			Texture2D texture2D = bfn(null);
			if (texture2D != null)
			{
				byte[] array = texture2D.EncodeToPNG();
				if (array != null)
				{
					string text = Mathf.FloorToInt(ej.GetCurrentTimeMs()).ToString("D8");
					File.WriteAllBytes("frame-" + text + ".png", array);
				}
				UnityEngine.Object.Destroy(texture2D);
			}
		}

		private static Camera bfk()
		{
			if (m_DummyCamera == null)
			{
				GameObject gameObject = GameObject.Find("AVPro Video Dummy Camera");
				if (gameObject == null)
				{
					gameObject = new GameObject("AVPro Video Dummy Camera");
					gameObject.hideFlags = HideFlags.DontSave | HideFlags.HideInHierarchy;
					gameObject.SetActive(value: false);
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					m_DummyCamera = gameObject.AddComponent<Camera>();
					m_DummyCamera.hideFlags = HideFlags.DontSave | HideFlags.HideInInspector;
					m_DummyCamera.cullingMask = 0;
					m_DummyCamera.clearFlags = CameraClearFlags.Nothing;
					m_DummyCamera.enabled = false;
				}
				else
				{
					m_DummyCamera = gameObject.GetComponent<Camera>();
				}
			}
			return m_DummyCamera;
		}

		[IteratorStateMachine(typeof(q))]
		private IEnumerator bfl(Texture2D a, n b, float c = -1f, bool d = true, int e = 1000, int f = 100)
		{
			return new q(0)
			{
				ba = this,
				tp = a,
				tt = b,
				tq = c,
				ts = d,
				tr = f
			};
		}

		public void bfm(Texture2D a, n b, float c = -1f, bool d = true, int e = 1000, int f = 100)
		{
			StartCoroutine(bfl(a, b, c, d, e, f));
		}

		public Texture2D bfn(Texture2D a, float b = -1f, bool c = true, int d = 1000, int e = 100)
		{
			Texture2D result = a;
			Texture texture = bfo(b, c, d, e);
			if (texture != null)
			{
				result = dl.bix(texture, ek.RequiresVerticalFlip(), dl.bil(ei.GetTextureTransform()), a);
			}
			return result;
		}

		private Texture bfo(float a = -1f, bool b = true, int c = 1000, int d = 100)
		{
			Texture result = null;
			if (m_Control != null)
			{
				if (a >= 0f)
				{
					Pause();
					float num = a * 1000f;
					if (ek.GetTexture() != null && Mathf.Abs(m_Control.GetCurrentTimeMs() - num) < (float)d)
					{
						result = ek.GetTexture();
					}
					else
					{
						int textureFrameCount = ek.GetTextureFrameCount();
						if (b)
						{
							m_Control.Seek(num);
						}
						else
						{
							m_Control.SeekFast(num);
						}
						bfb(textureFrameCount, c);
						result = ek.GetTexture();
					}
				}
				else
				{
					result = ek.GetTexture();
				}
			}
			return result;
		}
	}
}
