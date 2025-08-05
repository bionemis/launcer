using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class dl
{
	public const string uv = "1.11.7";

	public static string big(cw a)
	{
		string text = "Unknown";
		switch (a)
		{
		case cw.WindowsPhone:
			return "Windows Phone";
		case cw.WindowsUWP:
			return "Windows UWP";
		case cw.MacOSX:
			return "macOS";
		default:
			return a.ToString();
		}
	}

	public static string bih(da a)
	{
		string text = string.Empty;
		switch (a)
		{
		case da.None:
			text = "No Error";
			break;
		case da.LoadFailed:
			text = "Loading failed.  File not found, codec not supported, video resolution too high or insufficient system resources.";
			if (SystemInfo.operatingSystem.StartsWith("Windows XP") || SystemInfo.operatingSystem.StartsWith("Windows Vista"))
			{
				text += " NOTE: Windows XP and Vista don't have native support for H.264 codec.  Consider using an older codec such as DivX or installing 3rd party codecs such as LAV Filters.";
			}
			break;
		case da.DecodeFailed:
			text = "Decode failed.  Possible codec not supported, video resolution/bit-depth too high, or insufficient system resources.";
			break;
		}
		return text;
	}

	public static string[] bii()
	{
		return new string[9]
		{
			big(cw.Windows),
			big(cw.MacOSX),
			big(cw.iOS),
			big(cw.tvOS),
			big(cw.Android),
			big(cw.WindowsPhone),
			big(cw.WindowsUWP),
			big(cw.WebGL),
			big(cw.PS4)
		};
	}

	public static void bij(string a, UnityEngine.Object b = null)
	{
		if (b == null)
		{
			Debug.LogWarning("[AVProVideo] " + a);
		}
		else
		{
			Debug.LogWarning("[AVProVideo] " + a, b);
		}
	}

	public static string bik(float a, bool b = false)
	{
		int num = Mathf.FloorToInt(a / 3600f);
		float num2 = (float)num * 60f * 60f;
		int num3 = Mathf.FloorToInt((a - num2) / 60f);
		num2 += (float)num3 * 60f;
		int num4 = Mathf.FloorToInt(a - num2);
		if (num <= 0)
		{
			if (b)
			{
				int num5 = (int)((a - Mathf.Floor(a)) * 1000f);
				return $"{num3:00}:{num4:00}:{num5:000}";
			}
			return $"{num3:00}:{num4:00}";
		}
		if (b)
		{
			int num6 = (int)((a - Mathf.Floor(a)) * 1000f);
			return string.Format("{2}:{0:00}:{1:00}:{3:000}", num3, num4, num, num6);
		}
		return string.Format("{2}:{0:00}:{1:00}", num3, num4, num);
	}

	public static db bil(float[] a)
	{
		db result = db.Landscape;
		if (a != null)
		{
			if (a[0] == 0f && a[1] == 1f && a[2] == -1f && a[3] == 0f)
			{
				result = db.Portrait;
			}
			else if (a[0] == 0f && a[1] == -1f && a[2] == 1f && a[3] == 0f)
			{
				result = db.PortraitFlipped;
			}
			else if (a[0] == 1f && a[1] == 0f && a[2] == 0f && a[3] == 1f)
			{
				result = db.Landscape;
			}
			else if (a[0] == -1f && a[1] == 0f && a[2] == 0f && a[3] == -1f)
			{
				result = db.LandscapeFlipped;
			}
		}
		return result;
	}

	public static Matrix4x4 bim(db a)
	{
		Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(0f, 1f, 0f), Quaternion.Euler(0f, 0f, -90f), Vector3.one);
		Matrix4x4 matrix4x2 = Matrix4x4.TRS(new Vector3(1f, 0f, 0f), Quaternion.Euler(0f, 0f, 90f), Vector3.one);
		Matrix4x4 matrix4x3 = Matrix4x4.TRS(new Vector3(1f, 1f, 0f), Quaternion.identity, new Vector3(-1f, -1f, 1f));
		Matrix4x4 result = Matrix4x4.identity;
		switch (a)
		{
		case db.LandscapeFlipped:
			result = matrix4x3;
			break;
		case db.Portrait:
			result = matrix4x;
			break;
		case db.PortraitFlipped:
			result = matrix4x2;
			break;
		}
		return result;
	}

	public static void bin(Material a, cy b)
	{
		switch (b)
		{
		case cy.Both:
			a.DisableKeyword("FORCEEYE_LEFT");
			a.DisableKeyword("FORCEEYE_RIGHT");
			a.EnableKeyword("FORCEEYE_NONE");
			break;
		case cy.Left:
			a.DisableKeyword("FORCEEYE_NONE");
			a.DisableKeyword("FORCEEYE_RIGHT");
			a.EnableKeyword("FORCEEYE_LEFT");
			break;
		case cy.Right:
			a.DisableKeyword("FORCEEYE_NONE");
			a.DisableKeyword("FORCEEYE_LEFT");
			a.EnableKeyword("FORCEEYE_RIGHT");
			break;
		}
	}

	public static void bio(Material a, dc b)
	{
		a.DisableKeyword("LAYOUT_NONE");
		a.DisableKeyword("LAYOUT_EQUIRECT180");
		if (b == dc.EquiRectangular180)
		{
			a.EnableKeyword("LAYOUT_EQUIRECT180");
		}
	}

	public static void bip(Material a, cx b, bool c)
	{
		a.DisableKeyword("STEREO_CUSTOM_UV");
		a.DisableKeyword("STEREO_TOP_BOTTOM");
		a.DisableKeyword("STEREO_LEFT_RIGHT");
		a.DisableKeyword("MONOSCOPIC");
		switch (b)
		{
		case cx.TopBottom:
			a.EnableKeyword("STEREO_TOP_BOTTOM");
			break;
		case cx.LeftRight:
			a.EnableKeyword("STEREO_LEFT_RIGHT");
			break;
		case cx.CustomUV:
			a.EnableKeyword("STEREO_CUSTOM_UV");
			break;
		}
		if (c)
		{
			a.EnableKeyword("STEREO_DEBUG");
		}
		else
		{
			a.DisableKeyword("STEREO_DEBUG");
		}
	}

	public static void biq(Material a, cz b)
	{
		a.DisableKeyword("ALPHAPACK_TOP_BOTTOM");
		a.DisableKeyword("ALPHAPACK_LEFT_RIGHT");
		a.DisableKeyword("ALPHAPACK_NONE");
		switch (b)
		{
		case cz.TopBottom:
			a.EnableKeyword("ALPHAPACK_TOP_BOTTOM");
			break;
		case cz.LeftRight:
			a.EnableKeyword("ALPHAPACK_LEFT_RIGHT");
			break;
		case cz.None:
			break;
		}
	}

	public static void bir(Material a, bool b)
	{
		if (QualitySettings.activeColorSpace == ColorSpace.Linear && !b)
		{
			a.EnableKeyword("APPLY_GAMMA");
		}
		else
		{
			a.DisableKeyword("APPLY_GAMMA");
		}
	}

	public static int bis(float a, float b)
	{
		a = Mathf.Max(0f, a);
		b = Mathf.Max(0f, b);
		return Mathf.FloorToInt(b * a);
	}

	public static float bit(int a, float b)
	{
		a = Mathf.Max(0, a);
		b = Mathf.Max(0f, b);
		float num = 1f / b;
		return (float)a * num + num * 0.5f;
	}

	public static float biu(float a, float b, int c)
	{
		a = Mathf.Max(0f, a);
		b = Mathf.Max(0f, b);
		c = Mathf.Max(0, c);
		int num = bis(a, b);
		return bit(c * Mathf.CeilToInt((float)(num + 1) / (float)c), b);
	}

	public static DateTime biv(double a)
	{
		TimeSpan value = TimeSpan.FromSeconds(a);
		return new DateTime(1970, 1, 1).Add(value);
	}

	public static void biw(Rect a, Texture b, ScaleMode c, cz d, Material e)
	{
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		float num = b.width;
		float num2 = b.height;
		switch (d)
		{
		case cz.LeftRight:
			num *= 0.5f;
			break;
		case cz.TopBottom:
			num2 *= 0.5f;
			break;
		}
		float num3 = num / num2;
		Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
		switch (c)
		{
		case ScaleMode.ScaleAndCrop:
		{
			float num7 = a.width / a.height;
			if (num7 > num3)
			{
				float num8 = num3 / num7;
				sourceRect = new Rect(0f, (1f - num8) * 0.5f, 1f, num8);
			}
			else
			{
				float num9 = num7 / num3;
				sourceRect = new Rect(0.5f - num9 * 0.5f, 0f, num9, 1f);
			}
			break;
		}
		case ScaleMode.ScaleToFit:
		{
			float num4 = a.width / a.height;
			if (num4 > num3)
			{
				float num5 = num3 / num4;
				a = new Rect(a.xMin + a.width * (1f - num5) * 0.5f, a.yMin, num5 * a.width, a.height);
			}
			else
			{
				float num6 = num4 / num3;
				a = new Rect(a.xMin, a.yMin + a.height * (1f - num6) * 0.5f, a.width, num6 * a.height);
			}
			break;
		}
		}
		Graphics.DrawTexture(a, b, sourceRect, 0, 0, 0, 0, GUI.color, e);
	}

	public static Texture2D bix(Texture a, bool b, db c, Texture2D d)
	{
		Texture2D texture2D = d;
		RenderTexture active = RenderTexture.active;
		int width = a.width;
		int height = a.height;
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
		if (c == db.Landscape)
		{
			if (!b)
			{
				Graphics.Blit(a, temporary);
			}
			else
			{
				GL.PushMatrix();
				RenderTexture.active = temporary;
				GL.LoadPixelMatrix(0f, temporary.width, 0f, temporary.height);
				Graphics.DrawTexture(sourceRect: new Rect(0f, 0f, 1f, 1f), screenRect: new Rect(0f, -1f, temporary.width, temporary.height), texture: a, leftBorder: 0, rightBorder: 0, topBorder: 0, bottomBorder: 0);
				GL.PopMatrix();
				GL.InvalidateState();
			}
		}
		if (texture2D == null)
		{
			texture2D = new Texture2D(width, height, TextureFormat.ARGB32, mipChain: false);
		}
		RenderTexture.active = temporary;
		texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0, recalculateMipMaps: false);
		texture2D.Apply(updateMipmaps: false, makeNoLongerReadable: false);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.active = active;
		return texture2D;
	}

	private static int biy(string a)
	{
		int result = 0;
		string[] array = a.Split(':', ',');
		if (array.Length == 4)
		{
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			int num3 = int.Parse(array[2]);
			result = int.Parse(array[3]) + (num3 + (num2 + num * 60) * 60) * 1000;
		}
		return result;
	}

	public static List<dk> biz(string a)
	{
		List<dk> list = null;
		if (!string.IsNullOrEmpty(a))
		{
			a = a.Trim();
			string[] array = new Regex("\n\r|\r\n|\n|\r").Split(a);
			if (array.Length >= 3)
			{
				list = new List<dk>(256);
				int num = 0;
				int num2 = 0;
				dk dk2 = null;
				for (int num3 = 0; num3 < array.Length; num3++)
				{
					switch (num2)
					{
					case 0:
						dk2 = new dk();
						dk2.ur = num;
						break;
					case 1:
					{
						string[] array2 = array[num3].Split(new string[1] { " --> " }, StringSplitOptions.RemoveEmptyEntries);
						if (array2.Length == 2)
						{
							dk2.ut = biy(array2[0]);
							dk2.uu = biy(array2[1]);
						}
						break;
					}
					default:
						if (!string.IsNullOrEmpty(array[num3]))
						{
							if (num2 == 2)
							{
								dk2.us = array[num3];
								break;
							}
							dk obj = dk2;
							obj.us = obj.us + "\n" + array[num3];
						}
						break;
					}
					if (string.IsNullOrEmpty(array[num3]) && num2 > 1)
					{
						list.Add(dk2);
						num2 = 0;
						num++;
						dk2 = null;
					}
					else
					{
						num2++;
					}
				}
				if (dk2 != null)
				{
					list.Add(dk2);
					dk2 = null;
				}
			}
			else
			{
				Debug.LogWarning("[AVProVideo] SRT format doesn't appear to be valid");
			}
		}
		return list;
	}
}
