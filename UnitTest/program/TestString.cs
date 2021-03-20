using System;
using System.Text;
using Logic.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.StringUtils;
using Algorithm.DataStructure.LinkedList;
using Algorithm.DataStructure.BinaryTree;
namespace UnitTest.program
{
	[TestClass]
	public class TestString
	{
		[TestMethod]
		public void TestConcat()
		{
			string invoices = "01136339,01136340,01600581,01600582,01600583,01136341,01136342,01136343,01136498,01136499,01600200,01600201,01600202,01136630,01136631,01136638,01136503,01136504,01136505,01136506,01136507,01136551,01136552,01136553,01136554,01136555,01136556,01136668,01599459,01599460,01599461,01136557,01136558,01136559,01136562,01136564,01136567,01136568,01136611,01136612,01136677,01600198,01136616,01136617,01136618,01136619,01136620,01136621,01136622,01136623,01600587,01596431,01136624,01136625,01136626,01136627,01136628,01136629,01136649,01136650,01136651,01136652,01136653,01136654,01136655,01136656,01136657,01136658,01136659,01136660,01136661,01136632,01136633,01136634,01136635,01136637,01136662,01136613,01136614,01136615,01136674,01136676,01136663,01136664,01136665,01136648,01600580,01136500,01136501,01136502,01136666,01136667,01136492,01136493,01136494,01599462,01599463,01600199,01600584,01600586";
			string[] xx = invoices.Split(',');
			SortUtil.BubbleSort(xx, Compare);
			StringBuilder sb = new StringBuilder();
			foreach(var i in xx)
			{
				sb.Append(i);
				sb.Append(",");
			}
			string res = StringUtil.DealInvoice(sb.Remove(sb.Length - 1, 1).ToString());
			Console.WriteLine(res);
		}

		[TestMethod]
		public void TestReverseList()
		{
			LinkedListTool tool = new LinkedListTool();
			LinkedNode node1 = new LinkedNode()
			{
				Value = 1
			};
			LinkedNode node2 = new LinkedNode()
			{
				Value = 2
			};
			LinkedNode node3 = new LinkedNode()
			{
				Value = 3
			};
			LinkedNode node4 = new LinkedNode()
			{
				Value = 4
			};
			node1.Next = node2;
			node2.Next = node3;
			node3.Next = node4;
			node4.Next = null;
			LinkedNode res = tool.Reverse3(node1);
			tool.PrintLinkedList(res);
		}
		private static bool Compare(string s1, string s2) => string.Compare(s1, s2) > 0;

		[TestMethod]
		public void TestBinaryTreeTraversal()
		{
			TreeNode one = new TreeNode()
			{
				Left = null,
				Right=null,
				Value=1
			};
			TreeNode two = new TreeNode()
			{
				Left = null,
				Right = null,
				Value=2
			};
			TreeNode three = new TreeNode()
			{
				Left=one,
				Right=two,
				Value=3
			};
			TreeNode root = new TreeNode()
			{
				Left=three,
				Right=null,
				Value=0
			};
			BinaryTreeTool tool = new BinaryTreeTool();
			tool.PreOrderTraversal2(root);
			Console.WriteLine();
			tool.PreOrderTraversal1(root);
			Console.WriteLine();
			tool.LevelTraversal(root);
			Console.WriteLine();
			tool.PostOrderTraversal3(root);
		}
	}
}
