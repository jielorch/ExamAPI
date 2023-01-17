using Exam.API.Helper;
using Exam.Domain.Interface;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Exam.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HashController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HashController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
               var result = await _unitOfWork.HashRepo.GetAll().ToListAsync();
                var data = from query in result
                           group query by query?.Date.Value.DayOfYear into g
                         //  group query by String.Format("{0:yyyy-MM-dd}", query.Date) into g
                           select new
                           {
                               date = String.Format("{0:yyyy-MM-dd}", g.Select(x => x.Date).FirstOrDefault()),
                               count = g.Count()
                           };
                return Ok(data);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            byte[]? encodedMessage = null;
            try
            {
                await Task.Run(() =>
                {
                    var factory = new ConnectionFactory { HostName = "localhost" };

                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: "hash", durable: false, exclusive: false,
                        autoDelete: false, arguments: null);

                    List<Hash> ListSha1 = new();
                    int limit = 1000; //change this to 40000

                    for(int i = 0; i < limit; i++)
                    {
                        Hash h = new()
                        {
                            Sha1 = StringGenerator.RandomString(5),
                            Date = DateTime.Now
                        };
                        ListSha1.Add(h);
                    }

                    var message = JsonSerializer.Serialize(ListSha1);

                     encodedMessage = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "hash", null, encodedMessage);

                   
                });

                return Ok(new { message = "Message sent" });
            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
