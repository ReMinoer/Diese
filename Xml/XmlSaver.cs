using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Diese.Xml
{
    internal class XmlSaver
    {
        private XmlWriter writer;

        public XmlSaver(XmlWriter writer)
        {
            this.writer = writer;
        }

        static public void Save<T>(string path, T x)
        {
            StreamWriter stream = new StreamWriter(path);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, x);
            stream.Close();
        }

        public void Write(string name, string value)
        {
            writer.WriteElementString(name, value);
        }

        public void Write<T>(T value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, value);
        }

        public void WriteList<T>(string name, List<T> value)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString("count", value.Count.ToString());

            if (value.Count != 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                foreach (T x in value)
                {
                    serializer.Serialize(writer, x);
                }
            }

            writer.WriteEndElement();
        }

        public void WriteAttribute(string name, string value)
        {
            writer.WriteAttributeString(name, value);
        }

        public void WriteAttribute<T>(string name, T value)
        {
            writer.WriteAttributeString(name, value.ToString<T>());
        }

        public void WriteCollection(XmlCollection c)
        {
            foreach (KeyValuePair<string, string> x in c)
                writer.WriteAttributeString(x.Key, x.Value);
        }
    }
}