using System.IO;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Debug Overlay", -99)]
	public class DebugOverlay : MonoBehaviour
	{
		[SerializeField]
		private MediaPlayer _mediaPlayer;

		[SerializeField]
		private int _guiDepth = -1000;

		[SerializeField]
		private float _displaySize = 1f;

		private int _debugOverlayCount;

		[SerializeField]
		private bool _displayControls = true;

		private const int s_GuiStartWidth = 10;

		private const int s_GuiWidth = 180;

		private int m_GuiPositionX = 10;

		public bool eb
		{
			get
			{
				return _displayControls;
			}
			set
			{
				_displayControls = value;
			}
		}

		public MediaPlayer ec
		{
			get
			{
				return _mediaPlayer;
			}
			set
			{
				if (_mediaPlayer != value)
				{
					_mediaPlayer = value;
				}
			}
		}

		private void bbh(int a)
		{
			m_GuiPositionX = Mathf.FloorToInt(10f * _displaySize + (float)(180 * a) * _displaySize);
		}

		private void Update()
		{
			_debugOverlayCount = 0;
		}

		private void OnGUI()
		{
			if (Event.current.type == EventType.Layout)
			{
				bbh(_debugOverlayCount++);
			}
			if (!(_mediaPlayer != null) || _mediaPlayer.ei == null)
			{
				return;
			}
			cu cu = _mediaPlayer.ei;
			ct ct = _mediaPlayer.ej;
			cv cv = _mediaPlayer.ek;
			GUI.depth = _guiDepth;
			GUI.matrix = Matrix4x4.TRS(new Vector3(m_GuiPositionX, 10f, 0f), Quaternion.identity, new Vector3(_displaySize, _displaySize, 1f));
			GUILayout.BeginVertical("box", GUILayout.MaxWidth(180f));
			GUILayout.Label(Path.GetFileName(_mediaPlayer.m_VideoPath));
			GUILayout.Label("Dimensions: " + cu.GetVideoWidth() + "x" + cu.GetVideoHeight() + "@" + cu.GetVideoFrameRate().ToString("F2"));
			GUILayout.Label("Time: " + (ct.GetCurrentTimeMs() * 0.001f).ToString("F1") + "s / " + (cu.GetDurationMs() * 0.001f).ToString("F1") + "s");
			GUILayout.Label("Rate: " + cu.GetVideoDisplayRate().ToString("F2") + "Hz");
			if (_mediaPlayer.m_Resample && _mediaPlayer.ef != null)
			{
				dn dn = _mediaPlayer.ef;
				GUILayout.BeginVertical();
				GUILayout.Label("Resampler Info:");
				GUILayout.Label("Resampler timestamp: " + dn.ge);
				GUILayout.Label("Resampler frames dropped: " + dn.fz);
				GUILayout.Label("Resampler frame displayed timer: " + dn.ga);
				GUILayout.EndVertical();
			}
			if (cv != null && cv.GetTexture() != null)
			{
				GUILayout.BeginHorizontal();
				Rect rect = GUILayoutUtility.GetRect(32f, 32f);
				GUILayout.Space(8f);
				Rect rect2 = GUILayoutUtility.GetRect(32f, 32f);
				Matrix4x4 matrix = GUI.matrix;
				if (cv.RequiresVerticalFlip())
				{
					GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0f, rect.y + rect.height / 2f));
				}
				GUI.DrawTexture(rect, cv.GetTexture(), ScaleMode.ScaleToFit, alphaBlend: false);
				GUI.DrawTexture(rect2, cv.GetTexture(), ScaleMode.ScaleToFit, alphaBlend: true);
				GUI.matrix = matrix;
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}
			if (_displayControls)
			{
				GUILayout.BeginHorizontal();
				if (ct.IsPaused())
				{
					if (GUILayout.Button("Play", GUILayout.Width(50f)))
					{
						ct.Play();
					}
				}
				else if (GUILayout.Button("Pause", GUILayout.Width(50f)))
				{
					ct.Pause();
				}
				float durationMs = cu.GetDurationMs();
				float currentTimeMs = ct.GetCurrentTimeMs();
				float num = GUILayout.HorizontalSlider(currentTimeMs, 0f, durationMs);
				if (num != currentTimeMs)
				{
					ct.Seek(num);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}
	}
}
