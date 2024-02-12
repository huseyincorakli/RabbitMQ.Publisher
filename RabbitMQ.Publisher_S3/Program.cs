using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ymsclgok:bW2XZJGz5gmJSCs7D0NFtcX_RxbHvkR-@sparrow.rmq.cloudamqp.com/ymsclgok");

using IConnection connection = factory.CreateConnection(); //Bağlantı Aktifleştirme
using IModel channel = connection.CreateModel(); //Kanal açma

//1.adım ==> exchange oluşturup adı ve tipini gir

channel.ExchangeDeclare(exchange: "direct-exchange-example", ExchangeType.Direct);

while (true)
{
    Console.Write("Mesaj:");
    string message= Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    //2.adım ilgili exchange'e routkey vererek hangi kuyruğa gideceğini belirt
    channel.BasicPublish(exchange:"direct-exchange-example",routingKey:"direct-queue-example",body:byteMessage);
}


Console.Read();