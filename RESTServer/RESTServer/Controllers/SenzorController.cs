using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTServer.Services;
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
                var reply = await SensorService.GetInstance().GetSensorData(idSenzora);
                Console.WriteLine("Odgovor: " + reply);
              
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
                var reply = await SensorService.GetInstance().AddSensorData(value);
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
                var reply = await SensorService.GetInstance().UpdateData(sensor);
                Console.WriteLine("Odgovor: " + reply.Poruka);
                return Ok(reply);
            }
            catch
            {
                return NotFound($"Greska prilikom promene vrednosti senzora: {sensor}");
            }
        }
       
        
        [HttpDelete("DeleteSensor/{id}")]
        public async Task<ActionResult<Odgovor>> DeleteAsync(string id)
        {

            try
            {
                Console.WriteLine("DeleteSensorCont");
                var reply = await SensorService.GetInstance().DeleteData(id);
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
