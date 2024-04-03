using System.Collections.Generic;
using System.Text.Json;

namespace Data
{
    public abstract class Serializer
    {
        public static string BookToJSON(IBook book)
        {
            return JsonSerializer.Serialize(book);
        }

        public static IBook JSONToBook(string json)
        {
            return JsonSerializer.Deserialize<Book>(json);
        }

        public static string StorageToJSON(List<IBook> books)
        {
            return JsonSerializer.Serialize(books);
        }

        public static List<IBook> JSONToStorage(string json)
        {
            return new List<IBook>(JsonSerializer.Deserialize<List<Book>>(json)!);
        }
    }
}
