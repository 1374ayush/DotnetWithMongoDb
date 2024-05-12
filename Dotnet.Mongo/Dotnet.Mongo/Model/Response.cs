namespace Dotnet.Mongo.Model
{
    public class Response<T> where T : class
    {
        public string message { get; set; } 
        public T data { get; set; }
    }
}
