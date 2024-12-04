public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public Dictionary<string, Object>? meta { get; set; } 

    public static ApiResponse<T> Create(int _Status, string _Message, T _Data)
    {
        var resp = new ApiResponse<T>
        {
            Code = _Status,
            Message = _Message,
            Data = _Data
        };

        return resp;
    }
}
