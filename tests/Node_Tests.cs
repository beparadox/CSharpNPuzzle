using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPuzzleTests
{
	[TestFixture]
	public class TestNode
	{
		[Test]
		public void TestNodeCreation()
		{
			int size = 9;
			int[] state = NPuzzleUtils.GenerateInitState(size);

			System.Diagnostics.Debug.WriteLine("State: ");
			System.Diagnostics.Debug.WriteLine(state.ToString());
			SearchTreeNode.Node<int [], int, int> node = new SearchTreeNode.Node<int [], int, int>(state);

		}

		[Test]
                public void TestExpand()
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			// int[] initial = NPuzzleUtils.GenerateInitState();
			SearchTreeNode.Node<int[],int,int> node = new SearchTreeNode.Node<int[],int,int>(goal);
			List<SearchTreeNode.Node<int[],int,int> > expandedNodes;
			Problem.NPuzzleProblem problem = NPuzzleUtils.CreateProblem(9);
			expandedNodes = node.Expand(problem);


			List<SearchTreeNode.Node<int[],int,int> > testNodes = new List<SearchTreeNode.Node<int[],int,int> >();

			//! actions for goal are -1 and 2
			//! state resulting from action 2
			int[] s1 = {1, 2, 3, 4, 5, 9, 7, 8, 6};
			//! state resulting from -1
			int[] s2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			SearchTreeNode.Node<int[], int, int> node2 = new SearchTreeNode.Node<int[], int, int>(s1, node, 2, 1); 

			SearchTreeNode.Node<int[], int, int> node3 = new SearchTreeNode.Node<int[], int, int>(s2, node, -1, 1); 
			testNodes.Add(node3);
			testNodes.Add(node2);
			// CollectionAssert.AreEquivalent(testNodes, expandedNodes);
			// Assert.That(testNodes, Is.EquivalentTo(expandedNodes));
			Assert.AreEqual(testNodes.Count, expandedNodes.Count);
		}

		[Test]
                public void ProblemTestChildNode()
		{

			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			//! action -1
			int[] s1 = {1, 2, 3, 4, 5, 9, 7, 8, 6};
			int[] s2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			
			SearchTreeNode.Node<int[],int,int> node = new SearchTreeNode.Node<int[],int,int>(goal);

			SearchTreeNode.Node<int[],int,int> childNode = new SearchTreeNode.Node<int[],int,int>(s2, node, -1, 1);

			SearchTreeNode.Node<int[],int,int> childNode2 = new SearchTreeNode.Node<int[],int,int>(s1, node, 2, 1);

			Problem.NPuzzleProblem problem = NPuzzleUtils.CreateProblem(9);
			SearchTreeNode.Node<int[],int,int> expectedNode = node.ChildNode(problem, -1);

			SearchTreeNode.Node<int[],int,int> expectedNode2 = node.ChildNode(problem, 2);

			//! Tests
			Assert.AreEqual(node.depth, 0);
			Assert.AreEqual(expectedNode.parent, childNode.parent);
			Assert.AreEqual(expectedNode.pathCost, childNode.pathCost);

			Assert.AreEqual(expectedNode.state, childNode.state);
			Assert.AreEqual(expectedNode.action, -1);
			Assert.AreEqual(expectedNode.depth, 1);
                        Assert.AreEqual(expectedNode2.parent, childNode2.parent);
			Assert.AreEqual(expectedNode2.pathCost, childNode2.pathCost);

			Assert.AreEqual(expectedNode2.state, childNode2.state);
			Assert.AreEqual(expectedNode2.action, 2);
			Assert.AreEqual(expectedNode2.depth, 1);

		}
	}
}
