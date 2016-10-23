namespace NPuzzleTests 
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Problem;

	[TestFixture]
	public class SearchTests 
	{
		[Test]
			/*!
			 * Test using a delegate
			 */
                public void TestDelegate()
		{
			Heuristics.HeuristicFunction<NPuzzleState<int[]>,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;

		}

		[Test]
                public void TestSearchClassCreation()
		{
			int size = 9;
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = NPuzzleUtils.CreateProblem(size);
			Heuristics.HeuristicFunction<NPuzzleState<int[]>,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
			//Search.BestFirstGraphSearch<int[], int, int, int> bfgs = new Search.BestFirstGraphSearch<int[], int, int, int>(problem, handler);
		}


		[Test]
                public void TestHashSet()
		{
                        int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			//! -1 (from goal), md = 1
			int[] s1 = {1, 2, 3, 4, 5, 6, 7, 9, 8};
			NPuzzleState<int[]> s1State = new NPuzzleState<int[]>(s1);
			//! md = 1
			int[] s7 = {1, 2, 3, 4, 5, 9, 7, 8, 6};

			NPuzzleState<int[]> s7State = new NPuzzleState<int[]>(s7);
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

			int[] s8 = {9, 1, 2, 4, 6, 3, 7, 5, 8};
			NPuzzleState<int[]> s8State = new NPuzzleState<int[]>(s8);
			HashSet<NPuzzleState<int[]>> explored = new HashSet<NPuzzleState<int[]>>();

			Assert.NotNull(explored.Comparer);
			Console.WriteLine(explored.Comparer.ToString());
			explored.Add(goalState);
			explored.Add(s1State);
			explored.Add(s2State);
			explored.Add(s3State);
			//explored.Add(s4);
			//explored.Add(s5State);
			explored.Add(s6State);
			explored.Add(s7State);

			Assert.True(explored.Contains(goalState));
			Assert.True(explored.Contains(s1State));
			Assert.True(explored.Contains(s7State));
			Assert.True(explored.Contains(s2State));
			Assert.True(explored.Contains(s3State));

			Assert.True(s8State.Equals(s6State));
			Assert.AreEqual(s8State.GetHashCode(), s6State.GetHashCode());
			Assert.True(explored.Contains(s8State));
			Assert.False(explored.Contains(s4State));
			Assert.False(explored.Contains(s5State));

		}

		private void PrintKeysandValues(SortedList slist)
		{

		}

                [Test]
		public void TestBestFirstGraphSearch()
		{
			int size = 9;
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			NPuzzleState<int[]> initial = NPuzzleUtils.GenerateInitState(size);

			Assert.True(NPuzzleUtils.AcceptableState(initial.State));

			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> npuzzle = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goalState, initial);

			Assert.AreEqual(npuzzle.InitialState, initial);
			Assert.AreEqual(npuzzle.GoalState, goal);

			//Heuristics.HeuristicFunction<NPuzzleState<int[]>,int, int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
			Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
	
                        try 
			{

        			Assert.AreEqual(npuzzle.InitialState, initial);
			        // Search.BestFirstGraphSearch<int[],int,int> bfgs = new Search.BestFirstGraphSearch<int[],int,int>(npuzzle, handler);
			        SearchTreeNode.Node<NPuzzleState<int[]>,int,int> initialNode = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(npuzzle.InitialState);	
//				Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>>();
			
			        Search.BestFirstGraphSearch<NPuzzleState<int[]>,int,int> bfgs = new Search.BestFirstGraphSearch<NPuzzleState<int[]>,int,int>(npuzzle, handler);

		         SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node = bfgs.Search();
			 List<int> solution = node.Solution();
			 Console.WriteLine("Printing solution:");
			 solution.ForEach(delegate(int a) {
					 Console.Write("{0} " , a);
					 
					 });

			} catch(NPuzzleUtils.InvalidProblemException ex) 
			{
				System.Console.WriteLine("There is an InvalidProblemException here doode");
				System.Console.WriteLine(ex.Message);
				throw ex;

			} catch(NPuzzleUtils.InvalidProblemPropertyException ex)
			{

				throw ex;
			} catch (System.NullReferenceException ex)
			{
				Console.WriteLine(ex);


			}

		}

                [Test]
		public void TestAStarGraphSearch()
		{
			int size = 9;
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);
			NPuzzleState<int[]> initial = NPuzzleUtils.GenerateInitState(size);

			Assert.True(NPuzzleUtils.AcceptableState(initial.State));

			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> npuzzle = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goalState, initial);

			Assert.AreEqual(npuzzle.InitialState, initial);
			Assert.AreEqual(npuzzle.GoalState, goal);

			//Heuristics.HeuristicFunction<NPuzzleState<int[]>,int, int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
			Heuristics.Heurfun<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> handler = Heuristics.NPuzzleHeuristics.AStarManhattanDistance;
	
                        try 
			{

        			Assert.AreEqual(npuzzle.InitialState, initial);
			        // Search.BestFirstGraphSearch<int[],int,int> bfgs = new Search.BestFirstGraphSearch<int[],int,int>(npuzzle, handler);
			        SearchTreeNode.Node<NPuzzleState<int[]>,int,int> initialNode = new SearchTreeNode.Node<NPuzzleState<int[]>,int,int>(npuzzle.InitialState);	
//				Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>> frontier = new Search.PriorityQueue<int, SearchTreeNode.Node<NPuzzleState<int[]>,int,int>>();
			
			        Search.AStarGraphSearch<NPuzzleState<int[]>,int,int> asgs = new Search.AStarGraphSearch<NPuzzleState<int[]>,int,int>(npuzzle, handler);

		         SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node = asgs.Search();
			 List<int> solution = node.Solution();
			 Console.WriteLine("Printing solution to AStar:");
			 solution.ForEach(delegate(int a) {
					 Console.Write("{0} " , a);
					 
					 });
			 Console.WriteLine("");

			} catch(NPuzzleUtils.InvalidProblemException ex) 
			{
				System.Console.WriteLine("There is an InvalidProblemException here doode");
				System.Console.WriteLine(ex.Message);
				throw ex;

			} catch(NPuzzleUtils.InvalidProblemPropertyException ex)
			{

				throw ex;
			} catch (System.NullReferenceException ex)
			{
				Console.WriteLine(ex);


			}

		}

		[Test]
		public void TestSomething()
		{
                      	int size = 9;
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			NPuzzleState<int[]> goalState = new NPuzzleState<int[]>(goal);

			NPuzzleState<int[]> initial = NPuzzleUtils.GenerateInitState(size);
			Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> npuzzle = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goalState, initial);

                        HashSet<NPuzzleState<int[]> > explored = new HashSet<NPuzzleState<int[]> >();
			//! first node in the frontier is the initialState
			SearchTreeNode.Node<NPuzzleState<int[]>, int, int> node = new SearchTreeNode.Node<NPuzzleState<int[]>, int, int>(npuzzle.InitialState);


		}

		public abstract class BaseClass
		{

			public BaseClass()
			{


			}
		}

		public class DerivedClass : BaseClass
		{
			public DerivedClass()
			{

			}

		}


	}
}
