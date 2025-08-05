using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class @do : BaseMediaPlayer
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	private struct a
	{
		public enum a
		{
			UpdateAllTextures = 0,
			FreeTextures = 1,
			WaitForNewFrame = 2
		}

		public const int wj = 262537216;

		[DllImport("AVProVideo")]
		public static extern bool Init(bool a, bool b);

		[DllImport("AVProVideo")]
		public static extern void Deinit();

		[DllImport("AVProVideo")]
		public static extern IntPtr GetPluginVersion();

		[DllImport("AVProVideo")]
		public static extern bool IsTrialVersion();

		[DllImport("AVProVideo")]
		public static extern IntPtr OpenSource(IntPtr a, [MarshalAs(UnmanagedType.LPWStr)] string b, int c, bool d, bool e, bool f, bool g, [MarshalAs(UnmanagedType.LPWStr)] string h, bool i, bool j, int k, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] IntPtr[] l, uint m, int n, uint o, uint p);

		[DllImport("AVProVideo")]
		public static extern IntPtr OpenSourceFromBuffer(IntPtr a, byte[] b, ulong c, int d, bool e, bool f, bool g, bool h, [MarshalAs(UnmanagedType.LPWStr)] string i, bool j, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] IntPtr[] k, uint l);

		[DllImport("AVProVideo")]
		public static extern IntPtr StartOpenSourceFromBuffer(IntPtr a, int b, ulong c);

		[DllImport("AVProVideo")]
		public static extern bool AddChunkToSourceBuffer(IntPtr a, byte[] b, ulong c, ulong d);

		[DllImport("AVProVideo")]
		public static extern IntPtr EndOpenSourceFromBuffer(IntPtr a, bool b, bool c, bool d, bool e, [MarshalAs(UnmanagedType.LPWStr)] string f, bool g, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] IntPtr[] h, uint i);

		[DllImport("AVProVideo")]
		public static extern void CloseSource(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern IntPtr GetPlayerDescription(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int GetLastErrorCode(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern long GetLastExtendedErrorCode(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void Play(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void Pause(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void SetMuted(IntPtr a, bool b);

		[DllImport("AVProVideo")]
		public static extern void SetVolume(IntPtr a, float b);

		[DllImport("AVProVideo")]
		public static extern void SetBalance(IntPtr a, float b);

		[DllImport("AVProVideo")]
		public static extern void SetLooping(IntPtr a, bool b);

		[DllImport("AVProVideo")]
		public static extern bool HasVideo(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool HasAudio(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int GetWidth(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int GetHeight(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern float GetFrameRate(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern float GetDuration(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int GetAudioTrackCount(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool IsPlaybackStalled(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool HasMetaData(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool CanPlay(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool IsSeeking(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool IsFinished(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool IsBuffering(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern float GetCurrentTime(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void SetCurrentTime(IntPtr a, float b, bool c);

		[DllImport("AVProVideo")]
		public static extern float GetPlaybackRate(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void SetPlaybackRate(IntPtr a, float b);

		[DllImport("AVProVideo")]
		public static extern int GetAudioTrack(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void SetAudioTrack(IntPtr a, int b);

		[DllImport("AVProVideo")]
		public static extern float GetBufferingProgress(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int GetBufferedRanges(IntPtr a, float[] b, int c);

		[DllImport("AVProVideo")]
		public static extern void StartExtractFrame(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void WaitForExtract(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern void Update(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern IntPtr GetTexturePointer(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern bool IsTextureTopDown(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int GetTextureFrameCount(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern long GetTextureTimeStamp(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern IntPtr GetRenderEventFunc_UpdateAllTextures();

		[DllImport("AVProVideo")]
		public static extern IntPtr GetRenderEventFunc_FreeTextures();

		[DllImport("AVProVideo")]
		public static extern IntPtr GetRenderEventFunc_WaitForNewFrame();

		[DllImport("AVProVideo")]
		public static extern void SetUnityAudioEnabled(IntPtr a, bool b);

		[DllImport("AVProVideo")]
		public static extern void GrabAudio(IntPtr a, float[] b, int c, int d);

		[DllImport("AVProVideo")]
		public static extern int GetAudioChannelCount(IntPtr a);

		[DllImport("AVProVideo")]
		public static extern int SetAudioChannelMode(IntPtr a, int b);

		[DllImport("AVProVideo")]
		public static extern void SetHeadOrientation(IntPtr a, float b, float c, float d, float e);

		[DllImport("AVProVideo")]
		public static extern void SetAudioFocusEnabled(IntPtr a, bool b);

		[DllImport("AVProVideo")]
		public static extern void SetAudioFocusProps(IntPtr a, float b, float c);

		[DllImport("AVProVideo")]
		public static extern void SetAudioFocusRotation(IntPtr a, float b, float c, float d, float e);
	}

	private bool wk = true;

	private bool wl;

	private string wm = string.Empty;

	private List<string> wn = new List<string>();

	private di wo;

	private bool uw;

	private bool ux;

	private bool wp;

	private float uz = 1f;

	private float wq;

	private bool vb;

	private bool wr;

	private bool ws;

	private int uk;

	private int ul;

	private float wt;

	private bool wu;

	private bool wv;

	private bool ww = true;

	private IntPtr wx = IntPtr.Zero;

	private Texture2D vd;

	private IntPtr b = IntPtr.Zero;

	private float wy;

	private int wz;

	private float xa = 1f;

	private df.a xb;

	private bool xc = true;

	private bool xd;

	private bool xe;

	private bool xf;

	private int xg = -1;

	private bool xh = true;

	private int xi;

	private float[] xj = new float[0];

	private static bool xk = false;

	private static string xl = "Plug-in not yet initialised";

	private static IntPtr xm;

	private static IntPtr xn;

	private static IntPtr xo;

	private int xp = QualitySettings.masterTextureLimit;

	public static bool bjz()
	{
		if (!xk)
		{
			try
			{
				if (!a.Init(QualitySettings.activeColorSpace == ColorSpace.Linear, b: true))
				{
					Debug.LogError("[AVProVideo] Failing to initialise platform");
				}
				else
				{
					xk = true;
					xl = bkh();
					xm = a.GetRenderEventFunc_UpdateAllTextures();
					xn = a.GetRenderEventFunc_FreeTextures();
					xo = a.GetRenderEventFunc_WaitForNewFrame();
				}
			}
			catch (DllNotFoundException ex)
			{
				Debug.LogError("[AVProVideo] Failed to load DLL. " + ex.Message);
			}
		}
		return xk;
	}

	public static void bka()
	{
		a.Deinit();
		xk = false;
	}

	public override int GetNumAudioChannels()
	{
		return a.GetAudioChannelCount(b);
	}

	public @do(df.a a, bool b, bool c, bool d, bool e, string f, bool g, bool h, List<string> i)
	{
		bkb(a, b, c, d, e, f, g, h, i);
	}

	public void bkb(df.a a, bool b, bool c, bool d, bool e, string f, bool g, bool h, List<string> i)
	{
		xb = a;
		xc = b;
		xd = c;
		xe = d;
		xf = e;
		wm = f;
		if (!string.IsNullOrEmpty(wm))
		{
			wm = wm.Trim();
		}
		wl = g;
		wk = h;
		wn = i;
		if (wn == null)
		{
			return;
		}
		for (int num = 0; num < wn.Count; num++)
		{
			if (!string.IsNullOrEmpty(wn[num]))
			{
				wn[num] = wn[num].Trim();
			}
		}
	}

	public override string GetVersion()
	{
		return xl;
	}

	private static int bkc()
	{
		if (AudioSettings.GetConfiguration().sampleRate != 0)
		{
			return AudioSettings.outputSampleRate;
		}
		return 0;
	}

	public override bool OpenVideoFromFile(string a, long b, string c, uint d = 0u, uint e = 0u, int f = 0)
	{
		CloseVideo();
		uint num = 0u;
		IntPtr[] array = null;
		if (wn != null && wn.Count > 0)
		{
			num = (uint)wn.Count;
			array = new IntPtr[wn.Count];
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				array[num2] = Marshal.StringToHGlobalUni(wn[num2]);
			}
		}
		this.b = @do.a.OpenSource(this.b, a, (int)xb, xc, xd, xe, xf, wm, wl, wk, bkc(), array, num, (int)wo, d, e);
		if (array != null)
		{
			for (int num3 = 0; num3 < array.Length; num3++)
			{
				Marshal.FreeHGlobal(array[num3]);
			}
		}
		if (this.b == IntPtr.Zero)
		{
			bkd(a);
			return false;
		}
		@do.a.SetUnityAudioEnabled(this.b, wl);
		return true;
	}

	public override bool OpenVideoFromBuffer(byte[] a)
	{
		CloseVideo();
		IntPtr[] array;
		if (wn.Count == 0)
		{
			array = null;
		}
		else
		{
			array = new IntPtr[wn.Count];
			for (int num = 0; num < array.Length; num++)
			{
				array[num] = Marshal.StringToHGlobalUni(wn[num]);
			}
		}
		b = @do.a.OpenSourceFromBuffer(b, a, (ulong)a.Length, (int)xb, xc, xd, xe, xf, wm, wl, array, (uint)wn.Count);
		if (array != null)
		{
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				Marshal.FreeHGlobal(array[num2]);
			}
		}
		if (b == IntPtr.Zero)
		{
			return false;
		}
		@do.a.SetUnityAudioEnabled(b, wl);
		return true;
	}

	public override bool StartOpenVideoFromBuffer(ulong a)
	{
		CloseVideo();
		b = @do.a.StartOpenSourceFromBuffer(b, (int)xb, a);
		return b != IntPtr.Zero;
	}

	public override bool AddChunkToVideoBuffer(byte[] a, ulong b, ulong c)
	{
		return @do.a.AddChunkToSourceBuffer(this.b, a, b, c);
	}

	public override bool EndOpenVideoFromBuffer()
	{
		IntPtr[] array;
		if (wn.Count == 0)
		{
			array = null;
		}
		else
		{
			array = new IntPtr[wn.Count];
			for (int num = 0; num < array.Length; num++)
			{
				array[num] = Marshal.StringToHGlobalUni(wn[num]);
			}
		}
		b = a.EndOpenSourceFromBuffer(b, xc, xd, xe, xf, wm, wl, array, (uint)wn.Count);
		if (array != null)
		{
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				Marshal.FreeHGlobal(array[num2]);
			}
		}
		if (b == IntPtr.Zero)
		{
			return false;
		}
		a.SetUnityAudioEnabled(b, wl);
		return true;
	}

	private void bkd(string a)
	{
		if ((xb == df.a.DirectShow || SystemInfo.operatingSystem.Contains("Windows 7") || SystemInfo.operatingSystem.Contains("Windows Vista") || SystemInfo.operatingSystem.Contains("Windows XP")) && a.Contains(".mp4"))
		{
			Debug.LogWarning("[AVProVideo] The native Windows DirectShow H.264 decoder doesn't support videos with resolution above 1920x1080. You may need to reduce your video resolution, switch to another codec (such as DivX or Hap), or install 3rd party DirectShow codec (eg LAV Filters).  This shouldn't be a problem for Windows 8 and above as it has a native limitation of 3840x2160.");
		}
	}

	public override void CloseVideo()
	{
		uk = 0;
		ul = 0;
		wt = 0f;
		wu = (wv = false);
		ws = false;
		wr = false;
		ux = false;
		uw = false;
		vb = false;
		wp = false;
		uz = 1f;
		wq = 0f;
		wz = 0;
		xa = 0f;
		wy = 0f;
		xg = -1;
		xh = true;
		wx = IntPtr.Zero;
		if (vd != null)
		{
			UnityEngine.Object.Destroy(vd);
			vd = null;
		}
		if (b != IntPtr.Zero)
		{
			a.CloseSource(b);
			b = IntPtr.Zero;
		}
		bkg(a.a.FreeTextures);
		base.CloseVideo();
	}

	public override void SetLooping(bool a)
	{
		vb = a;
		@do.a.SetLooping(b, a);
	}

	public override bool IsLooping()
	{
		return vb;
	}

	public override bool HasMetaData()
	{
		return ws;
	}

	public override bool HasAudio()
	{
		return wu;
	}

	public override bool HasVideo()
	{
		return wv;
	}

	public override bool CanPlay()
	{
		return wr;
	}

	public override void Play()
	{
		uw = true;
		ux = false;
		a.Play(b);
	}

	public override void Pause()
	{
		uw = false;
		ux = true;
		a.Pause(b);
	}

	public override void Stop()
	{
		uw = false;
		ux = false;
		a.Pause(b);
	}

	public override bool IsSeeking()
	{
		return a.IsSeeking(b);
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
		return a.IsFinished(b);
	}

	public override bool IsBuffering()
	{
		return a.IsBuffering(b);
	}

	public override float GetDurationMs()
	{
		return a.GetDuration(b) * 1000f;
	}

	public override int GetVideoWidth()
	{
		return uk;
	}

	public override int GetVideoHeight()
	{
		return ul;
	}

	public override float GetVideoFrameRate()
	{
		return wt;
	}

	public override float GetVideoDisplayRate()
	{
		return xa;
	}

	public override Texture GetTexture(int a)
	{
		Texture result = null;
		if (@do.a.GetTextureFrameCount(b) > 0)
		{
			result = vd;
		}
		return result;
	}

	public override int GetTextureFrameCount()
	{
		return a.GetTextureFrameCount(b);
	}

	public override long GetTextureTimeStamp()
	{
		return a.GetTextureTimeStamp(b);
	}

	public override bool RequiresVerticalFlip()
	{
		return ww;
	}

	public override void Seek(float a)
	{
		_isSeekingStarted = true;
		@do.a.SetCurrentTime(b, a / 1000f, c: false);
	}

	public override void SeekFast(float a)
	{
		_isSeekingStarted = true;
		@do.a.SetCurrentTime(b, a / 1000f, c: true);
	}

	public override float GetCurrentTimeMs()
	{
		return a.GetCurrentTime(b) * 1000f;
	}

	public override void SetPlaybackRate(float a)
	{
		@do.a.SetPlaybackRate(b, a);
	}

	public override float GetPlaybackRate()
	{
		return a.GetPlaybackRate(b);
	}

	public override float GetBufferingProgress()
	{
		return a.GetBufferingProgress(b);
	}

	public override int GetBufferedTimeRangeCount()
	{
		return xi;
	}

	public override bool GetBufferedTimeRange(int a, ref float b, ref float c)
	{
		bool result = false;
		if (a >= 0 && a < xi)
		{
			result = true;
			b = 1000f * xj[a * 2];
			c = 1000f * xj[a * 2 + 1];
		}
		return result;
	}

	public override void MuteAudio(bool a)
	{
		wp = a;
		@do.a.SetMuted(b, wp);
	}

	public override bool IsMuted()
	{
		return wp;
	}

	public override void SetVolume(float a)
	{
		uz = a;
		@do.a.SetVolume(b, a);
	}

	public override float GetVolume()
	{
		return uz;
	}

	public override void SetBalance(float a)
	{
		wq = a;
		@do.a.SetBalance(b, a);
	}

	public override float GetBalance()
	{
		return wq;
	}

	public override int GetAudioTrackCount()
	{
		return a.GetAudioTrackCount(b);
	}

	public override int GetCurrentAudioTrack()
	{
		return a.GetAudioTrack(b);
	}

	public override void SetAudioTrack(int a)
	{
		xg = a;
	}

	public override int GetVideoTrackCount()
	{
		int result = 0;
		if (HasVideo())
		{
			result = 1;
		}
		return result;
	}

	public override bool IsPlaybackStalled()
	{
		bool flag = a.IsPlaybackStalled(b);
		if (!flag)
		{
			flag = base.IsPlaybackStalled();
		}
		return flag;
	}

	public override string GetCurrentAudioTrackId()
	{
		return string.Empty;
	}

	public override int GetCurrentAudioTrackBitrate()
	{
		return 0;
	}

	public override int GetCurrentVideoTrack()
	{
		return 0;
	}

	public override void SetVideoTrack(int a)
	{
	}

	public override string GetCurrentVideoTrackId()
	{
		return string.Empty;
	}

	public override int GetCurrentVideoTrackBitrate()
	{
		return 0;
	}

	public override bool WaitForNextFrame(Camera a, int b)
	{
		@do.a.StartExtractFrame(this.b);
		bkg(@do.a.a.WaitForNewFrame);
		a.Render();
		@do.a.WaitForExtract(this.b);
		return b != @do.a.GetTextureFrameCount(this.b);
	}

	public override void SetAudioChannelMode(di a)
	{
		wo = a;
		@do.a.SetAudioChannelMode(b, (int)a);
	}

	public override void SetAudioHeadRotation(Quaternion a)
	{
		@do.a.SetHeadOrientation(b, a.x, a.y, a.z, a.w);
	}

	public override void ResetAudioHeadRotation()
	{
		a.SetHeadOrientation(b, Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w);
	}

	public override void SetAudioFocusEnabled(bool a)
	{
		@do.a.SetAudioFocusEnabled(b, a);
	}

	public override void SetAudioFocusProperties(float a, float b)
	{
		@do.a.SetAudioFocusProps(this.b, a, b);
	}

	public override void SetAudioFocusRotation(Quaternion a)
	{
		@do.a.SetAudioFocusRotation(b, a.x, a.y, a.z, a.w);
	}

	public override void ResetAudioFocus()
	{
		a.SetAudioFocusEnabled(b, b: false);
		a.SetAudioFocusProps(b, 0f, 90f);
		a.SetAudioFocusRotation(b, 0f, 0f, 0f, 1f);
	}

	public override void Update()
	{
		a.Update(b);
		_lastError = (da)a.GetLastErrorCode(b);
		if (xg >= 0 && wu)
		{
			a.SetAudioTrack(b, xg);
			xg = -1;
		}
		xi = a.GetBufferedRanges(b, xj, xj.Length / 2);
		if (xi > xj.Length / 2)
		{
			xj = new float[xi * 2];
			xi = a.GetBufferedRanges(b, xj, xj.Length / 2);
		}
		UpdateSubtitles();
		if (!wr)
		{
			if (!ws && a.HasMetaData(b))
			{
				if (a.HasVideo(b))
				{
					uk = a.GetWidth(b);
					ul = a.GetHeight(b);
					wt = a.GetFrameRate(b);
					if (uk > 0 && ul > 0)
					{
						wv = true;
						if (Mathf.Max(uk, ul) > SystemInfo.maxTextureSize)
						{
							Debug.LogError($"[AVProVideo] Video dimensions ({uk}x{ul}) larger than maxTextureSize ({SystemInfo.maxTextureSize} for current build target)");
							uk = (ul = 0);
							wv = false;
						}
					}
					if (wv && a.HasAudio(b))
					{
						wu = true;
					}
				}
				else if (a.HasAudio(b))
				{
					wu = true;
				}
				if (wv || wu)
				{
					ws = true;
				}
				_playerDescription = Marshal.PtrToStringAnsi(a.GetPlayerDescription(b));
				xh = !_playerDescription.Contains("MF-MediaEngine-Hardware");
				dl.bij("Using playback path: " + _playerDescription + " (" + uk + "x" + ul + "@" + GetVideoFrameRate().ToString("F2") + ")");
				if (wv)
				{
					bke();
				}
			}
			if (ws)
			{
				wr = a.CanPlay(b);
			}
		}
		if (!wv)
		{
			return;
		}
		IntPtr texturePointer = a.GetTexturePointer(b);
		if (vd != null && wx != IntPtr.Zero && wx != texturePointer)
		{
			uk = a.GetWidth(b);
			ul = a.GetHeight(b);
			if (texturePointer == IntPtr.Zero || uk != vd.width || ul != vd.height)
			{
				if (uk != vd.width || ul != vd.height)
				{
					dl.bij("Texture size changed: " + uk + " X " + ul);
					bke();
				}
				wx = IntPtr.Zero;
				UnityEngine.Object.Destroy(vd);
				vd = null;
			}
			else if (wx != texturePointer)
			{
				vd.UpdateExternalTexture(texturePointer);
				wx = texturePointer;
			}
		}
		if (xp != QualitySettings.masterTextureLimit)
		{
			if (vd != null && wx != IntPtr.Zero && vd.GetNativeTexturePtr() == IntPtr.Zero)
			{
				vd.UpdateExternalTexture(wx);
			}
			xp = QualitySettings.masterTextureLimit;
		}
		if (vd == null && uk > 0 && ul > 0 && texturePointer != IntPtr.Zero)
		{
			ww = a.IsTextureTopDown(b);
			bool linear = !xh && QualitySettings.activeColorSpace == ColorSpace.Linear;
			vd = Texture2D.CreateExternalTexture(uk, ul, TextureFormat.RGBA32, xd, linear, texturePointer);
			if (vd != null)
			{
				vd.name = "AVProVideo";
				wx = texturePointer;
				ApplyTextureProperties(vd);
			}
			else
			{
				Debug.LogError("[AVProVideo] Failed to create texture");
			}
		}
	}

	public override long GetLastExtendedErrorCode()
	{
		return a.GetLastExtendedErrorCode(b);
	}

	private void bke()
	{
		if ((uk == 720 || ul == 480) && _playerDescription.Contains("DirectShow"))
		{
			Debug.LogWarning("[AVProVideo] If video fails to play then it may be due to the resolution being higher than 1920x1080 which is the limitation of the Microsoft DirectShow H.264 decoder.\nTo resolve this you can either use Windows 8 or above (and disable 'Force DirectShow' option), resize your video, use a different codec (such as Hap or DivX), or install a 3rd party H.264 decoder such as LAV Filters.");
		}
		else if (uk > 1920 || ul > 1080)
		{
			_playerDescription.Contains("MF-MediaEngine-Software");
		}
	}

	private void bkf()
	{
		wy += Time.deltaTime;
		if (wy >= 0.5f)
		{
			int textureFrameCount = a.GetTextureFrameCount(b);
			xa = (float)(textureFrameCount - wz) / wy;
			wy -= 0.5f;
			if (wy >= 0.5f)
			{
				wy = 0f;
			}
			wz = textureFrameCount;
		}
	}

	public override void Render()
	{
		bkf();
		bkg(a.a.UpdateAllTextures);
	}

	public override void Dispose()
	{
		CloseVideo();
	}

	public override void GrabAudio(float[] a, int b, int c)
	{
		@do.a.GrabAudio(this.b, a, b, c);
	}

	public override bool PlayerSupportsLinearColorSpace()
	{
		return xh;
	}

	private static void bkg(a.a a)
	{
		switch (a)
		{
		case @do.a.a.UpdateAllTextures:
			GL.IssuePluginEvent(xm, 0);
			break;
		case @do.a.a.FreeTextures:
			GL.IssuePluginEvent(xn, 0);
			break;
		case @do.a.a.WaitForNewFrame:
			GL.IssuePluginEvent(xo, 0);
			break;
		}
	}

	private static string bkh()
	{
		return Marshal.PtrToStringAnsi(a.GetPluginVersion());
	}

	public override void OnEnable()
	{
		base.OnEnable();
		if (vd != null && wx != IntPtr.Zero && vd.GetNativeTexturePtr() == IntPtr.Zero)
		{
			vd.UpdateExternalTexture(wx);
		}
		xp = QualitySettings.masterTextureLimit;
	}
}
