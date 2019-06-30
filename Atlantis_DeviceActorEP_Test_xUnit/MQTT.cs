using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Atlantis_DeviceActorEP_Test_xUnit
{
    class MQTT
    {
        public string BrokerAddress { get; set; }
        public string TopicAddress { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int ConnectionPort { get; set; }

        public MqttClient Client { get; set; }
        public string ClientId { get; set; }

        List<string> MessageRecive { get; set; }

        public MQTT()
        {
            MessageRecive = new List<string>();

            LoadConfig();
            StartMQTT();
        }

        private void LoadConfig()
        {
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
                UserName = configuration.GetConnectionString("UserName");
                UserPassword = configuration.GetConnectionString("UserPassword");
                ConnectionPort = Convert.ToInt32(configuration.GetConnectionString("ConnectionPort"));
            }
            catch { }
        }

        private void StartMQTT()
        {
            try
            {
                //Initialize Client
                Client = new MqttClient(BrokerAddress, ConnectionPort, true, null, null, MqttSslProtocols.TLSv1_2);
                Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                ClientId = Guid.NewGuid().ToString();
                Client.Connect(ClientId, UserName, UserPassword);

                //Subscibe
                if (BrokerAddress != "" & TopicAddress != "") Client.Subscribe(new string[] { TopicAddress }, new byte[] { 2 });
            }
            catch { }
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string mess = Encoding.UTF8.GetString(e.Message);
            MessageRecive.Add(mess);
        }

        public bool CheckMessageReception(string mess)
        {
            return MessageRecive.Contains(mess);
        }
    }
}
