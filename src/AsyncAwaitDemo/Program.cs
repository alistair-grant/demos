public static class Program
{
    private static readonly TimeSpan DelayTime = TimeSpan.FromSeconds(2);

    public static async Task Main()
    {
        Console.WriteLine("Calling method that awaits delay before returning...");
        Console.WriteLine();

        WriteNow();
        await MethodThatAwaitsDelay();
        WriteNow();

        Console.WriteLine();
        Console.WriteLine("Calling method that returns delay Task immediately...");
        Console.WriteLine();

        WriteNow();
        await MethodThatReturnsDelayTask();
        WriteNow();

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(intercept: true);
    }

    private static async Task MethodThatAwaitsDelay()
    {
        await Task.Delay(DelayTime);
    }

    private static Task MethodThatReturnsDelayTask()
    {
        return Task.Delay(DelayTime);
    }

    private static void WriteNow()
    {
        Console.WriteLine(DateTime.Now.ToString("T"));
    }
}