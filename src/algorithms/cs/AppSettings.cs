using System.Xml.Serialization;

namespace Studying.Leetcode
{
    public class AppSettings
    {
        [XmlElement]
        public string ProgramName { get; set; }
        [XmlElement]
        public string ProgramDescription { get; set; }
        [XmlElement]
        public string ClassName { get; set; }
    }
}