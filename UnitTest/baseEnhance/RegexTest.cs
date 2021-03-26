using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.RegularExpressions;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class RegexTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			string pattern = @"\bg[0-9]+w{1,8}d";
			string input = "gg12132wwwdd";
			MatchCollection mc = Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
			foreach(Match i in mc)
			{
				Console.WriteLine(i.Index);
			}
		}
		[TestMethod]
		public void Test2()
		{
			string[] regexStr = { "/test/{xx}", "/test/{xx}/{xxo}", "/test/ying/{xx}", "/test/ying", "/{0}/{1}" };
			bool xx = Regex.IsMatch(regexStr[4], "(/\\w+)*(/{\\w+})+");
			Console.WriteLine(xx);

			Console.WriteLine(Regex.Match(regexStr[0], "{\\w+}").Value);
		}
	}
}
