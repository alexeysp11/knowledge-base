namespace KnowledgeBase.Examples.MultithreadingTests;

public class Program
{
    public static void Main()
    {
        // var instance = new SynchronizationPromitives();
        var instance = new TestConfigureAwait();
        instance.Execute();
    }
}