using System;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class dn
{
	private class a
	{
		public RenderTexture vj;

		public long vk;

		public bool vl;
	}

	public enum b
	{
		POINT = 0,
		LINEAR = 1
	}

	private List<a[]> vm = new List<a[]>();

	private MediaPlayer vn;

	private RenderTexture[] vo;

	private int vp;

	private int vq;

	private int vr;

	private long vs;

	private float vt;

	private Material vu;

	private b vv;

	private string vw = "";

	private long vx = -1L;

	private int vy;

	private long vz;

	private int wa;

	private long wb;

	private const string we = "_t";

	private const string wf = "_AfterTex";

	private int wg;

	private int wh;

	private float wi;

	public int fz => vy;

	public int ga => wa;

	public long gb
	{
		get
		{
			return vs;
		}
		set
		{
			vs = value;
		}
	}

	public float gc
	{
		get
		{
			return vt;
		}
		set
		{
			vt = value;
		}
	}

	public float gd { get; private set; }

	public long ge { get; private set; }

	public Texture[] gf => vo;

	public void bjk(MediaPlayer a, MediaPlayerEvent.a b, da c)
	{
		switch (b)
		{
		case MediaPlayerEvent.a.MetaDataReady:
			wi = a.ei.GetVideoFrameRate();
			vt = 0f;
			if (wi > 0f)
			{
				vt = (float)vr / wi;
			}
			break;
		case MediaPlayerEvent.a.Closing:
			Reset();
			break;
		}
	}

	public dn(MediaPlayer a, string b, int c = 2, b d = dn.b.LINEAR)
	{
		vr = Mathf.Max(2, c);
		a.em.bib(bjk);
		vn = a;
		Shader shader = Shader.Find("AVProVideo/BlendFrames");
		if (shader != null)
		{
			vu = new Material(shader);
			wh = Shader.PropertyToID("_t");
			wg = Shader.PropertyToID("_AfterTex");
		}
		else
		{
			Debug.LogError("[AVProVideo] Failed to find BlendFrames shader");
		}
		vv = d;
		vw = b;
		Debug.Log("[AVProVideo] Resampler " + vw + " started");
	}

	public void Reset()
	{
		vx = -1L;
		vs = 0L;
		bjw();
	}

	public void bjm()
	{
		bjn();
		if (vu != null)
		{
			UnityEngine.Object.Destroy(vu);
		}
	}

	private void bjn()
	{
		for (int num = 0; num < vm.Count; num++)
		{
			for (int num2 = 0; num2 < vm[num].Length; num2++)
			{
				if (vm[num][num2].vj != null)
				{
					RenderTexture.ReleaseTemporary(vm[num][num2].vj);
					vm[num][num2].vj = null;
				}
			}
			if (vo != null && vo[num] != null)
			{
				RenderTexture.ReleaseTemporary(vo[num]);
			}
		}
		vo = null;
	}

	private void bjo()
	{
		bjn();
		vm.Clear();
		vo = new RenderTexture[vn.ek.GetTextureCount()];
		for (int num = 0; num < vn.ek.GetTextureCount(); num++)
		{
			Texture texture = vn.ek.GetTexture(num);
			vm.Add(new a[vr]);
			for (int num2 = 0; num2 < vr; num2++)
			{
				vm[num][num2] = new a();
			}
			for (int num3 = 0; num3 < vm[num].Length; num3++)
			{
				vm[num][num3].vj = RenderTexture.GetTemporary(texture.width, texture.height, 0);
				vm[num][num3].vk = 0L;
				vm[num][num3].vl = false;
			}
			vo[num] = RenderTexture.GetTemporary(texture.width, texture.height, 0);
			vo[num].filterMode = texture.filterMode;
			vo[num].wrapMode = texture.wrapMode;
			vo[num].anisoLevel = texture.anisoLevel;
		}
	}

	private bool bjp()
	{
		for (int num = 0; num < vn.ek.GetTextureCount(); num++)
		{
			Texture texture = vn.ek.GetTexture(num);
			for (int num2 = 0; num2 < vm.Count; num2++)
			{
				if (vm[num][num2].vj == null || vm[num][num2].vj.width != texture.width || vm[num][num2].vj.height != texture.height)
				{
					return false;
				}
			}
			if (vo == null || vo[num] == null || vo[num].width != texture.width || vo[num].height != texture.height)
			{
				return false;
			}
		}
		return true;
	}

	private int bjq(int a)
	{
		if (a >= vm.Count)
		{
			return -1;
		}
		int num = -1;
		float num2 = float.MaxValue;
		int num3 = -1;
		float num4 = float.MaxValue;
		for (int num5 = 0; num5 < vm[a].Length; num5++)
		{
			if (vm[a][num5].vl)
			{
				float num6 = (float)(vm[a][num5].vk - vs) / 10000000f;
				if (num6 < num4)
				{
					num3 = num5;
					num4 = num6;
				}
				float num7 = vt - num6;
				if (num7 >= 0f && num7 < num2)
				{
					num2 = num7;
					num = num5;
				}
			}
		}
		if (num < 0)
		{
			if (num3 < 0)
			{
				return -1;
			}
			return num3;
		}
		return num;
	}

	private int bjr(int a)
	{
		if (a >= vm.Count)
		{
			return -1;
		}
		int result = -1;
		float num = float.MaxValue;
		for (int num2 = 0; num2 < vm[a].Length; num2++)
		{
			if (vm[a][num2].vl)
			{
				float num3 = (float)(vm[a][num2].vk - vs) / 10000000f;
				float num4 = Mathf.Abs(vt - num3);
				if (num4 < num)
				{
					result = num2;
					num = num4;
				}
			}
		}
		return result;
	}

	private void bjs()
	{
		for (int num = 0; num < vm.Count; num++)
		{
			int num2 = bjr(num);
			if (num2 >= 0)
			{
				vo[num].DiscardContents();
				Graphics.Blit(vm[num][num2].vj, vo[num]);
				ge = (wb = vm[num][num2].vk);
			}
		}
	}

	private void bjt(int a, int b)
	{
		vo[b].DiscardContents();
		Graphics.Blit(vm[b][a].vj, vo[b]);
		ge = (wb = vm[b][a].vk);
	}

	private void bju(int a, int b, int c, float d)
	{
		vu.SetFloat(wh, d);
		vu.SetTexture(wg, vm[a][c].vj);
		vo[a].DiscardContents();
		Graphics.Blit(vm[a][b].vj, vo[a], vu);
		ge = (long)Mathf.Lerp(vm[a][b].vk, vm[a][c].vk, d);
		wb = vm[a][b].vk;
	}

	private void bjv()
	{
		for (int num = 0; num < vm.Count; num++)
		{
			int num2 = bjq(num);
			if (num2 < 0)
			{
				continue;
			}
			float num3 = (float)(vm[num][num2].vk - vs) / 10000000f;
			if (num3 > vt)
			{
				bjt(num2, num);
				gd = -1f;
				continue;
			}
			int num4 = (num2 + 1) % vm[num].Length;
			float num5 = (float)(vm[num][num4].vk - vs) / 10000000f;
			if (num5 < num3)
			{
				bjt(num2, num);
				gd = 2f;
				continue;
			}
			float num6 = num5 - num3;
			float num7 = (vt - num3) / num6;
			bju(num, num2, num4, num7);
			gd = num7;
		}
	}

	private void bjw()
	{
		vt = (float)(vr / 2) / wi;
		for (int num = 0; num < vm.Count; num++)
		{
			for (int num2 = 0; num2 < vm[num].Length; num2++)
			{
				vm[num][num2].vl = false;
			}
		}
		vp = (vq = 0);
	}

	private float bjx()
	{
		int num = 0;
		long num2 = 0L;
		for (int num3 = 0; num3 < vm[0].Length; num3++)
		{
			if (!vm[0][num3].vl)
			{
				continue;
			}
			long num4 = long.MaxValue;
			for (int num5 = num3 + 1; num5 < vm[0].Length; num5++)
			{
				if (vm[0][num5].vl)
				{
					long num6 = Math.Abs(vm[0][num3].vk - vm[0][num5].vk);
					if (num6 < num4)
					{
						num4 = num6;
					}
				}
			}
			if (num4 != long.MaxValue)
			{
				num2 += num4;
				num++;
			}
		}
		if (num > 1)
		{
			num2 /= num;
		}
		return 10000000f / (float)num2;
	}

	public void Update()
	{
		if (vn.ek == null || vn.ek == null || vn.ek.GetTexture() == null)
		{
			return;
		}
		if (!bjp())
		{
			bjo();
		}
		long textureTimeStamp = vn.ek.GetTextureTimeStamp();
		if (textureTimeStamp != vx)
		{
			float num = Mathf.Abs(textureTimeStamp - vx);
			float num2 = 10000000f / wi;
			if (num > num2 * 1.1f && num < num2 * 3.1f)
			{
				vy += (int)((double)((num - num2) / num2) + 0.5);
			}
			vx = textureTimeStamp;
		}
		long textureTimeStamp2 = vn.ek.GetTextureTimeStamp();
		bool flag = !vn.ej.IsSeeking();
		if (vp != vq || vm[0][vq].vl)
		{
			int num3 = (vq + vm[0].Length - 1) % vm[0].Length;
			if (textureTimeStamp2 == vm[0][num3].vk)
			{
				flag = false;
			}
		}
		bool flag2 = vp != vq || !vm[0][vq].vl;
		if (flag)
		{
			if (vp == vq && !vm[0][vq].vl)
			{
				vs = textureTimeStamp2;
			}
			if (vq == vp && vm[0][vq].vl)
			{
				vp = (vp + 1) % vm[0].Length;
			}
			for (int num4 = 0; num4 < vn.ek.GetTextureCount(); num4++)
			{
				Texture texture = vn.ek.GetTexture(num4);
				vm[num4][vq].vj.DiscardContents();
				Graphics.Blit(texture, vm[num4][vq].vj);
				vm[num4][vq].vk = textureTimeStamp2;
				vm[num4][vq].vl = true;
			}
			vq = (vq + 1) % vm[0].Length;
		}
		bool flag3 = vp != vq || !vm[0][vq].vl;
		if (flag3)
		{
			for (int num5 = 0; num5 < vm.Count; num5++)
			{
				vo[num5].DiscardContents();
				Graphics.Blit(vm[num5][vp].vj, vo[num5]);
				wb = vm[num5][vp].vk;
			}
		}
		else if (flag2 && wi <= 0f)
		{
			wi = bjx();
			vt = (float)(vr / 2) / wi;
		}
		if (vn.ej.IsPaused())
		{
			bjw();
		}
		if (!flag3 && vn.ej.IsPlaying() && !vn.ej.IsFinished())
		{
			long num6 = vm[0][(vp + vr / 2) % vr].vk - vs;
			double num7 = Mathf.Abs((float)((double)vt * 10000000.0) - (float)num6);
			double num8 = (float)(vm[0].Length / 2) / wi * 10000000f;
			if (num7 > num8)
			{
				vt = (float)num6 / 10000000f;
			}
			if (vv == b.POINT)
			{
				bjs();
			}
			else if (vv == b.LINEAR)
			{
				bjv();
			}
			vt += Time.unscaledDeltaTime;
		}
	}

	public void bjy()
	{
		if (vz != wb)
		{
			vz = wb;
			wa = 0;
		}
		wa++;
	}
}
