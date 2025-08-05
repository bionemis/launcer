using System;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
	[Header("打包时读xml")]
	[SerializeField]
	private bool isDoubleScreen;

	[Header("是否大屏（未使用）")]
	[SerializeField]
	public bool IsBig;

	[HideInInspector]
	public Transform PoolObj;

	private bool isClose;

	public static GameEntry a { get; private set; }

	public bool IsDoubleScreen => t.IsDoubleScreen;

	public static w r { get; private set; }

	public static bb s { get; private set; }

	public static bc t { get; private set; }

	public static bz u { get; private set; }

	public static cd v { get; private set; }

	public static bg w { get; private set; }

	public static r a { get; private set; }

	public static bd x { get; private set; }

	public static u y { get; private set; }

	public static bj z { get; private set; }

	public static t ba { get; private set; }

	public static z bb { get; private set; }

	private void Awake()
	{
		GameEntry.a = this;
		Screen.fullScreen = true;
		r = new w();
		u = new bz();
		u.mo();
		t = new bc();
		t.@if();
		s = new bb();
		s.hz();
		y = new u();
		y.gk();
		a = new r();
		a.fo();
		x = new bd();
		x.ij();
		w = new bg();
		w.jd();
		z = new bj();
		z.ka();
		ba = new t();
		ba.fz();
		bb = new z();
		bb.hj();
		PoolObj = base.transform.Find("PoolObj");
		v = new cd();
		v.oc();
	}

	private void Start()
	{
		z.Start();
		a.Start();
		x.Start();
		bb.Start();
	}

	private void Update()
	{
		a?.Update();
		x?.Update();
		w?.Update();
		bb?.Update();
		Input.GetKeyDown(KeyCode.A);
	}

	public void Close()
	{
		if (isClose)
		{
			return;
		}
		isClose = true;
		try
		{
			bb?.Close();
			x?.Close();
			a?.Close();
			y?.Close();
			s?.Close();
			t?.Close();
			u?.Close();
			v?.Close();
			w?.Close();
			z?.Close();
			r?.Close();
		}
		catch (Exception ex)
		{
			k.ca("Close():" + ex.Message);
		}
	}

	private void OnApplicationQuit()
	{
		Close();
	}
}
