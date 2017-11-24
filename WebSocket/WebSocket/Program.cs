using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ChatApp;
using System.Threading;

namespace WebSocket
{
    public class Program
    {
        static ChatServer server; // сервер
        static Thread listenThread; // потока для прослушивания

        public static void Main(string[] args)
        {
            try
            {
                server = new ChatServer();
                listenThread = new Thread(new ThreadStart(server.listening));
                listenThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
