using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class ChangeVideoExample : MonoBehaviour
	{
		[SerializeField]
		private MediaPlayer _mediaPlayer;

		public void bks(string a)
		{
			_mediaPlayer.bdu(MediaPlayer.b.RelativeToStreamingAssetsFolder, a);
		}

		private void OnGUI()
		{
			if (GUILayout.Button("video1"))
			{
				bks("video1.mp4");
			}
			if (GUILayout.Button("video2"))
			{
				bks("video2.mp4");
			}
		}
	}
}
