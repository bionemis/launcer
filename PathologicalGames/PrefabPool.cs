using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PathologicalGames
{
	[Serializable]
	public class PrefabPool
	{
		private sealed class a : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public PrefabPool ba;

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
				PrefabPool prefabPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					if (prefabPool.dn)
					{
						UnityEngine.Debug.Log($"SpawnPool {prefabPool.spawnPool.poolName} ({prefabPool.prefab.name}): CULLING TRIGGERED! Waiting {prefabPool.cullDelay}sec to begin checking for despawns...");
					}
					z = new WaitForSeconds(prefabPool.cullDelay);
					y = 1;
					return true;
				case 1:
					y = -1;
					goto IL_01a5;
				case 2:
					y = -1;
					goto IL_01a5;
				case 3:
					{
						y = -1;
						if (prefabPool.dq == 0)
						{
							prefabPool.spawnPool.prefabs.ze(prefabPool.prefab.name);
							prefabPool.prefab = null;
							prefabPool.prefabGO = null;
							if (prefabPool.OnPrefabPoolClear != null)
							{
								prefabPool.OnPrefabPoolClear(prefabPool);
							}
						}
						return false;
					}
					IL_01a5:
					if (prefabPool.dq > prefabPool.cullAbove)
					{
						for (int i = 0; i < prefabPool.cullMaxPerPass; i++)
						{
							if (prefabPool.dq <= prefabPool.cullAbove)
							{
								break;
							}
							if (prefabPool._despawned.Count > 0)
							{
								Transform transform = prefabPool._despawned[0];
								prefabPool._despawned.RemoveAt(0);
								prefabPool.spawnPool.wt(transform.gameObject);
								if (prefabPool.dn)
								{
									UnityEngine.Debug.Log($"SpawnPool {prefabPool.spawnPool.poolName} ({prefabPool.prefab.name}): CULLING to {prefabPool.cullAbove} instances. Now at {prefabPool.dq}.");
								}
							}
							else if (prefabPool.dn)
							{
								UnityEngine.Debug.Log($"SpawnPool {prefabPool.spawnPool.poolName} ({prefabPool.prefab.name}): CULLING waiting for despawn. Checking again in {prefabPool.cullDelay}sec");
								break;
							}
						}
						z = new WaitForSeconds(prefabPool.cullDelay);
						y = 2;
						return true;
					}
					if (prefabPool.dn)
					{
						UnityEngine.Debug.Log($"SpawnPool {prefabPool.spawnPool.poolName} ({prefabPool.prefab.name}): CULLING FINISHED! Stopping");
					}
					prefabPool.cullingActive = false;
					z = null;
					y = 3;
					return true;
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

		private sealed class b : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int y;

			private object z;

			public PrefabPool ba;

			public object kt;

			private int ku;

			private int kv;

			private int kw;

			private int kx;

			private int ky;

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
			public b(int a)
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
				PrefabPool prefabPool = ba;
				switch (num)
				{
				default:
					return false;
				case 0:
					y = -1;
					z = new WaitForSeconds(prefabPool.preloadDelay);
					y = 1;
					return true;
				case 1:
				{
					y = -1;
					int num2 = prefabPool.preloadAmount - prefabPool.dq;
					if (num2 <= 0)
					{
						return false;
					}
					ku = num2 % prefabPool.preloadFrames;
					kv = num2 / prefabPool.preloadFrames;
					prefabPool.forceLoggingSilent = true;
					kx = 0;
					goto IL_013b;
				}
				case 2:
					{
						y = -1;
						ky++;
						goto IL_010d;
					}
					IL_013b:
					if (kx >= prefabPool.preloadFrames)
					{
						break;
					}
					kw = kv;
					if (kx == prefabPool.preloadFrames - 1)
					{
						kw += ku;
					}
					ky = 0;
					goto IL_010d;
					IL_010d:
					if (ky < kw)
					{
						Transform transform = prefabPool.yv(kt);
						if (transform != null)
						{
							prefabPool.yr(transform, b: false);
						}
						z = null;
						y = 2;
						return true;
					}
					if (prefabPool.dq > prefabPool.preloadAmount)
					{
						break;
					}
					kx++;
					goto IL_013b;
				}
				prefabPool.forceLoggingSilent = false;
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

		public int PrefabPoolId;

		public Action<PrefabPool> OnPrefabPoolClear;

		public Transform prefab;

		public GameObject prefabGO;

		public int preloadAmount = 1;

		public bool preloadTime;

		public int preloadFrames = 2;

		public float preloadDelay;

		public bool limitInstances;

		public int limitAmount = 100;

		public bool limitFIFO;

		public bool cullDespawned;

		public int cullAbove = 50;

		public int cullDelay = 60;

		public int cullMaxPerPass = 5;

		public bool _logMessages;

		private bool forceLoggingSilent;

		public SpawnPool spawnPool;

		private bool cullingActive;

		internal List<Transform> _spawned = new List<Transform>();

		internal List<Transform> _despawned = new List<Transform>();

		private bool _preloaded;

		public bool dn
		{
			get
			{
				if (forceLoggingSilent)
				{
					return false;
				}
				if (spawnPool.logMessages)
				{
					return spawnPool.logMessages;
				}
				return _logMessages;
			}
		}

		public List<Transform> @do => new List<Transform>(_spawned);

		public List<Transform> dp => new List<Transform>(_despawned);

		public int dq => 0 + _spawned.Count + _despawned.Count;

		internal bool dr
		{
			get
			{
				return _preloaded;
			}
			private set
			{
				_preloaded = value;
			}
		}

		public PrefabPool(Transform a, int b = 0)
		{
			PrefabPoolId = b;
			prefab = a;
			prefabGO = a.gameObject;
		}

		public PrefabPool()
		{
		}

		internal void yj()
		{
			prefabGO = prefab.gameObject;
			_spawned = new List<Transform>();
			_despawned = new List<Transform>();
		}

		internal void yk()
		{
			if (dn)
			{
				UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName}: Cleaning up PrefabPool for {prefabGO.name}...");
			}
			foreach (Transform item in _despawned)
			{
				if (item != null && spawnPool != null)
				{
					spawnPool.wt(item.gameObject);
				}
			}
			foreach (Transform item2 in _spawned)
			{
				if (item2 != null && spawnPool != null)
				{
					spawnPool.wt(item2.gameObject);
				}
			}
			_spawned.Clear();
			_despawned.Clear();
			prefab = null;
			prefabGO = null;
			spawnPool = null;
		}

		internal bool yq(Transform a)
		{
			return yr(a, b: true);
		}

		internal bool yr(Transform a, bool b)
		{
			if (dn)
			{
				UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName} ({prefab.name}): Despawning '{a.name}'");
			}
			_spawned.Remove(a);
			_despawned.Add(a);
			if (b)
			{
				a.gameObject.BroadcastMessage("OnDespawned", spawnPool, SendMessageOptions.DontRequireReceiver);
			}
			a.gameObject.SetActive(value: false);
			if (!cullingActive && cullDespawned && dq > cullAbove)
			{
				cullingActive = true;
				spawnPool.StartCoroutine(ys());
			}
			return true;
		}

		[IteratorStateMachine(typeof(a))]
		internal IEnumerator ys()
		{
			return new a(0)
			{
				ba = this
			};
		}

		public void yt(string a, Transform b)
		{
			spawnPool.prefabs.zd(a, b);
		}

		internal Transform yu(Vector3 a, Quaternion b, object c = null)
		{
			if (limitInstances && limitFIFO && _spawned.Count >= limitAmount)
			{
				Transform transform = _spawned[0];
				if (dn)
				{
					UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName} ({prefab.name}): LIMIT REACHED! FIFO=True. Calling despawning for {transform}...");
				}
				yq(transform);
				spawnPool.ks.Remove(transform);
			}
			Transform transform2;
			if (_despawned.Count == 0)
			{
				transform2 = yw(a, b, c);
			}
			else
			{
				transform2 = _despawned[0];
				_despawned.RemoveAt(0);
				_spawned.Add(transform2);
				if (transform2 == null)
				{
					throw new MissingReferenceException("Make sure you didn't delete a despawned instance directly.");
				}
				if (dn)
				{
					UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName} ({prefab.name}): respawning '{transform2.name}'.");
				}
				transform2.position = a;
				transform2.rotation = b;
				transform2.gameObject.SetActive(value: true);
			}
			return transform2;
		}

		public Transform yv(object a)
		{
			return yw(Vector3.zero, Quaternion.identity, a);
		}

		public Transform yw(Vector3 a, Quaternion b, object c = null)
		{
			if (limitInstances && dq >= limitAmount)
			{
				if (dn)
				{
					UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName} ({prefab.name}): LIMIT REACHED! Not creating new instances! (Returning null)");
				}
				return null;
			}
			if (a == Vector3.zero)
			{
				a = spawnPool.dl.position;
			}
			if (b == Quaternion.identity)
			{
				b = spawnPool.dl.rotation;
			}
			Transform transform = spawnPool.ws(prefabGO, a, b, c).transform;
			zc(transform);
			if (!spawnPool.dontReparent)
			{
				bool worldPositionStays = !(transform is RectTransform);
				transform.SetParent(spawnPool.dl, worldPositionStays);
			}
			if (spawnPool.matchPoolScale)
			{
				transform.localScale = Vector3.one;
			}
			if (spawnPool.matchPoolLayer)
			{
				yx(transform, spawnPool.gameObject.layer);
			}
			_spawned.Add(transform);
			if (dn)
			{
				UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName} ({prefab.name}): Spawned new instance '{transform.name}'.");
			}
			return transform;
		}

		private void yx(Transform a, int b)
		{
			a.gameObject.layer = b;
			foreach (Transform item in a)
			{
				yx(item, b);
			}
		}

		internal void yy(Transform a, bool b)
		{
			zc(a);
			if (b)
			{
				a.gameObject.SetActive(value: false);
				_despawned.Add(a);
			}
			else
			{
				_spawned.Add(a);
			}
		}

		internal void yz(object a)
		{
			if (dr)
			{
				UnityEngine.Debug.Log($"SpawnPool {spawnPool.poolName} ({prefab.name}): Already preloaded! You cannot preload twice. If you are running this through code, make sure it isn't also defined in the Inspector.");
				return;
			}
			dr = true;
			if (prefab == null)
			{
				UnityEngine.Debug.LogError($"SpawnPool {spawnPool.poolName} ({prefab.name}): Prefab cannot be null.");
				return;
			}
			if (limitInstances && preloadAmount > limitAmount)
			{
				UnityEngine.Debug.LogWarning($"SpawnPool {spawnPool.poolName} ({prefab.name}): You turned ON 'Limit Instances' and entered a 'Limit Amount' greater than the 'Preload Amount'! Setting preload amount to limit amount.");
				preloadAmount = limitAmount;
			}
			if (cullDespawned && preloadAmount > cullAbove)
			{
				UnityEngine.Debug.LogWarning($"SpawnPool {spawnPool.poolName} ({prefab.name}): You turned ON Culling and entered a 'Cull Above' threshold greater than the 'Preload Amount'! This will cause the culling feature to trigger immediatly, which is wrong conceptually. Only use culling for extreme situations. See the docs.");
			}
			if (preloadTime)
			{
				if (preloadFrames > preloadAmount)
				{
					UnityEngine.Debug.LogWarning($"SpawnPool {spawnPool.poolName} ({prefab.name}): Preloading over-time is on but the frame duration is greater than the number of instances to preload. The minimum spawned per frame is 1, so the maximum time is the same as the number of instances. Changing the preloadFrames value...");
					preloadFrames = preloadAmount;
				}
				spawnPool.StartCoroutine(za(a));
			}
			else
			{
				forceLoggingSilent = true;
				while (dq < preloadAmount)
				{
					Transform a2 = yv(a);
					yr(a2, b: false);
				}
				forceLoggingSilent = false;
			}
		}

		[IteratorStateMachine(typeof(b))]
		private IEnumerator za(object a)
		{
			return new b(0)
			{
				ba = this,
				kt = a
			};
		}

		public bool zb(Transform a)
		{
			if (prefabGO == null)
			{
				UnityEngine.Debug.LogError($"SpawnPool {spawnPool.poolName}: PrefabPool.prefabGO is null");
			}
			if (@do.Contains(a))
			{
				return true;
			}
			if (dp.Contains(a))
			{
				return true;
			}
			return false;
		}

		private void zc(Transform a)
		{
			a.name += (dq + 1).ToString("#000");
		}
	}
}
