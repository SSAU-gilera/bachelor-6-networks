using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace FRp
{
    class FTP_Helper
    {
        public static string ConnectServer(FTP_Client Client,out WebRequest request)//открываем поток на чтение
        {
            request = null;
            try
            {
                request = WebRequest.Create(Client.Host);
                request.Credentials = new NetworkCredential(Client.Login, Client.Password);//считываем логин и пароль
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                return "OK";
            }
            catch( Exception ex)
            {
                return ex.Message;
            }                
        }

        public static List<Status> ProcessServer(FTP_Client client, ref DirectoryElement root)//обработка сервера
        {
            List<Status> statusList = new List<Status>();
            var connectionResult = ConnectServer(client, out WebRequest request);
            if (connectionResult != "OK")
            {
                statusList.Add(new Status("ERROR", connectionResult));
                return statusList;
            }
            string[] split_resp;
            try
            {
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())//получаем запрос с севрера
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))//открываем для чтения потока
                {
                    var responseData = reader.ReadToEnd();
                    split_resp = responseData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                }

                foreach (var dirData in split_resp)
                {
                    var fileName = getFileName(dirData);
                    var newPath = client.Host + "/" + fileName;
                     if (dirData.StartsWith("d"))//
                     {
                        var subdir = new DirectoryElement(fileName, true);
                        FTP_Client newClient = new FTP_Client(
                            newPath,
                            client.Login,
                            client.Password
                            );
                        List<Status> recStatus = ProcessServer(newClient,ref subdir);
                        root.addElem(subdir);

                        statusList.AddRange(recStatus);

                        statusList.Add(new Status("OK", $"Директорий по адресу {newPath} обработан успешно"));
                    }
                     else
                     {
                        root.addElem(new DirectoryElement(fileName, false));
                        statusList.Add(new Status("OK", $"Файл по адресу {newPath} обработан успешно"));
                     }
                }
            }
            catch (Exception ex)
            {
                statusList.Add(new Status("ERROR", ex.Message));
                return statusList;
            }
            return statusList;
        }

        public static string getFileName(string dirData)
        {
            string[] dirAttributes = dirData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return dirAttributes[dirAttributes.Length - 1];
        }
    }
}
