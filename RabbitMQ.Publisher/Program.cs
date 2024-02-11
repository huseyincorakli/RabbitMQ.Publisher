using RabbitMQ.Client;
using System.Text;

#region Bağlantı Oluşturma

ConnectionFactory factory = new();
factory.Uri = new("amqps://ymsclgok:bW2XZJGz5gmJSCs7D0NFtcX_RxbHvkR-@sparrow.rmq.cloudamqp.com/ymsclgok");

#endregion

#region Bağlantıyı Aktifleştirme ve Kanal Açma

using IConnection connection = factory.CreateConnection(); //Bağlantı Aktifleştirme
using IModel channel =  connection.CreateModel(); //Kanal açma
#endregion

#region Queue Oluşturma 
//rabbitmq sunucusunda oluşan sorunlardan dolayı kuyruğu ve mesajları kalıcı yapmak için durable true olmalı
channel.QueueDeclare(queue:"example-queue",exclusive:false,durable:true);
//ibasicproperties interface türünde properties oluşturup persistent özelliğini true yaparak basicpublish içeriisnde vermemiz gerekmektedir.
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

#endregion

#region Queue'a Mesaj Gönderme
for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+i);
    await Task.Delay(100);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message,basicProperties:properties);
}
#endregion

Console.Read();