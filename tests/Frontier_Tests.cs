namespace NPuzzleTests
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Heuristics;

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

			node = f.Pop();
			Assert.AreEqual(f.Count(), 5);
			node = f.Pop();
			Assert.AreEqual(f.Count(), 4);
			node = f.Pop();
			Assert.AreEqual(f.Count(), 3);
			node = f.Pop();
			Assert.AreEqual(f.Count(), 2);
			node = f.Pop();
			Assert.AreEqual(f.Count(), 1);
			node = f.Pop();
			Assert.AreEqual(f.Count(), 0);






		}

		[Test]
		public void TestPop()
		{
			Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > f = GetFrontier();
			int c = f.Count();
			int i = 0; 
			int[] heurs = new int[f.Count()];

			SearchTreeNode.NPuzzleNode<int[],int,int>  node;
			while (f.Count() > 0)
			{
				node = f.Pop();
				heurs[i] = NPuzzleHeuristics.ManhattanDistance(node);
				i++;
			}

			for (int j = 0; j < i - 1; j++) 
			{
				Assert.True(heurs[j] <= heurs[j + 1]);
			}
			Assert.AreEqual(i, c);
			Assert.IsNull(f.Pop());

		}

		[Test]
         	public void TestAppendToo()
		{

			Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> > frontier = new Search.Frontier<int, SearchTreeNode.NPuzzleNode<int[],int,int> >();
			int size = 9;
			SearchTreeNode.NPuzzleNode<int[],int,int>[] nodeArray = new SearchTreeNode.NPuzzleNode<int[],int,int>[100];
			SearchTreeNode.NPuzzleNode<int[],int,int> node; 
			for (int i = 0; i < 100; i++) 
			{
				int[] istate = NPuzzleUtils.GenerateInitState(size);
				node = new SearchTreeNode.NPuzzleNode<int[],int,int>(istate);
				nodeArray[i] = node;
				int heur = NPuzzleHeuristics.ManhattanDistance(node);
				frontier.Append(heur, node);

			}

			for (int i = 0; i < 100; i++)
			{
				int heur = NPuzzleHeuristics.ManhattanDistance(nodeArray[i]);
				Assert.True(frontier.In(heur, nodeArray[i]));
			}
			int j = 0;
			int[] heurArray = new int[100];
 
			while (frontier.Count() > 0)
			{
				node = frontier.Pop();
				heurArray[j] = NPuzzleHeuristics.ManhattanDistance(node);
				j++;
			}

			for (j = 0; j < 99; j++) 
			{
				Assert.True(heurArray[j] <= heurArray[j + 1]);
			}

		}

		[Test]
                public void PlayWithSortedList()
		{
			SortedList<int, List<int> > slist = new SortedList<int, List<int> >();
			int[] keys = {1, 2, 3, 4, 5};
			int[] values = new int[25];
			for (int i = 1; i < 6; i++)
			{

			}
		}

		[Test]
                public void TestHashSet()
		{
			HashSet<int[]> explored = new HashSet<int[]>();
			int size = 9, num = 100;
			int[][] states = new int[num][];
			int[] state;
			int[] goal = {1,2,3,4,5,6,7,8,9};
			for (int i = 0; i < num; i++) 
			{
                                state = NPuzzleUtils.GenerateInitState(size);
				states[i] = state;
				explored.Add(state);
			}
			Assert.False(explored.Contains(goal));
			explored.Add(goal);

			for (int i = 0; i < num; i++)
			{
				Assert.True(explored.Contains(states[i]));
			}
			Assert.True(explored.Contains(goal));

		}

		[Test] 
		public void TestListContains()
		{
			int[] s1 = {1,2,3,4,5,6,7,8,9};
			int[] s2 = {1,2,3,4,5,9,7,8,6};
			int[] s3 = {1,2,9,4,5,3,7,8,6};
			int[] s4 = {1,2,9,4,5,3,7,8,6};
			List<int[]> list = new List<int[]>();
			list.Add(s1);
			Assert.True(list.Contains(s1));
			Assert.False(list.Contains(s2));
			list.Add(s2);
			Assert.True(list.Contains(s2));
			Assert.False(list.Contains(s3));
			list.Add(s3);
			Assert.True(list.Contains(s3));

			if (!list.Contains(s4)) 
			{
				Console.WriteLine("Write Line: s4 not in list");

			}
		}
	}
}
