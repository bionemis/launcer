using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Apply To Mesh", 300)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class ApplyToMesh : MonoBehaviour
	{
		[Header("Media Source")]
		[SerializeField]
		private MediaPlayer _media;

		[Tooltip("Default texture to display when the video texture is preparing")]
		[SerializeField]
		private Texture2D _defaultTexture;

		[Space(8f)]
		[Header("Renderer Target")]
		[SerializeField]
		private Renderer _mesh;

		[SerializeField]
		private string _texturePropertyName = "_MainTex";

		[SerializeField]
		private Vector2 _offset = Vector2.zero;

		[SerializeField]
		private Vector2 _scale = Vector2.one;

		private bool _isDirty;

		private Texture _lastTextureApplied;

		private int _propTexture;

		private static int _propStereo;

		private static int _propAlphaPack;

		private static int _propApplyGamma;

		private static int _propLayout;

		private const string PropChromaTexName = "_ChromaTex";

		private static int _propChromaTex;

		private const string PropYpCbCrTransformName = "_YpCbCrTransform";

		private static int _propYpCbCrTransform;

		private const string PropUseYpCbCrName = "_UseYpCbCr";

		private static int _propUseYpCbCr;

		private static int _propCroppingScalars;

		public MediaPlayer dt
		{
			get
			{
				return _media;
			}
			set
			{
				bar(value);
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

		public Renderer dz
		{
			get
			{
				return _mesh;
			}
			set
			{
				if (_mesh != value)
				{
					_mesh = value;
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
				_media.em.bib(baq);
			}
		}

		public void bap()
		{
			_isDirty = true;
			LateUpdate();
		}

		private void baq(MediaPlayer a, MediaPlayerEvent.a b, da c)
		{
			if (b == MediaPlayerEvent.a.FirstFrameReady || b == MediaPlayerEvent.a.PropertiesChanged)
			{
				bap();
			}
		}

		private void bar(MediaPlayer a)
		{
			if (_media != a)
			{
				if (_media != null)
				{
					_media.em.bic(baq);
				}
				_media = a;
				if (_media != null)
				{
					_media.em.bib(baq);
				}
				_isDirty = true;
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
								bas(texture2, _media.ek.RequiresVerticalFlip(), i);
							}
						}
					}
					flag = true;
				}
			}
			if (!flag)
			{
				if (_defaultTexture != _lastTextureApplied)
				{
					_isDirty = true;
				}
				if (_isDirty)
				{
					bas(_defaultTexture, b: false);
				}
			}
		}

		private void bas(Texture a, bool b, int c = 0)
		{
			if (!(_mesh != null))
			{
				return;
			}
			_isDirty = false;
			Material[] materials = _mesh.materials;
			if (materials == null)
			{
				return;
			}
			foreach (Material material in materials)
			{
				if (!(material != null))
				{
					continue;
				}
				switch (c)
				{
				case 0:
					material.SetTexture(_propTexture, a);
					_lastTextureApplied = a;
					if (a != null)
					{
						if (b)
						{
							material.SetTextureScale(_propTexture, new Vector2(_scale.x, 0f - _scale.y));
							material.SetTextureOffset(_propTexture, Vector2.up + _offset);
						}
						else
						{
							material.SetTextureScale(_propTexture, _scale);
							material.SetTextureOffset(_propTexture, _offset);
						}
					}
					break;
				case 1:
					if (material.HasProperty(_propUseYpCbCr) && material.HasProperty(_propChromaTex))
					{
						material.EnableKeyword("USE_YPCBCR");
						material.SetTexture(_propChromaTex, a);
						material.SetMatrix(_propYpCbCrTransform, _media.ek.GetYpCbCrTransform());
						if (b)
						{
							material.SetTextureScale(_propChromaTex, new Vector2(_scale.x, 0f - _scale.y));
							material.SetTextureOffset(_propChromaTex, Vector2.up + _offset);
						}
						else
						{
							material.SetTextureScale(_propChromaTex, _scale);
							material.SetTextureOffset(_propChromaTex, _offset);
						}
					}
					break;
				}
				if (!(_media != null))
				{
					continue;
				}
				if (material.HasProperty(_propLayout))
				{
					dl.bio(material, _media.eh);
				}
				if (material.HasProperty(_propStereo))
				{
					dl.bip(material, _media.m_StereoPacking, _media.m_DisplayDebugStereoColorTint);
				}
				if (material.HasProperty(_propAlphaPack))
				{
					dl.biq(material, _media.m_AlphaPacking);
				}
				if (material.HasProperty(_propApplyGamma))
				{
					if (a == _defaultTexture || _media.ei == null)
					{
						dl.bir(material, b: true);
					}
					else
					{
						dl.bir(material, _media.ei.PlayerSupportsLinearColorSpace());
					}
				}
				_propCroppingScalars |= 0;
			}
		}

		private void OnEnable()
		{
			if (_mesh == null)
			{
				_mesh = GetComponent<MeshRenderer>();
				if (_mesh == null)
				{
					Debug.LogWarning("[AVProVideo] No mesh renderer set or found in gameobject");
				}
			}
			_propTexture = Shader.PropertyToID(_texturePropertyName);
			_isDirty = true;
			if (_mesh != null)
			{
				LateUpdate();
			}
		}

		private void OnDisable()
		{
			bas(_defaultTexture, b: false);
		}

		private void OnDestroy()
		{
			bar(null);
		}
	}
}
