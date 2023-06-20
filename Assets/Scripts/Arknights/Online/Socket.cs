﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using OnlineGame;
using UnityEngine.PlayerLoop;

namespace Arknights
{
    public static class Socket
    {
        public static TcpClient client;
        private static string ip = "127.0.0.1";
        private static int port = 13000;

        public static void Connect()
        {
            if (client != null && client.Connected)
            {
                return;
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //这个事件在意外退出，ios，和uwp上不会触发,详见unity文档
            Application.quitting += () =>
            {
                client.Close();
                client = null;
                cancellationTokenSource.Cancel();
            };


            try
            {
                client = new TcpClient(ip, port);
                
                UniTask.RunOnThreadPool(() =>
                    {
                        while (true)
                        {
                            if (client is not { Connected: true })
                            {
                                Debug.Log("连接断开");
                                return;
                            }

                            //循环把待发送队列里的消息发出去
                            while (Dispacher.GetWaitingSendMsg() is { } msg)
                            {
                                Debug.Log(msg.GetType().ToString() + msg);
                                Dispacher.Send(client.GetStream(), msg);
                            }
                        }
                    }, cancellationToken:
                    cancellationTokenSource.Token).Forget();

                //接收消息并存到待分发队列
                UniTask.RunOnThreadPool(() =>
                    {
                        var stream = client.GetStream();
                        Dispacher.ReceiveMsg(stream);
                    },
                    cancellationToken: cancellationTokenSource.Token).Forget();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public static void Disconnect()
        {
            if (client is not { Connected: true })
                return;

            client.Close();
            Debug.Log(client.Connected);
            client = null;
        }

        // private static void HeartBeatTimer()
        // {
        //     var t = new System.Timers.Timer();
        //     t.Interval = 60000;
        //     t.Elapsed += (sender, args) =>
        //     {
        //         if (client is not { Connected: true })
        //         {
        //             Debug.Log("连接断开");
        //             t.Close();
        //             return;
        //         }
        //
        //         var msg = new heart_beat_c2s { ResCode = 1 };
        //         Dispacher.SendMsg(msg);
        //     };
        //     t.Enabled = true;
        // }
    }
}