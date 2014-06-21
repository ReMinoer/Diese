using System.Xml.Serialization;

namespace Diese.Xml
{
    public interface IXmlCollectable : IXmlSerializable
    {
        XmlCollection GenerateXmlCollection();

        void Initialize(XmlCollection c);
    }
}