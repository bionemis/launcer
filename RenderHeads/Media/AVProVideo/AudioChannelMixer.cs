using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	public class AudioChannelMixer : MonoBehaviour
	{
		private const int MaxChannels = 8;

		[Range(0f, 1f)]
		[SerializeField]
		private float[] _channels;

		public float[] ea
		{
			get
			{
				return _channels;
			}
			set
			{
				_channels = value;
			}
		}

		private void Reset()
		{
			_channels = new float[8];
			for (int i = 0; i < 8; i++)
			{
				_channels[i] = 1f;
			}
		}

		private void bav(int a)
		{
			float[] array = new float[a];
			if (_channels != null && _channels.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (i < _channels.Length)
					{
						array[i] = _channels[i];
					}
					else
					{
						array[i] = 1f;
					}
				}
			}
			else
			{
				for (int j = 0; j < a; j++)
				{
					array[j] = 1f;
				}
			}
			_channels = array;
		}

		private void OnAudioFilterRead(float[] a, int b)
		{
			if (b != _channels.Length)
			{
				bav(b);
			}
			int num = 0;
			int num2 = a.Length / b;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < b; j++)
				{
					a[num] *= _channels[j];
					num++;
				}
			}
		}
	}
}
