using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class SphereDemo : MonoBehaviour
	{
		[SerializeField]
		private bool _zeroCameraPosition = true;

		[SerializeField]
		private bool _allowRecenter;

		[SerializeField]
		private bool _allowVrToggle;

		[SerializeField]
		private bool _lockPitch;

		private float _spinX;

		private float _spinY;

		private static bool blh()
		{
			bool result = false;
			List<XRDisplaySubsystem> list = new List<XRDisplaySubsystem>();
			SubsystemManager.GetInstances(list);
			foreach (XRDisplaySubsystem item in list)
			{
				if (item.running)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void Start()
		{
			if (!blh() && SystemInfo.supportsGyroscope)
			{
				Input.gyro.enabled = true;
				if (base.transform.parent != null)
				{
					base.transform.parent.Rotate(new Vector3(90f, 0f, 0f));
				}
			}
		}

		private void OnDestroy()
		{
			if (SystemInfo.supportsGyroscope)
			{
				Input.gyro.enabled = false;
			}
		}

		private void Update()
		{
			if (blh())
			{
				if (_allowRecenter && !Input.GetMouseButtonDown(0))
				{
					Input.GetKeyDown(KeyCode.Space);
				}
				if (_allowVrToggle && Input.GetKeyDown(KeyCode.V))
				{
					XRSettings.enabled = !XRSettings.enabled;
				}
				return;
			}
			if (SystemInfo.supportsGyroscope)
			{
				base.transform.localRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, 0f - Input.gyro.attitude.z, 0f - Input.gyro.attitude.w);
				return;
			}
			if (Input.GetMouseButton(0))
			{
				float value = 40f * (0f - Input.GetAxis("Mouse X")) * Time.deltaTime;
				float value2 = 0f;
				if (!_lockPitch)
				{
					value2 = 40f * Input.GetAxis("Mouse Y") * Time.deltaTime;
				}
				value = Mathf.Clamp(value, -0.5f, 0.5f);
				value2 = Mathf.Clamp(value2, -0.5f, 0.5f);
				_spinX += value;
				_spinY += value2;
			}
			if (!Mathf.Approximately(_spinX, 0f) || !Mathf.Approximately(_spinY, 0f))
			{
				base.transform.Rotate(Vector3.up, _spinX);
				base.transform.Rotate(Vector3.right, _spinY);
				_spinX = Mathf.MoveTowards(_spinX, 0f, 5f * Time.deltaTime);
				_spinY = Mathf.MoveTowards(_spinY, 0f, 5f * Time.deltaTime);
			}
		}

		private void LateUpdate()
		{
			if (_zeroCameraPosition)
			{
				Camera.main.transform.position = Vector3.zero;
			}
		}
	}
}
