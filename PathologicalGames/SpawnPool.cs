using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PathologicalGames
{
	[AddComponentMenu("Path-o-logical/PoolManager/SpawnPool")]
	public sealed class SpawnPool : MonoBehaviour, IList<Transform>, ICollection<Transform>, IEnumerable<Transform>, IEnumerable
	{
		public delegate GameObject a(GameObject a, Vector3 b, Quaternion c);

		public delegate void b(GameObject a);

		private sealed class c : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public Transform kf;

			public float kg;

			public bool kh;

			public SpawnPool ba;

			public Transform ki;

			private GameObject kj;

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
			public c(int a)
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
				SpawnPool spawnPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					kj = kf.gameObject;
					break;
				case 1:
					y = -1;
					if (!kj.activeInHierarchy)
					{
						return false;
					}
					kg -= Time.deltaTime;
					break;
				}
				if (kg > 0f)
				{
					z = null;
					y = 1;
					return true;
				}
				if (kh)
				{
					spawnPool.xq(kf, ki);
				}
				else
				{
					spawnPool.xp(kf);
				}
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

		private sealed class d : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public AudioSource kk;

			public SpawnPool ba;

			private GameObject kl;

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
			public d(int a)
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
				SpawnPool spawnPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					z = null;
					y = 1;
					return true;
				case 1:
					y = -1;
					kl = kk.gameObject;
					break;
				case 2:
					y = -1;
					break;
				}
				if (kk.isPlaying)
				{
					z = null;
					y = 2;
					return true;
				}
				if (!kl.activeInHierarchy)
				{
					kk.Stop();
					return false;
				}
				spawnPool.xp(kk.transform);
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

		private sealed class e : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public ParticleSystem km;

			public SpawnPool ba;

			private float kn;

			private GameObject ko;

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
			public e(int a)
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
				SpawnPool spawnPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					z = new WaitForSeconds(km.main.startDelay.constantMax + 0.25f);
					y = 1;
					return true;
				case 1:
					y = -1;
					kn = 0f;
					ko = km.gameObject;
					break;
				case 2:
					y = -1;
					break;
				}
				if (km.IsAlive(withChildren: true) && ko.activeInHierarchy)
				{
					kn += Time.deltaTime;
					if (kn > spawnPool.maxParticleDespawnTime)
					{
						UnityEngine.Debug.LogWarning($"SpawnPool {spawnPool.poolName}: Timed out while listening for all particles to die. Waited for {spawnPool.maxParticleDespawnTime}sec.");
					}
					z = null;
					y = 2;
					return true;
				}
				if (ko.activeInHierarchy)
				{
					spawnPool.xp(km.transform);
					km.Clear(withChildren: true);
				}
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

		private sealed class f : IEnumerator<Transform>, IEnumerator, IDisposable
		{
			private int y;

			private Transform z;

			public SpawnPool ba;

			private int kp;

			Transform IEnumerator<Transform>.Current
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
			public f(int a)
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
				SpawnPool spawnPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					kp = 0;
					break;
				case 1:
					y = -1;
					kp++;
					break;
				}
				if (kp < spawnPool.ks.Count)
				{
					z = spawnPool.ks[kp];
					y = 1;
					return true;
				}
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

		private sealed class g : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public SpawnPool ba;

			private int kp;

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
			public g(int a)
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
				SpawnPool spawnPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					kp = 0;
					break;
				case 1:
					y = -1;
					kp++;
					break;
				}
				if (kp < spawnPool.ks.Count)
				{
					z = spawnPool.ks[kp];
					y = 1;
					return true;
				}
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

		public string poolName = "";

		public bool matchPoolScale;

		public bool matchPoolLayer;

		public bool dontReparent;

		public bool _dontDestroyOnLoad;

		public bool logMessages;

		public List<PrefabPool> _perPrefabPoolOptions = new List<PrefabPool>();

		public Dictionary<object, bool> prefabsFoldOutStates = new Dictionary<object, bool>();

		public float maxParticleDespawnTime = 300f;

		public cn prefabs = new cn();

		public Dictionary<object, bool> _editorListItemStates = new Dictionary<object, bool>();

		private List<PrefabPool> kr = new List<PrefabPool>();

		internal List<Transform> ks = new List<Transform>();

		public a instantiateDelegates;

		public b destroyDelegates;

		public bool dk
		{
			get
			{
				return _dontDestroyOnLoad;
			}
			set
			{
				_dontDestroyOnLoad = value;
				if (dl != null)
				{
					UnityEngine.Object.DontDestroyOnLoad(dl.gameObject);
				}
			}
		}

		public Transform dl { get; private set; }

		public Dictionary<string, PrefabPool> dm
		{
			get
			{
				Dictionary<string, PrefabPool> dictionary = new Dictionary<string, PrefabPool>();
				for (int i = 0; i < kr.Count; i++)
				{
					dictionary[kr[i].prefabGO.name] = kr[i];
				}
				return dictionary;
			}
		}

		public Transform this[int a]
		{
			get
			{
				return ks[a];
			}
			set
			{
				throw new NotImplementedException("Read-only.");
			}
		}

		public int Count => ks.Count;

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		private void Awake()
		{
			if (_dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			dl = base.transform;
			if (poolName == "")
			{
				poolName = dl.name.Replace("Pool", "");
				poolName = poolName.Replace("(Clone)", "");
			}
			if (logMessages)
			{
				UnityEngine.Debug.Log($"SpawnPool {poolName}: Initializing..");
			}
			for (int i = 0; i < _perPrefabPoolOptions.Count; i++)
			{
				if (_perPrefabPoolOptions[i].prefab == null)
				{
					UnityEngine.Debug.LogWarning($"Initialization Warning: Pool '{poolName}' contains a PrefabPool with no prefab reference. Skipping.");
					continue;
				}
				_perPrefabPoolOptions[i].yj();
				wu(_perPrefabPoolOptions[i]);
			}
			ck.ka.wb(this);
		}

		internal GameObject ws(GameObject a, Vector3 b, Quaternion c, object d = null)
		{
			if (instantiateDelegates != null)
			{
				return instantiateDelegates(a, b, c);
			}
			return cl.vs(a, b, c, d);
		}

		internal void wt(GameObject a)
		{
			if (destroyDelegates != null)
			{
				destroyDelegates(a);
			}
			else
			{
				cl.vt(a);
			}
		}

		private void OnDestroy()
		{
			if (logMessages)
			{
				UnityEngine.Debug.Log($"SpawnPool {poolName}: Destroying...");
			}
			if (ck.ka.wd(this))
			{
				ck.ka.wc(this);
			}
			StopAllCoroutines();
			ks.Clear();
			foreach (PrefabPool item in kr)
			{
				item.yk();
			}
			kr.Clear();
			prefabs.zf();
		}

		public void wu(PrefabPool a, object b = null)
		{
			if ((xw(a.prefab) != null) ? true : false)
			{
				throw new Exception($"Prefab '{a.prefab}' is already in  SpawnPool '{poolName}'. Prefabs can be in more than 1 SpawnPool but cannot be in the same SpawnPool twice.");
			}
			a.spawnPool = this;
			kr.Add(a);
			prefabs.zd(a.prefab.name, a.prefab);
			if (!a.dr)
			{
				if (logMessages)
				{
					UnityEngine.Debug.Log($"SpawnPool {poolName}: Preloading {a.preloadAmount} {a.prefab.name}");
				}
				a.yz(b);
			}
		}

		public void wv(Transform a, string b, bool c, bool d)
		{
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError("Unexpected Error: PrefabPool.prefabGO is null");
					return;
				}
				if (kr[i].prefabGO.name == b)
				{
					kr[i].yy(a, c);
					if (logMessages)
					{
						UnityEngine.Debug.Log($"SpawnPool {poolName}: Adding previously unpooled instance {a.name}");
					}
					if (d)
					{
						bool worldPositionStays = !(a is RectTransform);
						a.SetParent(dl, worldPositionStays);
					}
					if (!c)
					{
						ks.Add(a);
					}
					return;
				}
			}
			UnityEngine.Debug.LogError($"SpawnPool {poolName}: PrefabPool {b} not found.");
		}

		public void Add(Transform a)
		{
			throw new NotImplementedException("Use SpawnPool.Spawn() to properly add items to the pool.");
		}

		public void ww(Transform a)
		{
			throw new NotImplementedException("Use Despawn() to properly manage items that should remain in the pool but be deactivated.");
		}

		public Transform wx(Transform a, Vector3 b, Quaternion c, Transform d, object e = null)
		{
			Transform transform;
			bool worldPositionStays;
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].prefabGO == a.gameObject)
				{
					transform = kr[i].yu(b, c, e);
					if (transform == null)
					{
						return null;
					}
					worldPositionStays = !(transform is RectTransform);
					if (d != null)
					{
						transform.SetParent(d, worldPositionStays);
					}
					else if (!dontReparent && transform.parent != dl)
					{
						transform.SetParent(dl, worldPositionStays);
					}
					ks.Add(transform);
					transform.gameObject.BroadcastMessage("OnSpawned", this, SendMessageOptions.DontRequireReceiver);
					return transform;
				}
			}
			PrefabPool prefabPool = new PrefabPool(a);
			wu(prefabPool, e);
			transform = prefabPool.yu(b, c, e);
			worldPositionStays = !(transform is RectTransform);
			if (d != null)
			{
				transform.SetParent(d, worldPositionStays);
			}
			else if (!dontReparent && transform.parent != dl)
			{
				transform.SetParent(dl, worldPositionStays);
			}
			ks.Add(transform);
			transform.gameObject.BroadcastMessage("OnSpawned", this, SendMessageOptions.DontRequireReceiver);
			return transform;
		}

		public Transform wy(Transform a, Vector3 b, Quaternion c, object d = null)
		{
			Transform transform = wx(a, b, c, null, d);
			if (transform == null)
			{
				return null;
			}
			return transform;
		}

		public Transform wz(Transform a, object b = null)
		{
			return wy(a, Vector3.zero, Quaternion.identity, b);
		}

		public Transform xa(Transform a, Transform b)
		{
			return wx(a, Vector3.zero, Quaternion.identity, b);
		}

		public Transform xb(GameObject a, Vector3 b, Quaternion c, Transform d)
		{
			return wx(a.transform, b, c, d);
		}

		public Transform xc(GameObject a, Vector3 b, Quaternion c)
		{
			return wy(a.transform, b, c);
		}

		public Transform xd(GameObject a)
		{
			return wz(a.transform);
		}

		public Transform xe(GameObject a, Transform b)
		{
			return xa(a.transform, b);
		}

		public Transform xf(string a)
		{
			Transform a2 = prefabs[a];
			return wz(a2);
		}

		public Transform xg(string a, Transform b)
		{
			Transform a2 = prefabs[a];
			return xa(a2, b);
		}

		public Transform xh(string a, Vector3 b, Quaternion c)
		{
			Transform a2 = prefabs[a];
			return wy(a2, b, c);
		}

		public Transform xi(string a, Vector3 b, Quaternion c, Transform d)
		{
			Transform a2 = prefabs[a];
			return wx(a2, b, c, d);
		}

		public AudioSource xj(AudioSource a, Vector3 b, Quaternion c)
		{
			return xm(a, b, c, null);
		}

		public AudioSource xk(AudioSource a)
		{
			return xm(a, Vector3.zero, Quaternion.identity, null);
		}

		public AudioSource xl(AudioSource a, Transform b)
		{
			return xm(a, Vector3.zero, Quaternion.identity, b);
		}

		public AudioSource xm(AudioSource a, Vector3 b, Quaternion c, Transform d)
		{
			Transform transform = wx(a.transform, b, c, d);
			if (transform == null)
			{
				return null;
			}
			AudioSource component = transform.GetComponent<AudioSource>();
			component.Play();
			StartCoroutine(yd(component));
			return component;
		}

		public ParticleSystem xn(ParticleSystem a, Vector3 b, Quaternion c)
		{
			return xo(a, b, c, null);
		}

		public ParticleSystem xo(ParticleSystem a, Vector3 b, Quaternion c, Transform d)
		{
			Transform transform = wx(a.transform, b, c, d);
			if (transform == null)
			{
				return null;
			}
			ParticleSystem component = transform.GetComponent<ParticleSystem>();
			StartCoroutine(ye(component));
			return component;
		}

		public void xp(Transform a)
		{
			bool flag = false;
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i]._spawned.Contains(a))
				{
					flag = kr[i].yq(a);
					break;
				}
				if (kr[i]._despawned.Contains(a))
				{
					UnityEngine.Debug.LogError($"SpawnPool {poolName}: {a.name} has already been despawned. You cannot despawn something more than once!");
					return;
				}
			}
			if (!flag)
			{
				UnityEngine.Debug.LogError($"SpawnPool {poolName}: {a.name} not found in SpawnPool");
			}
			else
			{
				ks.Remove(a);
			}
		}

		public void xq(Transform a, Transform b)
		{
			bool worldPositionStays = !(a is RectTransform);
			a.SetParent(b, worldPositionStays);
			xp(a);
		}

		public void xr(Transform a, float b)
		{
			StartCoroutine(xt(a, b, c: false, null));
		}

		public void xs(Transform a, float b, Transform c)
		{
			StartCoroutine(xt(a, b, c: true, c));
		}

		[IteratorStateMachine(typeof(c))]
		private IEnumerator xt(Transform a, float b, bool c, Transform d)
		{
			return new c(0)
			{
				ba = this,
				kf = a,
				kg = b,
				kh = c,
				ki = d
			};
		}

		public void xu()
		{
			List<Transform> list = new List<Transform>(ks);
			for (int i = 0; i < list.Count; i++)
			{
				xp(list[i]);
			}
		}

		public bool xv(Transform a)
		{
			return ks.Contains(a);
		}

		public PrefabPool xw(Transform a)
		{
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError($"SpawnPool {poolName}: PrefabPool.prefabGO is null");
				}
				if (kr[i].prefabGO == a.gameObject)
				{
					return kr[i];
				}
			}
			return null;
		}

		public PrefabPool xx(int a)
		{
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].PrefabPoolId == 0)
				{
					UnityEngine.Debug.LogError($"SpawnPool {poolName}: PrefabPool PrefabPoolId is 0");
				}
				if (kr[i].PrefabPoolId == a)
				{
					return kr[i];
				}
			}
			return null;
		}

		public void xy(PrefabPool a)
		{
			kr.Remove(a);
		}

		public void xz(PrefabPool a)
		{
			kr.Add(a);
		}

		public PrefabPool ya(GameObject a)
		{
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError($"SpawnPool {poolName}: PrefabPool.prefabGO is null");
				}
				if (kr[i].prefabGO == a)
				{
					return kr[i];
				}
			}
			return null;
		}

		public Transform yb(Transform a)
		{
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].zb(a))
				{
					return kr[i].prefab;
				}
			}
			return null;
		}

		public GameObject yc(GameObject a)
		{
			for (int i = 0; i < kr.Count; i++)
			{
				if (kr[i].zb(a.transform))
				{
					return kr[i].prefabGO;
				}
			}
			return null;
		}

		[IteratorStateMachine(typeof(d))]
		private IEnumerator yd(AudioSource a)
		{
			return new d(0)
			{
				ba = this,
				kk = a
			};
		}

		[IteratorStateMachine(typeof(e))]
		private IEnumerator ye(ParticleSystem a)
		{
			return new e(0)
			{
				ba = this,
				km = a
			};
		}

		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (Transform k in ks)
			{
				list.Add(k.name);
			}
			return string.Join(", ", list.ToArray());
		}

		public bool Contains(Transform a)
		{
			throw new NotImplementedException("Use IsSpawned(Transform instance) instead.");
		}

		public void CopyTo(Transform[] a, int b)
		{
			ks.CopyTo(a, b);
		}

		[IteratorStateMachine(typeof(f))]
		public IEnumerator<Transform> GetEnumerator()
		{
			return new f(0)
			{
				ba = this
			};
		}

		[IteratorStateMachine(typeof(g))]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new g(0)
			{
				ba = this
			};
		}

		public int IndexOf(Transform a)
		{
			throw new NotImplementedException();
		}

		public void Insert(int a, Transform b)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int a)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		private bool yf(Transform a)
		{
			throw new NotImplementedException();
		}

		bool ICollection<Transform>.Remove(Transform a)
		{
			//ILSpy generated this explicit interface implementation from .override directive in yf
			return this.yf(a);
		}
	}
}
