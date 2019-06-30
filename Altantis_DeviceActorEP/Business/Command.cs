using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceActorEP.Business
{
    public class Command
    {
        public string MacAddress { get; set; }
        public string ActorName { get; set; }
        public string Action { get; set; }

        public Command(string macAddress, string actorName, string action)
        {
            MacAddress = macAddress;
            ActorName = actorName;
            Action = action;
        }
    }
}
