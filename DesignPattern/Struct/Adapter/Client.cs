using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Struct.Adapter
{
	public class Client
	{
		public void TestAdapter()
		{
			RoundHole hole = new RoundHole(5);
			RoundPeg peg = new RoundPeg(5);
			hole.Fits(peg);

			SquarePeg smallPeg = new SquarePeg(5);
			SquarePeg largePeg = new SquarePeg(10);

			//hole.Fits(smallPeg);

			SquarePegAdapter small = new SquarePegAdapter(smallPeg);
			SquarePegAdapter large = new SquarePegAdapter(largePeg);
			hole.Fits(small);
			hole.Fits(large);
		}
	}
}
