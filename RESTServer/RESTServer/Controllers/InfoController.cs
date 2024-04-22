using Microsoft.AspNetCore.Mvc;
using RESTServer.Services;

namespace RESTServer
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : Controller
    {
        [HttpPut("GetMaxData")]
        public async Task<ActionResult<Value>> GetAsync([FromBody] Query query)
        {

            try
            {
                Console.WriteLine("GetMaxDataCont");
                var reply = await SensorService.GetInstance().GetMaxPodaci(query);
                Console.WriteLine("Odgovor: " + reply);

                return Ok(reply);
            }
            catch
            {
                return NotFound($"There is no measurements for sensor Id = {query.IdSenzora}");
            }
        }
        [HttpPut("GetMinData")]
        public async Task<ActionResult<Value>> GetAsync2([FromBody] Query query)
        {
            try
            {
                Console.WriteLine("GetMinDataCont");
                var reply = await SensorService.GetInstance().GetMinPodaci(query);
                Console.WriteLine("Odgovor: " + reply);

                return Ok(reply);
            }
            catch
            {
                return NotFound($"There is no measurements for sensor Id = {query.IdSenzora}");
            }
        }
        [HttpPut("GetAvgData")]
        public async Task<ActionResult<Value>> GetAsync3([FromBody] Query query)
        {
            try
            {
                Console.WriteLine("GetAvgDataCont");
                var reply =await SensorService.GetInstance().GetAvgPodaci(query);
                Console.WriteLine("Odgovor: " + reply);

                return Ok(reply);
            }
            catch
            {
                return NotFound($"There is no measurements for sensor Id = {query.IdSenzora}");
            }
        }
       
    }
}
