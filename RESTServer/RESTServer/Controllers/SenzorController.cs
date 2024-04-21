using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace RESTServer
{
    [ApiController]
    [Route("[controller]")]
    public class SenzorController : Controller
    {
        

        [HttpGet("{idSenzora}")]
        public ActionResult<SenzorPodaci> Get(string idSenzora)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new SenzorSoba.GreeterClient(channel);
                var response = await client.SayHelloAsync(new HelloRequest { Name = "World" });
                return new SenzorPodaci("vreme","ajkdfhasf",10,20,false,false,0,23);
            }
            catch
            {
                return NotFound($"There is no measurements for sensor Id = {idSenzora}"); 
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] SenzorPodaci value)
        {
            return Ok();    
            //return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }




    }
}
