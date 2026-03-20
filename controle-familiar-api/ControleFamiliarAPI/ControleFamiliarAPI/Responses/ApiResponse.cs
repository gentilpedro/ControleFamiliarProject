namespace ControleFamiliarAPI.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
        }
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}