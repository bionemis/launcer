using System.Threading;

public class u
{
	public delegate void a(string a);

	public delegate void b(string a);

	public delegate void c(string a);

	private SynchronizationContext cn;

	public event a ck;

	public event b cl;

	public event c cm;

	public void gk()
	{
		cn = SynchronizationContext.Current;
		ck += go;
		cl += gq;
		cm += gp;
	}

	public void Close()
	{
		ck -= go;
		cl -= gq;
		cm -= gp;
	}

	public void gl(string a)
	{
		this.ck(a);
	}

	public void gm(string a)
	{
		this.cl(a);
	}

	public void gn(string a)
	{
		this.cm(a);
	}

	private void go(string a)
	{
		cn.Post(gr, a);
	}

	private void gp(string a)
	{
		cn.Post(gr, a);
	}

	private void gq(string a)
	{
		cn.Post(gr, a);
	}

	private void gr(object a)
	{
	}
}
