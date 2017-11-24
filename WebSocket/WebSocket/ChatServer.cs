using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApp
{
    public class ChatServer
    {
        static TcpListener tcpListener; // сервер для прослушивания
        List<ChatClient> clients = new List<ChatClient>(); // все подключения

        public void AddConnection(ChatClient client)
        {
            clients.Add(client);
        }

        public void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ChatClient client = clients.FirstOrDefault(c => c.ID == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);
        }

        public void listening()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Server is starting. Wait connection...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ChatClient clientObject = new ChatClient(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Chating));
                    clientThread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Disconnect();
            }
        }

            // трансляция сообщения подключенным клиентам
        protected internal void ProcessingOfMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ID != id) // если id клиента не равно id отправляющего
                {
                    clients[i].stream.Write(data, 0, data.Length); //передача данных
                }
            }
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
        }
    }
}
