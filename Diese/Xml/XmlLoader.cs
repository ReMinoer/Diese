using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace Diese.Xml
{
    public class XmlLoader
    {
        private XmlReader reader;
        private bool isAttributesRead;

        public XmlLoader(XmlReader reader)
        {
            this.reader = reader;
            isAttributesRead = false;
        }

        static public T Load<T>(string path)
        {
            StreamReader stream = new StreamReader(path);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result = (T)serializer.Deserialize(stream);
            stream.Close();
            return result;
        }

        public void End()
        {
            if (isAttributesRead)
                reader.ReadEndElement();
            else
                reader.ReadStartElement();
        }

        public string Read(string name)
        {
            if (!isAttributesRead) EndOfAttributesReading();

            return reader.ReadElementString(name);
        }

        public T Read<T>()
        {
            if (!isAttributesRead) EndOfAttributesReading();

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result = (T)serializer.Deserialize(reader);
            return result;
        }

        public List<T> ReadList<T>(string name)
        {
            if (!isAttributesRead) EndOfAttributesReading();

            List<T> list = new List<T>();

            reader.MoveToAttribute("count");
            int count = int.Parse(reader.Value);
            reader.ReadStartElement(name);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            for (int i = 0; i < count; i++)
                list.Add((T)serializer.Deserialize(reader));

            reader.ReadEndElement();
            return list;
        }

        public string ReadAttribute(string name)
        {
            return reader[name];
        }

        public T ReadAttribute<T>(string name)
        {
            return reader[name].Parse<T>();
        }

        public XmlCollection ReadCollection()
        {
            XmlCollection c = new XmlCollection();

            while (reader.MoveToNextAttribute())
                c.Add(reader.Name, reader.Value);

            return c;
        }

        private void EndOfAttributesReading()
        {
            reader.ReadStartElement();
            isAttributesRead = true;
        }
    }
}
