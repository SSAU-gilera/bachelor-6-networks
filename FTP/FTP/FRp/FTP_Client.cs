
namespace FRp
{
    class FTP_Client
    {
        public string Host { get; }
        public string Login { get; }
        public string Password { get; }

        public FTP_Client(string host,string login,string password)
        {
            Host = host;
            Login = login;
            Password = password;
        }

    }
}
