using System.Collections.Generic;
using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class Vehicle
    {
        [ProtoMember(1)]
        public int SpeedMax { get; set; }

        [ProtoMember(2)]
        public IList<Wheel> Wheels { get; set; }

        public Dictionary<string, Passenger> Passengers { get; set; }
        public int CurrentSpeed { get; set; }

        public Vehicle()
        {
            Passengers = new Dictionary<string, Passenger>();
            Wheels = new List<Wheel>();
        }
    }
}