using System;

namespace Tools.Exceptions
{
	public class RouteNotMatchException : Exception
	{
		public RouteNotMatchException(string message) : base(message){ }

		public RouteNotMatchException(string message, Exception innerException) : base(message,innerException){ }

		public RouteNotMatchException() : base(){ }
	}
}
