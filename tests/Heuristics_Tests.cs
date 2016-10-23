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
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			//! -1, md = 1
			int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> s1State = new NPuzzleState<int[]>(s1);
			//! 2, md = 2
			int[] s2 = {1, 2, 3, 4, 9, 6, 7, 5, 8};
			NPuzzleState<int[]> s2State = new NPuzzleState<int[]>(s2);
			//! 1, md 3
			int[] s3 = {1, 2, 3, 4, 6, 9, 7, 5, 8};
			NPuzzleState<int[]> s3State = new NPuzzleState<int[]>(s3);

			//! 2 md = 4
			int[] s4 = {1, 2, 9, 4, 6, 3, 7, 5, 8};
			NPuzzleState<int[]> s4State = new NPuzzleState<int[]>(s4);
			//! -1 md = 5
			int[] s5 = {1, 9, 2, 4, 6, 3, 7, 5, 8};
			NPuzzleState<int[]> s5State = new NPuzzleState<int[]>(s5);
                        //! -1 md = 6
			int[] s6 = {9, 1, 2, 4, 6, 3, 7, 5, 8};
			NPuzzleState<int[]> s6State = new NPuzzleState<int[]>(s6);
			//! 3 + 2 + 1 + 1 + 1 + 1 + 3
			int[] s7 = {6, 4, 3, 1, 9, 5, 8, 7, 2};
			NPuzzleState<int[]> s7State = new NPuzzleState<int[]>(s7);
			//! 2 + 2 + 1 + 1 + 2 = 8
			int[] s8 = {1, 6, 3, 8, 4, 5, 7, 2, 9};
			NPuzzleState<int[]> s8State = new NPuzzleState<int[]>(s8);
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node8 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s8State);

			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(node8), 8);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node7 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s7State);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(node7), 12);

			List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> nodes = TestNode.CreateNodesForTesting();

			Assert.AreEqual(nodes[0].state.State, goal);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[0]), 0);
			Assert.AreEqual(nodes[1].state.State, s1);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[1]), 1);
			Assert.AreEqual(nodes[2].state.State, s2);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[2]), 2);
			Assert.AreEqual(nodes[3].state.State, s3);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[3]), 3);

			Assert.AreEqual(nodes[4].state.State, s4);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[4]), 4);
			Assert.AreEqual(nodes[5].state.State, s5);
			Assert.AreEqual(NPuzzleHeuristics.ManhattanDistance(nodes[5]), 5);

		}
	}
}
