using System;
using System.Collections.Generic;
using NUnit.Framework;
using Problem;

namespace NPuzzleTests
{
	[TestFixture]
	public class TestNode
	{
		[Test]
		public void TestNodeCreation()
		{
			int size = 9;
			//TODO: change NPuzzleUtils.GenerateInitState
			NPuzzleState<int[]> state = NPuzzleUtils.GenerateInitState(size);

			System.Diagnostics.Debug.WriteLine("State: ");
			System.Diagnostics.Debug.WriteLine(state.State.ToString());
			SearchTreeNode.Node<NPuzzleState<int[]>, int, int> node = new SearchTreeNode.Node<NPuzzleState<int[]>, int, int>(state);

		}

		[Test]
                public void TestExpand()
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};

			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal); 
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> rootNode= new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(goalState);

			List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > expandedNodes;
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = NPuzzleUtils.CreateProblem(9);
			expandedNodes = rootNode.Expand(problem);


			List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > testNodes = new List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> >();

			//! actions for goal are -1 and 2
			//! state resulting from action 2
			int[] s1 = {1, 2, 3, 4, 5, 9, 7, 8, 6};
			NPuzzleState<int[]> s1State= new NPuzzleState<int[]>(s1); 
			//! state resulting from -1
			int[] s2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> s2State= new NPuzzleState<int[]>(s2); 
			SearchTreeNode.Node<NPuzzleState<int[]>, int, int> node2 = new SearchTreeNode.Node<NPuzzleState<int[]>, int, int>(s1State, rootNode, 2, 1); 

			SearchTreeNode.Node<NPuzzleState<int[]>, int, int> node3 = new SearchTreeNode.Node<NPuzzleState<int[]>, int, int>(s2State, rootNode, -1, 1); 
			testNodes.Add(node3);
			testNodes.Add(node2);
			Assert.AreEqual(testNodes.Count, expandedNodes.Count);
			Assert.AreEqual(expandedNodes[0].state, node3.state);

			// Assert.True(expandedNodes[0].state.Equals( node3.state));
			Assert.AreEqual(expandedNodes[0].parent, node3.parent);

			Assert.True(expandedNodes[0].parent.Equals(node3.parent));
			Assert.AreEqual(expandedNodes[0].action, node3.action);

			Assert.True(expandedNodes[0].action.Equals(node3.action));
			Assert.AreEqual(expandedNodes[0].depth, node3.depth);
			Assert.True(expandedNodes[0].depth.Equals( node3.depth));

			Assert.AreEqual(expandedNodes[0].pathCost, node3.pathCost);

			Assert.True(expandedNodes[0].pathCost.Equals(node3.pathCost));
			// Assert.True(expandedNodes[0].Equals(node3));
			// Assert.AreEqual(expandedNodes[0], node3);

			// Assert.AreEqual(expandedNodes[1], node2);
			// CollectionAssert.AreEqual(testNodes, expandedNodes);
			// Assert.That(testNodes, Is.EquivalentTo(expandedNodes));
		}

		[Test]
                public void TestChildNode()
		{

			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> goalState= new NPuzzleState<int[]>(goal); 
			//! action -1
			int[] s1 = {1, 2, 3, 4, 5, 9, 7, 8, 6};
			NPuzzleState<int[]> s1State= new NPuzzleState<int[]>(s1); 
			int[] s2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> s2State= new NPuzzleState<int[]>(s2); 
			
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(goalState);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> childNode = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s2State, node, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> childNode2 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s1State, node, 2, 1);

			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = NPuzzleUtils.CreateProblem(9);
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> expectedNode = node.ChildNode(problem, -1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> expectedNode2 = node.ChildNode(problem, 2);

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

		[Test]
                public void TestPath()
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			//! -1
			int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			//! 2
			int[] s2 = {1, 2, 3, 4, 9, 6, 7, 5, 8};

			//! 1
			int[] s3 = {1, 2, 3, 4, 6, 9, 7, 5, 8};

			//! 2
			int[] s4 = {1, 2, 9, 4, 6, 3, 7, 5, 8};

			//! -1
			int[] s5 = {1, 9, 2, 4, 6, 3, 7, 5, 8};

			NPuzzleState<int[]> goalState= new NPuzzleState<int[]>(goal); 

			NPuzzleState<int[]> s1State= new NPuzzleState<int[]>(s1); 

			NPuzzleState<int[]> s2State= new NPuzzleState<int[]>(s2); 

			NPuzzleState<int[]> s3State= new NPuzzleState<int[]>(s3); 

			NPuzzleState<int[]> s4State= new NPuzzleState<int[]>(s4); 

			NPuzzleState<int[]> s5State= new NPuzzleState<int[]>(s5); 
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> rootNode= new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(goalState);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node1 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s1State, rootNode, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node2 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s2State, node1, 2, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node3 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s3State, node2, 1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node4 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s4State, node3, 2, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node5 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(s5State, node4, -1, 1);

			List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int> > path = node5.Path();
			Assert.AreEqual(s5State, path[0].state);
			Assert.AreEqual(path[0].action, -1);

			Assert.AreEqual(s4State, path[1].state);
			Assert.AreEqual(path[1].action, 2);

			Assert.AreEqual(s3State, path[2].state);

			Assert.AreEqual(s2State, path[3].state);
		}

		public static List<SearchTreeNode.Node<Problem.NPuzzleState<int[]>,int,int> > CreateNodesForTesting()
		{
			//! md = 0
                        int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			//! -1 (from goal), md = 1
			int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};

			//! md = 1
			int[] s7 = {1, 2, 3, 4, 5, 9, 7, 8, 6};
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

			//! TODO: Could refactor this. Create dictionaries with state, action, md value, and use a loop
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> rootNode= new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(goal));

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node1 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s1), rootNode, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node2 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s2), node1, 2, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node3 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s3), node2, 1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node4 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s4), node3, 2, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node5 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s5), node4, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node6 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s6), node5, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node7 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s7), rootNode, 2, 1);

			List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> nodes = new List<SearchTreeNode.Node<NPuzzleState<int[]>,int,int>>();
			nodes.Add(rootNode);
			nodes.Add(node1);

			nodes.Add(node2);

			nodes.Add(node3);

			nodes.Add(node4);

			nodes.Add(node5);

			nodes.Add(node6);

			nodes.Add(node7);

			return nodes;
		}

		[Test]
                public void TestSolution()
		{
                        int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			//! -1
			int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};

			NPuzzleState<int[]> s1State = new NPuzzleState<int[]>(s1);
			//! 2
			int[] s2 = {1, 2, 3, 4, 9, 6, 7, 5, 8};

			NPuzzleState<int[]> s2State = new NPuzzleState<int[]>(s2);
			//! 1
			int[] s3 = {1, 2, 3, 4, 6, 9, 7, 5, 8};

			NPuzzleState<int[]> s3State = new NPuzzleState<int[]>(s3);
			//! 2
			int[] s4 = {1, 2, 9, 4, 6, 3, 7, 5, 8};

			NPuzzleState<int[]> s4State = new NPuzzleState<int[]>(s4);

			//! -1
			int[] s5 = {1, 9, 2, 4, 6, 3, 7, 5, 8};

			NPuzzleState<int[]> s5State = new NPuzzleState<int[]>(s5);
		
                        //! -1
			int[] s6 = {9, 1, 2, 4, 6, 3, 7, 5, 8};

			NPuzzleState<int[]> s6State = new NPuzzleState<int[]>(s6);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> rootNode= new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(goalState);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node1 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s1), rootNode, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node2 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s2), node1, 2, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node3 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s3), node2, 1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node4 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s4), node3, 2, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node5 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s5), node4, -1, 1);

			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node6 = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s6), node5, -1, 1);

			List<int> solution = node6.Solution();

			Assert.AreEqual(solution[0], -1);

			Assert.AreEqual(solution[1], -1);

			Assert.AreEqual(solution[2], 2);
		}

		[Test]
                public void TestArrayEquality()
		{
			int[] state = {1, 2, 3, 4, 5, 6, 7, 8, 9};
                        Type valueType = state.GetType();
			var expectedType = typeof(Array);
			Assert.True(valueType.IsArray);
		//	Assert.True(expectedType.IsAssignableFrom(valueType.GetElementType()));
		}

		[Test]
                public void TestNodeTypeCasting()
		{

			int[] s6 = {9, 1, 2, 4, 6, 3, 7, 5, 8};
			SearchTreeNode.Node<NPuzzleState<int[]>,int,int> rootNode= new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(new NPuzzleState<int[]>(s6));
		}
	}
}
