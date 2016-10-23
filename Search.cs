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
		where C:IComparable 
		where E:IEquatable<E>
	{
		private PriorityQueue<C, Node<E,A,C> > frontier;
		private AbstractProblem<E, A, C> problem;
		// private Heuristics.Heurfun<C, Node<E, A, C>> heurfun;
		//private Heuristics.HeuristicFunction<E,A,C> heurfun;

		public BestFirstGraphSearch(AbstractProblem<E, A, C> p, Heuristics.Heurfun<C, Node<E,A,C>> hf /*, PriorityQueue<C, Node<E,A,C>> f*/) 
		{
			frontier = new PriorityQueue<C, Node<E,A,C> >(hf); // f
			problem = p;
		        //heurfun = hf;
		}

		protected BestFirstGraphSearch() {}

		public Node<E, A, C> Search()
		{
			HashSet<E> explored = new HashSet<E>();
			//! first node in the frontier is the InitialState
			Node<E, A, C> node = new Node<E, A, C>(problem.InitialState);
			Console.Write("Initial node: ");
			Console.WriteLine(node.state.ToString());
			//! if it happens to be the goal state, return it, we're done
			if (problem.GoalTest(node.state)) return node;
			//C heur = heurfun(node);
			frontier.Append(/*heur,*/node);

		        while (frontier.Count() > 0 /*&& frontier.Count() < 100*/)
			{
				node = frontier.Pop();

				if (node == null)
				{
					NPuzzleUtils.NullSearchNodeException ex = new NPuzzleUtils.NullSearchNodeException("Node popper off frontier is null");
					throw ex;
				}
				int[] s = {1,2,3,4,5,6,7,8,9};

				if (problem.GoalTest(node.state)) return node;
				explored.Add(node.state);
				//Console.WriteLine("Popped from frontier: {0}", node.state.ToString());

				node.Expand(problem).ForEach(
						delegate(Node<E, A, C> n)
						{
						    // heur = heurfun(n);
						    bool inPriorityQueue = frontier.InPriorityQueue(n);
						    if (!explored.Contains(n.state) && !inPriorityQueue)
						    {
						    //Console.WriteLine("Appending to frontier: {0}", n.state.ToString());
						       frontier.Append(/*heur,*/ n);
						    } else if (inPriorityQueue) 
						    {
						       Node<E, A, C> incumbent = frontier.GetIncumbent(n);
						       if (frontier.Heurfun(n).CompareTo(frontier.Heurfun(incumbent)) == 1)
						       {
						       // remove incumbent
						       frontier.RemoveIncumbent(incumbent);
						       // add n
						       frontier.Append(n);


						       }

				//		    Console.WriteLine("It's in the frontier");

						    }

						}
						);
			}	

			return default(Node<E,A,C>);
		}


	}

	public class AStarGraphSearch<E, A, C> : BestFirstGraphSearch<E, A, C>  
		where C:IComparable
		where E:IEquatable<E>
	{
		// private Heuristics.Heurfun<C, Node<E,A,C>> hf;
		// private PriorityQueue<C, Node<E,A,C>> frontier;
		//
		//! TODO: Is there a better way to include the use
		//! of a heuristic function for AStar graph searches?
		//! Generally, it seems like it would be preferable
		//! to pass in a heuristic function, and add the
		//! result of the heuristic function to the 
		//! pathCost of reaching the node from the root
		//! node, like:
		//
		//! delegate (Node<E,A,C> node) 
		//! {
		//! 	return node.pathCost + hf(node);
		//! }
		//
		//! Problem is, the compiler throws an error because
		//! both values are of type C, and there is no way
		//! to add such values at this point in time. Generally
		//! C should represent a numerical value of some kind
		//! , and hence the addition operator should work.
		//! But the compiler doesn't know this. So what type
		//! restriction can we impose on C? 
		//
		//! For now, simply

		/*!
		 * 
		 *
		 * @param {Heuristics.Heurfun<Cost, Node<State, Action, Cost>>} hf - a heuristic function which 
		 */
		public AStarGraphSearch(AbstractProblem<E, A, C> p, Heuristics.Heurfun<C, Node<E,A,C>> hf) : base(p, hf)
		{

		}
	}
}



