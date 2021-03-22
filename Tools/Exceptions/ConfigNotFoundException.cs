using System;

namespace Tools.Exceptions
{
	public class ConfigNotFoundException : Exception
	{
		public ConfigNotFoundException(string message) : base(message) { }

		public ConfigNotFoundException(string message, Exception innerException) : base(message, innerException) { }

		public ConfigNotFoundException() : base() { }
	}
}
