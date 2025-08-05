using UnityEngine;

namespace RenderHeads.Media.AVProVideo.Demos
{
	public class ChangeStereoMode : MonoBehaviour
	{
		public MediaPlayer _mediaPlayer;

		public ApplyToMesh _applyToMesh;

		private void Update()
		{
			if (_mediaPlayer == null || _applyToMesh == null)
			{
				Debug.LogError("Fields cannot be null");
			}
			else if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_mediaPlayer.m_StereoPacking = cx.None;
				_applyToMesh.bap();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_mediaPlayer.m_StereoPacking = cx.LeftRight;
				_applyToMesh.bap();
			}
		}
	}
}
