using System;
using Problem;
using System.Collections.Generic;

namespace Heuristics
{
	public delegate C HeuristicFunction<S,A,C>(SearchTreeNode.Node<S, A, C> node) 
		where C:IComparable
		where S:IEquatable<S>; 

	public delegate Key Heurfun<Key, Value>(Value v);

	public class NPuzzleHeuristics {

		// public static Func
		/*!
		 * Calculate the total city-block distance for the
		 * state
		 */
		public static int ManhattanDistance(SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node) 
                // public static int ManhattanDistance(SearchTreeNode.Node<S,A,C>
		{
			int md = 0;
			int[] state = node.state.State;
			int dim = (int) Math.Sqrt(state.Length);

			for (int i = 0; i < state.Length; i++) {
				if (state[i] != state.Length) {
					//! need to determine how far away, in city-block distance, this element
					//! is from it's place in the
					//! goal state. It's index in the goal state is one less then the value. 
					//! so we need to calculate the city block distance between the current 
					//! index (i) and state[i] - 1
					//! To do this,  

					md += Math.Abs((i - state[i] + 1) / dim);
					md += Math.Abs((i - state[i] + 1) % dim);
				}
			}

			node.hv = md;

			return md;
		}


		public static int AStarManhattanDistance(SearchTreeNode.Node<NPuzzleState<int[]>,int,int> node) 
		{
			return node.pathCost + ManhattanDistance(node);
		}
	}

}
