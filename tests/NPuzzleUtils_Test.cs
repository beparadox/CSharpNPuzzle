using System;
using NUnit.Framework;

namespace NPuzzleTests
{
	[TestFixture]
	public class NPuzzleUtils_Tests
	{
		[Test]
		public void GenerateInitState()
		{
			int [] state;
			int size = 9;
			state = NPuzzleUtils.generateInitState(size);
			Assert.That(state, Has.Exactly(1).EqualTo(9));

			Assert.That(state, Has.Exactly(1).EqualTo(5));

			Assert.That(state, Has.Exactly(8).LessThan(9));

			Assert.That(state, Has.Exactly(8).GreaterThan(1));



		}

	}


}	
