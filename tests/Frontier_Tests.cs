namespace NPuzzleTests
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NUnit.Framework;

	[TestFixture]
	public class TestFrontier
	{
		[Test]
                public void TestFrontierCreation()
		{


		}

		private Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > GetFrontier()
		{
			Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > frontier = new Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> >();
			 //SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = new SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > >();

			Heuristics.HeuristicFunction<int[],int,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;

			List<SearchTreeNode.NPuzzleNode<int[],int,int> > nodes = TestNode.CreateNodesForTesting();

			//! md = 12
			int[] s7 = {6, 4, 3, 1, 9, 5, 8, 7, 2};
			SearchTreeNode.NPuzzleNode<int[],int,int>  node7 = new SearchTreeNode.NPuzzleNode<int[],int,int> (s7);
			nodes.Add(node7);

			//! 2 + 2 + 1 + 1 + 2 = 8
			int[] s8 = {1, 6, 3, 8, 4, 5, 7, 2, 9};
			SearchTreeNode.NPuzzleNode<int[],int,int>  node8 = new SearchTreeNode.NPuzzleNode<int[],int,int> (s8);
			nodes.Add(node8);
			foreach(var node in nodes)
			{
				int md = handler(node);
				frontier.Append(md, node);
			}
			return frontier;
		}

		[Test]
                public void TestFrontierContainsKey()
		{
			Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = f.GetFrontier();

			Assert.True(frontier.ContainsKey(0));
			Assert.True(frontier.ContainsKey(1));
                        Assert.True(frontier.ContainsKey(2));
			Assert.True(frontier.ContainsKey(3));
			Assert.True(frontier.ContainsKey(4));
			Assert.True(frontier.ContainsKey(5));
			Assert.True(frontier.ContainsKey(6));
			Assert.True(frontier.ContainsKey(8));
			Assert.True(frontier.ContainsKey(12));
			//Assert.False(frontier.ContainsKey(13));
			//Assert.False(frontier.ContainsKey(7));
                        frontier.Remove(0);
			Assert.AreEqual(frontier[1].Count, 2);
			frontier[1].RemoveAt(0);
			Assert.AreEqual(frontier[1].Count, 1);
			frontier.Remove(1);
			Assert.False(frontier.ContainsKey(0));
			Assert.False(frontier.ContainsKey(1));
		}

		[Test]
		public void TestFrontierCount()
		{
                        Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = f.GetFrontier();


			Assert.AreEqual(frontier.Count, 9);
			Assert.AreEqual(frontier[0].Count, 1);
			Assert.AreEqual(frontier[1].Count, 2);
			Assert.AreEqual(frontier[12].Count, 1);
			frontier[12].RemoveAt(0);
			Assert.AreEqual(frontier[12].Count, 0);
			frontier.Remove(12);
			Assert.AreEqual(frontier.Count, 8);
		}

		[Test]
		public void TestFrontierIndexOfKey()
		{

//			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = GetFrontier();
                        Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = f.GetFrontier();


			//! there should be keys of 0, 1, 2, 4, 5, 6, 8, and 12. 
			Assert.AreEqual(frontier.IndexOfKey(3), 3);
			Assert.AreEqual(frontier.IndexOfKey(8), 7);
			Assert.AreEqual(frontier.IndexOfKey(12), 8);
			Assert.True(frontier.Remove(0));
			Assert.AreEqual(frontier.IndexOfKey(3), 2);
			Assert.AreEqual(frontier.IndexOfKey(8), 6);
			Assert.AreEqual(frontier.IndexOfKey(12), 7);
		}

		[Test]
                public void TestFrontierKeys()
	        {
                       Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = f.GetFrontier();


//			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = GetFrontier();
			IList<int> keys = frontier.Keys;
			Assert.AreEqual(keys[0],0);
			frontier.Remove(0);
			Assert.AreEqual(keys[0],1);
			frontier.Remove(1);
			Assert.AreEqual(keys[0],2);
		}

		[Test]
		/*! Test removing elements from the frontier
		 */
		public void TestCount()
		{
                      Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = f.GetFrontier();

			Assert.AreEqual(f.Count(), 10);
			SearchTreeNode.NPuzzleNode<int[],int,int>  node = f.Pop();
			int[] s = {1,2,3,4,5,6,7,8,9};
			Assert.AreEqual(node.state, s);
			Assert.AreEqual(f.Count(), 9);
                        int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			//! md = 1
			int[] s7 = {1, 2, 3, 4, 5, 9, 7, 8, 6};
			//! 2, md = 2
			int[] s2 = {1, 2, 3, 4, 9, 6, 7, 5, 8};
			node = f.Pop();
			Assert.AreEqual(node.state, s1);
			Assert.AreEqual(f.Count(), 8);

			node = f.Pop();
			Assert.AreEqual(node.state, s7);
			Assert.AreEqual(f.Count(), 7);

			node = f.Pop();
			Assert.AreEqual(node.state, s2);
			Assert.AreEqual(f.Count(), 6);



		}

		[Test]
		public void TestPop()
		{
			Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			int c = f.Count();
			int i = 0; 

			SearchTreeNode.NPuzzleNode<int[],int,int>  node;
			while (f.Count() > 0)
			{
				node = f.Pop();
				i++;
			}
			Assert.AreEqual(i, c);
			Assert.IsNull(f.Pop());

		}

	}
}
