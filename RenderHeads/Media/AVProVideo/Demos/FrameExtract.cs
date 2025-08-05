using System.IO;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class FrameExtract : MonoBehaviour
	{
		private const int xt = 8;

		public MediaPlayer _mediaPlayer;

		public bool _accurateSeek;

		public int _timeoutMs = 250;

		public GUISkin _skin;

		public bool _asyncExtract;

		public bool _saveToJPG;

		private string xu;

		private bool xv;

		private float xw;

		private int xx = -1;

		private Texture2D vd;

		private RenderTexture xy;

		private void Start()
		{
			_mediaPlayer.em.bib(bkj);
			xy = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
			xy.useMipMap = false;
			xy.autoGenerateMips = false;
			xy.antiAliasing = 1;
			xy.Create();
			RenderTexture.active = xy;
			GL.Clear(clearDepth: false, clearColor: true, Color.black, 0f);
			RenderTexture.active = null;
		}

		public void bkj(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			if (b != MediaPlayerEvent.a.MetaDataReady && b == MediaPlayerEvent.a.FirstFrameReady)
			{
				bkk();
			}
		}

		private void bkk()
		{
			cu cu = _mediaPlayer.ei;
			if (vd != null)
			{
				Object.Destroy(vd);
				vd = null;
			}
			int videoWidth = cu.GetVideoWidth();
			int videoHeight = cu.GetVideoHeight();
			vd = new Texture2D(videoWidth, videoHeight, TextureFormat.ARGB32, mipChain: false);
			xw = _mediaPlayer.ei.GetDurationMs() / 1000f / 8f;
			xu = Path.GetFileName(_mediaPlayer.m_VideoPath);
		}

		private void OnDestroy()
		{
			if (vd != null)
			{
				Object.Destroy(vd);
				vd = null;
			}
			if (xy != null)
			{
				RenderTexture.ReleaseTemporary(xy);
				xy = null;
			}
		}

		private void Update()
		{
			if (vd != null && xx >= 0 && xx < 8)
			{
				bkm();
			}
		}

		private void bkl(Texture2D a)
		{
			if (_saveToJPG)
			{
				string text = xu + "-" + xx + ".jpg";
				Debug.Log("Writing frame to file: " + text);
				File.WriteAllBytes(text, a.EncodeToJPG());
			}
			GL.PushMatrix();
			RenderTexture.active = xy;
			GL.LoadPixelMatrix(0f, xy.width, xy.height, 0f);
			Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
			float num = 8f;
			float num2 = (float)xy.width / 8f - num;
			float num3 = num2 / ((float)a.width / (float)a.height);
			float x = (num2 + num) * (float)xx;
			Graphics.DrawTexture(new Rect(x, (float)xy.height / 2f - num3 / 2f, num2, num3), a, sourceRect, 0, 0, 0, 0);
			RenderTexture.active = null;
			GL.PopMatrix();
			GL.InvalidateState();
			xv = false;
			xx++;
		}

		private void bkm()
		{
			if (!xv)
			{
				xv = true;
				float num = (float)xx * xw;
				if (!_asyncExtract)
				{
					vd = _mediaPlayer.bfn(vd, num, _accurateSeek, _timeoutMs);
					bkl(vd);
				}
				else
				{
					_mediaPlayer.bfm(vd, bkl, num, _accurateSeek, _timeoutMs);
				}
			}
		}

		private void OnGUI()
		{
			GUI.skin = _skin;
			if (xy != null)
			{
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), xy, ScaleMode.ScaleToFit, alphaBlend: false);
			}
			float num = 4f * ((float)Screen.height / 1080f);
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(num, num, 1f));
			GUILayout.Space(16f);
			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(expand: true), GUILayout.Width((float)Screen.width / num));
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Start Extracting Frames"))
			{
				xx = 0;
				RenderTexture.active = xy;
				GL.Clear(clearDepth: false, clearColor: true, Color.black, 0f);
				RenderTexture.active = null;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
	}
}
