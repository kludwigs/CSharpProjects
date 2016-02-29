/*

Instructions
------------
Use generics to improve this program.
Feel free to refactor the code as needed, however, the output should remain unchanged.

*/

using System;

public class Program
{
	public void Main()
	{
		ResponseString responseString = new ResponseString { Success = true, StatusCode = 200, Data = "{ foo: \"bar\" }" };
		ResponseDecimal responseDecimal = new ResponseDecimal { Success = true, StatusCode = 200, Data = 19.99M };
		ResponseDateTime responseDateTime = new ResponseDateTime { Success = true, StatusCode = 200, Data = DateTime.Parse("4/13/2015 4:00PM") };
		
        GenericPrint("String Response", responseString.Data);
        GenericPrint("Decimal Response", responseDecimal.Data);
        GenericPrint("DateTime Response", responseDateTime.Data);
    }
    public static void GenericPrint<T>(string msg, T data)
    {
        Console.WriteLine("{1}: {0}",msg, data);
    }
}

public class ResponseString 
{
	public bool Success { get; set; }
	public int StatusCode { get; set; }
	public string Data { get; set; }
}

public class ResponseDecimal 
{
	public bool Success { get; set; }
	public int StatusCode { get; set; }
	public decimal? Data { get; set; }
}

public class ResponseDateTime
{
	public bool Success { get; set; }
	public int StatusCode { get; set; }
	public DateTime Data { get; set; }
}
