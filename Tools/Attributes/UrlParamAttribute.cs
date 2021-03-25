using System;

namespace Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Parameter,AllowMultiple =false,Inherited =false)]
	public class UrlParamAttribute : Attribute
	{
		public string Name{ get; }

		public UrlParamAttribute(string name = "")
		{
			Name = name;
		}
	}
}
