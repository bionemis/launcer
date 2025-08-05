using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
	private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public LoadScene ba;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return z;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return z;
			}
		}

		[DebuggerHidden]
		public a(int a)
		{
			y = a;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = y;
			LoadScene loadScene = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
				y = -1;
				z = new WaitForSeconds(0.2f);
				y = 1;
				return true;
			case 1:
				y = -1;
				loadScene.async = SceneManager.LoadSceneAsync(global::a.i);
				loadScene.async.allowSceneActivation = false;
				goto IL_00b9;
			case 2:
				y = -1;
				goto IL_00ab;
			case 3:
				{
					y = -1;
					break;
				}
				IL_00b9:
				if (loadScene.async.progress < 0.9f)
				{
					loadScene.toProgress = (int)loadScene.async.progress * 100;
					goto IL_00ab;
				}
				loadScene.toProgress = 100;
				break;
				IL_00ab:
				if (loadScene.displayProgress < loadScene.toProgress)
				{
					loadScene.displayProgress++;
					z = new WaitForEndOfFrame();
					y = 2;
					return true;
				}
				goto IL_00b9;
			}
			if (loadScene.displayProgress < loadScene.toProgress)
			{
				loadScene.displayProgress++;
				z = new WaitForEndOfFrame();
				y = 3;
				return true;
			}
			loadScene.async.allowSceneActivation = true;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[SerializeField]
	private Slider m_SdrScene;

	[SerializeField]
	private Text m_TxtScene;

	private AsyncOperation async;

	private int displayProgress;

	private int toProgress;

	private bool load_file_over;

	private void Awake()
	{
		Screen.fullScreen = true;
	}

	private void Start()
	{
		StartCoroutine(de());
	}

	private void Update()
	{
		m_TxtScene.text = displayProgress.ToString();
		m_SdrScene.value = (float)displayProgress / 100f;
	}

	[IteratorStateMachine(typeof(a))]
	private IEnumerator de()
	{
		return new a(0)
		{
			ba = this
		};
	}

	private void OnDestroy()
	{
	}
}
