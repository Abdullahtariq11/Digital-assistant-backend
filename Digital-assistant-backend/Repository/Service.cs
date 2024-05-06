using Digital_assistant_backend.Data;

namespace Digital_assistant_backend;

public class Service<T>
{
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public T? Data { get; private set; }

    
     Service(bool Success,string? Message, T? Data)
    {
        this.Success=Success;
        this.Message=Message;
        this.Data=Data;
    }

    public static Service<T> success (T data)
    {
        return new Service<T> (true,null,data);
    }
    public static Service<T> failure (string message)
    {
        return new Service<T> (false,message,default);
    }


}
