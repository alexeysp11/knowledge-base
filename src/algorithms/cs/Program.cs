using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Studying.Leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "app.config"; 
            XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
            AppSettings appSettings;
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                appSettings = (AppSettings)serializer.Deserialize(reader);
            }
            var type = System.Type.GetType(appSettings.ClassName); 
            ILeetcodeProblem problem = System.Activator.CreateInstance(type) as ILeetcodeProblem;
            problem.Execute();
        }
    }
}
