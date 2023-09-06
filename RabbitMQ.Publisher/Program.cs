using RabbitMQ.Client;
using System.Text;

#region Bağlantı Oluşturma

ConnectionFactory factory = new();
factory.Uri = new("X");

#endregion

#region Bağlantıyı Aktifleştirme ve Kanal Açma

using IConnection connection = factory.CreateConnection(); //Bağlantı Aktifleştirme
using IModel channel =  connection.CreateModel(); //Kanal açma
#endregion

#region Queue Oluşturma 

channel.QueueDeclare(queue:"example-queue",exclusive:false);

#endregion

#region Queue'a Mesaj Gönderme
for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+i);
    await Task.Delay(1000);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}
#endregion

Console.Read();