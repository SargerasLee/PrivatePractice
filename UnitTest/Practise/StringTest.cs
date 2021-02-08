using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace UnitTest.Practise
{
	[TestClass]
	public class StringTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			TreeNode root = new TreeNode(10);
			TreeNode first1 = new TreeNode(3);
			TreeNode first2 = new TreeNode(5);
			TreeNode second3 = new TreeNode(8);
			TreeNode second4 = new TreeNode(4);
			root.left = first1;
			root.right = first2;
			first1.left = null;
			first1.right = null;
			first2.left = second3;
			first2.right = second4;
			//    10
			// 3       5
			//        8,4
			ZigzagLevelOrder(root);
		}


		/// <summary>
		/// 389. 找不同
		/// 给定两个字符串 s 和 t，它们只包含小写字母。
		/// 字符串 t 由字符串 s 随机重排，然后在随机位置添加一个字母。
		/// 请找出在 t 中被添加的字母
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public char FindTheDifference(string s, string t)
		{
			/*
			 字符串  字符转为int 累加相减 再转为char
			 */
			int sSum = 0;
			int tSum = 0;
			foreach(var i in s)
			{
				sSum += i;
			}
			foreach(var j in t)
			{
				tSum += j;
			}
			return (char)(tSum - sSum);
		}

		/// <summary>
		/// 387. 字符串中的第一个唯一字符
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public int FirstUniqChar(string s)
		{
			if (s.Length == 1)
				return 0;
			for(int i=0;i<s.Length;i++)
			{
				for(int j =i+1;j<s.Length;j++)
				{
					if(s[i]==s[j])
					{
						break;
					}
					if(j==s.Length-1)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>
		/// 316. 去除重复字母
		/// 给你一个字符串 s ，请你去除字符串中重复的字母，使得每个字母只出现一次。
		/// 需保证 返回结果的字典序最小（要求不能打乱其他字符的相对位置）。
		/// 输入：s = "cbacdcbc"   输出："acdb",字母都为小写
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public string RemoveDuplicateLetters(string s)
		{
			//cbacdcbc
			//acdb
			bool[] vis = new bool[26];
			int[] num = new int[26];//记录字母出现的次数
			for (int i = 0; i < s.Length; i++)
			{
				num[s[i] - 'a']++;
			}

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				char ch = s[i];
				if (!vis[ch - 'a'])
				{
					while (sb.Length > 0 && sb[sb.Length - 1] > ch)
					{
						if (num[sb[sb.Length - 1] - 'a'] > 0)
						{
							vis[sb[sb.Length - 1] - 'a'] = false;
							sb.Remove(sb.Length - 1,1);
						}
						else
						{
							break;
						}
					}
					vis[ch - 'a'] = true;
					sb.Append(ch);
				}
				num[ch - 'a'] -= 1;
			}
			return sb.ToString();
		}


		public void ZigzagLevelOrder(TreeNode root)
		{
			IList<IList<int>> list = new List<IList<int>>();
			Each(root,false,list);
		}

		private void Each(TreeNode root,bool flag, IList<IList<int>> list)
		{
			if (root != null)
			{
				Console.WriteLine(root.val);
				if (flag)
				{
					if (root.left != null)
					{
						Each(root.left, false, list);
					}
					if (root.right != null)
					{
						Each(root.right, false, list);
					} 
				}
				else
				{
					if (root.right != null)
					{
						Each(root.right, true, list);
					}
					if (root.left != null)
					{
						Each(root.left, true, list);
					}
				}
				
			}
		}
	}
	public class TreeNode
	{
		public int val;
		public TreeNode left;
		public TreeNode right;
		public TreeNode(int x) { val = x; }
	}
}
