using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class Wheel
    {
        [ProtoMember(1)]
        public double Wear { get; set; }
    }
}