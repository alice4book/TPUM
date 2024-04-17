

namespace ServerPresention
{
    [Serializable]
    public abstract class ServerCommand
    {
        public string Header { get; set; }

        protected ServerCommand(string header)
        {
            Header = header;
        }
    }

    [Serializable]
    public class GetBooksCommand : ServerCommand
    {
        public static string StaticHeader = "GetBooks";

        public GetBooksCommand()
        : base(StaticHeader)
        {

        }
    }

    [Serializable]
    public class SellBookCommand : ServerCommand
    {
        public static string StaticHeader = "RemoveBooks";

        public List<Guid> BookIDs { get; set; }

        public SellBookCommand(List<Guid> ids)
        : base(StaticHeader)
        {
            BookIDs = ids;
        }
    }
    [Serializable]
    public abstract class ServerResponse
    {
        public string Header { get; private set; }

        protected ServerResponse(string header)
        {
            Header = header;
        }
    }

    [Serializable]
    public class SendBooksResponse : ServerResponse
    {
        public static readonly string StaticHeader = "SendBooks";

        public BookInfo[]? Books { get; set; }

        public SendBooksResponse()
            : base(StaticHeader)
        {
        }
    }

    [Serializable]
    public class BookInfo
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Author { get; }
        public float Price { get; }
        public string Type { get; }
        public BookInfo()
        {

            Title = "None";
            Description = "None";
            Author = "None";
            Price = 0.0f;
            Type = "None";
            Id = Guid.Empty;
        }

        public BookInfo(string title, string description, string author, float price, string type, Guid id)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Type = type;
            Id = id;
        }
    }
}
