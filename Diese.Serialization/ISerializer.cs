using System.IO;
using Diese.Modelization;

namespace Diese.Serialization
{
    public interface ISerializer<T>
    {
        T Load(string path);
        T Load(Stream stream);
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
    }

    public interface ISerializer<T, TModel>
        where TModel : IDataModel<T>
    {
        void Load(out T obj, string path);
        void Load(out T obj, Stream stream);
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
        TModel LoadModel(Stream stream);
        void SaveModel(TModel model, Stream stream);
    }
}