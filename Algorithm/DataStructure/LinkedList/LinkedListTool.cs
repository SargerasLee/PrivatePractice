using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DataStructure.LinkedList
{
	public class LinkedListTool
	{
		#region 反转链表

		//1 使用栈
		//2 使用双指针
		//3 使用递归

		public LinkedNode Reverse1(LinkedNode head)
		{
			Stack<LinkedNode> nodes = new Stack<LinkedNode>();
			while(head!=null)
			{
				nodes.Push(head);
				head = head.Next;
			}
			LinkedNode newHead = new LinkedNode()
			{
				Value = 0
			};
			LinkedNode n = newHead;
			while(nodes.Peek()!=null)
			{
				newHead.Next = nodes.Pop();
				newHead = newHead.Next;
			}
			return n.Next;
		}
		public LinkedNode Reverse2(LinkedNode head)
		{
			if (head == null) return null;
			LinkedNode pre = null, current = head, temp;
			while(current!=null)
			{
				temp = current.Next;//临时节点指向 原链表后一个节点
				current.Next = pre;//当前节点指向 反转表的头
				pre = current;//pre  指向   反转表的头
				current = temp;//当前节点   指向  临时节点
			}
			return pre;
		}
		public LinkedNode Reverse3(LinkedNode head)
		{
			//好好想想，有点绕
			if (head == null || head.Next==null) return head;//左边防止第一次就是null，右边用于递归
			//递归对head节点之后的链表进行逆转，逆转后的头节点是newhead
			LinkedNode newhead = Reverse3(head.Next);
			head.Next.Next = head;//反转的尾和原链表的最后一个 正好差2个位置，画图
			//因为head.Next==newhead 即 newhead.Next=head;这样写保证了递归返回的节点为反转后的头结点
			head.Next = null;
			return newhead;
		}

		public void PrintLinkedList(LinkedNode head)
		{
			while(head!=null)
			{
				Console.Write(head.Value+"	");
				head = head.Next;
			}
		}
		#endregion
	}

	public class LinkedNode
	{
		public int Value{ get; set; }
		public LinkedNode Next{ get; set; }
	}
}
