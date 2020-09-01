using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Thread
{
	public class TaskThreadUtil
	{
		async Task TestAsync()
		{
			await Task.Run(() => { });
			System.Threading.Thread.Sleep(3000);
		}
	}
}
