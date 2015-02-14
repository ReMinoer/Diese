using System.IO;
using System.Xml.Serialization;
using Diese.Modelization.Test.Samples;
using NUnit.Framework;

namespace Diese.Serialization.Test
{
    [TestFixture]
    internal class XmlSerializerExtensionTest
    {
        [Test]
        public void FromPath()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new XmlSerializer(typeof(Vehicle));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            serializer.Save(vehicleA, path);
            var vehicleB = serializer.Load<Vehicle>(path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed == vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromPathByModel()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new XmlSerializer(typeof(VehicleDataModel));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            serializer.Save<Vehicle, VehicleDataModel>(vehicleA, path);

            serializer.Load<Vehicle, VehicleDataModel>(out vehicleB, path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void FromStream()
        {
            // Prerequisites
            var serializer = new XmlSerializer(typeof(Vehicle));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            var vehicleB = serializer.Load<Vehicle>(stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed == vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromStreamByModel()
        {
            // Prerequisites
            var serializer = new XmlSerializer(typeof(VehicleDataModel));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            var stringWriter = new StringWriter();
            serializer.Save<Vehicle, VehicleDataModel>(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            serializer.Load<Vehicle, VehicleDataModel>(out vehicleB, stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}