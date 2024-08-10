using KnowledgeBase.Examples.TrickyScheduler; 

Console.WriteLine("Schedule test");
try
{
    var scheduleString = "*.*.* * *:*:0.0"; 
    // var input = System.DateTime.Now; 
    var input = new System.DateTime(2023, 2, 12, 0, 14, 23, 34); 
    // var expected = input.AddMinutes(1).AddSeconds(-input.Second); 
    Schedule schedule = new Schedule(scheduleString); 
    
    // Act
    var actual = schedule.PrevEvent(input); 
    System.Console.WriteLine($"input: '{input}'"); 
    // System.Console.WriteLine($"expected: '{expected}'"); 
    System.Console.WriteLine($"actual: '{actual}'"); 
}
catch (System.Exception ex)
{
    System.Console.WriteLine(ex); 
}
