using System;
using System.Collections.Generic;

namespace Evaluator
{
	internal class Contestant
	{
		private string firstName;

		private string lastName;

		private string location;

		private string iD;

		private Dictionary<string, string> pb;

		private int probCount;

		private int total;

		private bool alreadyEvaluated;

		public Dictionary<string, string> PB
		{
			get
			{
				return this.pb;
			}
		}

		public bool AlreadyEvaluated
		{
			get
			{
				return this.alreadyEvaluated;
			}
			set
			{
				this.alreadyEvaluated = value;
			}
		}

		public int Total
		{
			get
			{
				return this.total;
			}
			set
			{
				this.total = value;
			}
		}

		public int ProbCount
		{
			get
			{
				return this.probCount;
			}
			set
			{
				this.probCount = value;
			}
		}

		public string ID
		{
			get
			{
				return this.iD;
			}
			set
			{
				this.iD = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		public void SetProb(string problem, string points, bool overRide)
		{
			if (points != "-")
			{
				try
				{
					this.alreadyEvaluated = true;
					if (overRide)
					{
						this.total -= int.Parse(this.pb[problem]);
					}
					this.total += int.Parse(points);
					this.pb[problem] = points;
				}
				catch (Exception)
				{
				}
			}
		}

		public Contestant(string ID, string n, string p, string L)
		{
			this.iD = ID;
			this.lastName = n;
			this.firstName = p;
			this.location = L;
			this.alreadyEvaluated = false;
			this.pb = new Dictionary<string, string>();
		}

		public Contestant()
		{
			this.lastName = null;
			this.firstName = null;
			this.location = null;
			this.alreadyEvaluated = false;
			this.pb = new Dictionary<string, string>();
		}
	}
}
