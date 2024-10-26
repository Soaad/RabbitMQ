using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

var properties=channel.CreateBasicProperties();
properties.Persistent = false;

Dictionary<string,object> dictionary= new Dictionary<string, object>();
dictionary.Add("name", "info");

properties.Headers=dictionary;  

const string message = "Hello Message";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "amq.headers", routingKey: string.Empty, basicProperties: properties, body: body);

Console.WriteLine($"[x] message is sent", message);

Console.WriteLine(" Press any key to exit");
Console.ReadLine();