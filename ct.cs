using UnityEngine;

public interface ct
{
	bool OpenVideoFromFile(string a, long b, string c, uint d = 0u, uint e = 0u, int f = 0);

	bool OpenVideoFromBuffer(byte[] a);

	bool StartOpenVideoFromBuffer(ulong a);

	bool AddChunkToVideoBuffer(byte[] a, ulong b, ulong c);

	bool EndOpenVideoFromBuffer();

	void CloseVideo();

	void SetLooping(bool a);

	bool IsLooping();

	bool HasMetaData();

	bool CanPlay();

	bool IsPlaying();

	bool IsSeeking();

	bool IsPaused();

	bool IsFinished();

	bool IsBuffering();

	bool IsSeekingStarted();

	void ResetEventState();

	void Play();

	void Pause();

	void Stop();

	void Rewind();

	void Seek(float a);

	void SeekFast(float a);

	void SeekWithTolerance(float a, float b, float c);

	float GetCurrentTimeMs();

	double GetCurrentDateTimeSecondsSince1970();

	dj[] GetSeekableTimeRanges();

	float GetPlaybackRate();

	void SetPlaybackRate(float a);

	void MuteAudio(bool a);

	bool IsMuted();

	void SetVolume(float a);

	void SetBalance(float a);

	float GetVolume();

	float GetBalance();

	int GetCurrentAudioTrack();

	void SetAudioTrack(int a);

	int GetCurrentVideoTrack();

	void SetVideoTrack(int a);

	float GetBufferingProgress();

	int GetBufferedTimeRangeCount();

	bool GetBufferedTimeRange(int a, ref float b, ref float c);

	da GetLastError();

	long GetLastExtendedErrorCode();

	void SetTextureProperties(FilterMode a = FilterMode.Bilinear, TextureWrapMode b = TextureWrapMode.Clamp, int c = 1);

	void GrabAudio(float[] a, int b, int c);

	int GetNumAudioChannels();

	void SetAudioHeadRotation(Quaternion a);

	void ResetAudioHeadRotation();

	void SetAudioChannelMode(di a);

	void SetAudioFocusEnabled(bool a);

	void SetAudioFocusProperties(float a, float b);

	void SetAudioFocusRotation(Quaternion a);

	void ResetAudioFocus();

	bool WaitForNextFrame(Camera a, int b);

	void SetPlayWithoutBuffering(bool a);

	void SetKeyServerURL(string a);

	void SetKeyServerAuthToken(string a);

	void SetDecryptionKeyBase64(string a);

	void SetDecryptionKey(byte[] a);

	bool IsExternalPlaybackSupported();

	bool IsExternalPlaybackActive();

	void SetAllowsExternalPlayback(bool a);

	void SetExternalPlaybackFillMode(dd a);
}
