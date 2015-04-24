using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class VehicleDataModel : IConfigurator<Vehicle>, ICreator<Vehicle>
    {
        [ProtoMember(1)]
        public int SpeedMax { get; set; }

        public void From(Vehicle obj)
        {
            SpeedMax = obj.SpeedMax;
        }

        public void Configure(Vehicle obj)
        {
            obj.SpeedMax = SpeedMax;
        }

        public Vehicle Create()
        {
            var obj = new Vehicle();
            Configure(obj);
            return obj;
        }
    }
}