using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern.Behavior.TemplateMethod
{
	/// <summary>
	/// 游戏AI抽象类，根据难度不同，其实现不同
	/// </summary>
	public abstract class GameAI
	{
		/// <summary>
		/// AI的思路（模板方法）
		/// </summary>
		public void TakeTurn()
		{
			CollectResources();
			Thread.Sleep(1000);
			BuildStructures();
			Thread.Sleep(1000);
			BuildUnits();
			Thread.Sleep(1000);
			Attack();
		}

		/// <summary>
		/// 收集资源
		/// </summary>
		protected abstract void CollectResources();
		/// <summary>
		/// 建造建筑
		/// </summary>
		protected abstract void BuildStructures();
		/// <summary>
		/// 生产军队
		/// </summary>
		protected abstract void BuildUnits();
		/// <summary>
		/// 攻击指令
		/// </summary>
		protected void Attack()
		{
			string position = ClosestEnemy();
			Thread.Sleep(1000);
			SendScounts(position);
			Thread.Sleep(1000);
			SendWarriors(position);
		}
		/// <summary>
		/// 派出哨兵
		/// </summary>
		/// <param name="position"></param>
		protected abstract void SendScounts(string position);
		/// <summary>
		/// 派出战士
		/// </summary>
		/// <param name="position"></param>
		protected abstract void SendWarriors(string position);
		/// <summary>
		/// 接近敌人
		/// </summary>
		/// <returns></returns>
		protected abstract string ClosestEnemy();
	}


	/// <summary>
	/// 异虫AI
	/// </summary>
	public class ZergAI : GameAI
	{
		protected override void BuildStructures()
		{
			Console.WriteLine("开始孵化王虫");
			Thread.Sleep(2000);
			Console.WriteLine("孵化完成");
			Console.WriteLine("开始建造孵化池");
			Thread.Sleep(4000);
			Console.WriteLine("建造完成");
		}

		protected override void BuildUnits()
		{
			Console.WriteLine("开始孵化跳虫");
			Thread.Sleep(2000);
			Console.WriteLine("孵化完成");
		}

		protected override string ClosestEnemy()
		{
			Console.WriteLine("发现敌人，位置在4,5");
			return "4,5";
		}

		protected override void CollectResources()
		{
			Console.WriteLine("采集水晶矿....");
		}

		protected override void SendScounts(string position)
		{
			Console.WriteLine("派出工蜂到"+position);
		}

		protected override void SendWarriors(string position)
		{
			Console.WriteLine("派出跳虫到" + position);
		}
	}

	/// <summary>
	/// 泰伦AI
	/// </summary>
	public class TyronAI : GameAI
	{
		protected override void BuildStructures()
		{
			Console.WriteLine("开始建造补给站");
			Thread.Sleep(2000);
			Console.WriteLine("建造完成完成");
			Console.WriteLine("开始建造兵营");
			Thread.Sleep(4000);
			Console.WriteLine("建造完成");
		}

		protected override void BuildUnits()
		{
			Console.WriteLine("开始生产陆战队员");
			Thread.Sleep(2000);
			Console.WriteLine("生产完成");
		}

		protected override string ClosestEnemy()
		{
			Console.WriteLine("发现敌人，位置在4,5");
			return "4,5";
		}

		protected override void CollectResources()
		{
			Console.WriteLine("采集水晶矿....");
		}

		protected override void SendScounts(string position)
		{
			Console.WriteLine("派出死神到" + position);
		}

		protected override void SendWarriors(string position)
		{
			Console.WriteLine("派出陆战队员到" + position);
		}
	}
}
