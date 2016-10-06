using System;
using System.Collections;
using System.Collections.Generic;
using SearchTreeNode;
using Problem;
//using Priority_Queue;

namespace Search
{
	/*public delegate void GraphSearchDel(AbstractProblem<E, A, C> problem, Func<int[], int> heurfun);*/
	public class GraphSearch<E, A, C> 
	{

		public GraphSearch(AbstractProblem<E, A, C> problem, Queue<E> queue) 
		{

		}

		public GraphSearch(AbstractProblem<E, A, C> problem, Stack<E> stack) 
		{

		}
	}
	/*
        public class BestFirstGraphSearch 
	{
		private Frontier<int, Node<int[],int,int> > frontier;
		// private SortedList<C, List<SearchTreeNode.Node<E, A, C> > > frontier;
		private NPuzzleProblem<int[], int, int> problem;
		private Heuristics.HeuristicFunction<int[],int,int,int> heurfun;

		public BestFirstGraphSearch(NPuzzleProblem<int[], int, int> problem, Heuristics.HeuristicFunction<int[],int,int,int> hf) 
		{
			frontier = new Frontier<int, Node<int[],int,int> >();
			this.problem = problem;
			heurfun = hf;

		}

		protected BestFirstGraphSearch() {}

		public Node<int[], int, int> Search()
		{
			HashSet<int[]> explored = new HashSet<int[]>();

			//! first node in the frontier is the initialState
			Node<int[], int, int> node = new Node<int[], int, int>(problem.InitialState);

		        
			//! if it happens to be the goal state, return it, we're done
			if (problem.GoalTest(node.state)) return node;
			int heur = heurfun(node);
			frontier.Append(heur, node);

			if(node.GetType() != typeof(Node<int[],int,int>))
			{
				Exception ex = new Exception("You fucked up");
				throw ex;
			}	
					



		        while (frontier.Count() > 0)
			{
				node = frontier.Pop();
				if (explored.Count >10000) break;
				if (node == null)
				{
					NPuzzleUtils.NullSearchNodeException ex = new NPuzzleUtils.NullSearchNodeException("Popped null node off frontier");
					throw ex;

				}
				if (problem.GoalTest(node.state)) return node;

				explored.Add(node.state);

				//node = this.frontier.RemoveAt(0);
				node.Expand(problem).ForEach(
						delegate(Node<int[], int, int> n)
						{
						    heur = heurfun(n);
						    if (!explored.Contains(n.state) && !frontier.In(heur, n))
						    {
						       frontier.Append(heur, n);

						    } //else if (frontier.In(heur, n)) 
						    //{
						      //bool r = frontier.ReplaceIncumbent(

						    // }
						}
						);
			}

			//return default(Node<int[],int,int>);
			return node;
		}
	}
	*/
	public class BestFirstGraphSearch<E, A, C, K > 
		where K:IComparable 
	{
		private Frontier<K, Node<E,A,C> > frontier;
		private AbstractProblem<E, A, C> problem;
		private Heuristics.HeuristicFunction<E,A,C,K> heurfun;

		public BestFirstGraphSearch(AbstractProblem<E, A, C> p, Heuristics.HeuristicFunction<E,A,C,K> hf) 
		{
			frontier = new Frontier<K, Node<E,A,C> >();
			problem = p;

			if (problem.GetType() != typeof(NPuzzleProblem<int[],int,int>))
			{
                        	NPuzzleUtils.InvalidProblemException ex = new NPuzzleUtils.InvalidProblemException("We don't have an npuzzle");
				throw ex;
			}
			if (problem.GetType() == typeof(AbstractProblem<E,A,C>))
			{
                        	NPuzzleUtils.InvalidProblemException ex = new NPuzzleUtils.InvalidProblemException("We don't have an npuzzle");
				throw ex;
			}

			heurfun = hf;
		}

		protected BestFirstGraphSearch() {}

		public Node<E, A, C> Search()
		{
			HashSet<E> explored = new HashSet<E>();

			//! first node in the frontier is the InitialState

			Node<E, A, C> node = new Node<E, A, C>(problem.InitialState);

			//! if it happens to be the goal state, return it, we're done
			if (problem.GoalTest(node.state)) return node;
			K heur = heurfun(node);
			frontier.Append(heur, node);

		        while (frontier.Count() > 0 /*&& frontier.Count() < 100*/)
			{
				node = frontier.Pop();

				if (node == null)
				{
					NPuzzleUtils.NullSearchNodeException ex = new NPuzzleUtils.NullSearchNodeException("Node popper off frontier is null");
					throw ex;

				}
				
				if (problem.GoalTest(node.state)) return node;
				explored.Add(node.state);

				node.Expand(problem).ForEach(
						delegate(Node<E, A, C> n)
						{
						    heur = heurfun(n);
						    if (!explored.Contains(n.state) && !frontier.In(heur, n))
						    {
						       frontier.Append(heur, n);

						    } else if (frontier.In(heur, n)) 
						    {
						      //bool r = frontier.ReplaceIncumbent(

						    }

						}
						);
			}	

			return default(Node<E,A,C>);
		}


	}

	/*
	public class AStarGraphSearch<E, A, C,K, P> : BestFirstGraphSearch<E, A, C,K, P>  where K:IComparable
	{
		public AStarGraphSearch() 
		{

		}
	}*/
}



