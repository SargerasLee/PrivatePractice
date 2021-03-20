using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DataStructure.BinaryTree
{
	public class BinaryTreeTool
	{
		#region 先，中，后序遍历
		public void PreOrderTraversal1(TreeNode root)
		{
			if (root == null) return;
			Console.WriteLine(root.Value);
			if (root.Left != null)
			{
				PreOrderTraversal1(root.Left);
			}
			if (root.Right!=null)
			{
				PreOrderTraversal1(root.Right);
			}
		}
		public void PreOrderTraversal2(TreeNode root)
		{
			/**
			 * 前序遍历按照根、左、右的顺序访问节点，非递归实现要使用到栈，我们先访问本节点，然后将本节点入栈，
			 * 用于下次遍历此节点的右子树，然后继续访问本节点的左孩子，重复，直到本节点的左孩子为NULL，
			 * 此时就可以退栈，直到栈顶节点的右孩子不为空，下一个要访问的节点就是栈顶元素的右孩子，将栈顶元素弹出。
			 * 如果找不到则遍历结束。
			 */
			Stack<TreeNode> stack = new Stack<TreeNode>();
			while(root!=null)
			{
				while(root!=null)
				{
					Console.WriteLine(root.Value);
					stack.Push(root);
					root = root.Left;	
				}
				while (stack.Count > 0 && stack.Peek().Right == null)
					stack.Pop();
				if (stack.Count <= 0)
					break;
				root = stack.Peek().Right;
				stack.Pop();
			}
		}
		public void InOrderTraversal1(TreeNode root)
		{
			if (root == null) return;	
			if (root.Left != null)
			{
				InOrderTraversal1(root.Left);
			}
			Console.WriteLine(root.Value);
			if (root.Right != null)
			{
				InOrderTraversal1(root.Right);
			}
		}
		public void InOrderTraversal2(TreeNode root)
		{
			/**
			 * 中序遍历的顺序是左、根、右，中序遍历的非递归实现和前序遍历差不多，只是在中序遍历中我们先不要访问
			 * 本节点，直接入栈，继续访问本节点的左孩子，当回退时再访问本节点。
			 */
			Stack<TreeNode> stack = new Stack<TreeNode>();
			while (root != null)
			{
				while (root != null)
				{
					stack.Push(root);
					root = root.Left;
				}
				while (stack.Count > 0)
				{
					TreeNode top = stack.Pop();
					Console.WriteLine(top.Value);
					if (top.Right!=null)
					{
						root = top.Right;
						break;
					}
				}
			}
		}
		public void PostOrderTraversal1(TreeNode root)
		{
			if (root == null) return;
			if (root.Left != null)
			{
				PostOrderTraversal1(root.Left);
			}
			if (root.Right != null)
			{
				PostOrderTraversal1(root.Right);
			}
			Console.WriteLine(root.Value);
		}
		[Obsolete]
		public void PostOrderTraversal2(TreeNode root)
		{
			/**
			 * 二叉树后序遍历的非递归实现和前两种遍历的实现不同，由于访问的顺序是左、右、根，
			 * 只有当本节点的左子树和右子树被访问后才能访问本节点，
			 * 我们用映射map记录每个节点入栈的次数，出栈时，当节点的入栈次数为1， 说明本节点的左子树被访问完了，
			 * 但是其右子树还没有被访问，此时不能访问本节点，让本节点再次入栈，访问本节点的右子树，
			 * 出栈时，当节点的入栈次数为2，说明本节点的左右子树都被访问完了，可以访问本节点。
			 */
			Dictionary<TreeNode, int> dict = new Dictionary<TreeNode, int>();//记录每个节点入栈的次数
			Stack<TreeNode> stack = new Stack<TreeNode>();
			while (root!=null)
			{
				while (root!=null)
				{
					stack.Push(root);//先不访问，直接入栈
					dict[root]++;//节点root入栈的次数加一
					root = root.Left;//继续访问左子树
				}
				while (stack.Count>0)
				{
					TreeNode tmp = stack.Peek();
					stack.Pop();
					//如果本节点入栈过两次则访问
					if (dict[tmp] == 2)
						Console.WriteLine(tmp.Value);
					else
					{//本节点的右子树还没有被访问
						stack.Push(tmp);//再次入栈
						dict[tmp]++;//入栈次数加一
						root = tmp.Right;//访问本节点的右子树
					}
					if (root != null) break;
				}
			}
		}
		public void PostOrderTraversal3(TreeNode root)
		{
			/**
			 * 双栈法
			 */
			if (root == null) return;
			Stack<TreeNode> s1 = new Stack<TreeNode>();
			Stack<TreeNode> s2 = new Stack<TreeNode>();
			s1.Push(root);
			while(s1.Count>0)
			{
				TreeNode tmp = s1.Pop();
				s2.Push(tmp);
				if (tmp.Left != null)
					s1.Push(tmp.Left);
				if (tmp.Right != null)
					s1.Push(tmp.Right);
			}

			foreach(TreeNode node in s2)
			{
				Console.WriteLine(node.Value);
			}
		}
		public void PostOrderTraversal4(TreeNode root)
		{
			Stack<TreeNode> stack = new Stack<TreeNode>();
			TreeNode cur = root;
			/* 用来记录最新出栈的节点，
			 * 如果当前节点的右儿子与flag相同，说明当前节点右子树已完成遍历
			 */
			TreeNode flag = null;
			while (cur != null)
			{
				stack.Push(cur);
				cur = cur.Left;
			}
			while (stack.Count>0)
			{
				cur = stack.Pop();
				if (cur.Right == null || cur.Right == flag)
				{
					Console.WriteLine(cur.Value);
					flag = cur;
				}
				else
				{
					stack.Push(cur);
					cur = cur.Right;
					while (cur != null)
					{
						stack.Push(cur);
						cur = cur.Left;
					}
				}
			}
		}
		public void PostOrderTraversal5(TreeNode root)
		{
			Stack<TreeNode> stack = new Stack<TreeNode>();
			TreeNode cur = root;
			TreeNode prior = null;
			while (cur != null || stack.Count>0)
			{
				if (cur != null)
				{
					stack.Push(cur);
					cur = cur.Left;
				}
				else
				{
					cur = stack.Pop();
					if (cur.Right == null || cur.Right == prior)
					{
						Console.WriteLine(cur.Value);
						prior = cur;
						cur = null;
					}
					else
					{
						stack.Push(cur);
						cur = cur.Right;
						stack.Push(cur);
						cur = cur.Left;
					}
				}
			}
		}

		#endregion

		#region 层次遍历
		//用队列

		public void LevelTraversal(TreeNode root)
		{
			Queue<TreeNode> queue = new Queue<TreeNode>();
			if (root == null) return;
			queue.Enqueue(root);
			while (queue.Count>0)
			{
				TreeNode tmp = queue.Dequeue();
				Console.WriteLine(tmp.Value);
				if(tmp.Left!=null)
				{
					queue.Enqueue(tmp.Left);
				}
				if(tmp.Right!=null)
				{
					queue.Enqueue(tmp.Right);
				}			
			}
		}
		#endregion

	}

	public class TreeNode
	{
		public int Value { get; set; }
		public TreeNode Left { get; set; }
		public TreeNode Right{ get; set; }

		public TreeNode() { }

		public TreeNode(int value,TreeNode left,TreeNode right)
		{
				Value = value;
				Left = left;
				Right = right;
		}
	}

	#region AVL树
	//任何结点的两个子树的高度差别最大为1
	//	LL    RR   LR =>RR+LL    RL=>LL+RR
	public class AVLTree<K> where K : IComparable<K>
	{
		private AVLTreeNode<K> mRoot;    // 根结点

		public AVLTree()
		{
			mRoot = null;
		}
		// AVL树的节点(内部类)
		public class AVLTreeNode<T> where T : IComparable<T>
		{
			public T Key { get; set; }            // 关键字(键值)
			public int Height { get; set; }       // 高度
			public AVLTreeNode<T> Left { get; set; }    // 左孩子
			public AVLTreeNode<T> Right { get; set; }    // 右孩子
			public AVLTreeNode(T key, AVLTreeNode<T> left, AVLTreeNode<T> right)
			{
				Key = key;
				Left = left;
				Right = right;
				Height = 0;
			}
			public AVLTreeNode(T key)
			{
				Key = key;
				Left = null;
				Right = null;
				Height = 0;
			}
		}

		private int Height(AVLTreeNode<K> tree)
		{
			if (tree != null)
				return tree.Height;
			return 0;
		}

		public int Height()
		{
			return Height(mRoot);
		}

		private int Max(int a, int b)
		{
			return a > b ? a : b;
		}
		private int Compare<K>(K k1,K k2)
		{
			return (k1 as IComparable<K>).CompareTo(k2);
		}

		private AVLTreeNode<K> Search(AVLTreeNode<K> tree,K key)
		{
			if (tree == null) return null;

			if(Compare(tree.Key,key)>0)
			{
				return Search(tree.Right, key);
			}
			else if(Compare(tree.Key, key) <0)
			{
				return Search(tree.Left, key);
			}
			else
			{
				return tree;
			}
		}

		public AVLTreeNode<K> Search(K key)
		{
			return Search(mRoot, key);
		}

		public void Insert(K key)
		{
			Insert(key, mRoot);
		}

		public void Remove(K key)
		{
			Remove(key, mRoot);
		}

		public AVLTreeNode<K> Insert(K x, AVLTreeNode<K> t)
		{
			if (t == null)
			{
				return new AVLTreeNode<K>(x);
			}
			//先比较 是插左边还是插右边
			int compareResult = x.CompareTo(t.Key);
			if (compareResult < 0)
			{//插到左子树上
				t.Left = Insert(x, t.Left);
				//插入之后要判断是否打破了平衡，因为插入的是左子树，
				// 只有左子树才会打破平衡，用左子树的高减去右子树的高
				if (Height(t.Left) - Height(t.Right) == 2)
				{
					//如果等于2，说明平衡被打破了，需要进行调整。就看选择什么方法调整
					if (x.CompareTo(t.Left.Key) < 0)
					{
						//如果x小于t的左子树的值，那么x会被插到t的左子树的左子树上，符合LL 用右旋转调整。
						t = RightRotate(t);
					}
					else
					{
						//如果x大于t的左子树的值，则会被插到t的左子树的右子树上，符合LR，用先左旋转后右旋转来矫正。
						t = LeftAndRightRotate(t);
					}
				}
			}
			else if (compareResult > 0)
			{//插到右子树上，逻辑和上面一样。
				t.Right = Insert(x, t.Right);
				if (Height(t.Right) - Height(t.Left) == 2)
				{
					if (x.CompareTo(t.Right.Key) > 0)
					{
						t = LeftRotate(t);
					}
					else
					{
						t = RightAndLeftRotate(t);
					}
				}
			}
			else
			{
				//已经有这个值了
			}
			t.Height = Math.Max(Height(t.Left), Height(t.Right)) + 1;
			return t;
		}

		private AVLTreeNode<K> Remove(K x, AVLTreeNode<K> t)
		{
			if (t == null)
				return null;
			int compareResult = x.CompareTo(t.Key);
			if (compareResult < 0)
			{
				t.Left = Remove(x, t.Left);
				//完了之后验证该子树是否平衡
				if (t.Right != null)
				{        //若右子树为空，则一定是平衡的，此时左子树相当对父节点深度最多为1, 所以只考虑右子树非空情况
					if (t.Left == null)
					{     //若左子树删除后为空，则需要判断右子树
						if (Height(t.Right) - t.Height == 2)
						{
							AVLTreeNode<K> k = t.Right;
							if (k.Right != null)
							{        //右子树存在，按正常情况单旋转
								t = LeftRotate(t);
							}
							else
							{                      //否则是右左情况，双旋转
								t = RightAndLeftRotate(t);
							}
						}
					}
					if (t.Left != null)
					{   //否则判断左右子树的高度差
						//左子树自身也可能不平衡，故先平衡左子树，再考虑整体
						AVLTreeNode<K> k = t.Left;
						//删除操作默认用右子树上最小节点补删除的节点
						//k的左子树高度不低于k的右子树
						if (k.Right != null)
						{
							if (Height(k.Left) - Height(k.Right) == 2)
							{
								AVLTreeNode<K> m = k.Left;
								if (m.Left != null)
								{     //左子树存在，按正常情况单旋转
									k = RightRotate(k);
								}
								else
								{                      //否则是左右情况，双旋转
									k = LeftAndRightRotate(k);
								}
							}
						}
						else
						{
							if (Height(k.Left) - k.Height == 2)
							{
								AVLTreeNode<K> m = k.Left;
								if (m.Left != null)
								{     //左子树存在，按正常情况单旋转
									k = RightRotate(k);
								}
								else
								{                      //否则是左右情况，双旋转
									k = LeftAndRightRotate(k);
								}
							}
						}
						if (Height(t.Right) - Height(t.Left) == 2)
						{
							//右子树自身一定是平衡的，左右失衡的话单旋转可以解决问题
							t = LeftRotate(t);
						}
					}
				}
				//完了之后更新height值
				t.Height = Math.Max(Height(t.Left), Height(t.Right)) + 1;
			}
			else if (compareResult > 0)
			{
				t.Right = Remove(x, t.Right);
				//下面验证子树是否平衡
				if (t.Left != null)
				{         //若左子树为空，则一定是平衡的，此时右子树相当对父节点深度最多为1
					t = BalanceChild(t);
				}
				//完了之后更新height值
				t.Height = Math.Max(Height(t.Left), Height(t.Right)) + 1;
			}
			else if (t.Left != null && t.Right != null)
			{
				//默认用其右子树的最小数据代替该节点的数据并递归的删除那个节点
				AVLTreeNode<K> min = t.Right;
				while (min.Left != null)
				{
					min = min.Left;
				}
				//t.Key = findMin(t.right).Key;
				t.Key = min.Key;
				t.Right = Remove(t.Key, t.Right);
				t = BalanceChild(t);
				//完了之后更新height值
				t.Height = Math.Max(Height(t.Left), Height(t.Right)) + 1;
			}
			else
			{
				t = (t.Left != null) ? t.Left : t.Right;
			}
			return t;
		}

		private AVLTreeNode<K> BalanceChild(AVLTreeNode<K> t)
		{
			if (t.Right == null)
			{        //若右子树删除后为空，则只需判断左子树与根的高度差
				if (Height(t.Left) - t.Height == 2)
				{
					AVLTreeNode<K> k = t.Left;
					if (k.Left != null)
					{
						t = RightRotate(t);
					}
					else
					{
						t = LeftAndRightRotate(t);
					}
				}
			}
			else
			{              //若右子树删除后非空，则判断左右子树的高度差
						   //右子树自身也可能不平衡，故先平衡右子树，再考虑整体
				AVLTreeNode<K> k = t.Right;
				//删除操作默认用右子树上最小节点（靠左）补删除的节点

				if (k.Left != null)
				{
					if (Height(k.Right) - Height(k.Left) == 2)
					{
						AVLTreeNode<K> m = k.Right;
						if (m.Right != null)
						{        //右子树存在，按正常情况单旋转
							k = LeftRotate(k);
						}
						else
						{                      //否则是右左情况，双旋转
							k = RightAndLeftRotate(k);
						}
					}
				}
				else
				{
					if (Height(k.Right) - k.Height == 2)
					{
						AVLTreeNode<K> m = k.Right;
						if (m.Right != null)
						{        //右子树存在，按正常情况单旋转
							k = LeftRotate(k);
						}
						else
						{                      //否则是右左情况，双旋转
							k = RightAndLeftRotate(k);
						}
					}
				}
				//左子树自身一定是平衡的，左右失衡的话单旋转可以解决问题
				if (Height(t.Left) - Height(t.Right) == 2)
				{
					t = RightRotate(t);
				}
			}
			return t;
		}

		private AVLTreeNode<K> RightRotate(AVLTreeNode<K> t)
		{
			AVLTreeNode<K> newTree = t.Left;
			t.Left = newTree.Right;
			newTree.Right = t;
			t.Height = Math.Max(Height(t.Left), Height(t.Right)) + 1;
			newTree.Height = Math.Max(Height(newTree.Left), Height(newTree.Right)) + 1;
			return newTree;
		}

		private AVLTreeNode<K> LeftRotate(AVLTreeNode<K> t)
		{
			//左旋，针对右右型，t为当前根节点    ****改变 当前根跟 和 其 右孩子的指向和高度****
			AVLTreeNode<K> newRoot = t.Right;//新的根节点 为 t  的右孩子，此时  t  已不再是根节点
			t.Right = newRoot.Left;//新根节点 的左孩子 变成  老根节点  的右孩子
			newRoot.Left = t;//新根节点的 左孩子为 t ，完成 右旋
			t.Height = Math.Max(Height(t.Left), Height(t.Right)) + 1;//求出子树   t  的高度（子树最大高度+1）
			newRoot.Height = Math.Max(Height(newRoot.Left), Height(newRoot.Right)) + 1;//求出新树的高度（子树最大高度+1）
			//因为 最底下是第一层，根为最高层，底下节点高度不受影响
			return newRoot;
		}

		private AVLTreeNode<K> LeftAndRightRotate(AVLTreeNode<K> t)
		{
			t.Left = LeftRotate(t.Left);
			return RightRotate(t);
		}

		private AVLTreeNode<K> RightAndLeftRotate(AVLTreeNode<K> t)
		{
			t.Right = RightRotate(t.Right);
			return LeftRotate(t);
		}

	}
	#endregion
}
