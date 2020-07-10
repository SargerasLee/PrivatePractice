using Entity;
using System;
using System.Collections.Generic;

namespace UnitTest
{
	public static class TestData
	{
		public static List<Book> ListData()
		{
			List<Book> books = new List<Book>{
				new Book{BookCode=1,Author="zhangsan",BookName="Oracle Programing",IsSuit=false,Price=19.9m,PublishDate=DateTime.Now.AddDays(-12) },
				new Book{BookCode=2,Author="lisi",BookName="Thinking in Java",IsSuit=false,Price=79.9m,PublishDate=DateTime.Now.AddDays(-1) },
				new Book{BookCode=3,Author="wangwu",BookName="Thinking in C#",IsSuit=true,Price=55.2m,PublishDate=DateTime.Now.AddDays(-20) },
				new Book{BookCode=4,Author="maliu",BookName="Javascript Programing",IsSuit=false,Price=39.9m,PublishDate=DateTime.Now.AddDays(-100) },
			};
			return books;
		}

		public static string JsonArrayData()
		{ 
			string json = "[{name:\"lisi\",age:\"30\"},{name:\"xxx\",age:\"66\"}";
			return json;
		}

		public static Friend MapData()
		{
			List<Friend> list1 = new List<Friend>();
			list1.Add(new Friend() { Name = "张三", IsSaleMan = false, Friends = null });
			list1.Add(new Friend() { Name = "李四", IsSaleMan = false, Friends = null });
			list1.Add(new Friend() { Name = "王五", IsSaleMan = false, Friends = null });

			List<Friend> list2 = new List<Friend>();
			list2.Add(new Friend() { Name = "lucy", IsSaleMan = false, Friends = null });
			list2.Add(new Friend() { Name = "tom", IsSaleMan = true, Friends = null });
			list2.Add(new Friend() { Name = "green", IsSaleMan = false, Friends = null });

			List<Friend> list3 = new List<Friend>();
			list3.Add(new Friend() { Name = "ming", IsSaleMan = false, Friends = list1 });
			list3.Add(new Friend() { Name = "ling", IsSaleMan = false, Friends = list2 });

			Friend friend = new Friend() { Name = "me", IsSaleMan = false, Friends = list3 };
			return friend;
		}

		public static int[,] MazeMapData()
		{
			int[,] maze = new int[6, 6];
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
			maze[2, 1] = 1;
			maze[2, 2] = 1;
			maze[3, 3] = 1;
			maze[4, 3] = 1;
			return maze;
		}
	}
}
