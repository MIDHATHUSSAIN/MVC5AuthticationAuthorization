//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using webAPIandMVC.Models;
//using webAPIandMVC.DBContext;

//namespace webAPIandMVC.Controllers
//{
//    public class WindPowerController : ApiController
//    {

//        //public class WindPowerData
//        //{
//        //    public int WindSpeed { get; set; } // in m/s
//        //    public int PowerOutput { get; set; } // in kW
//        //}


//        //[HttpPost]
//        //public IHttpActionResult PostWindPowerData([FromBody] WindPowerData data)
//        //{
//        //    if (data == null)
//        //    {
//        //        return BadRequest("Invalid data.");
//        //    }

//        //    // Example: Save to database or log (you can replace this with your DB logic)
//        //    System.Diagnostics.Debug.WriteLine($"Received Data: WindSpeed = {data.WindSpeed}, PowerOutput = {data.PowerOutput}");

//        //    return Ok("Data received successfully.");
//        //}
//        private readonly WindPowerDB_Context _db = new WindPowerDB_Context();

//       

//    }
//}
using System.Web.Http;
using webAPIandMVC.Models;
using webAPIandMVC.DBContext;
using System.Linq;

namespace webAPIandMVC.Controllers
{
    public class WindPowerController : ApiController
    {
        private readonly WindPowerDB_Context _db = new WindPowerDB_Context();

        [HttpPost]
        public IHttpActionResult PostWindPowerData([FromBody] WindModel data)
        {
            if (data == null)
            {
                return BadRequest("Invalid data.");
            }

            _db.WindPowerDatas.Add(data);
            _db.SaveChanges();

            return Ok("Data saved to database successfully.");
        }
        [HttpGet]
        public IHttpActionResult GetAllWindPowerData()
        {
            var windData = _db.WindPowerDatas.ToList(); 
            if (windData == null || !windData.Any())
            {
                return NotFound(); 
            }
            return Ok(windData); 
        }

        [HttpGet]
        public IHttpActionResult GetWindPowerDataById(int id)
        {
            var windData = _db.WindPowerDatas.Find(id); 
            if (windData == null)
            {
                return NotFound(); 
            }
            return Ok(windData); 
        }
    }
}
