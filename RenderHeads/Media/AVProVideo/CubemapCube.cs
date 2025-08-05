using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	[AddComponentMenu("AVPro Video/Cubemap Cube (VR)", 400)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class CubemapCube : MonoBehaviour
	{
		public enum Layout
		{
			FacebookTransform32 = 0,
			Facebook360Capture = 1
		}

		private Mesh _mesh;

		protected MeshRenderer _renderer;

		[SerializeField]
		protected Material _material;

		[SerializeField]
		private MediaPlayer _mediaPlayer;

		[SerializeField]
		private float expansion_coeff = 1.01f;

		[SerializeField]
		private Layout _layout;

		private Texture _texture;

		private bool _verticalFlip;

		private int _textureWidth;

		private int _textureHeight;

		private static int _propApplyGamma;

		private static int _propStereo;

		private static int _propUseYpCbCr;

		private const string PropChromaTexName = "_ChromaTex";

		private static int _propChromaTex;

		private const string PropYpCbCrTransformName = "_YpCbCrTransform";

		private static int _propYpCbCrTransform;

		public MediaPlayer dt
		{
			get
			{
				return _mediaPlayer;
			}
			set
			{
				_mediaPlayer = value;
			}
		}

		private void Awake()
		{
			if (_propStereo == 0)
			{
				_propStereo = Shader.PropertyToID("Stereo");
			}
			if (_propApplyGamma == 0)
			{
				_propApplyGamma = Shader.PropertyToID("_ApplyGamma");
			}
			if (_propUseYpCbCr == 0)
			{
				_propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
			}
			if (_propChromaTex == 0)
			{
				_propChromaTex = Shader.PropertyToID("_ChromaTex");
			}
			if (_propYpCbCrTransform == 0)
			{
				_propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
			}
		}

		private void Start()
		{
			if (_mesh == null)
			{
				_mesh = new Mesh();
				_mesh.MarkDynamic();
				MeshFilter component = GetComponent<MeshFilter>();
				if (component != null)
				{
					component.mesh = _mesh;
				}
				_renderer = GetComponent<MeshRenderer>();
				if (_renderer != null)
				{
					_renderer.material = _material;
				}
				bbb();
			}
		}

		private void OnDestroy()
		{
			if (_mesh != null)
			{
				MeshFilter component = GetComponent<MeshFilter>();
				if (component != null)
				{
					component.mesh = null;
				}
				Object.Destroy(_mesh);
				_mesh = null;
			}
			if (_renderer != null)
			{
				_renderer.material = null;
				_renderer = null;
			}
		}

		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			Texture texture = null;
			bool flag = false;
			if (_mediaPlayer != null && _mediaPlayer.ej != null)
			{
				if (_mediaPlayer.ek != null)
				{
					Texture texture2 = ((_mediaPlayer.ef == null || _mediaPlayer.ef.gf == null) ? null : _mediaPlayer.ef.gf[0]);
					texture = (_mediaPlayer.m_Resample ? texture2 : _mediaPlayer.ek.GetTexture());
					flag = _mediaPlayer.ek.RequiresVerticalFlip();
					if (_texture != texture || _verticalFlip != flag || (texture != null && (_textureWidth != texture.width || _textureHeight != texture.height)))
					{
						_texture = texture;
						if (texture != null)
						{
							bbc(texture.width, texture.height, flag);
						}
					}
					if (_renderer.material.HasProperty(_propApplyGamma) && _mediaPlayer.ei != null)
					{
						dl.bir(_renderer.material, _mediaPlayer.ei.PlayerSupportsLinearColorSpace());
					}
					if (_renderer.material.HasProperty(_propStereo))
					{
						dl.bip(_renderer.material, _mediaPlayer.m_StereoPacking, _mediaPlayer.m_DisplayDebugStereoColorTint);
					}
					if (_renderer.material.HasProperty(_propUseYpCbCr) && _mediaPlayer.ek.GetTextureCount() == 2)
					{
						_renderer.material.EnableKeyword("USE_YPCBCR");
						Texture texture3 = ((_mediaPlayer.ef == null || _mediaPlayer.ef.gf == null) ? null : _mediaPlayer.ef.gf[1]);
						_renderer.material.SetTexture(_propChromaTex, _mediaPlayer.m_Resample ? texture3 : _mediaPlayer.ek.GetTexture(1));
						_renderer.material.SetMatrix(_propYpCbCrTransform, _mediaPlayer.ek.GetYpCbCrTransform());
					}
				}
				_renderer.material.mainTexture = _texture;
			}
			else
			{
				_renderer.material.mainTexture = null;
			}
		}

		private void bbb()
		{
			Vector3 vector = new Vector3(-0.5f, -0.5f, -0.5f);
			Vector3[] array = new Vector3[24]
			{
				new Vector3(0f, -1f, 0f) - vector,
				new Vector3(0f, 0f, 0f) - vector,
				new Vector3(0f, 0f, -1f) - vector,
				new Vector3(0f, -1f, -1f) - vector,
				new Vector3(0f, 0f, 0f) - vector,
				new Vector3(-1f, 0f, 0f) - vector,
				new Vector3(-1f, 0f, -1f) - vector,
				new Vector3(0f, 0f, -1f) - vector,
				new Vector3(-1f, 0f, 0f) - vector,
				new Vector3(-1f, -1f, 0f) - vector,
				new Vector3(-1f, -1f, -1f) - vector,
				new Vector3(-1f, 0f, -1f) - vector,
				new Vector3(-1f, -1f, 0f) - vector,
				new Vector3(0f, -1f, 0f) - vector,
				new Vector3(0f, -1f, -1f) - vector,
				new Vector3(-1f, -1f, -1f) - vector,
				new Vector3(0f, -1f, -1f) - vector,
				new Vector3(0f, 0f, -1f) - vector,
				new Vector3(-1f, 0f, -1f) - vector,
				new Vector3(-1f, -1f, -1f) - vector,
				new Vector3(-1f, -1f, 0f) - vector,
				new Vector3(-1f, 0f, 0f) - vector,
				new Vector3(0f, 0f, 0f) - vector,
				new Vector3(0f, -1f, 0f) - vector
			};
			Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90f, Vector3.right), Vector3.one);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = matrix4x.MultiplyPoint(array[i]);
			}
			_mesh.vertices = array;
			_mesh.triangles = new int[36]
			{
				0, 1, 2, 0, 2, 3, 4, 5, 6, 4,
				6, 7, 8, 9, 10, 8, 10, 11, 12, 13,
				14, 12, 14, 15, 16, 17, 18, 16, 18, 19,
				20, 21, 22, 20, 22, 23
			};
			_mesh.normals = new Vector3[24]
			{
				new Vector3(-1f, 0f, 0f),
				new Vector3(-1f, 0f, 0f),
				new Vector3(-1f, 0f, 0f),
				new Vector3(-1f, 0f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0f, 0f, -1f),
				new Vector3(0f, 0f, -1f),
				new Vector3(0f, 0f, -1f),
				new Vector3(0f, 0f, -1f)
			};
			bbc(512, 512, c: false);
		}

		private void bbc(int a, int b, bool c)
		{
			_textureWidth = a;
			_textureHeight = b;
			_verticalFlip = c;
			float num = a;
			float num2 = b;
			float num3 = num / 3f;
			float num4 = Mathf.Floor((expansion_coeff * num3 - num3) / 2f);
			float num5 = num4 / num;
			float num6 = num4 / num2;
			Vector2[] array = null;
			if (_layout == Layout.Facebook360Capture)
			{
				array = new Vector2[24]
				{
					new Vector2(1f / 3f + num5, 0.5f - num6),
					new Vector2(2f / 3f - num5, 0.5f - num6),
					new Vector2(2f / 3f - num5, 0f + num6),
					new Vector2(1f / 3f + num5, 0f + num6),
					new Vector2(1f / 3f + num5, 1f - num6),
					new Vector2(2f / 3f - num5, 1f - num6),
					new Vector2(2f / 3f - num5, 0.5f + num6),
					new Vector2(1f / 3f + num5, 0.5f + num6),
					new Vector2(0f + num5, 0.5f - num6),
					new Vector2(1f / 3f - num5, 0.5f - num6),
					new Vector2(1f / 3f - num5, 0f + num6),
					new Vector2(0f + num5, 0f + num6),
					new Vector2(2f / 3f + num5, 1f - num6),
					new Vector2(1f - num5, 1f - num6),
					new Vector2(1f - num5, 0.5f + num6),
					new Vector2(2f / 3f + num5, 0.5f + num6),
					new Vector2(2f / 3f + num5, 0f + num6),
					new Vector2(2f / 3f + num5, 0.5f - num6),
					new Vector2(1f - num5, 0.5f - num6),
					new Vector2(1f - num5, 0f + num6),
					new Vector2(1f / 3f - num5, 1f - num6),
					new Vector2(1f / 3f - num5, 0.5f + num6),
					new Vector2(0f + num5, 0.5f + num6),
					new Vector2(0f + num5, 1f - num6)
				};
			}
			else if (_layout == Layout.FacebookTransform32)
			{
				array = new Vector2[24]
				{
					new Vector2(1f / 3f + num5, 1f - num6),
					new Vector2(2f / 3f - num5, 1f - num6),
					new Vector2(2f / 3f - num5, 0.5f + num6),
					new Vector2(1f / 3f + num5, 0.5f + num6),
					new Vector2(1f / 3f + num5, 0.5f - num6),
					new Vector2(2f / 3f - num5, 0.5f - num6),
					new Vector2(2f / 3f - num5, 0f + num6),
					new Vector2(1f / 3f + num5, 0f + num6),
					new Vector2(0f + num5, 1f - num6),
					new Vector2(1f / 3f - num5, 1f - num6),
					new Vector2(1f / 3f - num5, 0.5f + num6),
					new Vector2(0f + num5, 0.5f + num6),
					new Vector2(2f / 3f + num5, 0.5f - num6),
					new Vector2(1f - num5, 0.5f - num6),
					new Vector2(1f - num5, 0f + num6),
					new Vector2(2f / 3f + num5, 0f + num6),
					new Vector2(0f + num5, 0f + num6),
					new Vector2(0f + num5, 0.5f - num6),
					new Vector2(1f / 3f - num5, 0.5f - num6),
					new Vector2(1f / 3f - num5, 0f + num6),
					new Vector2(1f - num5, 1f - num6),
					new Vector2(1f - num5, 0.5f + num6),
					new Vector2(2f / 3f + num5, 0.5f + num6),
					new Vector2(2f / 3f + num5, 1f - num6)
				};
			}
			if (c)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].y = 1f - array[i].y;
				}
			}
			_mesh.uv = array;
			_mesh.UploadMeshData(markNoLongerReadable: false);
		}
	}
}
