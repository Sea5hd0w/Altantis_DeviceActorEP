using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Altantis_DeviceActorEP.Service
{
    public class MQTT
    {
        private static readonly Lazy<MQTT> _lazy = new Lazy<MQTT>(() => new MQTT());
        public static MQTT Instance { get { return _lazy.Value; } }

        public string BrokerAddress { get; set; }
        public string TopicAddress { get; set; }
        public string RawDataDetination { get; set; }

        public MqttClient Client { get; set; }
        public string ClientId { get; set; }

        public string Status { get; set; }

        private MQTT()
        {
            LoadConfig();
            if (Status == "") StartMQTT();
        }

        private void LoadConfig()
        {
            Status = "";
            try
            {
                BrokerAddress = "";
                TopicAddress = "";
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("config.json")
                    .Build();
                BrokerAddress = configuration.GetConnectionString("BrokerAddress");
                TopicAddress = configuration.GetConnectionString("TopicAddress");
                RawDataDetination = configuration.GetConnectionString("RawDataDetination");
            }
            catch { Status = "🔴 - EndPoint Down - Config file Error"; }
        }

        private void StartMQTT()
        {
            try
            {
                //Initialize Client
                Client = new MqttClient(BrokerAddress);
                ClientId = Guid.NewGuid().ToString();
                Client.Connect(ClientId);

                Status = "🔵 - EndPoint Up - Connected to " + BrokerAddress + TopicAddress;
            }
            catch { Status = "🔴 - EndPoint Down - MQTT connection Error "; }
        }

        public void Client_MqttPublishMsg(string command)
        {
            Business.Command temp = Mapper.MapperCommand.DAOToBusiness(new DAO.Command(command));

            if (temp != null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(temp);
                Console.WriteLine(json);
                Client.Publish(TopicAddress, Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
        }
    }
}
