using System.IO;
using Diese.Modelization;

namespace Diese.Serialization
{
    public abstract class Serializer<T> : ISerializer<T>
    {
        public T Load(string path)
        {
            var streamReader = new StreamReader(path);
            T obj = Load(streamReader.BaseStream);
            streamReader.Close();
            return obj;
        }

        public void Save(T obj, string path)
        {
            var streamWriter = new StreamWriter(path);
            Save(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        public abstract T Load(Stream stream);
        public abstract void Save(T obj, Stream stream);
    }

    public abstract class Serializer<T, TModel> : ISerializer<T, TModel>
        where TModel : IDataModel<T>, new()
    {
        public void Load(out T obj, string path)
        {
            var streamReader = new StreamReader(path);
            Load(out obj, streamReader.BaseStream);
            streamReader.Close();
        }

        public void Load(out T obj, Stream stream)
        {
            TModel model = LoadModel(stream);
            model.To(out obj);
        }

        public void Save(T obj, string path)
        {
            var streamWriter = new StreamWriter(path);
            Save(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        public void Save(T obj, Stream stream)
        {
            var model = new TModel();
            model.From(obj);
            SaveModel(model, stream);
        }

        public abstract TModel LoadModel(Stream stream);
        public abstract void SaveModel(TModel model, Stream stream);
    }
}