using System;

namespace Evaluator
{
	internal class UserState2
	{
		public string tst = "";

		public string pct = "";

		public string tmp = "";

		public string mem = "";

		public string msg = "";

		public int caz;

		public UserState2(string ts, string p, string t, string mm, string ms)
		{
			this.tst = ts;
			this.pct = p;
			this.tmp = t;
			this.mem = mm;
			this.msg = ms;
		}
	}
}
