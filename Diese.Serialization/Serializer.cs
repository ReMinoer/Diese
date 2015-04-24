using System;
using System.IO;
using System.Linq;
using Diese.Modelization;

namespace Diese.Serialization
{
    public abstract class Serializer<T> : ISerializer<T>
    {
        public T Instantiate(string path)
        {
            var streamReader = new StreamReader(path);
            T obj = Instantiate(streamReader.BaseStream);
            streamReader.Close();
            return obj;
        }

        public void Save(T obj, string path)
        {
            var streamWriter = new StreamWriter(path);
            Save(obj, streamWriter.BaseStream);
            streamWriter.Close();
        }

        public abstract T Instantiate(Stream stream);
        public abstract void Save(T obj, Stream stream);
    }

    public abstract class Serializer<T, TModel> : ISerializer<T, TModel>
        where TModel : IDataModel<T>, new()
    {
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

        public void Load(T obj, string path)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(IConfigurator<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement IConfigurator<T>",
                    typeof(TModel)));

            var streamReader = new StreamReader(path);
            Load(obj, streamReader.BaseStream);
            streamReader.Close();
        }

        public void Load(T obj, Stream stream)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(IConfigurator<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement IConfigurator<T>",
                    typeof(TModel)));

            var model = LoadModel(stream) as IConfigurator<T>;
            if (model != null)
                model.Configure(obj);
        }

        public T Instantiate(string path)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(ICreator<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement ICreator<T>", typeof(TModel)));

            var streamReader = new StreamReader(path);
            T obj = Instantiate(streamReader.BaseStream);
            streamReader.Close();

            return obj;
        }

        public T Instantiate(Stream stream)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(ICreator<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement ICreator<T>", typeof(TModel)));

            var model = LoadModel(stream) as ICreator<T>;
            if (model == null)
                throw new InvalidOperationException(string.Format("{0} does not implement ICreator<T>", typeof(TModel)));

            return model.Create();
        }

        public abstract TModel LoadModel(Stream stream);
        public abstract void SaveModel(TModel model, Stream stream);
    }
}