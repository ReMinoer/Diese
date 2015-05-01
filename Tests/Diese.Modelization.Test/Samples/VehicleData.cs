using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class VehicleData : IConfigurator<Vehicle>, ICreator<Vehicle>
    {
        [ProtoMember(1)]
        public int SpeedMax { get; set; }

        [ProtoMember(2)]
        public CreatorDictionary<string, Passenger, PassengerData> Passengers { get; set; }

        [ProtoMember(3)]
        public CreatorList<Wheel, WheelData> Wheels { get; set; }

        public VehicleData()
        {
            Passengers = new CreatorDictionary<string, Passenger, PassengerData>();
            Wheels = new CreatorList<Wheel, WheelData>();
        }

        public void From(Vehicle obj)
        {
            SpeedMax = obj.SpeedMax;
            Passengers.From(obj.Passengers);
            Wheels.From(obj.Wheels);
        }

        public void Configure(Vehicle obj)
        {
            obj.SpeedMax = SpeedMax;
            obj.Passengers = Passengers.Create();
            obj.Wheels = Wheels.Create();
        }

        public Vehicle Create()
        {
            var obj = new Vehicle();
            Configure(obj);
            return obj;
        }
    }
}