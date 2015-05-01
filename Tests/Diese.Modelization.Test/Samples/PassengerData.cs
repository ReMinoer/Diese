using ProtoBuf;

namespace Diese.Modelization.Test.Samples
{
    [ProtoContract]
    public class PassengerData : IConfigurator<Passenger>, ICreator<Passenger>
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public int Age { get; set; }

        public void From(Passenger obj)
        {
            Name = obj.Name;
            Age = obj.Age;
        }

        public void Configure(Passenger obj)
        {
            obj.Name = Name;
            obj.Age = Age;
        }

        public Passenger Create()
        {
            var obj = new Passenger();
            Configure(obj);
            return obj;
        }
    }
}