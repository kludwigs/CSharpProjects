/*

Instructions
------------
Create an implementation of the Robot class with the following traits:

- Name the derived class Sonny
- Sonny's version should be 2.0.0.0
- Sonny's greeting should be "Hello, my name is Sonny" (bonus points for using reflection)
- Add a fourth law: "A robot may not harm humanity, or, by inaction, allow humanity to come to harm"

*/

using System;
using System.Collections.Generic;

public class Program
{
	public void Main()
	{
        Version ver; 

        string v1 = "2.0.0.0";
        ver = new Version(v1);
        Robot robot = new Sonny(ver);		
		
		Console.WriteLine("Greeting: {0}", robot.Greeting());
		Console.WriteLine("Robot Version: {0}", robot.Version);
		
		List<string> laws = robot.GetLaws();
		foreach(string law in laws) 
		{
			Console.WriteLine("Law #{0}: {1}", laws.IndexOf(law) + 1, law);
		}
	}
}

// Do you work here...
public class Sonny : Robot
{
	public override List<string> GetLaws() 
	{
        var myList = base.GetLaws();
        myList.Add("A robot may not harm humanity, or, by inaction, allow humanity to come to harm");
        return myList;
	}
	
    public Sonny(Version x) : base(x)    {}
	
    public override string Greeting()
    {
        return "Hello, my name is Sonny";
    }
}

public abstract class Robot
{
    private readonly List<string> _laws = new List<string>
	{
		"A robot may not injure a human being or, through inaction, allow a human being to come to harm.",
		"A robot must obey the orders given it by human beings, except where such orders would conflict with the First Law.",
		"A robot must protect its own existence as long as such protection does not conflict with the First or Second Law."			
	};

    protected Robot(Version version)
    {
        Version = version;
    }

    public abstract string Greeting();

    public Version Version { get; set; }

    public virtual List<string> GetLaws()
    {
    	return _laws;
    }
}
