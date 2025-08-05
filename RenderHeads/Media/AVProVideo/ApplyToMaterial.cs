using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Apply To Material", 300)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class ApplyToMaterial : MonoBehaviour
	{
		[Header("Media Source")]
		[Space(8f)]
		[SerializeField]
		private MediaPlayer _media;

		[Tooltip("Default texture to display when the video texture is preparing")]
		[SerializeField]
		private Texture2D _defaultTexture;

		[Space(8f)]
		[Header("Material Target")]
		[SerializeField]
		private Material _material;

		[SerializeField]
		private string _texturePropertyName = "_MainTex";

		[SerializeField]
		private Vector2 _offset = Vector2.zero;

		[SerializeField]
		private Vector2 _scale = Vector2.one;

		private bool _isDirty;

		private Texture _lastTextureApplied;

		private int _propTexture;

		private Texture _originalTexture;

		private Vector2 _originalScale = Vector2.one;

		private Vector2 _originalOffset = Vector2.zero;

		private static int _propStereo;

		private static int _propAlphaPack;

		private static int _propApplyGamma;

		private static int _propLayout;

		private static int _propCroppingScalars;

		private const string PropChromaTexName = "_ChromaTex";

		private static int _propChromaTex;

		private const string PropYpCbCrTransformName = "_YpCbCrTransform";

		private static int _propYpCbCrTransform;

		private const string PropUseYpCbCrName = "_UseYpCbCr";

		private static int _propUseYpCbCr;

		public MediaPlayer dt
		{
			get
			{
				return _media;
			}
			set
			{
				zx(value);
			}
		}

		public Texture2D du
		{
			get
			{
				return _defaultTexture;
			}
			set
			{
				if (_defaultTexture != value)
				{
					_defaultTexture = value;
					_isDirty = true;
				}
			}
		}

		public Material dv
		{
			get
			{
				return _material;
			}
			set
			{
				if (_material != value)
				{
					_material = value;
					_isDirty = true;
				}
			}
		}

		public string dw
		{
			get
			{
				return _texturePropertyName;
			}
			set
			{
				if (_texturePropertyName != value)
				{
					_texturePropertyName = value;
					_propTexture = Shader.PropertyToID(_texturePropertyName);
					_isDirty = true;
				}
			}
		}

		public Vector2 dx
		{
			get
			{
				return _offset;
			}
			set
			{
				if (_offset != value)
				{
					_offset = value;
					_isDirty = true;
				}
			}
		}

		public Vector2 dy
		{
			get
			{
				return _scale;
			}
			set
			{
				if (_scale != value)
				{
					_scale = value;
					_isDirty = true;
				}
			}
		}

		private void Awake()
		{
			if (_propStereo == 0)
			{
				_propStereo = Shader.PropertyToID("Stereo");
			}
			if (_propAlphaPack == 0)
			{
				_propAlphaPack = Shader.PropertyToID("AlphaPack");
			}
			if (_propApplyGamma == 0)
			{
				_propApplyGamma = Shader.PropertyToID("_ApplyGamma");
			}
			if (_propLayout == 0)
			{
				_propLayout = Shader.PropertyToID("Layout");
			}
			if (_propChromaTex == 0)
			{
				_propChromaTex = Shader.PropertyToID("_ChromaTex");
			}
			if (_propYpCbCrTransform == 0)
			{
				_propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
			}
			if (_propUseYpCbCr == 0)
			{
				_propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
			}
			if (_propCroppingScalars == 0)
			{
				_propCroppingScalars = Shader.PropertyToID("_CroppingScalars");
			}
			if (_media != null)
			{
				_media.em.bib(zz);
			}
		}

		private void zx(MediaPlayer a)
		{
			if (_media != a)
			{
				if (_media != null)
				{
					_media.em.bic(zz);
				}
				_media = a;
				if (_media != null)
				{
					_media.em.bib(zz);
				}
				_isDirty = true;
			}
		}

		public void zy()
		{
			_isDirty = true;
			LateUpdate();
		}

		private void zz(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			if (b == MediaPlayerEvent.a.FirstFrameReady || b == MediaPlayerEvent.a.PropertiesChanged)
			{
				zy();
			}
		}

		private void LateUpdate()
		{
			bool flag = false;
			if (_media != null && _media.ek != null)
			{
				Texture texture = ((_media.ef == null || _media.ef.gf == null) ? null : _media.ef.gf[0]);
				Texture texture2 = (_media.m_Resample ? texture : _media.ek.GetTexture());
				if (texture2 != null)
				{
					if (texture2 != _lastTextureApplied)
					{
						_isDirty = true;
					}
					if (_isDirty)
					{
						int num = (_media.m_Resample ? 1 : _media.ek.GetTextureCount());
						for (int i = 0; i < num; i++)
						{
							Texture texture3 = ((_media.ef == null || _media.ef.gf == null) ? null : _media.ef.gf[i]);
							texture2 = (_media.m_Resample ? texture3 : _media.ek.GetTexture(i));
							if (texture2 != null)
							{
								baa(texture2, _media.ek.RequiresVerticalFlip(), i);
							}
						}
					}
					flag = true;
				}
			}
			if (flag)
			{
				return;
			}
			if (_defaultTexture != _lastTextureApplied)
			{
				_isDirty = true;
			}
			if (_isDirty)
			{
				if (_material != null && _material.HasProperty(_propUseYpCbCr))
				{
					_material.DisableKeyword("USE_YPCBCR");
				}
				baa(_defaultTexture, b: false);
			}
		}

		private void baa(Texture a, bool b, int c = 0)
		{
			if (!(_material != null))
			{
				return;
			}
			_isDirty = false;
			switch (c)
			{
			case 0:
				if (string.IsNullOrEmpty(_texturePropertyName))
				{
					_material.mainTexture = a;
					_lastTextureApplied = a;
					if (a != null)
					{
						if (b)
						{
							_material.mainTextureScale = new Vector2(_scale.x, 0f - _scale.y);
							_material.mainTextureOffset = Vector2.up + _offset;
						}
						else
						{
							_material.mainTextureScale = _scale;
							_material.mainTextureOffset = _offset;
						}
					}
					break;
				}
				_material.SetTexture(_propTexture, a);
				_lastTextureApplied = a;
				if (a != null)
				{
					if (b)
					{
						_material.SetTextureScale(_texturePropertyName, new Vector2(_scale.x, 0f - _scale.y));
						_material.SetTextureOffset(_texturePropertyName, Vector2.up + _offset);
					}
					else
					{
						_material.SetTextureScale(_texturePropertyName, _scale);
						_material.SetTextureOffset(_texturePropertyName, _offset);
					}
				}
				break;
			case 1:
				if (_material.HasProperty(_propUseYpCbCr))
				{
					_material.EnableKeyword("USE_YPCBCR");
				}
				if (!_material.HasProperty(_propChromaTex))
				{
					break;
				}
				_material.SetTexture(_propChromaTex, a);
				_material.SetMatrix(_propYpCbCrTransform, _media.ek.GetYpCbCrTransform());
				if (a != null)
				{
					if (b)
					{
						_material.SetTextureScale(_propChromaTex, new Vector2(_scale.x, 0f - _scale.y));
						_material.SetTextureOffset(_propChromaTex, Vector2.up + _offset);
					}
					else
					{
						_material.SetTextureScale(_propChromaTex, _scale);
						_material.SetTextureOffset(_propChromaTex, _offset);
					}
				}
				break;
			}
			if (_media != null)
			{
				if (_material.HasProperty(_propLayout))
				{
					dl.bio(_material, _media.eh);
				}
				if (_material.HasProperty(_propStereo))
				{
					dl.bip(_material, _media.m_StereoPacking, _media.m_DisplayDebugStereoColorTint);
				}
				if (_material.HasProperty(_propAlphaPack))
				{
					dl.biq(_material, _media.m_AlphaPacking);
				}
				if (_material.HasProperty(_propApplyGamma) && _media.ei != null)
				{
					dl.bir(_material, _media.ei.PlayerSupportsLinearColorSpace());
				}
				_propCroppingScalars |= 0;
			}
		}

		private void Start()
		{
			bab();
			LateUpdate();
		}

		private void OnEnable()
		{
			bab();
			_propTexture = Shader.PropertyToID(_texturePropertyName);
			_isDirty = true;
			LateUpdate();
		}

		private void OnDisable()
		{
			bac();
		}

		private void bab()
		{
			if (_material != null)
			{
				if (string.IsNullOrEmpty(_texturePropertyName))
				{
					_originalTexture = _material.mainTexture;
					_originalScale = _material.mainTextureScale;
					_originalOffset = _material.mainTextureOffset;
				}
				else
				{
					_originalTexture = _material.GetTexture(_texturePropertyName);
					_originalScale = _material.GetTextureScale(_texturePropertyName);
					_originalOffset = _material.GetTextureOffset(_texturePropertyName);
				}
			}
		}

		private void bac()
		{
			if (_material != null)
			{
				if (string.IsNullOrEmpty(_texturePropertyName))
				{
					_material.mainTexture = _originalTexture;
					_material.mainTextureScale = _originalScale;
					_material.mainTextureOffset = _originalOffset;
				}
				else
				{
					_material.SetTexture(_texturePropertyName, _originalTexture);
					_material.SetTextureScale(_texturePropertyName, _originalScale);
					_material.SetTextureOffset(_texturePropertyName, _originalOffset);
				}
			}
		}
	}
}
