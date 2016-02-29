/*

Instructions
------------
Reverse the string and output the result.
For the purposes of this exercise, please avoid using Array.Reverse().

*/

using System;

public class Program
{
	public void Main()
	{
		string str = "Reverse me!!";
		
		char[] revchar = new char[str.Length];
		
		int j=0;
		
		for (int i = str.Length -1; i>=0; i--)
		{
			revchar[j++] = str[i];
		}
		string revstr = new string(revchar);
		
		
		Console.WriteLine("Answer: {0}", revstr);
	}
}