

namespace FRp
{
    class Status
    {
        public string ConnectionStatus { get; }
        public string Message { get; }

        public Status(string status,string message)
        {
            ConnectionStatus = status;
            Message = message;
        }
    }
}
