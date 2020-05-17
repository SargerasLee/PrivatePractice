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
			//搜索的队列，如果没找到，就吧friend的所有关系都加入到队列
			Queue<Friend> queue = new Queue<Friend>();
			//存放已经被搜索过的friend，防止死循环
			List<Friend> checkedList=new List<Friend>();
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
							//添加到队列里的元素没有被搜索过
							if(!checkedList.Contains(firstFriend))
								queue.Enqueue(firstFriend);
						} 
					}
					checkedList.Add(first);
				}
			}

			return null;
		}
	}
}
