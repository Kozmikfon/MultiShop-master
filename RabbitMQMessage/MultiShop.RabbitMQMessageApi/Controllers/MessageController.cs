using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace MultiShop.RabbitMQMessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMessage()
        {
            var connectionfactory = new ConnectionFactory()
            {
                HostName= "localhost",
                
            };
            var connection= connectionfactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("Kuyruk1",false,false,false,arguments:null);
            var messageContent="Merhaba bu bir RabbitMQ kuyruk mesajıdır";
            var byteMessageContent=Encoding.UTF8.GetBytes(messageContent);
            channel.BasicPublish(exchange: "", routingKey: "Kuyruk1", basicProperties: null,body:byteMessageContent);

            return Ok("Mesajınız alınmıştır");
        }
       
    }
}
