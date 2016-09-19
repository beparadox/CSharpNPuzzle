using System;
using System.Collections;

class NPuzzleUtils
{
	/*!
	 * An acceptable state for the NPuzzle is an array of ints
	 * containing the first state.Length positive integers 
	 * and has an even number of inversions,
	 * not counting the largest element
	 */
	public static bool acceptableState(int[] state)
	{
		int inversions = countInversions(state);
		if (correctElements(state) && inversions % 2 == 0) return true;
		else return false;
	}

	public static bool correctElements(int[] state)
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
	 * piggybacking on merge sort) isn't worth it
	 */
	public static int countInversions(int[] state)
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

	public static void swap(int[] state, int index1, int index2)
	{
		int tmp = state[index1];
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

	public class InvalidEmptyIndexException : Exception
	{
		public InvalidEmptyIndexException(string message)
		{
			System.Console.WriteLine("Your empty element index does not match");
			System.Console.WriteLine(message);

		}
	}
}
