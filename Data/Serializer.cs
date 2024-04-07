using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Data
{
    public abstract class Serializer
    {
        public static string SerializeBook(IBook book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IBook));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, book);
                return writer.ToString();
            }
        }

        public static IBook DeserializeBook(string book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IBook));
            using (StringReader reader = new StringReader(book))
            {
                IBook ibook = serializer.Deserialize(reader) as IBook;
                if (ibook == null)
                {
                    return null;
                }

                return new Book(ibook.Id,ibook.Title, ibook.Description, ibook.Author, ibook.Price, ibook.Type);
            }
        }
    }
}
