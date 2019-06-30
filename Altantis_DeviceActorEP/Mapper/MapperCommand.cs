using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceActorEP.Mapper
{
    public static class MapperCommand
    {
        public static Business.Command DAOToBusiness(DAO.Command dao)
        {
            try
            {
                var jo = JObject.Parse(dao.Content);
                return new Business.Command(jo["macAddress"].ToString(), jo["actorName"].ToString(), jo["action"].ToString());
            }
            catch { return null; }
        }
    }
}
