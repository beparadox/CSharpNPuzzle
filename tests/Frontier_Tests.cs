namespace NPuzzleTests
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Heuristics;
	using Problem;

	[TestFixture]
	public class TestPriorityQueue
	{
		[Test]
                public void TestPriorityQueueCreation()
		{


		}

		private Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > GetPriorityQueue()
		{

			// Heuristics.HeuristicFunction<NPuzzleState<int[]>,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
                        Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance; 
			Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> >(handler);
			 //SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = new SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > >();


			List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > nodes = TestNode.CreateNodesForTesting();

			//! md = 12
			int[] s7 = {6, 4, 3, 1, 9, 5, 8, 7, 2};
			NPuzzleState<int[]> s7State = new NPuzzleState<int[]>(s7);
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>  node7 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int> (s7State);
			nodes.Add(node7);

			//! 2 + 2 + 1 + 1 + 2 = 8
			int[] s8 = {1, 6, 3, 8, 4, 5, 7, 2, 9};
			NPuzzleState<int[]> s8State = new NPuzzleState<int[]>(s8);
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>  node8 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int> (s8State);
			nodes.Add(node8);
			foreach(var node in nodes)
			{
				//int md = handler(node);
				frontier.Append(node);
			}
			return frontier;
		}

		[Test]
                public void TestPriorityQueueContainsKey()
		{
			Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > f = GetPriorityQueue();
			SortedList<int, List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > > frontier = f.GetPriorityQueue();

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
		public void TestPriorityQueueCount()
		{
                        Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > f = GetPriorityQueue();
			SortedList<int, List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > > frontier = f.GetPriorityQueue();


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
		public void TestPriorityQueueIndexOfKey()
		{

//			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = GetPriorityQueue();
                        Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > f = GetPriorityQueue();
			SortedList<int, List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > > frontier = f.GetPriorityQueue();


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
                public void TestPriorityQueueKeys()
	        {
                       Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > f = GetPriorityQueue();
			SortedList<int, List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > > frontier = f.GetPriorityQueue();


//			SortedList<int, List<SearchTreeNode.NPuzzleNode<int[],int,int> > > frontier = GetPriorityQueue();
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
                      Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > f = GetPriorityQueue();
			SortedList<int, List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > > frontier = f.GetPriorityQueue();

			Assert.AreEqual(f.Count(), 10);
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>  node = f.Pop();
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
			Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > f = GetPriorityQueue();
			int c = f.Count();
			int i = 0; 
			int[] heurs = new int[f.Count()];

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>  node;
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

		private	Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> GetPriorityQueueToo()
		{

                        Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance; 
			Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> >(handler);
			int size = 9;
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[] nodeArray = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[100];
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node; 
			for (int i = 0; i < 100; i++) 
			{
				NPuzzleState<int[]> istate = NPuzzleUtils.GenerateInitState(size);
				node = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(istate);
				nodeArray[i] = node;
				//int heur = NPuzzleHeuristics.ManhattanDistance(node);
				frontier.Append(node);
			}

			return frontier;
		}

		[Test]
         	public void TestAppendToo()
		{

                        Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance; 
			Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> >(handler);
			int size = 9;
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[] nodeArray = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[100];
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node; 
			for (int i = 0; i < 100; i++) 
			{
				NPuzzleState<int[]> istate = NPuzzleUtils.GenerateInitState(size);
				node = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(istate);
				nodeArray[i] = node;
				//int heur = NPuzzleHeuristics.ManhattanDistance(node);
				frontier.Append(node);

			}

			for (int i = 0; i < 100; i++)
			{
				int heur = NPuzzleHeuristics.ManhattanDistance(nodeArray[i]);
				Assert.True(frontier.InPriorityQueue(nodeArray[i]));
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
                public void TestGetIncumbent()
		{

			Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
                        Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> >(handler);
			int size = 9;
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[] nodeArray = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[100];
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node; 
			for (int i = 0; i < 100; i++) 
			{
				NPuzzleState<int[]> istate = NPuzzleUtils.GenerateInitState(size);
				node = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(istate);
				nodeArray[i] = node;
				int heur = NPuzzleHeuristics.ManhattanDistance(node);
				frontier.Append(node);

			}

			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual(nodeArray[i], frontier.GetIncumbent(nodeArray[i]));
			}
		}

		[Test]
                public void RemoveIncumbent()
		{
			Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
			Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int> >(handler);
			int size = 9;
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[] nodeArray = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>[100];
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node; 
			for (int i = 0; i < 100; i++) 
			{
				NPuzzleState<int[]> istate = NPuzzleUtils.GenerateInitState(size);
				node = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(istate);
				nodeArray[i] = node;
				int heur = NPuzzleHeuristics.ManhattanDistance(node);
				frontier.Append(node);

			}

                        for (int i = 0; i < 100; i++)
			{
				frontier.RemoveIncumbent(nodeArray[i]);
				Assert.False(frontier.InPriorityQueue(nodeArray[i]));
			}


		}


		[Test]
                public void TestHashSet()
		{
			HashSet<NPuzzleState<int[]>> explored = new HashSet<NPuzzleState<int[]>>();
			int size = 9, num = 100;
			NPuzzleState<int[]>[] states = new NPuzzleState<int[]>[num];
			NPuzzleState<int[]> state;
			int[] goal = {1,2,3,4,5,6,7,8,9};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			for (int i = 0; i < num; i++) 
			{
                                state = NPuzzleUtils.GenerateInitState(size);
				states[i] = state;
				explored.Add(state);
			}
			Assert.False(explored.Contains(goalState));
			explored.Add(goalState);

			for (int i = 0; i < num; i++)
			{
				Assert.True(explored.Contains(states[i]));
			}
			Assert.True(explored.Contains(goalState));

		}

		[Test] 
		public void TestListContains()
		{
			int[] s1 = {1,2,3,4,5,6,7,8,9};
			NPuzzleState<int[]> s1State = new NPuzzleState<int[]>(s1);
			int[] s2 = {1,2,3,4,5,9,7,8,6};

			NPuzzleState<int[]> s2State = new NPuzzleState<int[]>(s2);
			int[] s3 = {1,2,9,4,5,3,7,8,6};

			NPuzzleState<int[]> s3State = new NPuzzleState<int[]>(s3);
			int[] s4 = {1,2,9,4,5,3,7,8,6};

			NPuzzleState<int[]> s4State = new NPuzzleState<int[]>(s4);
			List<NPuzzleState<int[]>> list = new List<NPuzzleState<int[]> >();
			list.Add(s1State);
			Assert.True(list.Contains(s1State));
			Assert.False(list.Contains(s2State));
			list.Add(s2State);
			Assert.True(list.Contains(s2State));
			Assert.False(list.Contains(s3State));
			list.Add(s3State);
			Assert.True(list.Contains(s3State));

			if (!list.Contains(s4State)) 
			{
				Console.WriteLine("Write Line: s4 not in list");

			}
		}
	}
}
