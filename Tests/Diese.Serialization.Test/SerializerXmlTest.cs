using System.IO;
using Diese.Modelization.Test.Samples;
using NUnit.Framework;

namespace Diese.Serialization.Test
{
    [TestFixture]
    internal class SerializerXmlTest
    {
        [Test]
        public void FromPath()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new SerializerXml<Vehicle>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            serializer.Save(vehicleA, path);
            Vehicle vehicleB = serializer.Load(path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed == vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromPathByModel()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new SerializerXml<Vehicle, VehicleDataModel>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            serializer.Save(vehicleA, path);

            serializer.Load(out vehicleB, path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void FromStream()
        {
            // Prerequisites
            var serializer = new SerializerXml<Vehicle>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            Vehicle vehicleB = serializer.Load(stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed == vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromStreamByModel()
        {
            // Prerequisites
            var serializer = new SerializerXml<Vehicle, VehicleDataModel>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            serializer.Load(out vehicleB, stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}