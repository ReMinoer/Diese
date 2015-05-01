using Diese.Modelization.Test.Samples;
using NUnit.Framework;

namespace Diese.Modelization.Test
{
    internal class DataModelTest
    {
        [Test]
        public void ModelFromObject()
        {
            var vehicle = new Vehicle {SpeedMax = 60, CurrentSpeed = 20};
            vehicle.Passengers.Add(new Passenger {Name = "John", Age = 20});
            vehicle.Passengers.Add(new Passenger {Name = "Charlie", Age = 10});
            var model = new VehicleData();

            Assert.IsTrue(model.SpeedMax == 0);
            Assert.IsTrue(model.Passengers.Count == 0);

            model.From(vehicle);

            Assert.IsTrue(model.SpeedMax == 60);
            Assert.IsTrue(model.Passengers.Count == 2);
            Assert.IsTrue(model.Passengers[0].Name == "John");
            Assert.IsTrue(model.Passengers[0].Age == 20);
            Assert.IsTrue(model.Passengers[1].Name == "Charlie");
            Assert.IsTrue(model.Passengers[1].Age == 10);
        }

        [Test]
        public void ModelConfigureObject()
        {
            var model = new VehicleData {SpeedMax = 60};
            model.Passengers.Add(new PassengerData {Name = "John", Age = 20});
            model.Passengers.Add(new PassengerData {Name = "Charlie", Age = 10});
            var vehicle = new Vehicle();

            Assert.IsTrue(vehicle.SpeedMax == 0);
            Assert.IsTrue(vehicle.CurrentSpeed == 0);
            Assert.IsTrue(vehicle.Passengers.Count == 0);

            model.Configure(vehicle);

            Assert.IsTrue(vehicle.SpeedMax == 60);
            Assert.IsTrue(vehicle.CurrentSpeed == 0);
            Assert.IsTrue(vehicle.Passengers.Count == 2);
            Assert.IsTrue(vehicle.Passengers[0].Name == "John");
            Assert.IsTrue(vehicle.Passengers[0].Age == 20);
            Assert.IsTrue(vehicle.Passengers[1].Name == "Charlie");
            Assert.IsTrue(vehicle.Passengers[1].Age == 10);
        }

        [Test]
        public void ModelCreateObject()
        {
            var model = new VehicleData {SpeedMax = 60};
            model.Passengers.Add(new PassengerData {Name = "John", Age = 20});
            model.Passengers.Add(new PassengerData {Name = "Charlie", Age = 10});

            Vehicle vehicle = model.Create();

            Assert.IsTrue(vehicle.SpeedMax == 60);
            Assert.IsTrue(vehicle.CurrentSpeed == 0);
            Assert.IsTrue(vehicle.Passengers.Count == 2);
            Assert.IsTrue(vehicle.Passengers[0].Name == "John");
            Assert.IsTrue(vehicle.Passengers[0].Age == 20);
            Assert.IsTrue(vehicle.Passengers[1].Name == "Charlie");
            Assert.IsTrue(vehicle.Passengers[1].Age == 10);
        }
    }
}