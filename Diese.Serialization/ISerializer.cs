using System.IO;

namespace Diese.Serialization
{
    public interface ISerializer<T>
    {
        T Load(string path);
        T Load(Stream stream);
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
    }
}