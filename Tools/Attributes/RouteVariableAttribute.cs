﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Parameter,AllowMultiple =false,Inherited =false)]
	public class RouteVariableAttribute : Attribute
	{
	}
}
