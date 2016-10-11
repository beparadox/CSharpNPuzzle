using System;
using Problem;
using System.Windows.Forms;

namespace Puzzle 
{
	class NPuzzle : Form
	{
		static void Main(string[] args)
		{

		}

		public NPuzzle()
		{
			try 
			{
				// NPuzzleProblem problem = new NPuzzleProblem(goalState, initState);
				// Application.Run(new NPuzzle());
			} catch (System.IndexOutOfRangeException ex)
			{
				System.Console.WriteLine(ex.ToString());

			}	


		}

	}
}

