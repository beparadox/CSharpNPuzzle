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

	public class BestFirstGraphSearch<E, A, C>
	{
		private SortedList frontier;
		private AbstractProblem<E, A, C> problem;
		private Heuristics.HeuristicFunction<E,A,C> heurfun;

		public BestFirstGraphSearch(AbstractProblem<E, A, C> problem, Heuristics.HeuristicFunction<E,A,C> hf) 
		{
			this.frontier = new SortedList();
			this.problem = problem;
			this.heurfun = hf;

		}

		protected BestFirstGraphSearch() {}

		public Node<E, A, C> search()
		{

			//! first node in the frontier is the initialState
			Node<E, A, C> node = new Node<E, A, C>(this.problem.initialState);
			//! if it happens to be the goal state, return it, we're done
			if (this.problem.GoalTest(node.state)) return node;
			this.frontier.Add(this.heurfun(node), node);

			return node;


		}


	}

	public class AStarGraphSearch<E, A, C> : BestFirstGraphSearch<E, A, C>
	{
		public AStarGraphSearch() 
		{

		}
	}

}



