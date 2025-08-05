using UnityEngine;

public interface cv
{
	int GetTextureCount();

	Texture GetTexture(int a = 0);

	int GetTextureFrameCount();

	bool SupportsTextureFrameCount();

	long GetTextureTimeStamp();

	bool RequiresVerticalFlip();

	Matrix4x4 GetYpCbCrTransform();
}
