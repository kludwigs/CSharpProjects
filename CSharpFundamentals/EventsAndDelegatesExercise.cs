/*

Instructions
------------
Create an instance of the ClockTower class with a delay of 10-1000 milliseconds.
Observe the OnChime event and output the messages using Console.WriteLine().

*/

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

public class Program
{	
	public void Main()
	{
		// Do your work here...
        ClockTower ct = new ClockTower(300);

        ct.OnChime += delegate(object o, ChimeEventArgs e)
        {
            Console.WriteLine(e.Message);
        };
		
		// Keep the main thread alive long enough to observe the event.
		Thread.Sleep(3000);
	}
}

public sealed class ClockTower
{
	private readonly Stopwatch _stopWatch = new Stopwatch();
	private readonly int _delayMilliseconds;
	
	public ClockTower(int delayMilliseconds)
	{
		_stopWatch.Start();
		_delayMilliseconds = delayMilliseconds;
		DelayedChime();
	}

	public delegate void ChimeEventHandler(object sender, ChimeEventArgs e);
	public event ChimeEventHandler OnChime;
	public async void DelayedChime()
	{
		await Task.Delay(_delayMilliseconds);
		OnChime(this, new ChimeEventArgs(_stopWatch.ElapsedMilliseconds));
		
		await Task.Delay(_delayMilliseconds);
		OnChime(this, new ChimeEventArgs(_stopWatch.ElapsedMilliseconds));
	}
}

public class ChimeEventArgs
{
	public ChimeEventArgs(long milliseconds)
	{
		Message = String.Format("The clock chimed after {0} seconds", milliseconds / 1000M);
	}

	public string Message { get; set; }
}