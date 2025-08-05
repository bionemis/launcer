using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
	[ExecuteInEditMode]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	[AddComponentMenu("AVPro Video/Display uGUI", 200)]
	public class DisplayUGUI : MaskableGraphic
	{
		[SerializeField]
		public MediaPlayer _mediaPlayer;

		[SerializeField]
		public Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);

		[SerializeField]
		public bool _setNativeSize;

		[SerializeField]
		public ScaleMode _scaleMode = ScaleMode.ScaleToFit;

		[SerializeField]
		public bool _noDefaultDisplay = true;

		[SerializeField]
		public bool _displayInEditor = true;

		[SerializeField]
		public Texture _defaultTexture;

		private int _lastWidth;

		private int _lastHeight;

		private bool _flipY;

		private Texture _lastTexture;

		private static Shader _shaderStereoPacking;

		private static Shader _shaderAlphaPacking;

		private static int _propAlphaPack;

		private static int _propVertScale;

		private static int _propStereo;

		private static int _propApplyGamma;

		private static int _propUseYpCbCr;

		private const string PropChromaTexName = "_ChromaTex";

		private static int _propChromaTex;

		private const string PropYpCbCrTransformName = "_YpCbCrTransform";

		private static int _propYpCbCrTransform;

		private static int _propCroppingScalars;

		private bool _userMaterial = true;

		private Material _material;

		private List<UIVertex> _vertices = new List<UIVertex>(4);

		private static List<int> QuadIndices = new List<int>(new int[6] { 0, 1, 2, 2, 3, 0 });

		public override Texture mainTexture
		{
			get
			{
				Texture result = Texture2D.whiteTexture;
				if (bbp())
				{
					Texture texture = ((_mediaPlayer.ef == null || _mediaPlayer.ef.gf == null) ? null : _mediaPlayer.ef.gf[0]);
					result = (_mediaPlayer.m_Resample ? texture : _mediaPlayer.ek.GetTexture());
				}
				else if (_noDefaultDisplay)
				{
					result = null;
				}
				else if (_defaultTexture != null)
				{
					result = _defaultTexture;
				}
				return result;
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
					SetMaterialDirty();
				}
			}
		}

		public Rect ee
		{
			get
			{
				return m_UVRect;
			}
			set
			{
				if (!(m_UVRect == value))
				{
					m_UVRect = value;
					SetVerticesDirty();
				}
			}
		}

		protected override void Awake()
		{
			if (_propAlphaPack == 0)
			{
				_propStereo = Shader.PropertyToID("Stereo");
				_propAlphaPack = Shader.PropertyToID("AlphaPack");
				_propVertScale = Shader.PropertyToID("_VertScale");
				_propApplyGamma = Shader.PropertyToID("_ApplyGamma");
				_propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
				_propChromaTex = Shader.PropertyToID("_ChromaTex");
				_propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
				_propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
				_propCroppingScalars = Shader.PropertyToID("_CroppingScalars");
			}
			base.Awake();
		}

		private static bool bbk(GameObject a)
		{
			if (a.GetComponent<Mask>() != null)
			{
				return true;
			}
			if (a.transform.parent != null)
			{
				return bbk(a.transform.parent.gameObject);
			}
			return false;
		}

		private static Shader bbl(Shader a, string b)
		{
			if (a == null)
			{
				a = Shader.Find(b);
				if (a == null)
				{
					Debug.LogWarning("[AVProVideo] Missing shader " + b);
				}
			}
			return a;
		}

		private static Shader bbm()
		{
			_shaderAlphaPacking = bbl(_shaderAlphaPacking, "AVProVideo/UI/Transparent Packed");
			return _shaderAlphaPacking;
		}

		private static Shader bbn()
		{
			_shaderStereoPacking = bbl(_shaderStereoPacking, "AVProVideo/UI/Stereo");
			return _shaderStereoPacking;
		}

		protected override void Start()
		{
			_userMaterial = m_Material != null;
			if (_userMaterial)
			{
				_material = new Material(material);
				material = _material;
			}
			base.Start();
		}

		protected override void OnDestroy()
		{
			if (_material != null)
			{
				material = null;
				UnityEngine.Object.Destroy(_material);
				_material = null;
			}
			base.OnDestroy();
		}

		private Shader bbo()
		{
			Shader shader = null;
			cx stereoPacking = _mediaPlayer.m_StereoPacking;
			if (stereoPacking != cx.None && (uint)(stereoPacking - 1) <= 1u)
			{
				shader = bbn();
			}
			cz alphaPacking = _mediaPlayer.m_AlphaPacking;
			if (alphaPacking != cz.None && (uint)(alphaPacking - 1) <= 1u)
			{
				shader = bbm();
			}
			if (shader == null && _mediaPlayer.ei != null && QualitySettings.activeColorSpace == ColorSpace.Linear && !_mediaPlayer.ei.PlayerSupportsLinearColorSpace())
			{
				shader = bbm();
			}
			if (shader == null && _mediaPlayer.ek != null && _mediaPlayer.ek.GetTextureCount() == 2)
			{
				shader = bbm();
			}
			return shader;
		}

		public bool bbp()
		{
			if (_mediaPlayer != null && _mediaPlayer.ek != null)
			{
				return _mediaPlayer.ek.GetTexture() != null;
			}
			return false;
		}

		private void bbq()
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
			Shader shader2 = bbo();
			if (shader != shader2)
			{
				if (_material != null)
				{
					material = null;
					UnityEngine.Object.Destroy(_material);
					_material = null;
				}
				if (shader2 != null)
				{
					_material = new Material(shader2);
				}
			}
			material = _material;
		}

		private void LateUpdate()
		{
			if (_setNativeSize)
			{
				SetNativeSize();
			}
			if (_lastTexture != mainTexture)
			{
				_lastTexture = mainTexture;
				SetVerticesDirty();
				SetMaterialDirty();
			}
			if (bbp() && mainTexture != null && (mainTexture.width != _lastWidth || mainTexture.height != _lastHeight))
			{
				_lastWidth = mainTexture.width;
				_lastHeight = mainTexture.height;
				SetVerticesDirty();
				SetMaterialDirty();
			}
			if (!_userMaterial && Application.isPlaying)
			{
				bbq();
			}
			if (!(material != null) || !(_mediaPlayer != null))
			{
				return;
			}
			if (material.HasProperty(_propUseYpCbCr) && _mediaPlayer.ek != null && _mediaPlayer.ek.GetTextureCount() == 2)
			{
				material.EnableKeyword("USE_YPCBCR");
				material.SetMatrix(_propYpCbCrTransform, _mediaPlayer.ek.GetYpCbCrTransform());
				Texture texture = ((_mediaPlayer.ef == null || _mediaPlayer.ef.gf == null) ? null : _mediaPlayer.ef.gf[1]);
				material.SetTexture(_propChromaTex, _mediaPlayer.m_Resample ? texture : _mediaPlayer.ek.GetTexture(1));
			}
			if (material.HasProperty(_propAlphaPack))
			{
				dl.biq(material, _mediaPlayer.m_AlphaPacking);
				if (_flipY && _mediaPlayer.m_AlphaPacking != cz.None)
				{
					material.SetFloat(_propVertScale, -1f);
				}
				else
				{
					material.SetFloat(_propVertScale, 1f);
				}
			}
			if (material.HasProperty(_propStereo))
			{
				dl.bip(material, _mediaPlayer.m_StereoPacking, _mediaPlayer.m_DisplayDebugStereoColorTint);
			}
			if (material.HasProperty(_propApplyGamma) && _mediaPlayer.ei != null)
			{
				dl.bir(material, _mediaPlayer.ei.PlayerSupportsLinearColorSpace());
			}
			_propCroppingScalars |= 0;
		}

		[ContextMenu("Set Native Size")]
		public override void SetNativeSize()
		{
			Texture texture = mainTexture;
			if (!(texture != null))
			{
				return;
			}
			int num = Mathf.RoundToInt((float)texture.width * ee.width);
			int num2 = Mathf.RoundToInt((float)texture.height * ee.height);
			if (_mediaPlayer != null)
			{
				if (_mediaPlayer.m_AlphaPacking == cz.LeftRight || _mediaPlayer.m_StereoPacking == cx.LeftRight)
				{
					num /= 2;
				}
				else if (_mediaPlayer.m_AlphaPacking == cz.TopBottom || _mediaPlayer.m_StereoPacking == cx.TopBottom)
				{
					num2 /= 2;
				}
			}
			base.rectTransform.anchorMax = base.rectTransform.anchorMin;
			base.rectTransform.sizeDelta = new Vector2(num, num2);
		}

		protected override void OnPopulateMesh(VertexHelper a)
		{
			a.Clear();
			bbv(_vertices);
			a.AddUIVertexStream(_vertices, QuadIndices);
		}

		[Obsolete("This method is not called from Unity 5.2 and above")]
		protected override void OnFillVBO(List<UIVertex> a)
		{
			bbv(a);
		}

		private void bbv(List<UIVertex> a)
		{
			_flipY = false;
			if (bbp())
			{
				_flipY = _mediaPlayer.ek.RequiresVerticalFlip();
			}
			Rect b = m_UVRect;
			Vector4 vector = bbw(_scaleMode, ref b);
			a.Clear();
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = color;
			simpleVert.position = new Vector2(vector.x, vector.y);
			simpleVert.uv0 = new Vector2(b.xMin, b.yMin);
			if (_flipY)
			{
				simpleVert.uv0 = new Vector2(b.xMin, 1f - b.yMin);
			}
			a.Add(simpleVert);
			simpleVert.position = new Vector2(vector.x, vector.w);
			simpleVert.uv0 = new Vector2(b.xMin, b.yMax);
			if (_flipY)
			{
				simpleVert.uv0 = new Vector2(b.xMin, 1f - b.yMax);
			}
			a.Add(simpleVert);
			simpleVert.position = new Vector2(vector.z, vector.w);
			simpleVert.uv0 = new Vector2(b.xMax, b.yMax);
			if (_flipY)
			{
				simpleVert.uv0 = new Vector2(b.xMax, 1f - b.yMax);
			}
			a.Add(simpleVert);
			simpleVert.position = new Vector2(vector.z, vector.y);
			simpleVert.uv0 = new Vector2(b.xMax, b.yMin);
			if (_flipY)
			{
				simpleVert.uv0 = new Vector2(b.xMax, 1f - b.yMin);
			}
			a.Add(simpleVert);
		}

		private Vector4 bbw(ScaleMode a, ref Rect b)
		{
			Vector4 result = Vector4.zero;
			if (mainTexture != null)
			{
				Vector4 zero = Vector4.zero;
				Vector2 vector = new Vector2(mainTexture.width, mainTexture.height);
				if (_mediaPlayer != null)
				{
					if (_mediaPlayer.m_AlphaPacking == cz.LeftRight || _mediaPlayer.m_StereoPacking == cx.LeftRight)
					{
						vector.x /= 2f;
					}
					else if (_mediaPlayer.m_AlphaPacking == cz.TopBottom || _mediaPlayer.m_StereoPacking == cx.TopBottom)
					{
						vector.y /= 2f;
					}
				}
				Rect pixelAdjustedRect = GetPixelAdjustedRect();
				int num = Mathf.RoundToInt(vector.x);
				int num2 = Mathf.RoundToInt(vector.y);
				Vector4 vector2 = new Vector4(zero.x / (float)num, zero.y / (float)num2, ((float)num - zero.z) / (float)num, ((float)num2 - zero.w) / (float)num2);
				if (vector.sqrMagnitude > 0f)
				{
					switch (a)
					{
					case ScaleMode.ScaleToFit:
					{
						float num7 = vector.x / vector.y;
						float num8 = pixelAdjustedRect.width / pixelAdjustedRect.height;
						if (num7 > num8)
						{
							float height = pixelAdjustedRect.height;
							pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num7);
							pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * base.rectTransform.pivot.y;
						}
						else
						{
							float width = pixelAdjustedRect.width;
							pixelAdjustedRect.width = pixelAdjustedRect.height * num7;
							pixelAdjustedRect.x += (width - pixelAdjustedRect.width) * base.rectTransform.pivot.x;
						}
						break;
					}
					case ScaleMode.ScaleAndCrop:
					{
						float num3 = vector.x / vector.y;
						float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
						if (num4 > num3)
						{
							float num5 = num3 / num4;
							b = new Rect(b.xMin, b.yMin * num5 + (1f - num5) * 0.5f, b.width, num5 * b.height);
						}
						else
						{
							float num6 = num4 / num3;
							b = new Rect(b.xMin * num6 + (0.5f - num6 * 0.5f), b.yMin, num6 * b.width, b.height);
						}
						break;
					}
					}
				}
				result = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * vector2.x, pixelAdjustedRect.y + pixelAdjustedRect.height * vector2.y, pixelAdjustedRect.x + pixelAdjustedRect.width * vector2.z, pixelAdjustedRect.y + pixelAdjustedRect.height * vector2.w);
			}
			return result;
		}
	}
}
