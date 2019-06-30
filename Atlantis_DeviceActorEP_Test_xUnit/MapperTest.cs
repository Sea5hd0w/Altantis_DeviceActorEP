using Altantis_DeviceActorEP.DAO;
using Altantis_DeviceActorEP.Mapper;
using System;
using Xunit;

namespace Atlantis_DeviceActorEP_Test_xUnit
{
    public class MapperTest
    {
        [Fact]
        public void Check_ValidJSONCommand_Format()
        {
            // Arrange
            var daoObject = new Command("{\"macAddress\":\"\", \"actorName\":\"\", \"action\":\"\"}");

            // Act
            var businessObject = MapperCommand.DAOToBusiness(daoObject);

            // Assert
            Assert.IsType<Altantis_DeviceActorEP.Business.Command>(businessObject);
        }

        [Fact]
        public void Check_ValidJSONCommand_Content()
        {
            // Arrange
            string MacAddress = "macAddress_test";
            string ActorName = "actorName_test";
            string Action = "action_on";

            var daoObject = new Command("{\"macAddress\":\"" + MacAddress + "\", \"actorName\":\"" + ActorName + "\", \"action\":\"" + Action + "\"}");
            var validBusinessObject = new Altantis_DeviceActorEP.Business.Command(MacAddress, ActorName, Action);

            // Act
            var businessObject = MapperCommand.DAOToBusiness(daoObject);

            // Assert
            Assert.True(Newtonsoft.Json.JsonConvert.SerializeObject(validBusinessObject) == Newtonsoft.Json.JsonConvert.SerializeObject(businessObject));
        }

        [Fact]
        public void Check_ValidJSONCommand_Exception()
        {
            // Arrange
            var daoObject = new Command("");

            // Act
            var businessObject = MapperCommand.DAOToBusiness(daoObject);

            // Assert
            Assert.Null(businessObject);

        }
    }
}
