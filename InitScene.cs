using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
	private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int y;

		private object z;

		public InitScene ba;

		private string bb;

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
			InitScene initScene = ba;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				y = -1;
				bb = "";
				bool flag = false;
				try
				{
					initScene.xmlDoc = new XmlDocument();
					initScene.xmlDoc.Load(h.k);
					initScene.nodeList = initScene.xmlDoc.SelectSingleNode("Content").ChildNodes;
					for (int num2 = 0; num2 < initScene.nodeList.Count; num2++)
					{
						if (initScene.nodeList[num2].Name.Equals("IsBig"))
						{
							flag = true;
							if (bool.Parse(initScene.nodeList[num2].InnerText))
							{
								Screen.SetResolution(1080, 1920, fullscreen: true);
								global::a.i = "Scenes/Big/Main";
								bb = "Scenes/Big/Start";
							}
							else
							{
								Screen.SetResolution(1280, 1024, fullscreen: true);
								global::a.i = "Scenes/Little/Main";
								bb = "Scenes/Little/Start";
							}
						}
					}
				}
				catch (Exception ex)
				{
					k.ca("Xml:" + ex.Message);
					global::a.k();
				}
				if (!flag)
				{
					k.ca("Setting文件夹，没有配置 IsBig 键值");
					global::a.k();
					return false;
				}
				z = new WaitForEndOfFrame();
				y = 1;
				return true;
			}
			case 1:
				y = -1;
				SceneManager.LoadScene(bb);
				return false;
			}
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

	private XmlDocument xmlDoc;

	private XmlNodeList nodeList;

	[SerializeField]
	private string version;

	private void Start()
	{
		global::a.j = version;
		StartCoroutine(dc());
	}

	[IteratorStateMachine(typeof(a))]
	public IEnumerator dc()
	{
		return new a(0)
		{
			ba = this
		};
	}
}
