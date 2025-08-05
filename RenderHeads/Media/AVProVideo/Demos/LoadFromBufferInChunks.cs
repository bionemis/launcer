using System;
using System.IO;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class LoadFromBufferInChunks : MonoBehaviour
	{
		[SerializeField]
		private MediaPlayer _mp;

		[SerializeField]
		private string _filename = string.Empty;

		private void Start()
		{
			if (_mp != null)
			{
				int num = 512;
				byte[] array = new byte[num];
				FileStream fileStream = File.OpenRead(_filename);
				long length = fileStream.Length;
				if (!_mp.bdw((ulong)length))
				{
					_mp.bel();
				}
				ulong num2 = 0uL;
				while (true)
				{
					int num3 = fileStream.Read(array, 0, num);
					if (num3 <= 0)
					{
						break;
					}
					if (!_mp.bdx(array, num2, (ulong)num3))
					{
						_mp.bel();
					}
					num2 += (ulong)num3;
				}
				if (!_mp.bdy())
				{
					_mp.bel();
				}
			}
			GC.Collect();
		}
	}
}
