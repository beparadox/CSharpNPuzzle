namespace NPuzzleTests
{
	using System;
	using System.Collections.Generic;
	using Heuristics;
	using Problem;
	using NUnit.Framework;

	[TestFixture]
	public class NPuzzleHeuristicsTest
	{
		[Test]
		public void TestManhattanDistance()
		{
			//! md = 0
                        int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			//! -1, md = 1
			int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			//! 2, md = 2
			int[] s2 = {1, 2, 3, 4, 9, 6, 7, 5, 8};

			//! 1, md 3
			int[] s3 = {1, 2, 3, 4, 6, 9, 7, 5, 8};

			//! 2 md = 4
			int[] s4 = {1, 2, 9, 4, 6, 3, 7, 5, 8};

			//! -1 md = 5
			int[] s5 = {1, 9, 2, 4, 6, 3, 7, 5, 8};
		
                        //! -1 md = 6
			int[] s6 = {9, 1, 2, 4, 6, 3, 7, 5, 8};

			//! 3 + 2 + 1 + 1 + 1 + 1 + 3
			int[] s7 = {6, 4, 3, 1, 9, 5, 8, 7, 2};

			//! 2 + 2 + 1 + 1 + 2 = 8
			int[] s8 = {1, 6, 3, 8, 4, 5, 7, 2, 9};
			SearchTreeNode.NPuzzleNode node8 = new SearchTreeNode.NPuzzleNode(s8);

			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(node8), 8);

			SearchTreeNode.NPuzzleNode node7 = new SearchTreeNode.NPuzzleNode(s7);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(node7), 12);

			List<SearchTreeNode.NPuzzleNode> nodes = TestNode.CreateNodesForTesting();

			Assert.AreEqual(nodes[0].state, goal);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[0]), 0);
			Assert.AreEqual(nodes[1].state, s1);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[1]), 1);
			Assert.AreEqual(nodes[2].state, s2);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[2]), 2);
			Assert.AreEqual(nodes[3].state, s3);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[3]), 3);

			Assert.AreEqual(nodes[4].state, s4);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[4]), 4);
			Assert.AreEqual(nodes[5].state, s5);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[5]), 5);







		}
	}
}
