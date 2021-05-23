using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace SportsStore.Server {
    public class ServerOperations
    {
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        private static NetworkStream stream;

        public static void ConnectToServer() {
            client = new TcpClient();
            try {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                // запускаем новый поток для получения данных
                //Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                //receiveThread.Start(); //старт потока
                //Console.WriteLine("Добро пожаловать, {0}", userName);
                //SendMessage();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                Application.Current.Shutdown();
            }
        }

        // отправка сообщений
        public static void SendData(string message) {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public static void SendByteData(byte[] data) {
            stream.Write(data, 0, data.Length);
        }

        // получение сообщений
        public static string ReceiveData() {
            byte[] data = new byte[256]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = stream.Read(data, 0, data.Length);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            return builder.ToString();
        }

        static void Disconnect() {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }
    }
}
