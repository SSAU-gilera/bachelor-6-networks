using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace FRp
{
    class FTP_requests
    {
        public const int FTP_PORT = 21;

        public FTP_requests ()
        {

        }

        //Requests
        #region

        public string GetPassRequest(string password)
        {
            return "PASS " + password + "\r\n";
        }

        public string GetUserRequest(string username)
        {
            return "USER " + username + "\r\n";
        }

        //Завершение сеанса
        public string GetQuitRequest()
        {
            return "QUIT\r\n";
        }

        //Переход в пассивный режим
        public string GetPasvRequest()
        {
            return "PASV\r\n";
        }

        //Переход в активный режим
        public string GetPostRequest()
        {
            return "POST\r\n";
        }

        //Сменить директорию
        public string GetCwdrequest(string curdir)
        {
            return $"CWD {curdir}\r\n";
        }

        //Переход в родительский каталог
        public string GetCdupRequest()
        {
            return "CDUP\r\n";
        }

        public string GetListrequest()
        {
            return "LIST\r\n";
        }
        #endregion

        public Status getConnectedSocket(string host, int port, out Socket socket)
        {
            socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(host), port);
                socket.Connect(ipPoint);
            }
            catch (WebException ex)
            {
                return new Status("ERROR",ex.Message);
            }
            if (socket.Connected)
            {
                return new Status("OK",$"Соединение с {host} по порту {port}");
            }
            return new Status("ERROR","Not Connected");
        }

        public Status GetResponse(Socket socket,string request)
        {
            byte[] req = Encoding.ASCII.GetBytes(request);

            var response =  "";
            byte[] resp = new byte[socket.ReceiveBufferSize];
            try
            {
                var bytesSent = socket.Send(req);//отправка
                int bytes = 0;

                using (MemoryStream m = new MemoryStream())//получение
                {
                    while (socket.Poll(1000000, SelectMode.SelectRead) &&
                        (bytes = socket.Receive(resp, socket.ReceiveBufferSize, SocketFlags.None)) > 0)
                    {
                        m.Write(resp, 0, bytes);
                    }
                    response = Encoding.ASCII.GetString(m.ToArray());
                }
  
            }
            catch (WebException ex)
            {
                return new Status("ERROR", ex.Message);
            }
            return new Status("OK", response);
        }

        public Status GetPassiveSocket(Socket socket, out Socket dataSocket)
        {
            dataSocket = null;
            Status connectStatus = GetResponse(socket,GetPasvRequest());
            if (connectStatus.ConnectionStatus == "ERROR")
            {
                return connectStatus;
            }

            var response = connectStatus.Message;
            // получаем <h1,h2,h3,h4,p1,p2>
            var start = response.IndexOf('(') + 1;
            var end = response.IndexOf(')') ;

            response = response.Substring(start,end - start);

            string[] IP_Port = response.Split(',');

            // ip adress xxx.xxx.xxx.xxx
            var ipAddress = $"{IP_Port[0]}.{IP_Port[1]}.{IP_Port[2]}.{IP_Port[3]}";

            //(p1 * 256) + p2 = data port
            int port = (int.Parse(IP_Port[4]) * 256) + int.Parse(IP_Port[5]);

            Status connectionStatus = getConnectedSocket(ipAddress, port, out dataSocket);
            if (connectionStatus.ConnectionStatus != "OK")
            {
                return connectionStatus;
            }
            return connectStatus;
        }

        public Status QuitSection(Socket socket)
        {
            Status connectStatus = GetResponse(socket,GetQuitRequest());
            return connectStatus;
        }
    }
}
