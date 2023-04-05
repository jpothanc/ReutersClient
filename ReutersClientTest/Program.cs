using ReutersClient;
Console.WriteLine("Reuters Client Test");

CancellationTokenSource cts = new CancellationTokenSource();
IReutersClient client =  ReutersClientFactory.Create(x =>
{

    x.SubConnectionString = "tcp://localhost:5557";
    x.SubTopic = "pubmdinfo";
    x.PubConnectionString = "tcp://localhost:5559";
    x.PubTopic = "subsymbols";
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
Task.Delay(5000).Wait();
client.Subscribe("6758.T");

//cts.Cancel();
Console.ReadLine();


