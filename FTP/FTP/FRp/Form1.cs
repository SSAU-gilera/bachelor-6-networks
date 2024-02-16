using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace FRp
{
    public partial class Form1 : Form
    {
        FTP_requests ftp = new FTP_requests();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, System.EventArgs e)
        {
            rtbStatus.Text = "";
            treeView.Nodes.Clear();

            var host = tbHost.Text;
            FTP_Client client = new FTP_Client(host, tbLogin.Text, tbPassword.Text);
            //MakeTree_Lib(client);
            MakeTree_MY(client);
        }

        private void CreateCatalogue(DirectoryElement directory, TreeNode inputDir)
        {
            foreach (var subelement in directory.subdirectories)
            {
                inputDir.Nodes.Add(subelement.Name);
                if (subelement.isFolder)
                {
                    CreateCatalogue(subelement, inputDir.Nodes[inputDir.Nodes.Count - 1]);
                }
            }
        }
        //test
        private void MakeTree_Lib(FTP_Client client)
        {      
            DirectoryElement root = new DirectoryElement(client.Host, true);
            List<Status> logs = FTP_Helper.ProcessServer(client, ref root);
            foreach (var log in logs)
            {
                rtbStatus.Text += log.ConnectionStatus + " " + log.Message + "\n";
            }
            if (logs[logs.Count - 1].ConnectionStatus != "ERROR")
            {
                treeView.Nodes.Add(root.Name);
                CreateCatalogue(root, treeView.Nodes[0]);
                treeView.ExpandAll();
            }
        }


        private void MakeTree_MY(FTP_Client client)
        {
            DirectoryElement root = new DirectoryElement(client.Host, true);

            //Создание сокета
            Status connectionStatus = ftp.getConnectedSocket(client.Host, FTP_requests.FTP_PORT, out Socket socket);
            printLog(connectionStatus);
            if(connectionStatus.ConnectionStatus == "ERROR")
            {
                return;
            }
            //Login
            //USER ____ PASS____
            var request = ftp.GetUserRequest(client.Login) + ftp.GetPassRequest(client.Password);
            connectionStatus = ftp.GetResponse(socket,request);
            printLog(connectionStatus);
            if (connectionStatus.ConnectionStatus == "ERROR")
            {
                return;
            }

            GetCatalogue(socket,client.Host,ref root, false);

            connectionStatus = ftp.QuitSection(socket);
            printLog(connectionStatus);

            socket.Shutdown(SocketShutdown.Send);
            socket.Close();

            //Вывод каталога       
            treeView.Nodes.Add(root.Name);
            CreateCatalogue(root, treeView.Nodes[0]);
            treeView.ExpandAll();
        }

        private void GetCatalogue(Socket socket ,string path, ref DirectoryElement root, bool isChildDir)
        {
            Status connectStatus;
            if(isChildDir)
            {
                //Переход в директорию если дочерний
                connectStatus = ftp.GetResponse(socket, ftp.GetCwdrequest(path));
                printLog(connectStatus);
                if (connectStatus.ConnectionStatus == "ERROR")
                {
                    return;
                }
            }                         
            //PASV - переход в пассивный режим,получение dataSocket(по нему получаем данные с сервака)
            var connectionStatus = ftp.GetPassiveSocket(socket, out Socket dataSocket);
            printLog(connectionStatus);
            if (connectionStatus.ConnectionStatus == "ERROR")
            {
                return;
            }
            //LIST запрос на список каталога
            byte[] req = Encoding.ASCII.GetBytes(ftp.GetListrequest());
            var bytesSent = socket.Send(req);//отправка
            var response = "";

            byte[] resp = new byte[socket.ReceiveBufferSize];
            int bytes = 0;
            using (MemoryStream m = new MemoryStream())
            {
                while (dataSocket.Poll(1000000, SelectMode.SelectRead) &&
                (bytes = dataSocket.Receive(resp, dataSocket.ReceiveBufferSize, SocketFlags.None)) > 0)
                {
                    m.Write(resp, 0, bytes);
                }//получение
                response = Encoding.ASCII.GetString(m.ToArray());
            }
            rtbStatus.Text += response + "\n";

            //разбиение строк о каждом файле/папке
            //-rwxr-x---    1 1227     1000           18 Nov 20  2015 .bash_logout
            //-rwxr - x-- - 1 1227     1000          193 Nov 20  2015.bash_profile
            string[] split_response = response.Split(
                new char[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);

            
            foreach (var dirData in split_response)
            {
                var fileName = getFileName(dirData);
                var newPath = path + "/" + fileName;
                if (dirData.StartsWith("d"))
                {
                    //Если директорий запускаем рекурсивно
                    //drwxr-xr-x    2 1227     1000         4096 Nov 22  2016 123
                    var subdir = new DirectoryElement(fileName, true);
                    GetCatalogue(socket,fileName, ref subdir, true);
                    root.addElem(subdir);
                    rtbStatus.Text += $"OK Директорий по адресу {newPath} обработан успешно\n";
                }
                /*else
                {
                    //если папка
                    //-rw-r--r--    1 1227     1000           21 Nov 22  2016 info.php
                    root.addElem(new DirectoryElement(fileName, false));
                    rtbStatus.Text += $"OK Файл по адресу {newPath} обработан успешно\n";
                }*/
            }

            //Переход в родительский директорий
           connectStatus = ftp.GetResponse(socket, ftp.GetCdupRequest());
            printLog(connectionStatus);

            dataSocket.Shutdown(SocketShutdown.Receive);
            dataSocket.Close();    
        }

        //-rwxr-x---    1 1227     1000          193 Nov 20  2015 -->.bash_profile<--
        private string getFileName(string dirData)
        {
            //
            string[] dirAttributes = dirData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return dirAttributes[dirAttributes.Length - 1];
        }

        //Вывод статуса в лог
        private void printLog(Status status)
        {
            rtbStatus.Text += status.ConnectionStatus + " " + status.Message + "\n";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
