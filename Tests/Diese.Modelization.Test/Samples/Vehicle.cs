using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class Vehicle
    {
        [ProtoMember(1)]
        public int SpeedMax { get; set; }
        [ProtoMember(2)]
        public int CurrentSpeed { get; set; }
    }
}