using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Struct.Composite
{
	public interface IGraphic
	{
		void Move(double x, double y);
		void Draw();
	}

	public class Dot : IGraphic
	{
		private double x;
		private double y;
		public Dot(double x,double y)
		{
			this.x = x;
			this.y = y;
		}
		
		public virtual void Draw()
		{
			Console.WriteLine("我是Dot");
		}

		public virtual void Move(double x, double y)
		{
			this.x += x;
			this.y += y;
		}
	}

	public class Circle : Dot
	{
		private double radius;
		public Circle(double x,double y, double radius) : base(x, y)
		{
			this.radius = radius;
		}

		public override void Draw()
		{
			Console.WriteLine("我是Circle");
		}
	}

	public class CompoundGraphic : IGraphic
	{
		private List<IGraphic> graphics = new List<IGraphic>(32);

		public void Add(IGraphic graphic)
		{
			graphics.Add(graphic);
		}

		public bool Remove(IGraphic graphic)
		{
			return graphics.Remove(graphic);
		}
		public void Draw()
		{
			foreach (IGraphic graphic in graphics)
			{
				graphic.Draw();
			}
		}

		public void Move(double x, double y)
		{
			foreach(IGraphic graphic in graphics)
			{
				graphic.Move(x, y);
			}
		}
	}

	public class ImageEditor
	{
		private CompoundGraphic all;

		public void Load()
		{
			all = new CompoundGraphic();
			all.Add(new Dot(1.0, 2.0));
			all.Add(new Circle(5.0, 3.0, 10.0));
		}

		public void GroupSelected(List<IGraphic> components)
		{
			var group = new CompoundGraphic();
			foreach(IGraphic component in components)
			{
				group.Add(component);
				all.Remove(component);
			}
			all.Add(group);
			all.Move(10, 20);
			all.Draw();
		}
	}
}
