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
            var passengerB = serializer.Instantiate<Passenger>(path);

            // Test
            Assert.IsTrue(passengerB.Name == passengerA.Name);
            Assert.IsTrue(passengerB.Age == passengerA.Age);
        }

        [Test]
        public void FromPathByCreator()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new XmlSerializer(typeof(VehicleData));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            serializer.Save<Vehicle, VehicleData>(vehicleA, path);
            Vehicle vehicleB = serializer.Instantiate<Vehicle, VehicleData>(path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void FromPathByConfigurator()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new XmlSerializer(typeof(VehicleData));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            var vehicleB = new Vehicle();

            // Process
            serializer.Save<Vehicle, VehicleData>(vehicleA, path);
            serializer.Load<Vehicle, VehicleData>(vehicleB, path);

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
            var passengerB = serializer.Instantiate<Passenger>(stringReader);

            // Test
            Assert.IsTrue(passengerB.Name == passengerA.Name);
            Assert.IsTrue(passengerB.Age == passengerA.Age);
        }

        [Test]
        public void FromStreamByCreator()
        {
            // Prerequisites
            var serializer = new XmlSerializer(typeof(VehicleData));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save<Vehicle, VehicleData>(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            Vehicle vehicleB = serializer.Instantiate<Vehicle, VehicleData>(stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void FromStreamByConfigurator()
        {
            // Prerequisites
            var serializer = new XmlSerializer(typeof(VehicleData));
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            var vehicleB = new Vehicle();

            // Process
            var stringWriter = new StringWriter();
            serializer.Save<Vehicle, VehicleData>(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            serializer.Load<Vehicle, VehicleData>(vehicleB, stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}