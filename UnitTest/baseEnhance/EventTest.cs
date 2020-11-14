using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class EventTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			CarEventListener xiaoming = new CarEventListener("小红");
			CarEventPublish publish = new CarEventPublish();
			CarWeakEventManager.CurrentManager.AddListener(publish, xiaoming);
			//publish.handler += xiaoming.Listening;
			publish.PulishEvent("保时捷");
		}

		[TestMethod]
		public void TestMethod2()
		{
			CarEventListener xiaoming = new CarEventListener("小红");
			CarEventPublish publish = new CarEventPublish();
			WeakEventManager<CarEventPublish, CarEventArgs>.AddHandler(publish, "handler", xiaoming.Listening);
			publish.PulishEvent("保时捷");
		}
	}

	public class CarEventArgs:EventArgs
	{
		public string Car{ get; private set; }

		public CarEventArgs(string car)
		{
			this.Car = car;
		}

	}

	public class CarEventListener : IWeakEventListener
	{
		private string name;
		public CarEventListener(string name)
		{
			this.name = name;
		}
		public void Listening(object sender,CarEventArgs carEvent)
		{
			Console.WriteLine($"{name}监听到事件{carEvent.Car}");
		}

		public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
		{
			Listening(sender, e as CarEventArgs);
			return true;
		}
	}

	public class CarEventPublish
	{
		public event EventHandler<CarEventArgs> handler;

		public void PulishEvent(string car)
		{
			if(handler!=null)
			{
				foreach(EventHandler<CarEventArgs> n in handler.GetInvocationList())
				{
					n(this, new CarEventArgs(car));

				}
			}
		}
	}

	public class CarWeakEventManager : WeakEventManager
	{

		public void AddListener(object source,IWeakEventListener listener)
		{
			CurrentManager.ProtectedAddListener(source, listener);
		}

		public void RemoveListener(object source,IWeakEventListener listener)
		{
			CurrentManager.ProtectedRemoveListener(source, listener);
		}
		protected override void StartListening(object source)
		{
			(source as CarEventPublish).handler += Deal;
		}
		void Deal(object sender,CarEventArgs e)
		{
			DeliverEvent(sender, e);
		}
		protected override void StopListening(object source)
		{
			(source as CarEventPublish).handler -= Deal;
		}

		public static CarWeakEventManager CurrentManager
		{
			get
			{
				var manager = GetCurrentManager(typeof(CarWeakEventManager)) as CarWeakEventManager;
				if(manager==null)
				{
					manager = new CarWeakEventManager();
					SetCurrentManager(typeof(CarWeakEventManager), manager);
				}
				return manager;
			}
		}
	}
}
