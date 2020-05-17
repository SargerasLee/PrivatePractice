using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Map
{
	public class MapUtil
	{

		/// <summary>
		/// 广度优先搜索,搜索是销售的朋友
		/// </summary>
		/// <param name="friend"></param>
		/// <returns></returns>
		public static string BreadthFirstSearch(Friend friend)
		{
			Queue<Friend> queue = new Queue<Friend>();
			queue.Enqueue(friend);
			while (queue.Count > 0)
			{
				var first = queue.Dequeue();
				if (first.IsSaleMan)
				{
					return first.Name;
				}
				else
				{
					if (first.Friends!=null)
					{
						foreach (var firstFriend in first.Friends)
						{
							queue.Enqueue(firstFriend);
						} 
					}
				}
			}

			return null;
		}
	}
}
