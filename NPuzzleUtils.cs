using System;
using System.Collections;
using Problem;

class NPuzzleUtils
{
	/*!
	 *  static Random _random = new Random();
	 *
	 *  Found on https://www.dotnetperls.com/fisher-yates-shuffle
	 *  Used for the Shuffle method
	 */
	static Random _random = new Random();

	/*!
	 * Generate a random acceptable initial state
	 *
	 */
	public static Problem.NPuzzleState<int[]> GenerateInitState(int size)
	{
		int[] state = new int[size];
		for (int i = 0; i < size; i++) state[i] = i + 1;

		NPuzzleUtils.Shuffle(state);

		while(!NPuzzleUtils.AcceptableState(state)) 
		{
		        NPuzzleUtils.Shuffle(state);
		}


		return new Problem.NPuzzleState<int[]>(state);
	}

	/*!
	 * Create a random instance of a problem
	 */
	public static Problem.NPuzzleProblem<Problem.NPuzzleState<int[]>, int, int> CreateProblem(int size)
	{
		if (!AcceptableLength(size))
		{
			string msg = String.Format("Length given: {0}", size);
			UnacceptableLengthException ex = new UnacceptableLengthException(msg);
			throw ex;
		}

		int[] goalState = new int[size];
		NPuzzleState<int[]> initial;
		for (int i = 0; i < size; i++) 
			goalState[i] = i + 1;

		NPuzzleState<int[]> goal = new NPuzzleState<int[]>(goalState);
		initial = GenerateInitState(size);

		Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int> problem = new Problem.NPuzzleProblem<NPuzzleState<int[]>, int, int>(goal, initial);
		return problem;
	}

	/*!
	 * An acceptable state for the NPuzzle is an array of ints
	 * containing whose size is equal to the square of an
	 * integer, 
	 * contains the first state.Length positive integers 
	 * and has an even number of inversions,
	 * not counting inversions with the largest element
	 */ 
	public static bool AcceptableState(int[] state)
	{
		int inversions = CountInversions(state);
		if (AcceptableLength(state.Length) && 
				CorrectElements(state) && 
				inversions % 2 == 0) 
			return true;
		else 
			return false;
	}

	/*!
	 * A state should have the length equal to the square
	 * of a number
	 *
	 */
	public static bool AcceptableLength(int size)
	{
		double sqrt = Math.Sqrt(size);
		if (sqrt == (double) Math.Floor(sqrt)) return true;
		else return false;
	}

	/*!
	 * Assert that a given array contains the correct elements
	 * for to represent an N-Puzzle state. For a state of size
	 * numElements, that should be all positive integers between
	 * 1 to numElements
	 */
	public static bool CorrectElements(int[] state)
	{
		int[] copy = new int[state.Length];
		state.CopyTo(copy, 0);
		Array.Sort(copy);

		for (int i = 0; i < copy.Length; i+=1) if (copy[i] != i + 1) return false;

		return true;
	}

	/*!
	 * Use the brute force approach to counting inversions
	 * As the input array will never be much larger than
	 * 36 elements (such n-puzzles are intractable at the
	 * moment), using O(n*log(n)) algorithms (such as the one
	 * piggybacking on merge sort) isn't worth the time.
	 *
	 * No need to count inversions with the largest element
	 * in the array, which should equal state.Length. 
	 */
	public static int CountInversions(int[] state)
	{
		int inversions = 0;

		for (int i = 0; i < state.Length - 1; i++)
		{
			for (int j = i + 1; j < state.Length; j++)
			{
				if (state[i] != state.Length && state[j] != state.Length && state[i] > state[j]) inversions += 1;

			}

		}

		return inversions;
	}

	/*!
	 * Shuffle an array. Found on https://dotnetperls.com/fisher-yates-shuffle
	 */
	public static void Shuffle<T>(T[] state)
	{

		int l = state.Length;
		for (int i = 0; i < l; i++) {
			//! NextDouble returns a double from
			//! 0 to 1
			int r = i + (int)(_random.NextDouble() * (l - i));
			NPuzzleUtils.Swap(state, i, r);
		}

	}

	public static void Swap<T>(T[] state, int index1, int index2)
	{
		T tmp = state[index1];
		state[index1] = state[index2];
		state[index2] = tmp;
	}

	public class MissingEmptyElementException : Exception
	{
		public MissingEmptyElementException(string message)
		{
			System.Console.WriteLine("A instance of an npuzzle must have an element in the underlying array that is equal to the length of the array (size of puzzle). In this case the length of the array is ");
			System.Console.WriteLine(message);

		}

	}

	public class InvalidProblemException : Exception
	{
		public InvalidProblemException(string message)
		{
			System.Console.WriteLine("An invalid problem was given for this search");
			System.Console.WriteLine(message);

		}

	}

	public class InvalidProblemPropertyException: Exception
	{
		public InvalidProblemPropertyException(string message)
		{
			System.Console.WriteLine("This instance of AbstractProblem has an invalid property");
			System.Console.WriteLine(message);

		}

	}

	public class InvalidEmptyIndexException : Exception
	{
		public InvalidEmptyIndexException(string message)
		{
			System.Console.WriteLine("Your empty element index does not match");
			System.Console.WriteLine(message);

		}
	}

	public class InvalidNPuzzleStatesException: Exception
	{
		public InvalidNPuzzleStatesException(string message)
		{
			System.Console.WriteLine("The goal state or the initial state for your NPuzzle problem is invalid.");
			System.Console.WriteLine(message);

		}

	}

	public class ResultAcceptableActionException: Exception
	{
		public ResultAcceptableActionException(string message)
		{
			System.Console.WriteLine("You gave an unacceptable action for the current state.");
			System.Console.WriteLine(message);

		}

	}

	public class UnacceptableLengthException : Exception
	{
		public UnacceptableLengthException(string message)
		{
	                System.Console.WriteLine("Length of a string must be the square of an integer");
			System.Console.WriteLine(message);



		}
	}

	public class NullSearchNodeException : Exception
	{
		public NullSearchNodeException(string message)
		{
			System.Console.WriteLine("Null node exception in search");
			System.Console.WriteLine(message);

		}


	}
}
