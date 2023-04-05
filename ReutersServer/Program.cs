using ReutersServer;

Console.WriteLine("Reuters Server Start");
var server = Server.Create(x =>
{
    x.PubConnectionString = "@tcp://*:5557";
    x.SubConnectionString = "tcp://localhost:5559";
    x.PubTopic = "pubmdinfo?";
    x.SubTopic = "subsymbols";

});
server.Start();

Console.ReadLine();



