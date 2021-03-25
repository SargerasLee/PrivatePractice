using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Core
{
	public abstract class RouteCassette
	{
		public abstract string Signature{ get; set; }
		public abstract MemberTypes MemberType{ get; set; }
		public abstract IList<Attribute> Attributes{ get; set; }
		public abstract IList<RouteCassette> Content{ get; set; }

		public abstract void Add(RouteCassette cassette);
		public abstract object Match(string route, params object[] objects);
	}
}
