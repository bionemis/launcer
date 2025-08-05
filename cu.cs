using UnityEngine;

public interface cu
{
	float GetDurationMs();

	int GetVideoWidth();

	int GetVideoHeight();

	Rect GetCropRect();

	float GetVideoFrameRate();

	float GetVideoDisplayRate();

	bool HasVideo();

	bool HasAudio();

	int GetAudioTrackCount();

	string GetAudioTrackId(int a);

	string GetCurrentAudioTrackId();

	int GetCurrentAudioTrackBitrate();

	int GetVideoTrackCount();

	string GetVideoTrackId(int a);

	string GetCurrentVideoTrackId();

	int GetCurrentVideoTrackBitrate();

	string GetPlayerDescription();

	bool PlayerSupportsLinearColorSpace();

	bool IsPlaybackStalled();

	float[] GetTextureTransform();

	long GetEstimatedTotalBandwidthUsed();
}
