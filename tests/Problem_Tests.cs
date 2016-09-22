using System;
using NUnit.Framework;

namespace NPuzzleTests
{
	[TestFixture]
	public class Problem_Tests
	{
		[Test]
                public void TestNPuzzleProblemCreation()
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			Problem.NPuzzleProblem problem = new Problem.NPuzzleProblem(goal, initial); 

		}
	}
}
