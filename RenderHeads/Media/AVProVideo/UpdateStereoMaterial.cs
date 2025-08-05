using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Update Stereo Material", 400)]
	[HelpURL("http://renderheads.com/products/avpro-video/")]
	public class UpdateStereoMaterial : MonoBehaviour
	{
		[Header("Stereo camera")]
		public Camera _camera;

		[Header("Rendering elements")]
		public MeshRenderer _renderer;

		public Graphic _uGuiComponent;

		public Material _material;

		[SerializeField]
		private cy _forceEyeMode;

		private static int _cameraPositionId;

		private static int _viewMatrixId;

		private cy _setForceEyeMode;

		private Camera _foundCamera;

		public cy fu
		{
			get
			{
				return _forceEyeMode;
			}
			set
			{
				_forceEyeMode = value;
			}
		}

		private void Awake()
		{
			if (_cameraPositionId == 0)
			{
				_cameraPositionId = Shader.PropertyToID("_cameraPosition");
			}
			if (_viewMatrixId == 0)
			{
				_viewMatrixId = Shader.PropertyToID("_ViewMatrix");
			}
			if (_camera == null)
			{
				Debug.LogWarning("[AVProVideo] No camera set for UpdateStereoMaterial component. If you are rendering in stereo then it is recommended to set this.");
			}
		}

		private void bhs(Material a, Camera b)
		{
			a.SetVector(_cameraPositionId, b.transform.position);
			a.SetMatrix(_viewMatrixId, b.worldToCameraMatrix.transpose);
			if (_forceEyeMode != _setForceEyeMode)
			{
				dl.bin(a, _forceEyeMode);
				_setForceEyeMode = _forceEyeMode;
			}
		}

		private void LateUpdate()
		{
			if (_camera != null && _foundCamera != _camera)
			{
				_foundCamera = _camera;
			}
			if (_foundCamera == null)
			{
				_foundCamera = Camera.main;
				if (_foundCamera == null)
				{
					Debug.LogWarning("[AVPro Video] Cannot find main camera for UpdateStereoMaterial, this can lead to eyes flickering");
					if (Camera.allCameras.Length != 0)
					{
						_foundCamera = Camera.allCameras[0];
						Debug.LogWarning("[AVPro Video] UpdateStereoMaterial using camera " + _foundCamera.name);
					}
				}
			}
			if (_renderer == null && _material == null)
			{
				_renderer = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (_foundCamera != null)
			{
				if (_renderer != null)
				{
					bhs(_renderer.material, _foundCamera);
				}
				if (_material != null)
				{
					bhs(_material, _foundCamera);
				}
				if (_uGuiComponent != null)
				{
					bhs(_uGuiComponent.material, _foundCamera);
				}
			}
		}
	}
}
