using System;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	public abstract class BaseMediaPlayer : cr, ct, cu, cv, cs, IDisposable
	{
		protected string _playerDescription = string.Empty;

		protected da _lastError;

		protected FilterMode _defaultTextureFilterMode = FilterMode.Bilinear;

		protected TextureWrapMode _defaultTextureWrapMode = TextureWrapMode.Clamp;

		protected int _defaultTextureAnisoLevel = 1;

		protected dj[] _seekableTimeRanges = new dj[0];

		protected bool _isSeekingStarted;

		private float _stallDetectionTimer;

		private int _stallDetectionFrame;

		protected List<dk> _subtitles;

		protected dk _currentSubtitle;

		public abstract string GetVersion();

		public abstract bool OpenVideoFromFile(string a, long b, string c, uint d = 0u, uint e = 0u, int f = 0);

		public virtual bool OpenVideoFromBuffer(byte[] a)
		{
			return false;
		}

		public virtual bool StartOpenVideoFromBuffer(ulong a)
		{
			return false;
		}

		public virtual bool AddChunkToVideoBuffer(byte[] a, ulong b, ulong c)
		{
			return false;
		}

		public virtual bool EndOpenVideoFromBuffer()
		{
			return false;
		}

		public virtual void CloseVideo()
		{
			_stallDetectionTimer = 0f;
			_stallDetectionFrame = 0;
			_lastError = da.None;
			_isSeekingStarted = false;
		}

		public abstract void SetLooping(bool a);

		public abstract bool IsLooping();

		public abstract bool HasMetaData();

		public abstract bool CanPlay();

		public abstract void Play();

		public abstract void Pause();

		public abstract void Stop();

		public virtual void Rewind()
		{
			SeekFast(0f);
		}

		public abstract void Seek(float a);

		public abstract void SeekFast(float a);

		public virtual void SeekWithTolerance(float a, float b, float c)
		{
			Seek(a);
		}

		public abstract float GetCurrentTimeMs();

		public virtual double GetCurrentDateTimeSecondsSince1970()
		{
			return 0.0;
		}

		public virtual dj[] GetSeekableTimeRanges()
		{
			return _seekableTimeRanges;
		}

		public abstract float GetPlaybackRate();

		public abstract void SetPlaybackRate(float a);

		public abstract float GetDurationMs();

		public abstract int GetVideoWidth();

		public abstract int GetVideoHeight();

		public virtual Rect GetCropRect()
		{
			return new Rect(0f, 0f, 0f, 0f);
		}

		public abstract float GetVideoDisplayRate();

		public abstract bool HasAudio();

		public abstract bool HasVideo();

		public abstract bool IsSeeking();

		public abstract bool IsPlaying();

		public abstract bool IsPaused();

		public abstract bool IsFinished();

		public abstract bool IsBuffering();

		public virtual bool WaitForNextFrame(Camera a, int b)
		{
			return false;
		}

		public virtual void SetPlayWithoutBuffering(bool a)
		{
		}

		public virtual void SetKeyServerURL(string a)
		{
		}

		public virtual void SetKeyServerAuthToken(string a)
		{
		}

		public virtual void SetDecryptionKeyBase64(string a)
		{
		}

		public virtual void SetDecryptionKey(byte[] a)
		{
		}

		public virtual bool IsExternalPlaybackSupported()
		{
			return false;
		}

		public virtual bool IsExternalPlaybackActive()
		{
			return false;
		}

		public virtual void SetAllowsExternalPlayback(bool a)
		{
		}

		public virtual void SetExternalPlaybackFillMode(dd a)
		{
		}

		public virtual int GetTextureCount()
		{
			return 1;
		}

		public abstract Texture GetTexture(int a = 0);

		public abstract int GetTextureFrameCount();

		public virtual bool SupportsTextureFrameCount()
		{
			return true;
		}

		public virtual long GetTextureTimeStamp()
		{
			return long.MinValue;
		}

		public abstract bool RequiresVerticalFlip();

		public virtual float[] GetTextureTransform()
		{
			return new float[6] { 1f, 0f, 0f, 1f, 0f, 0f };
		}

		public virtual Matrix4x4 GetYpCbCrTransform()
		{
			return Matrix4x4.identity;
		}

		public abstract void MuteAudio(bool a);

		public abstract bool IsMuted();

		public abstract void SetVolume(float a);

		public virtual void SetBalance(float a)
		{
		}

		public abstract float GetVolume();

		public virtual float GetBalance()
		{
			return 0f;
		}

		public abstract int GetAudioTrackCount();

		public virtual string GetAudioTrackId(int a)
		{
			return a.ToString();
		}

		public abstract int GetCurrentAudioTrack();

		public abstract void SetAudioTrack(int a);

		public abstract string GetCurrentAudioTrackId();

		public abstract int GetCurrentAudioTrackBitrate();

		public virtual int GetNumAudioChannels()
		{
			return -1;
		}

		public virtual void SetAudioHeadRotation(Quaternion a)
		{
		}

		public virtual void ResetAudioHeadRotation()
		{
		}

		public virtual void SetAudioChannelMode(di a)
		{
		}

		public virtual void SetAudioFocusEnabled(bool a)
		{
		}

		public virtual void SetAudioFocusProperties(float a, float b)
		{
		}

		public virtual void SetAudioFocusRotation(Quaternion a)
		{
		}

		public virtual void ResetAudioFocus()
		{
		}

		public abstract int GetVideoTrackCount();

		public virtual string GetVideoTrackId(int a)
		{
			return a.ToString();
		}

		public abstract int GetCurrentVideoTrack();

		public abstract void SetVideoTrack(int a);

		public abstract string GetCurrentVideoTrackId();

		public abstract int GetCurrentVideoTrackBitrate();

		public abstract float GetVideoFrameRate();

		public virtual long GetEstimatedTotalBandwidthUsed()
		{
			return -1L;
		}

		public abstract float GetBufferingProgress();

		public abstract void Update();

		public abstract void Render();

		public abstract void Dispose();

		public da GetLastError()
		{
			return _lastError;
		}

		public virtual long GetLastExtendedErrorCode()
		{
			return 0L;
		}

		public string GetPlayerDescription()
		{
			return _playerDescription;
		}

		public virtual bool PlayerSupportsLinearColorSpace()
		{
			return true;
		}

		public virtual int GetBufferedTimeRangeCount()
		{
			return 0;
		}

		public virtual bool GetBufferedTimeRange(int a, ref float b, ref float c)
		{
			return false;
		}

		public void SetTextureProperties(FilterMode a = FilterMode.Bilinear, TextureWrapMode b = TextureWrapMode.Clamp, int c = 0)
		{
			_defaultTextureFilterMode = a;
			_defaultTextureWrapMode = b;
			_defaultTextureAnisoLevel = c;
			for (int i = 0; i < GetTextureCount(); i++)
			{
				ApplyTextureProperties(GetTexture(i));
			}
		}

		protected virtual void ApplyTextureProperties(Texture a)
		{
			if (a != null)
			{
				a.filterMode = _defaultTextureFilterMode;
				a.wrapMode = _defaultTextureWrapMode;
				a.anisoLevel = _defaultTextureAnisoLevel;
			}
		}

		public virtual void GrabAudio(float[] a, int b, int c)
		{
		}

		protected bool IsExpectingNewVideoFrame()
		{
			if (HasVideo() && !IsFinished() && (!IsPaused() || IsPlaying()))
			{
				return true;
			}
			return false;
		}

		public virtual bool IsPlaybackStalled()
		{
			if (SupportsTextureFrameCount() && IsExpectingNewVideoFrame())
			{
				int textureFrameCount = GetTextureFrameCount();
				if (textureFrameCount != _stallDetectionFrame)
				{
					_stallDetectionTimer = 0f;
					_stallDetectionFrame = textureFrameCount;
				}
				else
				{
					_stallDetectionTimer += Time.deltaTime;
				}
				return _stallDetectionTimer > 0.75f;
			}
			_stallDetectionTimer = 0f;
			return false;
		}

		public bool IsSeekingStarted()
		{
			if (!_isSeekingStarted)
			{
				return IsSeeking();
			}
			return true;
		}

		public void ResetEventState()
		{
			_isSeekingStarted = false;
		}

		public bool LoadSubtitlesSRT(string a)
		{
			if (string.IsNullOrEmpty(a))
			{
				_subtitles = null;
				_currentSubtitle = null;
			}
			else
			{
				_subtitles = dl.biz(a);
				_currentSubtitle = null;
			}
			return _subtitles != null;
		}

		public virtual void UpdateSubtitles()
		{
			if (_subtitles == null)
			{
				return;
			}
			float currentTimeMs = GetCurrentTimeMs();
			int num = 0;
			if (_currentSubtitle != null && !_currentSubtitle.bif(currentTimeMs))
			{
				if (currentTimeMs > (float)_currentSubtitle.uu)
				{
					num = _currentSubtitle.ur + 1;
				}
				_currentSubtitle = null;
			}
			if (_currentSubtitle != null)
			{
				return;
			}
			for (int i = num; i < _subtitles.Count; i++)
			{
				if (_subtitles[i].bif(currentTimeMs))
				{
					_currentSubtitle = _subtitles[i];
					break;
				}
			}
		}

		public virtual int GetSubtitleIndex()
		{
			int result = -1;
			if (_currentSubtitle != null)
			{
				result = _currentSubtitle.ur;
			}
			return result;
		}

		public virtual string GetSubtitleText()
		{
			string result = string.Empty;
			if (_currentSubtitle != null)
			{
				result = _currentSubtitle.us;
			}
			return result;
		}

		public virtual void OnEnable()
		{
		}
	}
}
