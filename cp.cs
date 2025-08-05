using System;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;

public class cp
{
	private static cp b;

	private Dictionary<MediaPlayer, HashSet<AudioOutput>> uc;

	private Dictionary<MediaPlayer, float[]> ud;

	public static cp a
	{
		get
		{
			if (b == null)
			{
				b = new cp();
			}
			return b;
		}
	}

	private cp()
	{
		uc = new Dictionary<MediaPlayer, HashSet<AudioOutput>>();
		ud = new Dictionary<MediaPlayer, float[]>();
	}

	public void bhu(AudioOutput a, MediaPlayer b, float[] c, int d, int e, AudioOutput.a f)
	{
		if (b == null || b.ej == null || !b.ej.IsPlaying())
		{
			return;
		}
		int numAudioChannels = b.ej.GetNumAudioChannels();
		if (numAudioChannels <= 0)
		{
			return;
		}
		if (!uc.ContainsKey(b))
		{
			uc[b] = new HashSet<AudioOutput>();
		}
		if (uc[b].Contains(a) || uc[b].Count == 0 || ud[b] == null)
		{
			uc[b].Clear();
			int num = c.Length / e * numAudioChannels;
			ud[b] = new float[num];
			bhv(b, ud[b], numAudioChannels);
			uc[b].Add(a);
		}
		int num2 = Math.Min(c.Length / e, ud[b].Length / numAudioChannels);
		int num3 = 0;
		int num4 = 0;
		switch (f)
		{
		case AudioOutput.a.Multiple:
		{
			int num9 = Math.Min(numAudioChannels, e);
			for (int num10 = 0; num10 < num2; num10++)
			{
				for (int num11 = 0; num11 < num9; num11++)
				{
					if (((1 << num11) & d) > 0)
					{
						c[num4 + num11] = ud[b][num3 + num11];
					}
				}
				num3 += numAudioChannels;
				num4 += e;
			}
			break;
		}
		case AudioOutput.a.Single:
		{
			int num5 = 0;
			for (int num6 = 0; num6 < 8; num6++)
			{
				if ((d & (1 << num6)) > 0)
				{
					num5 = num6;
					break;
				}
			}
			if (num5 >= numAudioChannels)
			{
				break;
			}
			for (int num7 = 0; num7 < num2; num7++)
			{
				for (int num8 = 0; num8 < e; num8++)
				{
					c[num4 + num8] = ud[b][num3 + num5];
				}
				num3 += numAudioChannels;
				num4 += e;
			}
			break;
		}
		}
	}

	private void bhv(MediaPlayer a, float[] b, int c)
	{
		a.ej.GrabAudio(b, b.Length, c);
	}
}
