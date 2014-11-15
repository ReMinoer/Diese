using System.IO;

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
}