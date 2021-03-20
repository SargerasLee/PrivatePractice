using System;

namespace Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class RouteMappingAttribute : Attribute
	{
		public string Value { get; set; }
	}
}
