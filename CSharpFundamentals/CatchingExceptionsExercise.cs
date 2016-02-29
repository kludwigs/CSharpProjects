/*

Instructions
------------
This program relies on an call to an external dependency that sometimes fails.
Handle the exception and ouput the appropriate response.

*/

using System;

public class Program
{
	public void Main()
	{
		Result result = GetResult();
		Console.WriteLine(result.Successful ? "It worked!!" : "Oops, something went wrong...");
	}

	private Result GetResult()
	{
		Result result = new Result();
		
		ExternalDependency service = new ExternalDependency();
		
		try
		{
			service.DoWork();
			result.Successful = true;
		}
		catch
		{
			result.Successful =false;
		}
		
		
		return result;
	}
}

public class Result
{
	public bool Successful { get; set; }
}

// Please do not change the following class.
// Assume that it is an external dependency that is out of your control.
sealed class ExternalDependency
{
	public void DoWork()
	{
		int[] array = new int[1];
		array[new Random().Next(2)] = 0;
	}
}