using RenderHeads.Media.AVProVideo;
using UnityEngine;

public sealed class dm : BaseMediaPlayer
{
	private bool uw;

	private bool ux;

	private float uy;

	private float uz;

	private float va = 1f;

	private bool vb;

	private int vc = 256;

	private int ul = 256;

	private Texture2D vd;

	private Texture2D ve;

	private Texture2D vf;

	private float vg;

	private int vh;

	private const float vi = 10f;

	public override string GetVersion()
	{
		return "0.0.0";
	}

	public override bool OpenVideoFromFile(string a, long b, string c, uint d = 0u, uint e = 0u, int f = 0)
	{
		ve = (Texture2D)Resources.Load("AVPro");
		vf = (Texture2D)Resources.Load("AVPro1");
		if ((bool)ve)
		{
			vc = ve.width;
			ul = ve.height;
		}
		vd = ve;
		vg = 0f;
		vh = 0;
		return true;
	}

	public override void CloseVideo()
	{
		vh = 0;
		Resources.UnloadAsset(ve);
		Resources.UnloadAsset(vf);
		base.CloseVideo();
	}

	public override void SetLooping(bool a)
	{
		vb = a;
	}

	public override bool IsLooping()
	{
		return vb;
	}

	public override bool HasMetaData()
	{
		return true;
	}

	public override bool CanPlay()
	{
		return true;
	}

	public override bool HasAudio()
	{
		return false;
	}

	public override bool HasVideo()
	{
		return false;
	}

	public override void Play()
	{
		uw = true;
		ux = false;
		vg = 0f;
	}

	public override void Pause()
	{
		uw = false;
		ux = true;
	}

	public override void Stop()
	{
		uw = false;
		ux = false;
	}

	public override bool IsSeeking()
	{
		return false;
	}

	public override bool IsPlaying()
	{
		return uw;
	}

	public override bool IsPaused()
	{
		return ux;
	}

	public override bool IsFinished()
	{
		if (uw)
		{
			return uy >= GetDurationMs();
		}
		return false;
	}

	public override bool IsBuffering()
	{
		return false;
	}

	public override float GetDurationMs()
	{
		return 10000f;
	}

	public override int GetVideoWidth()
	{
		return vc;
	}

	public override int GetVideoHeight()
	{
		return ul;
	}

	public override float GetVideoDisplayRate()
	{
		return 10f;
	}

	public override Texture GetTexture(int a)
	{
		return vd;
	}

	public override int GetTextureFrameCount()
	{
		return vh;
	}

	public override bool RequiresVerticalFlip()
	{
		return false;
	}

	public override void Seek(float a)
	{
		_isSeekingStarted = true;
		uy = a;
	}

	public override void SeekFast(float a)
	{
		_isSeekingStarted = true;
		uy = a;
	}

	public override void SeekWithTolerance(float a, float b, float c)
	{
		_isSeekingStarted = true;
		uy = a;
	}

	public override float GetCurrentTimeMs()
	{
		return uy;
	}

	public override void SetPlaybackRate(float a)
	{
		va = a;
	}

	public override float GetPlaybackRate()
	{
		return va;
	}

	public override float GetBufferingProgress()
	{
		return 0f;
	}

	public override void MuteAudio(bool a)
	{
	}

	public override bool IsMuted()
	{
		return true;
	}

	public override void SetVolume(float a)
	{
		uz = a;
	}

	public override float GetVolume()
	{
		return uz;
	}

	public override int GetAudioTrackCount()
	{
		return 0;
	}

	public override int GetCurrentAudioTrack()
	{
		return 0;
	}

	public override void SetAudioTrack(int a)
	{
	}

	public override int GetVideoTrackCount()
	{
		return 0;
	}

	public override int GetCurrentVideoTrack()
	{
		return 0;
	}

	public override string GetCurrentAudioTrackId()
	{
		return "";
	}

	public override int GetCurrentAudioTrackBitrate()
	{
		return 0;
	}

	public override void SetVideoTrack(int a)
	{
	}

	public override string GetCurrentVideoTrackId()
	{
		return "";
	}

	public override int GetCurrentVideoTrackBitrate()
	{
		return 0;
	}

	public override float GetVideoFrameRate()
	{
		return 0f;
	}

	public override void Update()
	{
		UpdateSubtitles();
		if (!uw)
		{
			return;
		}
		uy += Time.deltaTime * 1000f;
		if (uy >= GetDurationMs())
		{
			uy = GetDurationMs();
			if (vb)
			{
				Rewind();
			}
		}
		vg += Time.deltaTime;
		if ((double)vg >= 0.1)
		{
			vg = 0f;
			vd = ((vd == ve) ? vf : ve);
			vh++;
		}
	}

	public override void Render()
	{
	}

	public override void Dispose()
	{
	}
}
