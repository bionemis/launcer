using UnityEngine;

public class MonoSingle<a> : MonoBehaviour where a : MonoSingle<a>
{
	private static a instance;

	public static a Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindObjectOfType<a>();
				if (instance == null)
				{
					Debug.LogError("On Find T Type:" + typeof(a));
					return null;
				}
			}
			return instance;
		}
	}
}
