using System;
using System.Reflection.Metadata;
namespace My_MVCApp
{
	
		public interface IMath
		{
			string AddNums(int a, int b);
		}
		//Service Class
		public class MyClass : IMath
		{
			public string AddNums(int a, int b)
			{
				return "The Sum is " + (a + b);
			}
		}
	

}
