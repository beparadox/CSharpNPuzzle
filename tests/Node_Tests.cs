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
			int[] state = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			SearchTreeNode.Node<int[],int,int> node = new SearchTreeNode.Node<int[],int,int>(state);
			List<SearchTreeNode.Node<int[],int,int> > nodes;
			// Problem.NPuzzleProblem
			// nodes = node.Expand();
		}
	}
}
