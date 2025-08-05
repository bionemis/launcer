using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	[RequireComponent(typeof(Transform))]
	public class AutoRotate : MonoBehaviour
	{
		private float x;

		private float xq;

		private float xr;

		private float xs;

		private void Awake()
		{
			bki();
		}

		private void Update()
		{
			base.transform.Rotate(x * Time.deltaTime, xq * Time.deltaTime, xr * Time.deltaTime);
			xs -= Time.deltaTime;
			if (xs <= 0f)
			{
				bki();
			}
		}

		private void bki()
		{
			float num = 32f;
			x = Random.Range(0f - num, num);
			xq = Random.Range(0f - num, num);
			xr = Random.Range(0f - num, num);
			xs = Random.Range(5f, 10f);
		}
	}
}
