using System.IO;
using System.Xml.Serialization;
using Diese.Modelization;

namespace Diese.Serialization
{
    public class XmlSerializer<T>
        where T : new()
    {
        private readonly XmlSerializer _serializer;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public T Load(string path)
        {
            var streamReader = new StreamReader(path);
            var obj = (T)_serializer.Deserialize(streamReader);
            streamReader.Close();
            return obj;
        }

        public void Save(T obj, string path)
        {
            var streamWriter = new StreamWriter(path);
            _serializer.Serialize(streamWriter, obj);
            streamWriter.Close();
        }
    }

    public class XmlSerializer<T, TModel>
        where TModel : IModel<T>, new()
    {
        private readonly XmlSerializer _serializer;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public void Load(T obj, string path)
        {
            var streamReader = new StreamReader(path);

            var model = (TModel)_serializer.Deserialize(streamReader);
            model.To(obj);

            streamReader.Close();
        }

        public void Save(T obj, string path)
        {
            var streamWriter = new StreamWriter(path);

            var model = new TModel();
            model.From(obj);
            _serializer.Serialize(streamWriter, model);

            streamWriter.Close();
        }
    }
}