using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    internal static class ServerStatics
    {
        //Commends
        public static readonly string GetBooksCommandHeader = "GetBooks";
        public static readonly string RemoveBooksCommandHeader = "RemoveBooks";
        //Responses
        public static readonly string SendsBooksResponseHeader = "SendBooks";
    }


    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal class BookInfo
    {
        [JsonProperty("Id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("Description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("IsSold", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty("Price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public float Price { get; set; }

        [JsonProperty("Type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
    //Commends
    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal abstract class ServerCommand
    {
        [JsonProperty("Header", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Header { get; set; }
    }
    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal partial class GetBooksCommand : ServerCommand
    {

    }

    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal class RemoveBookCommand : ServerCommand
    {
        [JsonProperty("BookIDs", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Guid> BookIDs { get; set; }
    }

    //Responses
    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal abstract class ServerResponse
    {
        [JsonProperty("Header", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Header { get; set; }
    }

    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal class SendBooksResponse : ServerResponse
    {
        [JsonProperty("Books", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BookInfo> Books { get; set; }
    }
}
