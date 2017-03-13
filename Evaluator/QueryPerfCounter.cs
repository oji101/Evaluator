using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Evaluator
{
	internal class QueryPerfCounter
	{
		private long start;

		private long stop;

		private long frequency;

		private decimal multiplier = new decimal(1000000000.0);

		[DllImport("KERNEL32")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

		public QueryPerfCounter()
		{
			if (!QueryPerfCounter.QueryPerformanceFrequency(out this.frequency))
			{
				MessageBox.Show("Nu suporta frecventa");
				throw new Win32Exception();
			}
		}

		public void Start()
		{
			QueryPerfCounter.QueryPerformanceCounter(out this.start);
		}

		public void Stop()
		{
			QueryPerfCounter.QueryPerformanceCounter(out this.stop);
		}

		public double Duration()
		{
			return (double)(this.stop - this.start) * (double)this.multiplier / (double)this.frequency;
		}
	}
}
