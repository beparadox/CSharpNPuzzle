namespace MyPractice
{
	public class Base
	{
		private int s = 1;
		private string mystring = "I run this class";
		public double pi = 3.1415;
		public int squared;

		public Base(int val)
		{
			squared = val*val;
		}

		public double GetArea(double radius)
		{
			return pi * radius * radius;
		}

		public string Mystring
		{
			get {return mystring;}
		}
	}

	public class Derived : Base
	{
		public Derived()
		{

		}


	}


}
