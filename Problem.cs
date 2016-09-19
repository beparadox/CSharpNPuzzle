using System;
using System.Collections.Generic;
namespace Problem
{
	/**
	 * Abstract class Problem. Given an initial state and goal state of type E,
	 * use other defined methods to 
	 *
	 *
	 */
	abstract public class AbstractProblem<E, A, C>
	{
		//! initial state
		public E initial;
		//! goal state
		public E goal;

		public AbstractProblem(E initial, E goal)
		{
			this.initial = initial;
			this.goal = goal;
		}	

		protected AbstractProblem() {}

		/*! return a list of possible actions in the
		 * given state 
		 * */
		abstract public List<A> actions(E state);

		/*! return the state resulting from applying action
		 * to state
		 * */
		abstract public E result(E state, A action);

		//! goal test
		abstract public bool goalTest(E state); 

		/*! get the pathCost given cost, which represents
		 * the cost to get to state1, and the cost of
		 * moving to state2 given action
		 */
		abstract public C pathCost(C cost, E state1, A action, E state2); 
	}

	// close constructed type
	public class NPuzzleProblem: AbstractProblem<int[], int, int>
	{
		public int dimension;
		public int size;
		int[] goalState, initialState;

		public NPuzzleProblem(int[] goal, int[] initial)
		{
			double sqrt = Math.Sqrt(goal.Length);
			if (goal.Length != initial.Length || 
					sqrt != Math.Floor(sqrt)) 
			{
				// TODO: throw error
			}
			size = goal.Length;
			dimension = (int) sqrt;

			goalState = goal;
			initialState = initial;
		}


		/*!
		 * Get all the actions possible in a givens state.
		 * First, we must locate the index of the 'empty'
		 * space in the 'board' (array), which in this case
		 * is equal to 'size'. 
		 *
		 * We can determine the possible actions by the direction the 
		 * 'empty' space can be moved: up, down, left or right
		 * We'll use the following conventions:
		 *
		 * up : 2
		 * down: -2
		 * left: -1
		 * right: 1
		 *
		 * 
		 */
		public override List<int> actions(int[] state) 
		{
			int emptyIndex = getEmptyIndex(state);
			List<int> actions = new List<int>();


			//! if emptyIndex is in the leftmost column,
			//! it can be divided by dimension
			//! Anything not in the leftmost column can be moved left, or - 1
			if (emptyIndex % dimension != 0) actions.Add(-1);

			//! if emptyIndex is in the rightmost column,
			//! then adding one to it will be divisible by dimension
			if ((emptyIndex + 1) % dimension != 0) actions.Add(1);

			//! if emptyIndex is greater than or equal to dim, it
			//! must not be in the first row, and hence
			//! can be moved up, or 2
			if (emptyIndex >= dimension) actions.Add(2);

			if (emptyIndex < size - dimension) actions.Add(-2); 

			return actions;
		}

		private bool acceptableAction(int[] state, int emptyIndex, int action) 
		{
			if (action != 1 && action != -1 && action != 2 && action != -2) return false;

			if (getEmptyIndex(state) == emptyIndex) {
				NPuzzleUtils.InvalidEmptyIndexException ex = new NPuzzleUtils.InvalidEmptyIndexException(emptyIndex.ToString());
				throw ex;
			}	

			//! right
			if (action == 1) {
				if ((emptyIndex + 1) % dimension != 0) return true;
				else return false;	

                        //! left
			} else if (action == -1) {
				if (emptyIndex % dimension != 0) return true;
				else return false;
			} else if (action == -2) {
				if (emptyIndex > size - dimension - 1) return true;
				else return false;
			} else {
				if (emptyIndex < size) return false;
				else return true;
			}

		}

		/*!
		 * get the index of the 'empty' space in the puzzle
		 * , which is equal to 'size'
		 */
		private int getEmptyIndex(int[] state)
		{
			for (int i = 0; i < state.Length; i++) {
				if (state[i] == size) return i;
			}

			NPuzzleUtils.MissingEmptyElementException ex = new NPuzzleUtils.MissingEmptyElementException(state.Length.ToString());
			throw ex; 

			return 0;
		}

		public override int[] result(int[] state, int action)
		{
			int[] newState = new int[state.Length];
			int emptyIndex;
			state.CopyTo(newState, 0);

		        emptyIndex = getEmptyIndex(state);
			if (acceptableAction(state, emptyIndex, action))
			{
				if (action == 1) 
					NPuzzleUtils.swap(newState, emptyIndex, emptyIndex + 1);
				else if (action == -1) 
					NPuzzleUtils.swap(newState, emptyIndex, emptyIndex - 1);
				else if (action == -2) 
					NPuzzleUtils.swap(newState, emptyIndex, emptyIndex + dimension);
				else 
					NPuzzleUtils.swap(newState, emptyIndex, emptyIndex - dimension);
			}

			return newState;


         	}

		public override bool goalTest(int[] state) 
		{
			if (this.goalState.Length != state.Length)
				//TODO: throw error;
			for(int i = 0; i < state.Length; i++)
			{
				if (goalState[i] != state[i]) return false;
			}
			return true;
		}

		public override int pathCost(int cost, int[] state1, int action, int[] state2)
		{
			return cost + 1;

		}

		public int stepCost() 
		{
			return 1;

		}
	}
}
