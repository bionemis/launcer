using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class n
{
	[StructLayout(LayoutKind.Auto)]
	private struct a : IAsyncStateMachine
	{
		public int y;

		public AsyncTaskMethodBuilder<o> bm;

		public string bq;

		public string br;

		private o bs;

		private Process bt;

		private TaskAwaiter<string> bo;

		private void MoveNext()
		{
			int num = y;
			o result2;
			try
			{
				if ((uint)num > 1u)
				{
					bs = new o();
				}
				try
				{
					if ((uint)num > 1u)
					{
						bt = fe();
					}
					try
					{
						TaskAwaiter<string> awaiter;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = bo;
								bo = default(TaskAwaiter<string>);
								num = (y = -1);
								goto IL_0162;
							}
							bt.StartInfo.FileName = bq;
							bt.StartInfo.Arguments = br;
							bt.Start();
							if (bt.StandardOutput.Peek() <= -1)
							{
								goto IL_00f1;
							}
							awaiter = bt.StandardOutput.ReadToEndAsync().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (y = 0);
								bo = awaiter;
								bm.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = bo;
							bo = default(TaskAwaiter<string>);
							num = (y = -1);
						}
						string result = awaiter.GetResult();
						bs.bx = result;
						goto IL_00f1;
						IL_00f1:
						if (bt.StandardError.Peek() > -1)
						{
							awaiter = bt.StandardError.ReadToEndAsync().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (y = 1);
								bo = awaiter;
								bm.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0162;
						}
						goto IL_0176;
						IL_0162:
						result = awaiter.GetResult();
						bs.bx = result;
						goto IL_0176;
						IL_0176:
						bt.WaitForExit();
						bs.bw = bt.ExitCode;
						bs.bv = true;
					}
					finally
					{
						if (num < 0 && bt != null)
						{
							((IDisposable)bt).Dispose();
						}
					}
					bt = null;
				}
				catch (Win32Exception ex)
				{
					bs.bv = false;
					bs.bx = $"{ex.NativeErrorCode},{co.zk(ex.NativeErrorCode)}";
				}
				catch (Exception ex2)
				{
					bs.bv = false;
					bs.bx = ex2.ToString();
				}
				result2 = bs;
			}
			catch (Exception exception)
			{
				y = -2;
				bm.SetException(exception);
				return;
			}
			y = -2;
			bm.SetResult(result2);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine a)
		{
			bm.SetStateMachine(a);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine a)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(a);
		}
	}

	public static int bu = 30;

	private static Process fe()
	{
		return new Process
		{
			StartInfo = 
			{
				CreateNoWindow = true,
				UseShellExecute = false,
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardInput = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				StandardOutputEncoding = Encoding.UTF8
			}
		};
	}

	private static string ff(Process a)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (a.StandardOutput.Peek() == -1)
		{
			stringBuilder.Append((char)a.StandardOutput.Read());
		}
		while (a.StandardOutput.Peek() > -1)
		{
			stringBuilder.Append((char)a.StandardOutput.Read());
		}
		return stringBuilder.ToString();
	}

	public static Process Start(string a, string b)
	{
		return Process.Start(new ProcessStartInfo(a)
		{
			Arguments = b,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardInput = true
		});
	}

	public static void fg(string a, string b)
	{
		Process process = Process.Start(new ProcessStartInfo(a)
		{
			Arguments = b,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardInput = true
		});
		StreamReader standardOutput = process.StandardOutput;
		do
		{
			string text = standardOutput.ReadLine();
			GameEntry.y.gm(text);
		}
		while (!standardOutput.EndOfStream);
		process.OutputDataReceived += fl;
		process.ErrorDataReceived += fm;
		process.WaitForExit();
		standardOutput.Close();
		process.Close();
	}

	public static void fh(string a, string b)
	{
		Process process = Process.Start(new ProcessStartInfo(a)
		{
			Arguments = b,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardInput = true
		});
		process.OutputDataReceived += fl;
		process.ErrorDataReceived += fm;
		process.BeginOutputReadLine();
		process.WaitForExit();
	}

	public static o fi(string a, string b)
	{
		o o2 = new o();
		try
		{
			using (Process process = fe())
			{
				process.StartInfo.FileName = a;
				process.StartInfo.Arguments = b;
				process.Start();
				if (process.StandardOutput.Peek() > -1)
				{
					o2.bx = process.StandardOutput.ReadToEnd();
				}
				if (process.StandardError.Peek() > -1)
				{
					o2.bx = process.StandardError.ReadToEnd();
				}
				process.WaitForExit();
				o2.bw = process.ExitCode;
				o2.bv = true;
			}
		}
		catch (Win32Exception ex)
		{
			o2.bv = false;
			o2.bx = $"{ex.NativeErrorCode},{co.zk(ex.NativeErrorCode)}";
		}
		catch (Exception ex2)
		{
			o2.bv = false;
			o2.bx = ex2.ToString();
		}
		return o2;
	}

	public static async Task<o> fj(string a, string b)
	{
		o o2 = new o();
		try
		{
			using (Process process = fe())
			{
				process.StartInfo.FileName = a;
				process.StartInfo.Arguments = b;
				process.Start();
				if (process.StandardOutput.Peek() > -1)
				{
					o2.bx = await process.StandardOutput.ReadToEndAsync();
				}
				if (process.StandardError.Peek() > -1)
				{
					o2.bx = await process.StandardError.ReadToEndAsync();
				}
				process.WaitForExit();
				o2.bw = process.ExitCode;
				o2.bv = true;
			}
		}
		catch (Win32Exception ex)
		{
			o2.bv = false;
			o2.bx = $"{ex.NativeErrorCode},{co.zk(ex.NativeErrorCode)}";
		}
		catch (Exception ex2)
		{
			o2.bv = false;
			o2.bx = ex2.ToString();
		}
		return o2;
	}

	public static o fk(string a, string b, string[] c)
	{
		o o2 = new o();
		try
		{
			using (Process process = fe())
			{
				process.StartInfo.FileName = a;
				process.StartInfo.Arguments = b;
				process.Start();
				process.StandardInput.WriteLine();
				o2.bx = ff(process);
				o2.by = new Dictionary<int, string>();
				for (int num = 0; num < c.Length; num++)
				{
					process.StandardInput.WriteLine(c[num] + "\r");
					Thread.Sleep(bu);
					o2.by.Add(num, ff(process));
				}
				process.WaitForExit();
				o2.bw = process.ExitCode;
				o2.bv = true;
			}
		}
		catch (Win32Exception ex)
		{
			o2.bv = false;
			o2.bx = $"{ex.NativeErrorCode},{co.zk(ex.NativeErrorCode)}";
		}
		catch (Exception ex2)
		{
			o2.bv = false;
			o2.bx = ex2.ToString();
		}
		return o2;
	}

	private static void fl(object a, DataReceivedEventArgs b)
	{
		GameEntry.y.gm(b.Data);
	}

	private static void fm(object a, DataReceivedEventArgs b)
	{
		UnityEngine.Debug.LogError("ErrorDataReceived =>" + b.Data);
		GameEntry.y.gn(b.Data);
	}
}
