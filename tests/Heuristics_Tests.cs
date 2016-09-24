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
