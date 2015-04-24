﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Diese.Modelization;

namespace Diese.Serialization
{
    public class SerializerXml<T> : Serializer<T>
        where T : new()
    {
        private readonly XmlSerializer _serializer;

        public SerializerXml()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public override T Instantiate(Stream stream)
        {
            return (T)_serializer.Deserialize(stream);
        }

        public override void Save(T obj, Stream stream)
        {
            _serializer.Serialize(stream, obj);
        }

        public T Instantiate(TextReader textReader)
        {
            return (T)_serializer.Deserialize(textReader);
        }

        public void Save(T obj, TextWriter textWriter)
        {
            _serializer.Serialize(textWriter, obj);
        }
    }

    public class SerializerXml<T, TModel> : Serializer<T, TModel>
        where TModel : IDataModel<T>, new()
    {
        private readonly XmlSerializer _serializer;

        public SerializerXml()
        {
            _serializer = new XmlSerializer(typeof(TModel));
        }

        public override TModel LoadModel(Stream stream)
        {
            return (TModel)_serializer.Deserialize(stream);
        }

        public override void SaveModel(TModel model, Stream stream)
        {
            _serializer.Serialize(stream, model);
        }

        public void Load(T obj, TextReader textReader)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(IConfigurator<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement IConfigurator<T>",
                    typeof(TModel)));

            var model = (TModel)_serializer.Deserialize(textReader) as IConfigurator<T>;
            if (model != null)
                model.Configure(obj);
        }

        public T Instantiate(TextReader textReader)
        {
            if (!typeof(TModel).GetInterfaces().Contains(typeof(ICreator<T>)))
                throw new InvalidOperationException(string.Format("{0} does not implement ICreator<T>", typeof(TModel)));

            var model = (TModel)_serializer.Deserialize(textReader) as ICreator<T>;
            if (model == null)
                throw new InvalidOperationException(string.Format("{0} does not implement ICreator<T>", typeof(TModel)));

            return model.Create();
        }

        public void Save(T obj, TextWriter textWriter)
        {
            var model = new TModel();
            model.From(obj);
            _serializer.Serialize(textWriter, model);
        }
    }
}