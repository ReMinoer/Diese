using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class VehicleDataModel : IDataModel<Vehicle>
    {
        [ProtoMember(1)]
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