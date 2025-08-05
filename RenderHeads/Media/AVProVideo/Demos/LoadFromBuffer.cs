using System;
using System.IO;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class LoadFromBuffer : MonoBehaviour
	{
		[SerializeField]
		private MediaPlayer _mp;

		[SerializeField]
		private string _filename = string.Empty;

		private void Start()
		{
			if (_mp != null)
			{
				byte[] array = File.ReadAllBytes(_filename);
				if (array != null)
				{
					_mp.bdv(array);
				}
			}
			GC.Collect();
		}
	}
}
