using System;
using System.IO;
using System.Linq;
using Diese.Modelization;

namespace Diese.Serialization
{
    public abstract class Serializer<T> : ISerializer<T>
    {
        public abstract T Instantiate(Stream stream);
        public abstract void Save(T obj, Stream stream);

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
    }

    public abstract class Serializer<T, TModel> : ISerializer<T, TModel>
        where TModel : IDataModel<T>, new()
    {
        public Action<TModel> DataConfiguration { get; set; }

        protected abstract TModel DeserializeModel(Stream stream);
        protected abstract void SerializeModel(TModel model, Stream stream);

        public TModel LoadModel(Stream stream)
        {
            TModel dataModel = DeserializeModel(stream);

            if (DataConfiguration != null)
                DataConfiguration(dataModel);

            return dataModel;
        }

        public void SaveModel(TModel model, Stream stream)
        {
            SerializeModel(model, stream);
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

        public void Load(T obj, string path)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(IConfigurationData<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement IConfigurationData<T>",
                    typeof(TModel)));

            var streamReader = new StreamReader(path);
            Load(obj, streamReader.BaseStream);
            streamReader.Close();
        }

        public void Load(T obj, Stream stream)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(IConfigurationData<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement IConfigurationData<T>",
                    typeof(TModel)));

            var model = LoadModel(stream) as IConfigurationData<T>;
            if (model != null)
                model.Configure(obj);
        }

        public T Instantiate(string path)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(ICreationData<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement ICreationData<T>", typeof(TModel)));

            var streamReader = new StreamReader(path);
            T obj = Instantiate(streamReader.BaseStream);
            streamReader.Close();

            return obj;
        }

        public T Instantiate(Stream stream)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(ICreationData<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement ICreationData<T>", typeof(TModel)));

            var model = LoadModel(stream) as ICreationData<T>;
            if (model == null)
                throw new InvalidOperationException(string.Format("{0} does not implement ICreationData<T>", typeof(TModel)));

            return model.Create();
        }
    }
}