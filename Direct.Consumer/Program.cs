﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

Console.WriteLine("[*] waiting for message");

var consumer=new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[X] recieved message {message}");

};

channel.BasicConsume(queue: "qu-notification",autoAck:true,consumer:consumer);

Console.WriteLine("Press enter to exit");
Console.ReadLine();