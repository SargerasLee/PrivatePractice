using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Immutable;
using System.Collections.Concurrent;
namespace UnitTest.baseEnhance
{
	[TestClass]
	public class CollectionTest
	{
		[TestMethod]
		public void TestQueue()
		{
			DocumentManager manager = new DocumentManager();
			Task.Factory.StartNew(new ProcessDocument(manager).Run, TaskCreationOptions.None);
			for (int i = 0; i < 50; i++)
			{
				manager.SetDocument(new Document { Name = $"test{i}", Author = "zhangsan" });
				Thread.Sleep(new Random().Next(20));
			}
		}

		[TestMethod]
		public void TestStack()
		{

		}

		[TestMethod]
		public void TestLinkedList()
		{
			LinkedList<Document> linkList = new LinkedList<Document>();
			linkList.GetType();
		}

		[TestMethod]
		public void TestSortList()
		{
			SortedList<string, string> sl = new SortedList<string, string>();

			foreach(KeyValuePair<string,string> keyValue in sl)
			{
				
			}
			foreach(string s in sl.Keys)
			{
				
			}
			foreach(string s in sl.Values)
			{
				
			}
		}

		[TestMethod]
		public void TestLookUp()
		{
			List<string> list = new List<string>();
			ILookup<string,string> lookUp = list.ToLookup(s => s);
		}

		[TestMethod]
		public void TestSortedDict()
		{
			SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
		}

		[TestMethod]
		public void TestSet()
		{
			HashSet<string> hashset = new HashSet<string>() { "ss", "ssa", "ssr" };
			HashSet<string> set = new HashSet<string>() { "aq", "231", "ss" };
			hashset.Overlaps(set);
			SortedSet<string> ss = new SortedSet<string>(hashset) { "212", "666" };
			ss.UnionWith(set);
			ss.UnionWith(hashset);
			foreach(var i in ss)
			{
				Console.WriteLine(i);
			}
		}

		[TestMethod]
		public void TestObserveCollection()
		{
			ObservableCollection<string> oc = new ObservableCollection<string>();
			oc.CollectionChanged += DataChanged;
			oc.Add("wangwu");
			oc.Insert(0, "zhangsan");
			oc.Move(0, 1);
			oc.Remove("wangwu");	
			oc.Clear();
		}

		private void DataChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Console.WriteLine(e.Action.ToString());
			
		}

		[TestMethod]
		public void TestBitArray()
		{
			BitArray bitArray = new BitArray(32);
			bitArray.SetAll(true);
			bitArray.Not();
			bitArray.Xor(new BitArray(32));
			BitVector32 vector32 = new BitVector32(32);
		}

		[TestMethod]
		public void TestImmuable()
		{
			ImmutableArray<string> immu = ImmutableArray.Create<string>();
			List<string> list = new List<string>() { "sss", "awq" };
			ImmutableArray<string>  ia = list.ToImmutableArray();
		}

		[TestMethod]
		public void TestConcurrentCollection()
		{
			ConcurrentStack<string> cs = new ConcurrentStack<string>();
			ConcurrentQueue<string> cq = new ConcurrentQueue<string>();
			ConcurrentDictionary<string, string> cd = new ConcurrentDictionary<string, string>();
			ConcurrentBag<string> cb = new ConcurrentBag<string>();
			BlockingCollection<string> vc = new BlockingCollection<string>();
			//vc.
		}
	}

	public class Document
	{
		public string Name{ get;set;}
		public string Author{ get; set; }
		public string Description{ get; set; }
	}

	public class DocumentManager
	{
		private Queue<Document> queue = new Queue<Document>(50);
		private Stack<Document> stack = new Stack<Document>(50);

		public Document GetDocument()
		{
			lock(this)
			{
				if(queue.Count>0)
				{
					var doc = queue.Dequeue();
					return doc;
				}		
				return null;
			}
		}

		public void SetDocument(Document doc)
		{
			lock(this)
			{
				Console.WriteLine($"添加文档{doc.Name}，线程："+Thread.CurrentThread.ManagedThreadId);
				queue.Enqueue(doc);
			}
		}

		public bool HasDocument()
		{
			return queue.Count > 0;
		}
	}

	public class ProcessDocument
	{
		private DocumentManager manager;
		public ProcessDocument(DocumentManager manager)
		{
			this.manager = manager;
		}
		public void Run()
		{
			Document doc;
			while(true)
			{
				if(manager.HasDocument())
				{
					doc = manager.GetDocument();
					Console.WriteLine($"取出文档{doc.Name}，线程：" + Thread.CurrentThread.ManagedThreadId);
				}
				
			}
		}
	}
}
