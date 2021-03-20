using System;

namespace Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class CustomComponentAttribute : Attribute
	{
	}
}
