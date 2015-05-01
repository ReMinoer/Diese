using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class WheelData : IConfigurator<Wheel>, ICreator<Wheel>
    {
        [ProtoMember(1)]
        public double Wear { get; set; }

        public void From(Wheel obj)
        {
            Wear = obj.Wear;
        }

        public void Configure(Wheel obj)
        {
            obj.Wear = Wear;
        }

        public Wheel Create()
        {
            var obj = new Wheel();
            Configure(obj);
            return obj;
        }
    }
}