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

    public class SerializerProtobuf<T, TModel> : Serializer<T, TModel>
        where TModel : IDataModel<T>, new()
    {
        public override TModel LoadModel(Stream stream)
        {
            return Serializer.Deserialize<TModel>(stream);
        }

        public override void SaveModel(TModel model, Stream stream)
        {
            Serializer.Serialize(stream, model);
        }
    }
}