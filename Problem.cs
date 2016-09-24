namespace Problem
{
        using System;
        using System.Collections.Generic;

	/**
	 * Abstract class Problem. Given an initial state and goal state of type E,
	 * use other defined methods to 
	 *
	 *
	 */
	abstract public class AbstractProblem<E, A, C>
	{
		//! initial state
		public E initialState;
		//! goal state
		public E goalState;

		public AbstractProblem(E initial, E goal)
		{
			initialState = initial;
			goalState = goal;
		}	

		protected AbstractProblem() {}

		/*! return a list of possible actions in the
		 * given state 
		 * */
		abstract public List<A> Actions(E state);

		/*! return the state resulting from applying action
		 * to state
		 * */
		abstract public E Result(E state, A action);

		//! goal test
		abstract public bool GoalTest(E state); 

		/*! get the pathCost given cost, which represents
		 * the cost to get to state1, and the cost of
		 * moving to state2 given action
		 */
		abstract public C PathCost(C cost, E state1, A action, E state2); 
	}

	// close constructed type
	public class NPuzzleProblem: AbstractProblem<int[], int, int>
	{
		public int dimension;
		public int size;
		public new int[] goalState, initialState;

		public NPuzzleProblem(int[] goal, int[] initial)
		{
			//! both goal and initial should be the same length
			//! goal should be a valid goal state (positive integers 1 through size in order
			//! initial should be an acceptableState (even number of inversions,
			//!     not including inversions with the largest
			//!     element in the state, which = size
			//! 
			string error = "Empty"; 
			bool errorFlag = false;

			if (!EqualStateLengths(goal, initial)) {
				error = "Goal state length and initial state length differ";
				errorFlag = true;
			} else 
                        if (!ValidStateLength(goal)) {
				error = "The lengt of a state should be equal to the square of an integer.";
				errorFlag = true;
			} else
			if (!ValidGoalState(goal)) {
				error = "Invalid goal state. It should be the positive integers from 1 to n, where n is the size of the puzzle";
				errorFlag = true;
			} else if (!ValidInitialState(initial)) {
                           	error = "Invalid initial state. It should contain all positive integers from 1 to n, where n is the size of the puzzle, and contain an even number of inversions when not comparing elements to the largest element of size n";
				errorFlag = true;

			}

		        if (errorFlag)
			{

	         		NPuzzleUtils.InvalidNPuzzleStatesException ex =
				new NPuzzleUtils.InvalidNPuzzleStatesException(error);
				throw ex;
			}

                      	double sqrt = Math.Sqrt(goal.Length);
			goalState = goal;
			initialState = initial;
			size = goal.Length;
			dimension = (int) sqrt;

		}


		private bool EqualStateLengths(int[] goal, int[] initial)
		{
			if (goal.Length != initial.Length) return false;
			else return true;
		}

                private bool ValidStateLength(int[] goal)
		{
			double sqrt = Math.Sqrt(goal.Length);
			if (sqrt != (double) Math.Floor(sqrt)) return false;
			else return true;
		}

		private bool ValidGoalState(int[] goal) 
		{
			int l = goal.Length;
			for (int i = 0; i < l; i++) {
				if (goal[i] != i + 1) return false;
			}

			return true;
		}

		private bool ValidInitialState(int[] initial)
		{
			return NPuzzleUtils.AcceptableState(initial);
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
		public override List<int> Actions(int[] state) 
		{
			int emptyIndex = GetEmptyIndex(state);
                        if (emptyIndex == -1) 
			{
				NPuzzleUtils.MissingEmptyElementException ex = new NPuzzleUtils.MissingEmptyElementException(state.Length.ToString());
				throw ex; 
			}
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

		/*!
		 * Determine whether or not an action is acceptable for the given state
		 */
		public bool AcceptableAction(int[] state, int emptyIndex, int action) 
		{
			if (action != 1 && action != -1 && action != 2 && action != -2) return false;

			if (GetEmptyIndex(state) != emptyIndex) {
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
			//! down
			} else if (action == -2) {
				if (emptyIndex < size - dimension) return true;
				else return false;
                        //! up
			} else {
				if (emptyIndex < dimension) return false;
				else return true;
			}

		}

		/*!
		 * get the index of the 'empty' space in the puzzle
		 * , which is equal to 'size'
		 */
		public int GetEmptyIndex(int[] state)
		{
			for (int i = 0; i < state.Length; i++) {
				if (state[i] == size) return i;
			}

			
			return -1;
		}

		public override int[] Result(int[] state, int action)
		{
			int[] newState = new int[state.Length];
			int emptyIndex;
			state.CopyTo(newState, 0);

		        emptyIndex = GetEmptyIndex(state);
			if (emptyIndex == -1) 
			{
				NPuzzleUtils.MissingEmptyElementException ex = new NPuzzleUtils.MissingEmptyElementException(state.Length.ToString());
				throw ex; 
			}
			if (AcceptableAction(state, emptyIndex, action))
			{
				if (action == 1) 
					NPuzzleUtils.Swap(newState, emptyIndex, emptyIndex + 1);
				else if (action == -1) 
					NPuzzleUtils.Swap(newState, emptyIndex, emptyIndex - 1);
				else if (action == -2) 
					NPuzzleUtils.Swap(newState, emptyIndex, emptyIndex + dimension);
				else 
					NPuzzleUtils.Swap(newState, emptyIndex, emptyIndex - dimension);
			} else {
				String msg = String.Format("You entered an invalid action: {0} for the state {1}", action, state.ToString());
				NPuzzleUtils.ResultAcceptableActionException ex = new NPuzzleUtils.ResultAcceptableActionException(msg);
				throw ex;

			}

			return newState;


         	}

		public override bool GoalTest(int[] state) 
		{
			if (this.goalState.Length != state.Length) 
			{
				return false;
			}
			for(int i = 0; i < state.Length; i++)
			{
				if (goalState[i] != state[i]) return false;
			}
			return true;
		}

		public override int PathCost(int cost, int[] state1, int action, int[] state2)
		{
			return cost + 1;

		}

		public int stepCost() 
		{
			return 1;

		}
	}

	/*!
	 * Create an abstract class for defining problem states. A
	 * state in the context of a problem could possible be a
	 * wide variety of types - a number, a string, an data structure
	 * an object. Keep it as general as possible. Make it 
	 * extand IEquatable because then comparisons for equality
	 * become possible
	 *
	 */
	abstract public class AbstractProblemState<S> : IEquatable<AbstractProblemState<S> >
	{
		public S state;
		public AbstractProblemState(S s)
		{
			state = s;
		}

		protected AbstractProblemState() {}

		/*public S State
		{
			get {return state;}
			set {state = value;}
		}*/

		public override bool Equals(object obj)
		{
			return this.Equals(obj as AbstractProblemState<S>);
		}

		abstract public bool Equals(AbstractProblemState<S> state);
	}

	public class NPuzzleProblemState: AbstractProblemState<int[]>
	{
		public override bool Equals(AbstractProblemState<int[]> state) 
		{
			if (Object.ReferenceEquals(state.state, null))
			{
				return false;
			}

			if (Object.ReferenceEquals(this.state, state.state)) 
			{
				return true;
			}

			if (this.state.GetType() != state.state.GetType())
				return false;


			int l = state.state.Length;

			if (this.state.Length != l) return false;

			for (int i = 0; i < l; i++) 
			{
				if (state.state[i] != this.state[i]) return false;
			}

			return true;
		}
	}
}
