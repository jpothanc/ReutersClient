using ProtoBuf;
using ReutersServer;

Console.WriteLine("Reuters Server Start");
var server = Server.Create(x =>
{
    x.PubConnectionString = "@tcp://*:5557";
    x.SubConnectionString = "tcp://localhost:5559";
    x.PubTopic = "";
    x.SubTopic = "subsymbols";
    //x.WithProtobuf();
    x.WithJson();

});
server.Start();

Console.ReadLine();



