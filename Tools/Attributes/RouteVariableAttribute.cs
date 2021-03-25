using System;

namespace Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Parameter,AllowMultiple =false,Inherited =false)]
	public class RouteVariableAttribute : Attribute
	{
		public string Name{ get; }

		public RouteVariableAttribute(string name = "")
		{
			Name = name;
		}
	}
}
