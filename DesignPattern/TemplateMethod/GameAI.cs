using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern.TemplateMethod
{
	public abstract class GameAI
	{
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

		protected abstract void CollectResources();
		protected abstract void BuildStructures();
		protected abstract void BuildUnits();
		protected void Attack()
		{
			string position = ClosestEnemy();
			Thread.Sleep(1000);
			SendScounts(position);
			Thread.Sleep(1000);
			SendWarriors(position);
		}
		protected abstract void SendScounts(string position);
		protected abstract void SendWarriors(string position);

		protected abstract string ClosestEnemy();
	}

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
