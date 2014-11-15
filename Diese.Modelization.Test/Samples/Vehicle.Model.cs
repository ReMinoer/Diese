namespace Diese.Modelization.Test.Samples
{
    public class VehicleModel : IModel<Vehicle>
    {
        public int SpeedMax { get; set; }

        public void From(Vehicle obj)
        {
            SpeedMax = obj.SpeedMax;
        }

        public void To(Vehicle obj)
        {
            obj.SpeedMax = SpeedMax;
        }
    }
}