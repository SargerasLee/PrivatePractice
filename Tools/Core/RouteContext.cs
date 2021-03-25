using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Core
{
	public class RouteContext
	{
		public Dictionary<string, string> UrlParams{ get; }

		public Dictionary<string, string> RouteParams{ get; }

		public object[] objects{ get; }
		public RouteContext(Dictionary<string, string> urlParams, Dictionary<string, string> routeParams, object[] objs)
		{
			UrlParams = urlParams;
			RouteParams = routeParams;
			objects = objs;
		}
	}
}
