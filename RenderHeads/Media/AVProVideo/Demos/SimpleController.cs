using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class SimpleController : MonoBehaviour
	{
		private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public SimpleController ba;

			private float yc;

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
			public a(int a)
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
				SimpleController simpleController = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					yc = 0.25f;
					goto IL_00e5;
				case 1:
					y = -1;
					goto IL_00e5;
				case 2:
					y = -1;
					z = new WaitForEndOfFrame();
					y = 3;
					return true;
				case 3:
					y = -1;
					z = new WaitForEndOfFrame();
					y = 4;
					return true;
				case 4:
					y = -1;
					if (!Application.isPlaying)
					{
						break;
					}
					if (!simpleController._mediaPlayer.bdu(simpleController.yg, simpleController.yh, simpleController._mediaPlayer.m_AutoStart))
					{
						UnityEngine.Debug.LogError("Failed to open video!");
						break;
					}
					goto IL_01a1;
				case 5:
					y = -1;
					goto IL_01a1;
				case 6:
					y = -1;
					z = new WaitForEndOfFrame();
					y = 7;
					return true;
				case 7:
					y = -1;
					z = new WaitForEndOfFrame();
					y = 8;
					return true;
				case 8:
					y = -1;
					break;
				case 9:
					{
						y = -1;
						break;
					}
					IL_01a1:
					if (Application.isPlaying && (blf(simpleController._mediaPlayer) || blg(simpleController._mediaPlayer)))
					{
						z = null;
						y = 5;
						return true;
					}
					z = new WaitForEndOfFrame();
					y = 6;
					return true;
					IL_00e5:
					if (yc > 0f && Application.isPlaying)
					{
						yc -= Time.deltaTime;
						yc = Mathf.Clamp(yc, 0f, 0.25f);
						simpleController._display._color = new Color(1f, 1f, 1f, yc / 0.25f);
						simpleController._display._mediaPlayer.ej.SetVolume(yc / 0.25f);
						z = null;
						y = 1;
						return true;
					}
					z = new WaitForEndOfFrame();
					y = 2;
					return true;
				}
				if (yc < 0.25f && Application.isPlaying)
				{
					yc += Time.deltaTime;
					yc = Mathf.Clamp(yc, 0f, 0.25f);
					simpleController._display._color = new Color(1f, 1f, 1f, yc / 0.25f);
					simpleController._display._mediaPlayer.ej.SetVolume(yc / 0.25f);
					z = null;
					y = 9;
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

		public string _folder = "AVProVideoSamples/";

		public string[] _filenames = new string[3] { "SampleSphere.mp4", "BigBuckBunny_360p30.mp3", "BigBuckBunny_720p30.mp4" };

		public string[] _streams;

		public MediaPlayer _mediaPlayer;

		public DisplayIMGUI _display;

		public GUISkin _guiSkin;

		private int uk;

		private int ul;

		private float yd;

		public bool _useFading = true;

		private Queue<string> ye = new Queue<string>(8);

		private float yf = 1f;

		private MediaPlayer.b yg;

		private string yh;

		private void Start()
		{
			_mediaPlayer.em.bib(blb);
		}

		private void OnDestroy()
		{
			_mediaPlayer.em.bic(blb);
		}

		public void blb(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			switch (b)
			{
			case MediaPlayerEvent.a.MetaDataReady:
			case MediaPlayerEvent.a.ResolutionChanged:
				bld();
				break;
			}
			blc(b);
		}

		private void blc(MediaPlayerEvent.a a)
		{
			UnityEngine.Debug.Log("[SimpleController] Event: " + a);
			ye.Enqueue(a.ToString());
			if (ye.Count > 5)
			{
				ye.Dequeue();
				yf = 1f;
			}
		}

		private void bld()
		{
			if (_mediaPlayer != null && _mediaPlayer.ei != null)
			{
				uk = _mediaPlayer.ei.GetVideoWidth();
				ul = _mediaPlayer.ei.GetVideoHeight();
				yd = _mediaPlayer.ei.GetDurationMs() / 1000f;
			}
		}

		private void Update()
		{
			if (!_useFading && _display != null && _display._mediaPlayer != null && _display._mediaPlayer.ej != null)
			{
				_display._color = Color.white;
				_display._mediaPlayer.ej.SetVolume(1f);
			}
			if (ye != null && ye.Count > 0)
			{
				yf -= Time.deltaTime;
				if (yf < 0f)
				{
					ye.Dequeue();
					yf = 1f;
				}
			}
		}

		private void ble(string a, bool b = false)
		{
			if (!b)
			{
				yg = MediaPlayer.b.RelativeToStreamingAssetsFolder;
			}
			else
			{
				yg = MediaPlayer.b.AbsolutePathOrURL;
			}
			yh = a;
			if (!_useFading)
			{
				if (!_mediaPlayer.bdu(yg, yh, _mediaPlayer.m_AutoStart))
				{
					UnityEngine.Debug.LogError("Failed to open video!");
				}
			}
			else
			{
				StartCoroutine("LoadVideoWithFading");
			}
		}

		private static bool blf(MediaPlayer a)
		{
			if (a != null && a.ek != null)
			{
				return a.ek.GetTextureFrameCount() <= 0;
			}
			return false;
		}

		private static bool blg(MediaPlayer a)
		{
			if (a != null && a.ej != null && a.ej.CanPlay() && a.ei.HasAudio())
			{
				return !a.ei.HasVideo();
			}
			return false;
		}

		[IteratorStateMachine(typeof(a))]
		private IEnumerator LoadVideoWithFading()
		{
			return new a(0)
			{
				ba = this
			};
		}

		private void OnGUI()
		{
			if (_mediaPlayer == null)
			{
				return;
			}
			GUI.depth = -10;
			if (_guiSkin != null)
			{
				GUI.skin = _guiSkin;
			}
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3((float)Screen.width / 960f, (float)Screen.height / 540f, 1f));
			GUILayout.BeginVertical("box");
			if (_mediaPlayer.ej != null)
			{
				GUILayout.Label("Loaded: " + _mediaPlayer.m_VideoPath);
				GUILayout.Label(string.Format("Size: {0}x{1} FPS: {3} Duration: {2}ms", uk, ul, _mediaPlayer.ei.GetDurationMs(), _mediaPlayer.ei.GetVideoFrameRate().ToString("F2")));
				GUILayout.Label("Updates: " + _mediaPlayer.ek.GetTextureFrameCount() + "    Rate: " + _mediaPlayer.ei.GetVideoDisplayRate().ToString("F1"));
				GUILayout.BeginHorizontal();
				_useFading = GUILayout.Toggle(_useFading, "Fade to Black During Loading");
				_mediaPlayer.m_AutoStart = GUILayout.Toggle(_mediaPlayer.m_AutoStart, "Auto Play After Load");
				bool loop = _mediaPlayer.m_Loop;
				bool flag = GUILayout.Toggle(loop, "Loop");
				if (flag != loop)
				{
					_mediaPlayer.m_Loop = flag;
					_mediaPlayer.ej.SetLooping(flag);
				}
				bool flag2 = _mediaPlayer.m_Muted;
				if (_mediaPlayer.ej != null)
				{
					flag2 = _mediaPlayer.ej.IsMuted();
				}
				bool flag3 = GUILayout.Toggle(flag2, "Mute");
				if (flag3 != flag2)
				{
					_mediaPlayer.m_Muted = flag3;
					_mediaPlayer.ej.MuteAudio(flag3);
				}
				GUILayout.EndHorizontal();
				int num = (int)_mediaPlayer.ej.GetCurrentTimeMs();
				int num2 = (int)GUILayout.HorizontalSlider(num, 0f, yd * 1000f);
				Rect lastRect = GUILayoutUtility.GetLastRect();
				float x = GUI.skin.horizontalSliderThumb.CalcSize(GUIContent.none).x;
				Rect position = lastRect;
				GUI.color = Color.green;
				position.xMin += x;
				position.y = position.yMax - 4f;
				position.width -= x * 1f;
				position.width *= _mediaPlayer.ej.GetBufferingProgress();
				position.height = 4f;
				GUI.DrawTexture(position, Texture2D.whiteTexture, ScaleMode.StretchToFill);
				GUI.color = Color.red;
				int bufferedTimeRangeCount = _mediaPlayer.ej.GetBufferedTimeRangeCount();
				for (int i = 0; i < bufferedTimeRangeCount; i++)
				{
					float b = 0f;
					float c = 0f;
					if (_mediaPlayer.ej.GetBufferedTimeRange(i, ref b, ref c))
					{
						position.xMin = x + lastRect.x + (lastRect.width - x * 1f) * (b / (yd * 1000f));
						position.xMax = x + lastRect.x + (lastRect.width - x * 1f) * (c / (yd * 1000f));
						GUI.DrawTexture(position, Texture2D.whiteTexture, ScaleMode.StretchToFill);
					}
				}
				GUI.color = Color.white;
				if (num2 != num)
				{
					_mediaPlayer.ej.Seek(num2);
				}
				if (!_mediaPlayer.ej.IsPlaying())
				{
					if (GUILayout.Button("Play"))
					{
						_mediaPlayer.ej.Play();
					}
				}
				else if (GUILayout.Button("Pause"))
				{
					_mediaPlayer.ej.Pause();
				}
				GUILayout.BeginHorizontal();
				int audioTrackCount = _mediaPlayer.ei.GetAudioTrackCount();
				int currentAudioTrack = _mediaPlayer.ej.GetCurrentAudioTrack();
				for (int j = 0; j < audioTrackCount; j++)
				{
					if (j == currentAudioTrack)
					{
						GUI.color = Color.green;
					}
					if (GUILayout.Button("Audio Track #" + (j + 1)))
					{
						_mediaPlayer.ej.SetAudioTrack(j);
					}
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.Label("Select a new file to play:");
			int num3 = GUILayout.SelectionGrid(-1, _filenames, 3);
			if (num3 >= 0)
			{
				ble(Path.Combine(_folder, _filenames[num3]));
			}
			GUILayout.Space(8f);
			GUILayout.Label("Select a new stream to play:");
			int num4 = GUILayout.SelectionGrid(-1, _streams, 1);
			if (num4 >= 0)
			{
				ble(_streams[num4], b: true);
			}
			GUILayout.Space(8f);
			GUILayout.Label("Recent Events: ");
			GUILayout.BeginVertical("box");
			int num5 = 0;
			foreach (string item in ye)
			{
				GUI.color = Color.white;
				if (num5 == 0)
				{
					GUI.color = new Color(1f, 1f, 1f, yf);
				}
				GUILayout.Label(item);
				num5++;
			}
			GUILayout.EndVertical();
			GUI.color = Color.white;
			GUILayout.EndVertical();
		}
	}
}
