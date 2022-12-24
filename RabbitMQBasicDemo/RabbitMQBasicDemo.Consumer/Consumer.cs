using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare("BasicTest", false, false, false, null);

    var consomer = new EventingBasicConsumer(channel);
    consomer.Received += (model, ea) =>
    {
        var body = ea.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        Console.WriteLine($"Recieved {message}");
    };

    channel.BasicConsume("BasicTest", true, consomer);
}

Console.ReadLine();