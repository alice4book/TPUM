using LogicServer;
using System.Xml.Serialization;
using ClientApi;
using System.Diagnostics;

namespace ServerPresention
{
    public abstract class Serializer
    {
        public static string SerializeBook(BookInfo book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookInfo));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, book);
                return writer.ToString();
            }
        }

        public static BookDTO DeserializeBook(string book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookInfo));
            using (StringReader reader = new StringReader(book))
            {
                BookInfo ibook = serializer.Deserialize(reader) as BookInfo;
                if (ibook == null)
                {
                    return null;
                }

                return new BookDTO
                {
                    Title = ibook.Title,
                    Description = ibook.Description,
                    Author = ibook.Author,
                    Price = ibook.Price,
                    Type = ibook.Type,
                    Id = ibook.Id
                };
            }
        }
    }
}
