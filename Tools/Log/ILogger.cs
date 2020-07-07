using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Log
{
	public interface ILogger
	{
		void Debug(params object[] objs);
		void Info(params object[] objs);
		void Error(params object[] objs);
	}
}
