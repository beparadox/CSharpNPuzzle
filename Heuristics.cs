using System;
using Problem;

namespace Heuristics
{
	public delegate C HeuristicFunction<S,A,C>(SearchTreeNode.Node<S, A, C> node);

	public class NPuzzleHeuristics
	{
		int size;
		int dim;

		public NPuzzleHeuristics(NPuzzleProblem npuzzle, string heuristic="manhattanDistance")
		{
			size = npuzzle.size;
			dim = npuzzle.dimension;
		}

		/*!
		 * Calculate the total city-block distance for the
		 * state
		 */
		public int manhattanDistance(int[] state) 
		{
			int md = 0;

			for (int i = 0; i < state.Length; i++) {
				if (state[i] != state.Length) {
					//! need to determine how far away, in city-block distance, this element
					//! is from it's place in the
					//! goal state. It's index in the goal state is one less then the value. 
					//! so we need to calculate the city block distance between the current 
					//! index (i) and state[i] - 1
					//! To do this,  

					md += Math.Abs((i - state[i] - 1) / dim);
					md += Math.Abs((i - state[i] - 1) % dim);
				}
			}

			return md;
		}

	}
}
