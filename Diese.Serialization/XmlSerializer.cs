using System.IO;
using System.Xml.Serialization;
using Diese.Modelization;

namespace Diese.Serialization
{
    public class XmlSerializer<T> : Serializer<T>
        where T : new()
    {
        private readonly XmlSerializer _serializer;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public override T Load(Stream stream)
        {
            return (T)_serializer.Deserialize(stream);
        }

        public override void Save(T obj, Stream stream)
        {
            _serializer.Serialize(stream, obj);
        }
    }

    public class XmlSerializer<T, TModel> : ModelSerializer<T, TModel>
        where TModel : IModel<T>, new()
    {
        private readonly XmlSerializer _serializer;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(TModel));
        }

        protected override TModel LoadModel(Stream stream)
        {
            return (TModel)_serializer.Deserialize(stream);
        }

        protected override void SaveModel(TModel model, Stream stream)
        {
            _serializer.Serialize(stream, model);
        }
    }
}