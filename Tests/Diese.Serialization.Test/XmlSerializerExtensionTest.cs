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

            var serializer = new XmlSerializer(typeof(Passenger));
            var passengerA = new Passenger {Name = "John", Age = 20};

            // Process
            serializer.Save(passengerA, path);
            var passengerB = serializer.Load<Passenger>(path);

            // Test
            Assert.IsTrue(passengerB.Name == passengerA.Name);
            Assert.IsTrue(passengerB.Age == passengerA.Age);
        }

        [Test]
        public void FromPathByModel()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new XmlSerializer(typeof(VehicleData));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            serializer.Save<Vehicle, VehicleData>(vehicleA, path);

            serializer.Load<Vehicle, VehicleData>(out vehicleB, path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void FromStream()
        {
            // Prerequisites
            var serializer = new XmlSerializer(typeof(Passenger));
            var passengerA = new Passenger {Name = "John", Age = 20};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(passengerA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            var passengerB = serializer.Load<Passenger>(stringReader);

            // Test
            Assert.IsTrue(passengerB.Name == passengerA.Name);
            Assert.IsTrue(passengerB.Age == passengerA.Age);
        }

        [Test]
        public void FromStreamByModel()
        {
            // Prerequisites
            var serializer = new XmlSerializer(typeof(VehicleData));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            var stringWriter = new StringWriter();
            serializer.Save<Vehicle, VehicleData>(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            serializer.Load<Vehicle, VehicleData>(out vehicleB, stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}