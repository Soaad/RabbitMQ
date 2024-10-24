using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

Console.WriteLine("[*] waiting for message");

ConsumeMessage("q.fanout1");
//ConsumeMessage("q.fanout2");
//ConsumeMessage("q.fanout3");



Console.WriteLine("Press enter to exit");
Console.ReadLine();

void ConsumeMessage(string queue)
{
    var consumer=new EventingBasicConsumer(channel);    
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        //do your processing here such as sending notification
        Console.WriteLine($"[X] recieved message {message} from Queue{ queue}");

    };
    channel.BasicConsume(queue:queue,autoAck:true,consumer:consumer);

}