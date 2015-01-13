using System.IO;

namespace Diese.Serialization
{
    public interface IDataModelSerializer<in T>
    {
        void Load(T obj, string path);
        void Load(T obj, Stream stream);
        void Save(T obj, string path);
        void Save(T obj, Stream stream);
    }
}