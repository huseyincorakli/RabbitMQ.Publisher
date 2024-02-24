using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ymsclgok:bW2XZJGz5gmJSCs7D0NFtcX_RxbHvkR-@sparrow.rmq.cloudamqp.com/ymsclgok");

using IConnection connection = factory.CreateConnection(); //Bağlantı Aktifleştirme
using IModel channel = connection.CreateModel(); //Kanal açma

string exchangeName = "fanout-exchange-example";

channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(1000);
    byte[] message = Encoding.UTF8.GetBytes($"Message {i}");
    
    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: string.Empty,
        body: message);
}