using System;
using System.Net.Sockets;
using System.Text;

namespace ChatApp
{
    public class ChatClient
    {
        protected internal string ID { get; private set; }
        protected internal NetworkStream stream { get; private set; }
        string Name;
        TcpClient client;
        ChatServer server;

        public ChatClient(TcpClient tcpClient, ChatServer s)
        {
            ID = Guid.NewGuid().ToString();
            client = tcpClient;
            server = s;
            s.AddConnection(this);
        }


        public void Chating()
        {
            stream = client.GetStream();
            string message;
            // получаем имя пользователя
            Name = GetMessage();

            message = Name + " online";

            server.ProcessingOfMessage(message, this.ID);
            Console.WriteLine(message);

            for (;;)
            {
                try
                {
                    message = GetMessage();
                    message = String.Format("{0}: {1}", Name, message);
                    Console.WriteLine(message);
                    server.ProcessingOfMessage(message, this.ID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

            // чтение входящего сообщения и преобразование в строку
        private string GetMessage()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            return builder.ToString();
        }

        protected internal void Close()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
