using ReutersClient;
Console.WriteLine("Reuters Client Test");

CancellationTokenSource cts = new CancellationTokenSource();
IReutersClient client =  ReutersClientFactory.Create(x =>
{

    x.ConnectionString = "tcp://localhost:5557";
    x.Topic = "marketdata";
    x.OnUpdate(md =>
    {
        Console.WriteLine($"Update Received");
        foreach (var key in md.Item.Keys)
        {
            Console.WriteLine($"{key}-{md.Item[key]}");
        }
    });
    x.CancellationTokenSource = cts;

});
//Task.Delay(5000).Wait();
//cts.Cancel();
Console.ReadLine();


