using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class Passenger
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public int Age { get; set; }
    }
}