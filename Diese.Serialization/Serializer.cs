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
        public void Initialization(T obj, string path)
        {
            var streamReader = new StreamReader(path);
            Initialization(obj, streamReader.BaseStream);
            streamReader.Close();
        }

        public void Initialization(T obj, Stream stream)
        {
            TModel model = LoadModel(stream);
            model.To(obj);
        }

        public T Load(string path)
        {
            var streamReader = new StreamReader(path);
            T obj = Load(streamReader.BaseStream);
            streamReader.Close();

            return obj;
        }

        public T Load(Stream stream)
        {
            TModel model = LoadModel(stream);
            return model.Create();
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