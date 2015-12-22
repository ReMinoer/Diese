using System.IO;
using Diese.Modelization;
using ProtoBuf;

namespace Diese.Serialization.Protobuf
{
    public class SerializerProtobuf<T> : Serializer<T>
        where T : new()
    {
        public override T Instantiate(Stream stream)
        {
            return Serializer.Deserialize<T>(stream);
        }

        public override void Save(T obj, Stream stream)
        {
            Serializer.Serialize(stream, obj);
        }
    }

    public class SerializerProtobuf<T, TDataModel> : Serializer<T, TDataModel>
        where TDataModel : IDataModel<T>, new()
    {
        protected override TDataModel DeserializeModel(Stream stream)
        {
            return Serializer.Deserialize<TDataModel>(stream);
        }

        protected override void SerializeModel(TDataModel model, Stream stream)
        {
            Serializer.Serialize(stream, model);
        }
    }
}