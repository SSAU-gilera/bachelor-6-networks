using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpParser
{
    class HttpRequest//отправляет запрос на сервер и получает ответ
    {
        const int BUFFER_SIZE = 1024;

        //Открытие сокета
        static string ConnectSocket(string address, int port, out Socket socket)//открывает сокеты, и если всё нормально и всё подключается к серверу, то продолжаем
        {
            socket = null;
            Socket skt = new Socket(
                   AddressFamily.InterNetwork,
                   SocketType.Stream,
                   ProtocolType.Tcp);
            try
            {
                skt.Connect(address, port);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            if (skt.Connected)
            {
                socket = skt;
                return "OK";
            }
            return "Not Connected";
          ;
        }

        //Отправка запроса,получение ответа от сервера
        public static ResultStatus GetPage(string host,string path, int port)//получает ответ с сервера через открытый сокет
        {
            var request = GetRequest(host, path);
            string page = "";

            byte[] data = Encoding.Default.GetBytes(request);//массив байтов
            byte[] bytesReveived = new byte[BUFFER_SIZE];//сда будем записывать ответ

            string SocketConnectionStatus = ConnectSocket(host, port,out Socket socket);//возвращаем сокет
            if (socket == null)
            {
                return new ResultStatus(page, SocketConnectionStatus,-1);
            }

            try
            {                
                int bytesSent = socket.Send(data);//отправляем запрос серверу
               
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(bytesReveived,0, bytesReveived.Length,0);//получение ответа построчно (количество полученных байтов)
                    page += Encoding.Default.GetString(bytesReveived,0,bytes);//накапливаем ответ сервера построчно
                }
                while (bytes > 0);
                socket.Shutdown(SocketShutdown.Both);//закрываем сокеты
                socket.Close();
                int pageSize = page.Length - page.IndexOf("Content-Type: ") - 14;//получаем размер страницы (по количеству символов)
            }
            catch (Exception e)
            {
                return new ResultStatus(page, e.Message,-1);
            }
            if (page == null)
                return new ResultStatus(page, " Страница не была загружена ", 0);

            return new ResultStatus(page, " Страница успешно загружена ", page.Length);
        }


        private static string GetRequest(string host,string path)//формирует запрос к серверу (сервер, страница на сервере)
        {
            string rn = "\r\n";
            string request = "";
            request =  path != null ?   
                "GET " + path + " HTTP/1.1" + rn  +
                "Host: " + host + rn + 
                "Connection: Close" + rn + rn :

                "GET / HTTP/1.1" + rn + 
                "Host: " + host + rn +  
                "Connection: Close" + rn + rn;
            return request;
        }

    }
}
