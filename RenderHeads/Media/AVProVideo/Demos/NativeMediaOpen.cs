using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class NativeMediaOpen : MonoBehaviour
	{
		[SerializeField]
		private MediaPlayer _player;

		private void Start()
		{
		}

		private void Update()
		{
		}

		private void OnGUI()
		{
			GUILayout.Button("Pick Video File");
			GUILayout.Button("Load 'myvideo.mp4' From Camera Roll");
			if (_player != null && !string.IsNullOrEmpty(_player.m_VideoPath))
			{
				GUILayout.Label("Currently Playing: " + _player.m_VideoPath);
			}
		}
	}
}
