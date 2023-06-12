using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Arknights
{
    public static class Socket
    {
        public static TcpClient client;
        public static void  CreateConnect(string server, Int32 port)
        {
            try
            {
                client = new TcpClient(server, port);
                
                NetworkStream stream = client.GetStream();

                Send(stream, "hello");

                Receive(stream, out string rcv);
                
                Debug.Log(rcv);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        private static void Send(NetworkStream stream, string msg)
        {
            byte[] msg_bytes = Encoding.UTF8.GetBytes(msg);
            stream.Write(msg_bytes, 0, msg_bytes.Length);
        }

        private static void Receive(NetworkStream stream, out string s)
        {
            byte[] rcv_bytes = new byte[1024];
            int rcv_length = stream.Read(rcv_bytes, 0, rcv_bytes.Length);
            s = Encoding.UTF8.GetString(rcv_bytes, 0, rcv_length);
        }

    }
}