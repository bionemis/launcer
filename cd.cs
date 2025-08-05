using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class cd
{
	private Dictionary<cc, PoolEntity> gm;

	public void oc()
	{
		gm = new Dictionary<cc, PoolEntity>();
		od(cc.UiItem, 10, 5, 2);
		od(cc.Effect, 2, 2, 2);
	}

	private void od(cc a, int b, int c, int d)
	{
		PoolEntity poolEntity = new PoolEntity();
		poolEntity.PoolId = (byte)a;
		poolEntity.PoolName = a.ToString();
		poolEntity.CullDespawned = true;
		poolEntity.CullAbove = b;
		poolEntity.CullDelay = c;
		poolEntity.CullMaxPerPass = d;
		gm[a] = poolEntity;
		SpawnPool spawnPool = ck.ka.vw(poolEntity.PoolName);
		spawnPool.dl.parent = GameEntry.a.PoolObj;
		spawnPool.dl.localPosition = Vector3.zero;
		poolEntity.Pool = spawnPool;
	}

	public Transform oe(cc a, Transform b)
	{
		PoolEntity poolEntity = gm[a];
		PrefabPool prefabPool = poolEntity.Pool.xw(b);
		if (prefabPool == null)
		{
			prefabPool = new PrefabPool(b);
			prefabPool.cullDespawned = poolEntity.CullDespawned;
			prefabPool.preloadAmount = poolEntity.PreloadAmount;
			prefabPool.cullAbove = poolEntity.CullAbove;
			prefabPool.cullDelay = poolEntity.CullDelay;
			prefabPool.cullMaxPerPass = poolEntity.CullMaxPerPass;
			poolEntity.Pool.wu(prefabPool);
		}
		return poolEntity.Pool.wz(b);
	}

	public bool of(cc a, Transform b, float c = 0f)
	{
		if (gm.Count <= 0)
		{
			Debug.Log("无回池");
			return false;
		}
		PoolEntity poolEntity = gm[a];
		if (a == cc.UiItem)
		{
			b.SetParent(MainControl.be);
		}
		else
		{
			b.SetParent(GameEntry.a.PoolObj);
		}
		poolEntity.Pool.xr(b, c);
		return true;
	}

	public void Close()
	{
		gm.Clear();
		gm = null;
	}
}
