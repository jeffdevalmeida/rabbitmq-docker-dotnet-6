using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Objects;
using System.Text;

namespace RabbitMQ.Sample.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(202, Type = typeof(Order))]
        public IActionResult Create([FromBody] Order order)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "order-queue", false, false, false, null);

                string message = JsonConvert.SerializeObject(order);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "order-queue", basicProperties: null, body: body);
            }

            return Accepted(order);
        }
    }
}
