using System;
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

    public interface ISerializer<T, TDataModel>
        where TDataModel : IDataModel<T>
    {
        Action<TDataModel> DataConfiguration { set; }
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
        void Load(T obj, string path);
        void Load(T obj, Stream stream);
        T Instantiate(string path);
        T Instantiate(Stream stream);
        TDataModel LoadModel(Stream stream);
        void SaveModel(TDataModel model, Stream stream);
    }
}