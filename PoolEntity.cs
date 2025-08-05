using System;
using PathologicalGames;

[Serializable]
public class PoolEntity
{
	public byte PoolId;

	public string PoolName;

	public int PreloadAmount;

	public bool CullDespawned = true;

	public int CullAbove = 5;

	public int CullDelay = 2;

	public int CullMaxPerPass = 2;

	public SpawnPool Pool;
}
