using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JCodes.Framework.TestWinForm.TcpDemo
{
    public class TcpServer
    {
        public void ConnetctDemo()
        {
            int recv;                       // 用于表示客户端发送的信息长度
            byte[] data = new byte[1024];   // 用于缓存客户端所发送的信息，通过socket传递信息必须为字节组
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 6003);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("waiting for a client ......");
            Socket client = newsock.Accept();

            IPEndPoint clientip = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("connect with client:" + clientip.Address + " at port:" + clientip.Port);
            string welcome = "welcome here";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                Console.WriteLine("recv=" + recv);
                if (recv == 0)
                    break;
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                client.Send(data, recv, SocketFlags.None);
            }

            Console.WriteLine("Disconnected from" + clientip.Address);
            client.Close();
            newsock.Close();
        }
    }
}
