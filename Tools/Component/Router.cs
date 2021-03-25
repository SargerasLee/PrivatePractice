using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Component
{
	public class Router
	{
		public static object Routing(string route, params object[] objects)
		{
			return ComponentDispatcher.GetInstance().Dispatch(route, objects);
		}
	}
}
