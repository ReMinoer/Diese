using Diese.Modelization.Test.Samples;
using NUnit.Framework;

namespace Diese.Modelization.Test
{
    internal class DataModelTest
    {
        [Test]
        public void ModelFromObject()
        {
            var vehicle = new Vehicle {SpeedMax = 50, CurrentSpeed = 20};
            var model = new VehicleDataModel();

            Assert.IsTrue(model.SpeedMax == 0);

            model.From(vehicle);

            Assert.IsTrue(model.SpeedMax == 50);
        }

        [Test]
        public void ModelToObject()
        {
            var model = new VehicleDataModel {SpeedMax = 60};
            var vehicle = new Vehicle();

            Assert.IsTrue(vehicle.SpeedMax == 0);
            Assert.IsTrue(vehicle.CurrentSpeed == 0);

            model.To(vehicle);

            Assert.IsTrue(model.SpeedMax == 60);
            Assert.IsTrue(vehicle.CurrentSpeed == 0);
        }
    }
}