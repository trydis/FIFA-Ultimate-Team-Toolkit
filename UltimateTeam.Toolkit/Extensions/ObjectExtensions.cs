using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace UltimateTeam.Toolkit.Extensions
{
    internal static class ObjectExtensions
    {
        public static void ThrowIfNullArgument(this object input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }
        }

        public static string DataContractSerializeObject<T>(T objectToSerialize)
        {
            string xmlString = string.Empty;

            using (MemoryStream memStm = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memStm, objectToSerialize);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    xmlString = streamReader.ReadToEnd();
                }
            }

            XDocument xmlDoc = XDocument.Parse(xmlString);
            return xmlDoc.Root.Value;
        }
    }
}