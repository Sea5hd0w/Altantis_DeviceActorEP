using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Atlantis_DeviceActorEP_Test_xUnit
{
    public class MQTTTest
    {
        Altantis_DeviceActorEP.Service.MQTT mqtt;

        public MQTTTest()
        {
            mqtt = Altantis_DeviceActorEP.Service.MQTT.Instance;
        }

        [Fact]
        public void Check_ConfigFile_Loading()
        {
            // Arrange

            // Act
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config.json")
                .Build();
            string BrokerAddress = configuration.GetConnectionString("BrokerAddress");
            string TopicAddress = configuration.GetConnectionString("TopicAddress");
            string UserName = configuration.GetConnectionString("UserName");
            string UserPassword = configuration.GetConnectionString("UserPassword");
            int ConnectionPort = Convert.ToInt32(configuration.GetConnectionString("ConnectionPort"));

            // Assert
            Assert.True((mqtt.BrokerAddress == BrokerAddress &&
                mqtt.TopicAddress == TopicAddress &&
                mqtt.UserName == UserName &&
                mqtt.UserPassword == UserPassword));
        }

        [Fact]
        public void Check_StartMQTTConnection()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(mqtt.Client.IsConnected);
        }

        [Fact]
        public void Check_StartMQTTPostMessage()
        {
            // Arrange
            string MacAddress = "xUnit";
            string ActorName = "xUnit_test";
            string Action = "Test";

            string message = "{\"macAddress\":\"xUnit\", \"actorName\":\"xUnit_test\", \"action\":\"Test\"}";
            MQTT reciver = new MQTT();

            Altantis_DeviceActorEP.Business.Command business = new Altantis_DeviceActorEP.Business.Command(MacAddress, ActorName, Action);

            // Act
            mqtt.Client_MqttPublishMsg(message);
            Thread.Sleep(1000);

            // Assert
            Assert.True(reciver.CheckMessageReception(Newtonsoft.Json.JsonConvert.SerializeObject(business)));
        }
    }
}