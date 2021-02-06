using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Struct.Adapter
{
	/// <summary>
	/// 经典 方钉圆孔问题 ，用适配器模式解决
	/// 适配器类
	/// </summary>
	public class SquarePegAdapter : RoundPeg
	{
		private SquarePeg squarePeg;
		public SquarePegAdapter(SquarePeg peg)
		{
			squarePeg = peg;
		}
		public double GetRadius()
		{
			return squarePeg.Width * Math.Sqrt(2) / 2;
		}
	}

	public class SquarePeg
	{
		public double Width { get; }

		public SquarePeg(double width)
		{
			Width = width;
		}
	}

	public class RoundPeg
	{
		public double Radius{ get; }

		public RoundPeg(double radius)
		{
			Radius = radius;
		}
		public RoundPeg(){ }
	}

	public class RoundHole
	{
		public double Radius{ get; }
		public RoundHole(double radius)
		{
			Radius = radius;
		}

		public bool Fits(RoundPeg peg)
		{
			return Radius >= peg.Radius;
		}
	}
}
