using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceActorEP.DAO
{
    public class Command
    {
        public string Content { get; set; }

        public Command(string content)
        {
            Content = content;
        }
    }
}
