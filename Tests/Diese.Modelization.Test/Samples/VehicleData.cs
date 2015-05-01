using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class VehicleData : IConfigurator<Vehicle>, ICreator<Vehicle>
    {
        [ProtoMember(1)]
        public int SpeedMax { get; set; }
        [ProtoMember(2)]
        public CreatorList<Passenger, PassengerData> Passengers { get; set; }

        public VehicleData()
        {
            Passengers = new CreatorList<Passenger, PassengerData>();
        }

        public void From(Vehicle obj)
        {
            SpeedMax = obj.SpeedMax;
            Passengers.From(obj.Passengers);
        }

        public void Configure(Vehicle obj)
        {
            obj.SpeedMax = SpeedMax;
            obj.Passengers = Passengers.Create();
        }

        public Vehicle Create()
        {
            var obj = new Vehicle();
            Configure(obj);
            return obj;
        }
    }
}