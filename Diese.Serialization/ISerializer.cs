using System.IO;
using Diese.Modelization;

namespace Diese.Serialization
{
    public interface ISerializer<T>
    {
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
        T Instantiate(string path);
        T Instantiate(Stream stream);
    }

    public interface ISerializer<T, TModel>
        where TModel : IDataModel<T>
    {
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
        void Load(T obj, string path);
        void Load(T obj, Stream stream);
        T Instantiate(string path);
        T Instantiate(Stream stream);
        TModel LoadModel(Stream stream);
        void SaveModel(TModel model, Stream stream);
    }
}