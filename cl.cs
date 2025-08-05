using UnityEngine;

public static class cl
{
	public delegate GameObject a(GameObject a, Vector3 b, Quaternion c, object d = null);

	public delegate void b(GameObject a);

	public static a kb;

	public static b kc;

	internal static GameObject vs(GameObject a, Vector3 b, Quaternion c, object d = null)
	{
		if (kb != null)
		{
			return kb(a, b, c, d);
		}
		return Object.Instantiate(a, b, c);
	}

	internal static void vt(GameObject a)
	{
		if (kc != null)
		{
			kc(a);
		}
		else
		{
			Object.Destroy(a);
		}
	}
}
