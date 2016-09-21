using System;
using Problem;
using System.Windows.Forms;

namespace Puzzle 
{
	class NPuzzle : Form
	{
		static void Main(string[] args)
		{
			NPuzzle npuzzle = new NPuzzle();

		}

		public NPuzzle()
		{
			Text = "Hello Mono World!";
			int[] goalState = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}; 
			int[] initState = new int[] {1, 2, 3, 4, 5, 9, 7, 8, 6};
			try 
			{
				NPuzzleProblem problem = new NPuzzleProblem(goalState, initState);
				Console.WriteLine(NPuzzleUtils.acceptableState(initState));
				// Application.Run(new NPuzzle());
			} catch (System.IndexOutOfRangeException ex)
			{
				System.Console.WriteLine(ex.ToString());

			}	


		}

	}
}

