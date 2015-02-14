using System.IO;
using System.Xml.Serialization;
using Diese.Modelization;

namespace Diese.Serialization
{
    static public class XmlSerializerExtension
    {
        // Classic

        static public T Load<T>(this XmlSerializer serializer, Stream stream)
        {
            return (T)serializer.Deserialize(stream);
        }

        static public void Save<T>(this XmlSerializer serializer, T obj, Stream stream)
        {
            serializer.Serialize(stream, obj);
        }

        static public T Load<T>(this XmlSerializer serializer, string path)
        {
            var streamReader = new StreamReader(path);
            var obj = serializer.Load<T>(streamReader.BaseStream);
            streamReader.Close();
            return obj;
        }

        static public void Save<T>(this XmlSerializer serializer, T obj, string path)
        {
            var streamWriter = new StreamWriter(path);
            serializer.Save(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        static public T Load<T>(this XmlSerializer serializer, TextReader textReader)
        {
            return (T)serializer.Deserialize(textReader);
        }

        static public void Save<T>(this XmlSerializer serializer, T obj, TextWriter textWriter)
        {
            serializer.Serialize(textWriter, obj);
        }

        // With model

        static public void Load<T, TModel>(this XmlSerializer serializer, out T obj, Stream stream)
            where TModel : IDataModel<T>
        {
            var model = serializer.Load<TModel>(stream);
            model.To(out obj);
        }

        static public void Save<T, TModel>(this XmlSerializer serializer, T obj, Stream stream)
            where TModel : IDataModel<T>, new()
        {
            var model = new TModel();
            model.From(obj);
            serializer.Save(model, stream);
        }

        static public void Load<T, TModel>(this XmlSerializer serializer, out T obj, string path)
            where TModel : IDataModel<T>
        {
            var streamReader = new StreamReader(path);
            serializer.Load<T, TModel>(out obj, streamReader.BaseStream);
            streamReader.Close();
        }

        static public void Save<T, TModel>(this XmlSerializer serializer, T obj, string path)
            where TModel : IDataModel<T>, new()
        {
            var streamWriter = new StreamWriter(path);
            serializer.Save<T, TModel>(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        static public void Load<T, TModel>(this XmlSerializer serializer, out T obj, TextReader textReader)
            where TModel : IDataModel<T>
        {
            var model = (TModel)serializer.Deserialize(textReader);
            model.To(out obj);
        }

        static public void Save<T, TModel>(this XmlSerializer serializer, T obj, TextWriter textWriter)
            where TModel : IDataModel<T>, new()
        {
            var model = new TModel();
            model.From(obj);
            serializer.Serialize(textWriter, model);
        }
    }
}