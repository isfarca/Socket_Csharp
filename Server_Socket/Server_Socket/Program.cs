using System;
using System.Text;
using System.Net.Sockets;

namespace Server_Socket
{
    class Program
    {
        static void Main(string[] args)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            TcpListener serverSocket = new TcpListener(8888);
#pragma warning restore CS0618 // Type or member is obsolete
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[10025];
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Data from client - " + dataFromClient);
                    string serverResponse = "Last Message from client - " + dataFromClient;
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch
                {
                    clientSocket.Close();
                    serverSocket.Stop();
                    Console.WriteLine(" >> exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
