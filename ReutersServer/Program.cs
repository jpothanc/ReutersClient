using NetMQ;
using NetMQ.Sockets;
using System.Text.Json;

Console.WriteLine("Reuters Publisher");
StartPublishing();

Console.ReadLine();

static void StartPublishing()
{
    using (var server = new PublisherSocket("@tcp://*:5557"))
    {
        string topic = "marketdata?";
        for (int i = 0; i < 10000000; i++)
        {
            var item = MarketDataItem.NewItem(i);
            var json = JsonSerializer.Serialize(item);

            var msg = $"{topic} {json}";
            Console.WriteLine(msg);
            server.SendFrame(msg);

            Thread.Sleep(1000);
        }
    }
}

public class MarketDataItem
{
    public int Id { get; set; }
    public int Ask { get; set; }
    public int Bid { get; set; }

    public static MarketDataItem NewItem(int id)
    {
        return new MarketDataItem()
        {
            Id = id,
            Ask = 0,
            Bid = 0
        };
    }
        
}



//using (var client = new RequestSocket(">tcp://localhost:5556"))  // connect
//{
//    // Send a message from the client socket
//    client.SendFrame("Hello");

//    // Receive the message from the server socket
//    string m1 = server.ReceiveFrameString();
//    Console.WriteLine("From Client: {0}", m1);

//    // Send a response back from the server
//    server.SendFrame("Hi Back");

//    // Receive the response from the client socket
//    string m2 = client.ReceiveFrameString();
//    Console.WriteLine("From Server: {0}", m2);
//}

