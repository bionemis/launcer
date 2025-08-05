using UnityEngine;
using UnityEngine.UI;

public class PlayModeBG : MonoBehaviour
{
	private Material jx;

	private float jy = -0.2f;

	private float jz = 1.5f;

	private void Awake()
	{
		jx = base.transform.GetComponent<Image>().material;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (jy <= 1.2f)
		{
			jy += Time.deltaTime * jz;
			Vector4 vector = jx.GetVector("_Offset");
			vector.w = jy;
			jx.SetVector("_Offset", vector);
		}
	}

	private void OnEnable()
	{
		jy = -0.2f;
	}
}
