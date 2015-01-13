using System.IO;
using System.Xml.Serialization;
using Diese.Modelization;

namespace Diese.Serialization.Xml
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

        public T Load(TextReader textReader)
        {
            return (T)_serializer.Deserialize(textReader);
        }

        public void Save(T obj, TextWriter textWriter)
        {
            _serializer.Serialize(textWriter, obj);
        }
    }

    public class XmlSerializer<T, TModel> : DataModelSerializer<T, TModel>
        where TModel : IDataModel<T>, new()
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

        public void Load(T obj, TextReader textReader)
        {
            var model = (TModel)_serializer.Deserialize(textReader);
            model.To(obj);
        }

        public void Save(T obj, TextWriter textWriter)
        {
            var model = new TModel();
            model.From(obj);
            _serializer.Serialize(textWriter, model);
        }
    }
}