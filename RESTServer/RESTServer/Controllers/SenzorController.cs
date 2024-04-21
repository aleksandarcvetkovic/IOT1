using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

namespace RESTServer
{
    [ApiController]
    [Route("[controller]")]
    public class SenzorController : Controller
    {
        

        [HttpGet("GetSensorData/{idSenzora}")]
        public async Task<ActionResult<SenzorPodaci>> GetAsync(string idSenzora)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
                var client = new SenzorSoba.SenzorSobaClient(channel);
                var reply = await client.GetPodaciAsync(new SenzorID { IdSenzora = idSenzora });
                Console.WriteLine("Odgovor: " + reply);
                //var reply = await client.PutPodaciAsync(new SenzorPodaci { IdSenzora = "90:0f:00:70:91:0a", Temp = 25.5f });
                //Console.WriteLine("Odgovor: " + reply.Poruka);
                ///var reply = await client.DeletePodaciAsync(new SenzorID { IdSenzora = "b8:27:eb:bf:9d:51" });
                //Console.WriteLine("Odgovor: " + reply.Poruka);
                return Ok(reply);
            }
            catch
            {
                return NotFound($"There is no measurements for sensor Id = {idSenzora}"); 
            }
        }
        [HttpPost("AddSensorData")]
        public async Task<ActionResult<Odgovor>> PostAsync([FromBody] SenzorPodaci value)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
                var client = new SenzorSoba.SenzorSobaClient(channel);
                var reply = await client.PutPodaciAsync(value);
                Console.WriteLine("Odgovor: " + reply.Poruka);
                ///var reply = await client.DeletePodaciAsync(new SenzorID { IdSenzora = "b8:27:eb:bf:9d:51" });
                //Console.WriteLine("Odgovor: " + reply.Poruka);
                return Ok(reply);
            }
            catch
            {
                return NotFound($"Greska prilikom dodavanja senzora: {value}");
            }
            
        }
       [HttpPut("UpdateSensor")]
        public async Task<ActionResult<Odgovor>> PutAsync([FromBody] SenzorPodaci sensor)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
                var client = new SenzorSoba.SenzorSobaClient(channel);
                var reply = await client.UpdatePodaciAsync(sensor);
                Console.WriteLine("Odgovor: " + reply.Poruka);
                return Ok(reply);
            }
            catch
            {
                return NotFound($"Greska prilikom brrisanja senzora: {sensor}");
            }
        }
       
        
        [HttpDelete("DeleteSensor/{id}")]
        public async Task<ActionResult<Odgovor>> DeleteAsync(string id)
        {

            try
            {
                var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
                var client = new SenzorSoba.SenzorSobaClient(channel);
                var reply = await client.DeletePodaciAsync(new SenzorID { IdSenzora = id });
                Console.WriteLine("Odgovor: " + reply.Poruka);
                return Ok(reply);
            }
            catch
            {
                return NotFound($"Greska prilikom brrisanja senzora: {id}");
            }


        }




    }
}
