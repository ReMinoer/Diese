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
            const string path = "test-result.proto";

            var serializer = new SerializerProtobuf<Vehicle, VehicleDataModel>();
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
            var serializer = new SerializerProtobuf<Vehicle>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};

            // Process
            var streamWriter = new StreamWriter("test-result.proto");
            serializer.Save(vehicleA, streamWriter.BaseStream);
            streamWriter.Close();

            var streamReader = new StreamReader("test-result.proto");
            Vehicle vehicleB = serializer.Load(streamReader.BaseStream);
            streamReader.Close();

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
            Assert.IsTrue(vehicleB.CurrentSpeed == vehicleA.CurrentSpeed);
        }

        [Test]
        public void FromStreamByModel()
        {
            // Prerequisites
            var serializer = new SerializerProtobuf<Vehicle, VehicleDataModel>();
            var vehicleA = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            Vehicle vehicleB;

            // Process
            var streamWriter = new StreamWriter("test-result.proto");
            serializer.Save(vehicleA, streamWriter.BaseStream);
            streamWriter.Close();

            var streamReader = new StreamReader("test-result.proto");
            serializer.Load(out vehicleB, streamReader.BaseStream);
            streamReader.Close();

            // Test
            Assert.IsTrue(vehicleB.SpeedMax == vehicleA.SpeedMax);
        }
    }
}