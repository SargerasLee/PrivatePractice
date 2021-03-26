using System;

namespace Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Parameter,AllowMultiple =false,Inherited =false)]
	public class RouteParamAttribute : ParameterAttribute
	{
		public RouteParamAttribute(string name = "")
		{
			Name = name;
		}
	}
}
