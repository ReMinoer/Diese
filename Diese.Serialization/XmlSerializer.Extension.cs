using System.IO;
using System.Xml.Serialization;
using Diese.Modelization;

namespace Diese.Serialization
{
    static public class XmlSerializerExtension
    {
        // Classic

        static public T Instantiate<T>(this XmlSerializer serializer, Stream stream)
        {
            return (T)serializer.Deserialize(stream);
        }

        static public void Save<T>(this XmlSerializer serializer, T obj, Stream stream)
        {
            serializer.Serialize(stream, obj);
        }

        static public T Instantiate<T>(this XmlSerializer serializer, string path)
        {
            var streamReader = new StreamReader(path);
            var obj = serializer.Instantiate<T>(streamReader.BaseStream);
            streamReader.Close();
            return obj;
        }

        static public void Save<T>(this XmlSerializer serializer, T obj, string path)
        {
            var streamWriter = new StreamWriter(path);
            serializer.Save(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        static public T Instantiate<T>(this XmlSerializer serializer, TextReader textReader)
        {
            return (T)serializer.Deserialize(textReader);
        }

        static public void Save<T>(this XmlSerializer serializer, T obj, TextWriter textWriter)
        {
            serializer.Serialize(textWriter, obj);
        }

        // With model

        static public T Instantiate<T, TModel>(this XmlSerializer serializer, Stream stream)
            where TModel : ICreator<T>
        {
            var model = serializer.Instantiate<TModel>(stream);
            return model.Create();
        }

        static public void Load<T, TModel>(this XmlSerializer serializer, T obj, Stream stream)
            where TModel : IConfigurator<T>
        {
            var model = serializer.Instantiate<TModel>(stream);
            model.Configure(obj);
        }

        static public void Save<T, TModel>(this XmlSerializer serializer, T obj, Stream stream)
            where TModel : IDataModel<T>, new()
        {
            var model = new TModel();
            model.From(obj);
            serializer.Save(model, stream);
        }

        static public T Instantiate<T, TModel>(this XmlSerializer serializer, string path)
            where TModel : ICreator<T>
        {
            var streamReader = new StreamReader(path);
            T obj = serializer.Instantiate<T, TModel>(streamReader.BaseStream);
            streamReader.Close();
            return obj;
        }

        static public void Load<T, TModel>(this XmlSerializer serializer, T obj, string path)
            where TModel : IConfigurator<T>
        {
            var streamReader = new StreamReader(path);
            serializer.Load<T, TModel>(obj, streamReader.BaseStream);
            streamReader.Close();
        }

        static public void Save<T, TModel>(this XmlSerializer serializer, T obj, string path)
            where TModel : IDataModel<T>, new()
        {
            var streamWriter = new StreamWriter(path);
            serializer.Save<T, TModel>(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        static public T Instantiate<T, TModel>(this XmlSerializer serializer, TextReader textReader)
            where TModel : ICreator<T>
        {
            var model = (TModel)serializer.Deserialize(textReader);
            return model.Create();
        }

        static public void Load<T, TModel>(this XmlSerializer serializer, T obj, TextReader textReader)
            where TModel : IConfigurator<T>
        {
            var model = serializer.Instantiate<TModel>(textReader);
            model.Configure(obj);
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