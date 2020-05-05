/************************************************************************
*Copyright  (c)   2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*命名空间    ：Logic.recursion
*文件名称    ：MazeUtil.cs
*版本号        :   2020|V1.0.0.0 
*=================================
*创 建 者      ：@ lichanghao01
*创建日期    ：2020/5/4 13:49:46 
*功能描述    ：
*使用说明    ：
*=================================
*修改日期    ：2020/5/4 13:49:46 
*修改者        ：lichanghao01
*修改描述    ：
*版本号        :   2020|V1.0.0.0 
***********************************************************************/
using System;

namespace Logic.Recursion
{
	public class MazeUtil
	{
		private static int JudgeCount = 0;

		/// <summary>
		/// 迷宫
		/// </summary>
		/// <param name="map">迷宫地图</param>
		/// <param name="beginRow">起始行</param>
		/// <param name="beginColumn">起始列</param>
		/// <returns></returns>
		public static bool DoMaze(int[,] map, int beginRow, int beginColumn)
		{
			//制定策略，下，右，上，左  依次走，0代表未走过，1代表墙，2代表通路，3代表不通
			int endRow = map.GetLength(0) - 2;
			int endColumn = map.GetLength(1) - 2;
			//如果终点已走通，退出回溯
			if (map[endRow, endColumn] == 2)
			{
				return true;
			}
			else
			{
				//如果当前点没走过
				if (map[beginRow, beginColumn] == 0)
				{
					//假设可以走通
					map[beginRow, beginColumn] = 2;
					//向下走
					if (DoMaze(map, beginRow + 1, beginColumn))
					{
						return true;
					}
					//向右走
					else if (DoMaze(map, beginRow, beginColumn + 1))
					{
						return true;
					}
					//向上走
					else if (DoMaze(map, beginRow - 1, beginColumn))
					{
						return true;
					}
					//向左走
					else if (DoMaze(map, beginRow, beginColumn - 1))
					{
						return true;
					}
					//都走不通，死路一条
					else
					{
						map[beginRow, beginColumn] = 3;
						return false;
					}
				}
				else
				{
					return false;
				} 
			}
		}

		/// <summary>
		/// 8皇后问题
		/// </summary>
		/// <param name="queen">值为皇后存放位置，下表为第几个皇后</param>
		/// <param name="count">解法数</param>
		/// <returns></returns>
		public static int DoQueen8(int[] queen,ref int count)
		{
			//不同皇后  不能 同一行，同一列，同一斜线，因为需要遍历数组，数组下标代表行数，所以行不会重复
			//先放置第 0个皇后，用回溯法
			JudgeCount = 0;
			Place(queen,0,ref count);
			return JudgeCount;
		}
		/// <summary>
		/// 递归方法放置皇后，初始为0，数组长度为总皇后个数
		/// </summary>
		/// <param name="queen">皇后位置数组</param>
		/// <param name="n">第几个皇后</param>
		/// <param name="count">存放解法数</param>
		public static void Place(int[] queen, int n,ref int count)
		{
			//如果当前放置的皇后 等于数组长度，说明已放置完，打印解法
			if (n == queen.Length)
			{
				count++;
				Print(queen);
				return;
			}
			//从第1列开始放置，判断是否有冲突，没有则放置下一个皇后，有则从下一列放置
			for (int i = 0; i < queen.Length; i++)
			{
				queen[n] = i;
				//判断是否冲突，不冲突放置下一个
				if (!IfConflict(queen, n))
				{
					Place(queen, n + 1, ref count);
				}
			}
		}

		/// <summary>
		/// 打印当前解法
		/// </summary>
		/// <param name="queen"></param>
		private static void Print(int[] queen)
		{
			foreach (int i in queen)
			{
				Console.Write(i+"  ");
			}
			Console.WriteLine();
		}

		/// <summary>
		/// 判断新摆放的皇后跟之前有没有冲突
		/// </summary>
		/// <param name="queen">皇后位置数组</param>
		/// <param name="n">第n个摆放的皇后，从0开始</param>
		/// <returns>true有冲突</returns>
		private static bool IfConflict(int[] queen, int n)
		{
			for (int i=0;i<n;i++)
			{
				//不能同一列， 同一斜线 
				if (queen[n] == queen[i] || Math.Abs(queen[n] - queen[i]) == Math.Abs(n - i))
				{
					JudgeCount++;
					return true;
				}
			}
			return false;
		}
	}
}
