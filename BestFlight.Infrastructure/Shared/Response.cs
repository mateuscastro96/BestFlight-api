namespace BestFlight.Infrastructure.Shared
{
    public class Response<T>
    {
        public T? Data { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; } = [];
        private Response(T? data, bool success, string message, List<string>? errors = null)
        {
            Data = data;
            Success = success;
            Message = message;
            Errors = errors ?? [];
        }

        public static Response<T> Ok(T data, string message = "Operação realizada com sucesso") =>
            new(data, true, message);

        public static Response<T> Fail(string message, List<string>? errors = null) =>
            new(default, false, message, errors);
    }
}
