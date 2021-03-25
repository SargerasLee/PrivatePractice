using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Core
{
	public class ClassCassette : RouteCassette
	{
		public override string Signature { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override MemberTypes MemberType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override IList<Attribute> Attributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override IList<RouteCassette> Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public override void Add(RouteCassette cassette)
		{
			throw new NotImplementedException();
		}

		public override object Match(string route, params object[] objects)
		{
			throw new NotImplementedException();
		}
	}
}
