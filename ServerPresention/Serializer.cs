using LogicServer;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ServerPresention
{
    public abstract class Serializer
    {
        public static string SerializeBook(BookDTO book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookDTO));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, book);
                return writer.ToString();
            }
        }

        public static BookDTO DeserializeBook(string book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookDTO));
            using (StringReader reader = new StringReader(book))
            {
                BookDTO ibook = serializer.Deserialize(reader) as BookDTO;
                if (ibook == null)
                {
                    return null;
                }

                return ibook;
            }
        }
    }
}
