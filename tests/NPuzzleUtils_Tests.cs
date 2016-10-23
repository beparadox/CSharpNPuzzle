namespace NPuzzleTests
{
	using System;
	using NUnit.Framework;
	using Problem;


	[TestFixture]
	public class NPuzzleUtils_Tests
	{
		[Test]
		/*!
		 * Don't test for inversions with the largest
		 * element in the array
		 */
                public void TestCountInversions()
		{
			int[] s1 = {9, 8, 7, 6, 5, 4, 3, 2, 1};
			Assert.AreEqual(NPuzzleUtils.CountInversions(s1), 28);
			NPuzzleUtils.Swap(s1, 1, 2);
			Assert.AreEqual(NPuzzleUtils.CountInversions(s1), 27);
			int[] s2 = {1, 2, 3, 4, 5, 6,7 ,8, 9};

			Assert.AreEqual(NPuzzleUtils.CountInversions(s2), 0);

			NPuzzleUtils.Swap(s2, 7, 8);

			Assert.AreEqual(NPuzzleUtils.CountInversions(s2), 0);

			NPuzzleUtils.Swap(s2, 6, 7);

			Assert.AreEqual(NPuzzleUtils.CountInversions(s2), 0);
		}

		[Test]
                public void TestCorrectElements()
		{
			int size = 9;
			int[] array = new int[size];
			for (int i = 0; i < size; i++) array[i] = i + 1;
			for (int j = 0 ; j < 10; j++) {
			  NPuzzleUtils.Shuffle(array);
			  Assert.AreEqual(NPuzzleUtils.CorrectElements(array), true);
			}



		}

		[Test]
		public void TestAcceptableState()
		{
			int[] state1 = {1, 2, 3, 4, 5, 6, 8, 7, 9};
			Assert.AreEqual(NPuzzleUtils.AcceptableState(state1), false);

			int[] state2 = {1, 2, 3, 4, 8, 5, 6, 7, 9};
			Assert.AreEqual(NPuzzleUtils.AcceptableState(state2), false);
			int[] state3 = {2, 1, 4, 3, 5, 6, 7, 8, 9};
			Assert.AreEqual(NPuzzleUtils.AcceptableState(state3), true);

		}

		[Test]
		public void GenerateInitStateTest()
		{
			NPuzzleState<int[]> state;
			int size = 9;
			state = NPuzzleUtils.GenerateInitState(size);
			Assert.That(state.State, Has.Exactly(1).EqualTo(size));

			Assert.That(state.State, Has.Exactly(1).EqualTo(size - 4));

			Assert.That(state.State, Has.Exactly(size - 1).LessThan(size));

			Assert.That(state.State, Has.Exactly(size - 1).GreaterThan(1));
			Assert.That(state.State, Has.Exactly(size - 4).GreaterThan(4));


         		Assert.AreEqual(NPuzzleUtils.AcceptableState(state.State), true);
	         	Console.WriteLine(state.ToString());

		}

		

	}


}	
