namespace NPuzzleTests 
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NUnit.Framework;

	[TestFixture]
	public class SearchTests 
	{
		[Test]
			/*!
			 * Test using a delegate
			 */
                public void TestDelegate()
		{
			Heuristics.HeuristicFunction<int[],int,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;

		}

		[Test]
                public void TestSearchClassCreation()
		{
			int size = 9;
			Problem.NPuzzleProblem<int[], int, int> problem = NPuzzleUtils.CreateProblem(size);
			Heuristics.HeuristicFunction<int[],int,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
			//Search.BestFirstGraphSearch<int[], int, int, int> bfgs = new Search.BestFirstGraphSearch<int[], int, int, int>(problem, handler);
		}


		[Test]
                public void TestHashSet()
		{
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

			HashSet<int[]> explored = new HashSet<int[]>();
			explored.Add(goal);
			explored.Add(s1);
			explored.Add(s7);
			explored.Add(s2);
			explored.Add(s3);
			//explored.Add(s4);
			explored.Add(s5);
			explored.Add(s6);

			Assert.True(explored.Contains(goal));
			Assert.True(explored.Contains(s1));
			Assert.True(explored.Contains(s7));
			Assert.True(explored.Contains(s2));
			Assert.True(explored.Contains(s3));
			Assert.True(explored.Contains(s5));
			Assert.False(explored.Contains(s4));

		}

		private void PrintKeysandValues(SortedList slist)
		{

		}

                [Test]
		public void TestBestFirstGraphSearch()
		{
			int size = 9;
			int[] goal = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			int[] initial = NPuzzleUtils.GenerateInitState(size);
			Assert.True(NPuzzleUtils.AcceptableState(initial));

			Problem.NPuzzleProblem<int[], int, int> npuzzle = new Problem.NPuzzleProblem<int[], int, int>(goal, initial);

			Assert.AreEqual(npuzzle.InitialState, initial);
			Assert.AreEqual(npuzzle.GoalState, goal);

			Heuristics.HeuristicFunction<int[],int,int,int> handler = Heuristics.NPuzzleHeuristics.ManhattanDistance;
	
                        try 
			{

        			Assert.AreEqual(npuzzle.InitialState, initial);
			        //Search.BestFirstGraphSearch<int[],int,int,int> bfgs = new Search.BestFirstGraphSearch<int[],int,int,int>(npuzzle, handler);
			        SearchTreeNode.NPuzzleNode<int[],int,int> initialNode = new SearchTreeNode.NPuzzleNode<int[],int,int>(npuzzle.InitialState);	
				
			        Search.BestFirstGraphSearch<int[],int,int,int> bfgs = new Search.BestFirstGraphSearch<int[],int,int,int>(npuzzle, handler, initialNode);

		         SearchTreeNode.Node<int[],int,int> node = bfgs.Search();
			} catch(NPuzzleUtils.InvalidProblemException ex) 
			{
				System.Console.WriteLine("There is an InvalidProblemException");
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
			int[] initial = NPuzzleUtils.GenerateInitState(size);
			Problem.NPuzzleProblem<int[], int, int> npuzzle = new Problem.NPuzzleProblem<int[], int, int>(goal, initial);

                        HashSet<int[]> explored = new HashSet<int[]>();
			//! first node in the frontier is the initialState
			SearchTreeNode.Node<int[], int, int> node = new SearchTreeNode.Node<int[], int, int>(npuzzle.InitialState);


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
