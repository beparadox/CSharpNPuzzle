namespace NPuzzleTests 
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NUnit.Framework;

	[TestFixture]
	public class SearchTests 
	{
		[Test]
			/*!
			 * Test using a delegate
			 */
                public void TestDelegate()
		{
			Heuristics.HeuristicFunction<int[],int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;

		}

		[Test]
                public void TestSearchClassCreation()
		{
			int size = 9;
			Problem.NPuzzleProblem problem = NPuzzleUtils.CreateProblem(size);
			Heuristics.HeuristicFunction<int[],int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
			Search.BestFirstGraphSearch<int[], int, int> bfgs = new Search.BestFirstGraphSearch<int[], int, int>(problem, handler);
		}

		private SortedList<int, List<SearchTreeNode.NPuzzleNode> > GetFrontier()
		{
			 SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = new SortedList<int, List<SearchTreeNode.NPuzzleNode> >();

			Heuristics.HeuristicFunction<int[],int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;

			List<SearchTreeNode.NPuzzleNode> nodes = TestNode.CreateNodesForTesting();

			//! md = 12
			int[] s7 = {6, 4, 3, 1, 9, 5, 8, 7, 2};
			SearchTreeNode.NPuzzleNode node7 = new SearchTreeNode.NPuzzleNode(s7);
			nodes.Add(node7);

			//! 2 + 2 + 1 + 1 + 2 = 8
			int[] s8 = {1, 6, 3, 8, 4, 5, 7, 2, 9};
			SearchTreeNode.NPuzzleNode node8 = new SearchTreeNode.NPuzzleNode(s8);
			nodes.Add(node8);
			foreach(var node in nodes)
			{
				int md = handler(node);
				//! if it contains the key,
				//! there should already exist a
				//! list at that index
				if (frontier.ContainsKey(md))
				{
					//! get the list at key md
					frontier[md].Add(node);

				} else {
					//! create a new list
					//! add the node to it
					//! set it in the sorted list at key md
					List<SearchTreeNode.NPuzzleNode> list = new List<SearchTreeNode.NPuzzleNode>();
					list.Add(node);
					frontier.Add(md, list);
				}
			}

			return frontier;
		}

		[Test]
                public void TestFrontierContainsKey()
		{
			SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = GetFrontier();
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
			SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = GetFrontier();
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

			SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = GetFrontier();
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
			SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = GetFrontier();
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
		public void TestRemoveFromFrontier()
		{
			SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = GetFrontier();
			IList<int> keys = frontier.Keys;
                        // to remove an  
			// frontier[keys[0]][0];
			// frontier[keys[0]].Remove
			// if (frontier[keys[0]].Count == 0) frontier.Remove(keys[0]);


		}

		[Test]
                public void TestHashSet()
		{

		}

		private void PrintKeysandValues(SortedList slist)
		{

		}

                [Test]
		public void TestBestFirstGraphSearch()
		{

		}


	}
}
