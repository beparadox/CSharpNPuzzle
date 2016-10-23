using System;
using System.Collections.Generic;
using NUnit.Framework;
using Problem;

namespace NPuzzleTests
{
	[TestFixture]
	public class Problem_Tests
	{
		private Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> CreateProblem(NPuzzleState<int[]> initial)
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(new NPuzzleState<int[]>(goal), initial); 
			return problem;
		}
	
		[Test]
                public void TestNPuzzleProblemCreation()
		{
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(initial);

			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goalState, initialState); 
			problem.Actions(goalState);

			Assert.Throws<NPuzzleUtils.InvalidNPuzzleStatesException>(() =>new Problem.NPuzzleProblem<NPuzzleState<int[]>,int,int>(initialState, goalState)); 
		}

		[Test]
                public void TestNPuzzleInitialState()
		{
                     	int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(initial);


			try {
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goalState, initialState); 
                        Assert.NotNull(problem);
			Assert.NotNull(problem.InitialState);
			Assert.AreEqual(problem.InitialState, initialState);
			Problem.AbstractProblem<NPuzzleState<int[]>,int,int> p2 = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goalState, initialState);
			Assert.NotNull(p2);
			Assert.True(p2.GoalTest(goalState));
			Assert.AreEqual(p2.Result(initialState, 1), goalState);
			List<int> results = new List<int>();
			results.Add(1);
			results.Add(-1);
			results.Add(2);
			Assert.AreEqual(p2.Actions(initialState), results);

			} catch(NPuzzleUtils.InvalidNPuzzleStatesException ex) {

			}

			
		}

		[Test]
                public void TestNPuzzleProblemActions()
		{
			int[] initial = {1, 2, 3, 4, 5, 6, 7, 9, 8};

			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(initial);

			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = CreateProblem(initialState);
			List<int> actions = problem.Actions(initialState);
			List<int> expected = new List<int>();
			expected.Add(-1);
			expected.Add(1);
			expected.Add(2);
			CollectionAssert.AreEquivalent(expected, actions);
			int[] initial2 = {9, 7, 4, 2, 6, 3, 5, 8, 1};

			NPuzzleState<int[]> initialState2 = new NPuzzleState<int[]>(initial2);
			problem = CreateProblem(initialState2);
			actions = problem.Actions(initialState2);
			expected = new List<int>();
			expected.Add(-2);
			expected.Add(1);
			CollectionAssert.AreEquivalent(expected, actions);
                        int[] initial3 = {2,4,8,6,3,9,7,1,5};

			NPuzzleState<int[]> initialState3 = new NPuzzleState<int[]>(initial3);
			problem = CreateProblem(initialState3);
			actions = problem.Actions(initialState3);
			expected = new List<int>();
			expected.Add(-2);
			expected.Add(2);
			expected.Add(-1);
			CollectionAssert.AreEquivalent(expected, actions);
                        int[] initial4 = {2,4,8,6,9,3,7,1,5};
			NPuzzleState<int[]> initialState4 = new NPuzzleState<int[]>(initial4);
			problem = CreateProblem(initialState4);
			actions = problem.Actions(initialState4);
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
			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(state);
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem =  CreateProblem(initialState);
			Assert.True(problem.AcceptableAction(initialState.State, 4, -1));
			Assert.True(problem.AcceptableAction(initialState.State, 4, 1));

			Assert.True(problem.AcceptableAction(initialState.State, 4, -2));

			Assert.True(problem.AcceptableAction(initialState.State, 4, 2));
			Assert.False(problem.AcceptableAction(initialState.State, 4, 3));

			Assert.False(problem.AcceptableAction(initialState.State, 4, 0));
			int[] state2 = {4,9,8,2,3,5,6,7,1};
			NPuzzleState<int[]> state2State = new NPuzzleState<int[]>(state2);
			problem =  CreateProblem(state2State);

			NPuzzleState<int[]> initialState2 = new NPuzzleState<int[]>(state2);
			Assert.True(problem.AcceptableAction(initialState2.State, 1, -1));

			Assert.True(problem.AcceptableAction(initialState2.State, 1, 1));

			Assert.True(problem.AcceptableAction(initialState2.State, 1, -2));

		}

                [Test]
                public void TestNPuzzleProblemGetEmptyIndex()
		{
			int[] state = {9,7,3,6,5,2,8,1,4};
			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(state);
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = CreateProblem(initialState);
			Assert.AreEqual(problem.GetEmptyIndex(initialState.State), 0);
			int[] state2 = {1, 7, 6, 5, 4, 3, 8, 2, 9};
			NPuzzleState<int[]> initialState2 = new NPuzzleState<int[]>(state2);

			Assert.AreEqual(problem.GetEmptyIndex(initialState2.State), 8);
			int[] state3 = {1, 2, 3, 4, 5, 6, 7, 8, 8};
			NPuzzleState<int[]> initialState3 = new NPuzzleState<int[]>(state3);

			// Assert.Throws<NPuzzleUtils.MissingEmptyElementException>(() =>problem.GetEmptyIndex(state3)); 
			Assert.AreEqual(problem.GetEmptyIndex(initialState3.State), -1);
		}

		[Test]
                public void TestNPuzzleProblemResult()
		{
			int[] state = {7,6,4,9,8,2,5,3,1};
			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(state);
			//! acceptable actions are -2, 2, 1
			//! -2
			int[] r1 = {7,6,4,5,8,2,9,3,1};

			NPuzzleState<int[]> r1State = new NPuzzleState<int[]>(r1);
			int[] r2 = {9,6,4,7,8,2,5,3,1};

			NPuzzleState<int[]> r2State = new NPuzzleState<int[]>(r2);
			int[] r3 = {7,6,4,8,9,2,5,3,1};

			NPuzzleState<int[]> r3State = new NPuzzleState<int[]>(r3);

			int[] s2 = {8,5,2,9,3,7,4,6,1};

			NPuzzleState<int[]> s2State = new NPuzzleState<int[]>(s2);
			// 2
			int[] s2r1 = {9,5,2,8,3,7,4,6,1};

			NPuzzleState<int[]> s2r1State = new NPuzzleState<int[]>(s2r1);
			// -2 
			int[] s2r2 = {8,5,2,4,3,7,9,6,1};

			NPuzzleState<int[]> s2r2State = new NPuzzleState<int[]>(s2r2);
			// 1
			int[] s2r3 = {8,5,2,3,9,7,4,6,1};

			NPuzzleState<int[]> s2r3State = new NPuzzleState<int[]>(s2r3);
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = CreateProblem(initialState);

			CollectionAssert.AreEquivalent(r1State, problem.Result(initialState, -2));

			CollectionAssert.AreEquivalent(r2State, problem.Result(initialState, 2));

			CollectionAssert.AreEquivalent(r3State, problem.Result(initialState, 2));
			
			CollectionAssert.AreEquivalent(s2r1State, problem.Result(s2State, 2));

			CollectionAssert.AreEquivalent(s2r2State, problem.Result(s2State, -2));

			CollectionAssert.AreEquivalent(s2r3State, problem.Result(s2State, 1));
			Assert.Throws<NPuzzleUtils.ResultAcceptableActionException>(() => problem.Result(initialState, 0));

		}

		[Test]
                public void TestNPuzzleProblemGoalTest()
		{
			int[] state = {1, 2, 3, 4, 5, 6, 7, 8, 9};

			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(state);
			int[] state2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};

			NPuzzleState<int[]> initialState2 = new NPuzzleState<int[]>(state2);
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = CreateProblem(initialState);
			Assert.False(problem.GoalTest(initialState2));
			Assert.True(problem.GoalTest(initialState));
		}

		[Test]
		public void TestNPuzzleStateEquals()
		{
			int[] state = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> initialState = new NPuzzleState<int[]>(state);
                        int[] state2 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> initialState2 = new NPuzzleState<int[]>(state2);
			Assert.False(initialState.Equals(initialState2));

			Assert.True(initialState.Equals(initialState));


		}


		[Test]
		public void TestNPuzzleStateHashCode()
		{


		}
	}
}
