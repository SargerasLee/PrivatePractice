using System;
using System.Net.Mail;
using Logic.Recursion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class TestMaze
	{
		[TestMethod]
		public void TestDoRecursion()
		{
			int[,] maze = new int[6,6];
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					maze[i, j] = 0;
				}
			}
			for (int i = 0; i < 6; i++)
			{
				maze[0, i] = 1;
				maze[5, i] = 1;
			}
			for (int i = 0; i < 6; i++)
			{
				maze[i, 0] = 1;
				maze[i, 5] = 1;
			}
			maze[2,1] = 1;
			maze[2,2] = 1;
			maze[3,3] = 1;
			maze[4,3] = 1;
			MazeUtil.DoMaze(maze, 1, 1);
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					Console.Write(maze[i,j]);
				}
				Console.WriteLine();
			}

		}

		[TestMethod]
		public void TestDoQueen8()
		{
			int[] queen=new int[8];
			int count = 0;
			int i = MazeUtil.DoQueen8(queen,ref count);
			Console.WriteLine(count);
			Console.WriteLine(i);
		}
	}
}
