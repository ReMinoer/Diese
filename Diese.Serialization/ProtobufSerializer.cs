using System.IO;
using Diese.Modelization;
using ProtoBuf;

namespace Diese.Serialization
{
    public class ProtobufSerializer<T> : Serializer<T>
        where T : new()
    {
        public override T Load(Stream stream)
        {
            return Serializer.Deserialize<T>(stream);
        }

        public override void Save(T obj, Stream stream)
        {
            Serializer.Serialize(stream, obj);
        }
    }

    public class ProtobufSerializer<T, TModel> : ModelSerializer<T, TModel>
        where TModel : IModel<T>, new()
    {
        protected override TModel LoadModel(Stream stream)
        {
            return Serializer.Deserialize<TModel>(stream);
        }

        protected override void SaveModel(TModel model, Stream stream)
        {
            Serializer.Serialize(stream, model);
        }
    }
}