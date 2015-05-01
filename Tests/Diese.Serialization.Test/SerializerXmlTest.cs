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

            var serializer = new SerializerXml<Passenger>();
            var passengerA = new Passenger {Name = "John", Age = 20};

            // Process
            serializer.Save(passengerA, path);
            Passenger passengerB = serializer.Instantiate(path);

            // Test
            Assert.IsTrue(passengerB.Name == passengerA.Name);
            Assert.IsTrue(passengerB.Age == passengerA.Age);
        }

        [Test]
        public void FromPathByModel()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new SerializerXml<Vehicle, VehicleData>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            serializer.Save(vehicleA, path);

            Vehicle vehicleB = serializer.Instantiate(path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void FromStream()
        {
            // Prerequisites
            var serializer = new SerializerXml<Passenger>();
            var vehicleA = new Passenger {Name = "John", Age = 20};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            Passenger vehicleB = serializer.Instantiate(stringReader);

            // Test
            Assert.IsTrue(vehicleB.Name == vehicleA.Name);
            Assert.IsTrue(vehicleB.Age == vehicleA.Age);
        }

        [Test]
        public void FromStreamByModel()
        {
            // Prerequisites
            var serializer = new SerializerXml<Vehicle, VehicleData>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            Vehicle vehicleB = serializer.Instantiate(stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void InitializationFromPath()
        {
            // Prerequisites
            const string path = "test-result.xml";

            var serializer = new SerializerXml<Vehicle, VehicleData>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            var vehicleB = new Vehicle {SpeedMax = 100, CurrentSpeed = 40};

            // Process
            serializer.Save(vehicleA, path);

            serializer.Load(vehicleB, path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void InitializationFromStream()
        {
            // Prerequisites
            var serializer = new SerializerXml<Vehicle, VehicleData>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            var vehicleB = new Vehicle {SpeedMax = 100, CurrentSpeed = 40};

            // Process
            var stringWriter = new StringWriter();
            serializer.Save(vehicleA, stringWriter);

            var stringReader = new StringReader(stringWriter.ToString());
            serializer.Load(vehicleB, stringReader);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}