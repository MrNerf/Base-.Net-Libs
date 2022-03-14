using System;
using System.Xml.Serialization;

namespace _09_SimpleSerialize
{
    [Serializable]
    [XmlRoot (Namespace = "https://github.com/MrNerf")]
    public class JamesBondCar:Car
    {
        [XmlAttribute]
        public bool CanFly;
        [XmlAttribute]
        public bool CanSubmerge;
    }
}
