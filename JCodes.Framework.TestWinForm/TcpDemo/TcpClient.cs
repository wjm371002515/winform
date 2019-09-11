using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JCodes.Framework.TestWinForm.TcpDemo
{
    public class TcpClient
    {
        public void ConnetctDemo()
        {
            byte[] data = new byte[1024];
            Socket newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.Write("please input the server ip:");
            string ipadd = Console.ReadLine();
            Console.WriteLine();
            Console.Write("please input the server port:");
            int port = Convert.ToInt32(Console.ReadLine());
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), port);
            try
            {
                newclient.Connect(ie);
            }
            catch (Exception ex)
            {
                Console.WriteLine("unable to connect to server");
                Console.WriteLine(ex.ToString());
            }
            int recv = newclient.Receive(data);
            string stringdata = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringdata);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                    break;
                newclient.Send(Encoding.ASCII.GetBytes(input));
                data = new byte[1024];
                recv = newclient.Receive(data);
                stringdata = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringdata);
            }

            Console.WriteLine("disconnect from servcer..");
            newclient.Shutdown(SocketShutdown.Both);
            newclient.Close();
        }
    }
}
