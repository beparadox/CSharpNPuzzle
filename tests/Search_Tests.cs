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

		[Test]
                public void TestFrontierAdd()
		{
			 SortedList<int, List<SearchTreeNode.NPuzzleNode> > frontier = new SortedList<int, List<SearchTreeNode.NPuzzleNode> >();
			//SortedList<int, List<SearchTreeNode.Node<int[],int,int> > frontier = new SortedList<int, List<SearchTreeNode.Node<int[],int,int> >();


			Heuristics.HeuristicFunction<int[],int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;

			List<SearchTreeNode.NPuzzleNode> nodes = TestNode.CreateNodesForTesting();
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
			Assert.True(frontier.ContainsKey(0));
			Assert.True(frontier.ContainsKey(1));
                        Assert.True(frontier.ContainsKey(2));
			Assert.True(frontier.ContainsKey(3));

			Assert.True(frontier.ContainsKey(4));

			
       
			System.Diagnostics.Debug.WriteLine("State: ");
		}

		private void PrintKeysandValues(SortedList slist)
		{

		}


	}
}
