using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

const string message = "Hello Message";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "amq.topic", routingKey: "h.y", basicProperties: null, body: body);

Console.WriteLine($"[x] message is sent", message);

Console.WriteLine(" Press any key to exit");
Console.ReadLine();