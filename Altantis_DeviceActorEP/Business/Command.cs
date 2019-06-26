using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceActorEP.Business
{
    public class Command
    {
        public string MacAddress { get; set; }
        public string DeviceType { get; set; }
        public string Action { get; set; }

        public Command(string macAddress, string deviceType, string action)
        {
            MacAddress = macAddress;
            DeviceType = deviceType;
            Action = action;
        }
    }
}
