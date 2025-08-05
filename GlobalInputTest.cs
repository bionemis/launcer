using UnityEngine;

public class GlobalInputTest : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (GInput.g(y.A))
		{
			Debug.Log("按下A");
		}
		if (GInput.g(y.A))
		{
			Debug.Log("按下A2");
		}
		if (GInput.b)
		{
			Debug.Log("按下按钮");
		}
		if (GInput.b)
		{
			Debug.Log("按下按钮2");
		}
	}
}
