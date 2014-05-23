using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Diese.Xml
{
    public class XmlCollection : IEnumerable
    {
        private Dictionary<string, string> elements;

        public int Count
        {
            get { return elements.Count; }
        }

        public XmlCollection()
        {
            elements = new Dictionary<string, string>();
        }

        public void Add(string name, string value)
        {
            elements.Add(name, value);
        }

        public void Add<T>(string name, T value)
        {
            elements.Add(name, value.ToString<T>());
        }

        public string Get(string name)
        {
            return elements[name];
        }

        public T Get<T>(string name)
        {
            try
            {
                if (typeof(T).IsEnum)
                    return elements[name].ParseEnum<T>();
                return elements[name].Parse<T>();
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public void Clear()
        {
            elements.Clear();
        }

        public string this[string key]
        {
            get { return elements[key]; }
            set { elements[key] = value; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }
    }
}
