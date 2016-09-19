using System;
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
		private List<E> frontier;

		public BestFirstGraphSearch(AbstractProblem<E, A, C> problem) 
		{
			this.frontier = new List<E>();

		}

		protected BestFirstGraphSearch() {}

		public Node<E, A, C> search()
		{

		}


	}

	public class AStarGraphSearch<E, A, C> : BestFirstGraphSearch<E, A, C>
	{
		public AStarGraphSearch() 
		{

		}

	}



}



