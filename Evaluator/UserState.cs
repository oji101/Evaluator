using System;

namespace Evaluator
{
	internal class UserState
	{
		public string err;

		public string msg;

		public string probl;

		public UserState(string e, string m, string p)
		{
			this.err = e;
			this.msg = m;
			this.probl = p;
		}
	}
}
