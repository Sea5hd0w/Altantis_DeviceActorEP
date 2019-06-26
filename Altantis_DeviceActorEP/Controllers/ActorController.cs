using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Altantis_DeviceActorEP.Controllers
{
    [Produces("application/json")]
    [Route("api/Actor")]
    public class ActorController : Controller
    {
        // GET: api/Actor
        [HttpGet]
        public string Get()
        {
            return Service.MQTT.Instance.Status;
        }

        // POST: api/Actor
        [HttpPost]
        public async Task<string> ReadStringDataManual()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                //Console.WriteLine(reader.ReadToEndAsync().Result);
                Service.MQTT.Instance.Client_MqttPublishMsg(reader.ReadToEndAsync().Result);
                return await reader.ReadToEndAsync();
            }
        }
    }
}
