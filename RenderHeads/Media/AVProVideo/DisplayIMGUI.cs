using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Display IMGUI", 200)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	[ExecuteInEditMode]
	public class DisplayIMGUI : MonoBehaviour
	{
		private const string PropChromaTexName = "_ChromaTex";

		private const string PropYpCbCrTransformName = "_YpCbCrTransform";

		public MediaPlayer _mediaPlayer;

		public bool _displayInEditor = true;

		public ScaleMode _scaleMode = ScaleMode.ScaleToFit;

		public Color _color = Color.white;

		public bool _alphaBlend;

		[SerializeField]
		private bool _useDepth;

		public int _depth;

		public bool _fullScreen = true;

		[Range(0f, 1f)]
		public float _x;

		[Range(0f, 1f)]
		public float _y;

		[Range(0f, 1f)]
		public float _width = 1f;

		[Range(0f, 1f)]
		public float _height = 1f;

		private static int _propAlphaPack;

		private static int _propVertScale;

		private static int _propApplyGamma;

		private static int _propChromaTex;

		private static int _propYpCbCrTransform;

		private static Shader _shaderAlphaPacking;

		private Material _material;

		private void Awake()
		{
			if (_propAlphaPack == 0)
			{
				_propAlphaPack = Shader.PropertyToID("AlphaPack");
				_propVertScale = Shader.PropertyToID("_VertScale");
				_propApplyGamma = Shader.PropertyToID("_ApplyGamma");
				_propChromaTex = Shader.PropertyToID("_ChromaTex");
				_propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
			}
		}

		private void Start()
		{
			if (!_useDepth)
			{
				base.useGUILayout = false;
			}
			if (_shaderAlphaPacking == null)
			{
				_shaderAlphaPacking = Shader.Find("AVProVideo/IMGUI/Texture Transparent");
				if (_shaderAlphaPacking == null)
				{
					Debug.LogWarning("[AVProVideo] Missing shader AVProVideo/IMGUI/Transparent Packed");
				}
			}
		}

		private void OnDestroy()
		{
			if (_material != null)
			{
				Object.Destroy(_material);
				_material = null;
			}
		}

		private Shader bbi()
		{
			Shader shader = null;
			cz alphaPacking = _mediaPlayer.m_AlphaPacking;
			if (alphaPacking != cz.None && (uint)(alphaPacking - 1) <= 1u)
			{
				shader = _shaderAlphaPacking;
			}
			if (shader == null && _mediaPlayer.ei != null && QualitySettings.activeColorSpace == ColorSpace.Linear && !_mediaPlayer.ei.PlayerSupportsLinearColorSpace())
			{
				shader = _shaderAlphaPacking;
			}
			if (shader == null && _mediaPlayer.ek != null && _mediaPlayer.ek.GetTextureCount() == 2)
			{
				shader = _shaderAlphaPacking;
			}
			return shader;
		}

		private void Update()
		{
			if (!(_mediaPlayer != null))
			{
				return;
			}
			Shader shader = null;
			if (_material != null)
			{
				shader = _material.shader;
			}
			Shader shader2 = bbi();
			if (shader != shader2)
			{
				if (_material != null)
				{
					Object.Destroy(_material);
					_material = null;
				}
				if (shader2 != null)
				{
					_material = new Material(shader2);
				}
			}
			if (_material != null)
			{
				if (_material.HasProperty(_propAlphaPack))
				{
					dl.biq(_material, _mediaPlayer.m_AlphaPacking);
				}
				if (_material.HasProperty(_propApplyGamma) && _mediaPlayer.ei != null)
				{
					dl.bir(_material, _mediaPlayer.ei.PlayerSupportsLinearColorSpace());
				}
			}
		}

		private void OnGUI()
		{
			if (_mediaPlayer == null)
			{
				return;
			}
			bool flag = false;
			Texture texture = null;
			_ = _displayInEditor;
			if (_mediaPlayer.ei != null && !_mediaPlayer.ei.HasVideo())
			{
				texture = null;
			}
			if (_mediaPlayer.ek != null)
			{
				if (_mediaPlayer.m_Resample)
				{
					if (_mediaPlayer.ef.gf != null && _mediaPlayer.ef.gf[0] != null)
					{
						texture = _mediaPlayer.ef.gf[0];
						flag = _mediaPlayer.ek.RequiresVerticalFlip();
					}
				}
				else if (_mediaPlayer.ek.GetTexture() != null)
				{
					texture = _mediaPlayer.ek.GetTexture();
					flag = _mediaPlayer.ek.RequiresVerticalFlip();
				}
				if (_mediaPlayer.ek.GetTextureCount() == 2 && _material != null)
				{
					Texture texture2 = ((_mediaPlayer.ef == null || _mediaPlayer.ef.gf == null) ? null : _mediaPlayer.ef.gf[1]);
					Texture value = (_mediaPlayer.m_Resample ? texture2 : _mediaPlayer.ek.GetTexture(1));
					_material.SetTexture(_propChromaTex, value);
					_material.SetMatrix(_propYpCbCrTransform, _mediaPlayer.ek.GetYpCbCrTransform());
					_material.EnableKeyword("USE_YPCBCR");
				}
			}
			if (!(texture != null) || (_alphaBlend && !(_color.a > 0f)))
			{
				return;
			}
			GUI.depth = _depth;
			GUI.color = _color;
			Rect rect = bbj();
			if (_material != null)
			{
				if (flag)
				{
					_material.SetFloat(_propVertScale, -1f);
				}
				else
				{
					_material.SetFloat(_propVertScale, 1f);
				}
				if (QualitySettings.activeColorSpace == ColorSpace.Linear && !GL.sRGBWrite)
				{
					GL.sRGBWrite = true;
					dl.biw(rect, texture, _scaleMode, _mediaPlayer.m_AlphaPacking, _material);
					GL.sRGBWrite = false;
				}
				else
				{
					dl.biw(rect, texture, _scaleMode, _mediaPlayer.m_AlphaPacking, _material);
				}
			}
			else
			{
				if (flag)
				{
					GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0f, rect.y + rect.height / 2f));
				}
				GUI.DrawTexture(rect, texture, _scaleMode, _alphaBlend);
			}
		}

		public Rect bbj()
		{
			return (!_fullScreen) ? new Rect(_x * (float)(Screen.width - 1), _y * (float)(Screen.height - 1), _width * (float)Screen.width, _height * (float)Screen.height) : new Rect(0f, 0f, Screen.width, Screen.height);
		}
	}
}
