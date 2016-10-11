using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPuzzleTests
{
	[TestFixture]
	public class Problem_Tests
	{
		private Problem.NPuzzleProblem<int[], int, int> CreateProblem(int[] initial)
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			Problem.NPuzzleProblem<int[], int, int> problem = new Problem.NPuzzleProblem<int[], int, int>(goal, initial); 
			return problem;
		}
	
		[Test]
                public void TestNPuzzleProblemCreation()
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			Problem.NPuzzleProblem<int[], int, int> problem = new Problem.NPuzzleProblem<int[], int, int>(goal, initial); 
			problem.Actions(goal);

			Assert.Throws<NPuzzleUtils.InvalidNPuzzleStatesException>(() =>new Problem.NPuzzleProblem<int[],int,int>(initial, goal)); 
		}

		[Test]
                public void TestNPuzzleInitialState()
		{
                     	int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			try {
			Problem.NPuzzleProblem<int[], int, int> problem = new Problem.NPuzzleProblem<int[], int, int>(goal, initial); 
                        Assert.NotNull(problem);
			Assert.NotNull(problem.InitialState);
			Assert.AreEqual(problem.InitialState, initial);
			Problem.AbstractProblem<int[],int,int> p2 = new Problem.NPuzzleProblem<int[], int, int>(goal, initial);
			Assert.NotNull(p2);
			Assert.True(p2.GoalTest(goal));
			Assert.AreEqual(p2.Result(initial, 1), goal);
			List<int> results = new List<int>();
			results.Add(1);
			results.Add(-1);
			results.Add(2);
			Assert.AreEqual(p2.Actions(initial), results);

			} catch(NPuzzleUtils.InvalidNPuzzleStatesException ex) {

			}

			
		}

		[Test]
                public void TestNPuzzleProblemActions()
		{
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			Problem.NPuzzleProblem<int[], int, int> problem = CreateProblem(initial);
			List<int> actions = problem.Actions(initial);
			List<int> expected = new List<int>();
			expected.Add(-1);
			expected.Add(1);
			expected.Add(2);
			CollectionAssert.AreEquivalent(expected, actions);
			int[] initial2 = {9, 7, 4, 2, 6, 3, 5, 8, 1};
			problem = CreateProblem(initial2);
			actions = problem.Actions(initial2);
			expected = new List<int>();
			expected.Add(-2);
			expected.Add(1);
			CollectionAssert.AreEquivalent(expected, actions);
                        int[] initial3 = {2,4,8,6,3,9,7,1,5};
			problem = CreateProblem(initial3);
			actions = problem.Actions(initial3);
			expected = new List<int>();
			expected.Add(-2);
			expected.Add(2);
			expected.Add(-1);
			CollectionAssert.AreEquivalent(expected, actions);
                        int[] initial4 = {2,4,8,6,9,3,7,1,5};
			problem = CreateProblem(initial4);
			actions = problem.Actions(initial4);
			expected = new List<int>();
			expected.Add(-2);
			expected.Add(2);
			expected.Add(-1);
			expected.Add(1);
			CollectionAssert.AreEquivalent(expected, actions);
		}

		[Test]
                public void TestNPuzzleProblemAcceptableAction()
		{
			int[] state = {5, 6, 2, 8, 9, 3, 1, 4, 7};
			Problem.NPuzzleProblem<int[], int, int> problem =  CreateProblem(state);
			Assert.True(problem.AcceptableAction(state, 4, -1));
			Assert.True(problem.AcceptableAction(state, 4, 1));

			Assert.True(problem.AcceptableAction(state, 4, -2));

			Assert.True(problem.AcceptableAction(state, 4, 2));
			Assert.False(problem.AcceptableAction(state, 4, 3));

			Assert.False(problem.AcceptableAction(state, 4, 0));
			int[] state2 = {4,9,8,2,3,5,6,7,1};
			problem =  CreateProblem(state2);

			Assert.True(problem.AcceptableAction(state2, 1, -1));

			Assert.True(problem.AcceptableAction(state2, 1, 1));

			Assert.True(problem.AcceptableAction(state2, 1, -2));

		}

                [Test]
                public void TestNPuzzleProblemGetEmptyIndex()
		{
			int[] state = {9,7,3,6,5,2,8,1,4};
			Problem.NPuzzleProblem<int[], int, int> problem = CreateProblem(state);
			Assert.AreEqual(problem.GetEmptyIndex(state), 0);
			int[] state2 = {1, 7, 6, 5, 4, 3, 8, 2, 9};

			Assert.AreEqual(problem.GetEmptyIndex(state2), 8);
			int[] state3 = {1, 2, 3, 4, 5, 6, 7, 8, 8};

			// Assert.Throws<NPuzzleUtils.MissingEmptyElementException>(() =>problem.GetEmptyIndex(state3)); 
			Assert.AreEqual(problem.GetEmptyIndex(state3), -1);
		}

		[Test]
                public void TestNPuzzleProblemResult()
		{
			int[] state = {7,6,4,9,8,2,5,3,1};
			//! acceptable actions are -2, 2, 1
			//! -2
			int[] r1 = {7,6,4,5,8,2,9,3,1};
			int[] r2 = {9,6,4,7,8,2,5,3,1};
			int[] r3 = {7,6,4,8,9,2,5,3,1};

			int[] s2 = {8,5,2,9,3,7,4,6,1};

			// 2
			int[] s2r1 = {9,5,2,8,3,7,4,6,1};
			// -2 
			int[] s2r2 = {8,5,2,4,3,7,9,6,1};
			// 1
			int[] s2r3 = {8,5,2,3,9,7,4,6,1};

			Problem.NPuzzleProblem<int[], int, int> problem = CreateProblem(state);
			CollectionAssert.AreEquivalent(r1, problem.Result(state, -2));

			CollectionAssert.AreEquivalent(r2, problem.Result(state, 2));

			CollectionAssert.AreEquivalent(r3, problem.Result(state, 2));
			
			CollectionAssert.AreEquivalent(s2r1, problem.Result(s2, 2));

			CollectionAssert.AreEquivalent(s2r2, problem.Result(s2, -2));

			CollectionAssert.AreEquivalent(s2r3, problem.Result(s2, 1));
			Assert.Throws<NPuzzleUtils.ResultAcceptableActionException>(() => problem.Result(state, 0));

		}

		[Test]
                public void TestNPuzzleProblemGoalTest()
		{
			int[] state = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] state2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			Problem.NPuzzleProblem<int[], int, int> problem = CreateProblem(state);
			Assert.False(problem.GoalTest(state2));
			Assert.True(problem.GoalTest(state));
		}
	}
}
