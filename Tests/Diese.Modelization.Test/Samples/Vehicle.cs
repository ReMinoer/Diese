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
        public List<Passenger> Passengers { get; set; }

        public int CurrentSpeed { get; set; }

        public Vehicle()
        {
            Passengers = new List<Passenger>();
        }
    }
}