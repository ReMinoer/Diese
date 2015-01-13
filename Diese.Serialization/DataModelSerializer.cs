using System.IO;
using Diese.Modelization;

namespace Diese.Serialization
{
    public abstract class DataModelSerializer<T, TModel> : IDataModelSerializer<T>
        where TModel : IDataModel<T>, new()
    {
        public void Load(T obj, string path)
        {
            var streamReader = new StreamReader(path);
            Load(obj, streamReader.BaseStream);
            streamReader.Close();
        }

        public void Load(T obj, Stream stream)
        {
            TModel model = LoadModel(stream);
            model.To(obj);
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

        protected abstract TModel LoadModel(Stream stream);
        protected abstract void SaveModel(TModel model, Stream stream);
    }
}