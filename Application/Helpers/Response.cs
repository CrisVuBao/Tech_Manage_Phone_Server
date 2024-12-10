namespace Tech_Manage_Server.Helpers
{
    public class Response<T>
    {
        public bool? Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public static Response<T> SuccessResult(string mess,T Data)
        {
            return new Response<T> { Success = true, Message = mess, Data = Data };
        }

        public static Response<T> Failure(string errorMessage)
        {
            return new Response<T> { Success = false, Message = errorMessage };
        }
    }
}
