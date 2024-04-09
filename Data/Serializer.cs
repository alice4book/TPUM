using System;
using System.IO;
using System.Xml.Serialization;
using BookInfo;

namespace Data
{
    public abstract class Serializer
    {
        public static string SerializeBook(BookInfo.BookInfo book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookInfo.BookInfo));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, book);
                return writer.ToString();
            }
        }

        public static IBook DeserializeBook(string book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookInfo.BookInfo));
            using (StringReader reader = new StringReader(book))
            {
                BookInfo.BookInfo ibook = serializer.Deserialize(reader) as BookInfo.BookInfo;
                if (ibook == null)
                {
                    return null;
                }

                return new Book(ibook.Id, ibook.Title, ibook.Description, ibook.Author, ibook.Price, (BookType)Enum.Parse(typeof(BookType), ibook.Type, true));
            }
        }
    }
}
