using System.IO;
using Diese.Modelization.Test.Samples;
using Diese.Serialization.Protobuf;
using NUnit.Framework;

namespace Diese.Serialization.Test
{
    [TestFixture]
    internal class SerializerProtobufTest
    {
        [Test]
        public void FromPath()
        {
            // Prerequisites
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle>();
            var vehicleA = new Vehicle
            {
                SpeedMax = 50,
                CurrentSpeed = 20
            };

            // Process
            serializer.Save(vehicleA, path);
            Vehicle vehicleB = serializer.Instantiate(path);

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed != vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromPathByModel()
        {
            // Prerequisites
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle, VehicleData>();
            var vehicleA = new Vehicle
            {
                SpeedMax = 50,
                CurrentSpeed = 20
            };

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
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle>();
            var vehicleA = new Vehicle
            {
                SpeedMax = 50,
                CurrentSpeed = 20
            };

            // Process
            var streamWriter = new StreamWriter(path);
            serializer.Save(vehicleA, streamWriter.BaseStream);
            streamWriter.Close();

            var streamReader = new StreamReader(path);
            Vehicle vehicleB = serializer.Instantiate(streamReader.BaseStream);
            streamReader.Close();

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed != vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromStreamByModel()
        {
            // Prerequisites
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle, VehicleData>();
            var vehicleA = new Vehicle
            {
                SpeedMax = 50,
                CurrentSpeed = 20
            };

            // Process
            var streamWriter = new StreamWriter(path);
            serializer.Save(vehicleA, streamWriter.BaseStream);
            streamWriter.Close();

            var streamReader = new StreamReader(path);
            Vehicle vehicleB = serializer.Instantiate(streamReader.BaseStream);
            streamReader.Close();

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }

        [Test]
        public void InitializationFromPath()
        {
            // Prerequisites
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle, VehicleData>();
            var vehicleA = new Vehicle
            {
                SpeedMax = 50,
                CurrentSpeed = 20
            };
            var vehicleB = new Vehicle
            {
                SpeedMax = 100,
                CurrentSpeed = 40
            };

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
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle, VehicleData>();
            var vehicleA = new Vehicle
            {
                SpeedMax = 50,
                CurrentSpeed = 20
            };
            var vehicleB = new Vehicle
            {
                SpeedMax = 100,
                CurrentSpeed = 40
            };

            // Process
            var streamWriter = new StreamWriter(path);
            serializer.Save(vehicleA, streamWriter.BaseStream);
            streamWriter.Close();

            var streamReader = new StreamReader(path);
            serializer.Load(vehicleB, streamReader.BaseStream);
            streamReader.Close();

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}