using System;
using Problem;
namespace Puzzle 
{
	class NPuzzle 
	{
		static void Main(string[] args)
		{
			int[] goalState = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}; 
			int[] initState = new int[] {1, 2, 3, 4, 5, 9, 7, 8, 6};
			try 
			{
				NPuzzleProblem problem = new NPuzzleProblem(goalState, initState);
				Console.WriteLine(NPuzzleUtils.acceptableState(initState));
			} catch (System.IndexOutOfRangeException ex)
			{
				System.Console.WriteLine(ex.ToString());



			}	


		}

	}
}

