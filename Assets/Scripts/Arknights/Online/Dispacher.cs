using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Google.Protobuf;
using OnlineGame;
using UnityEditor;
using UnityEngine;

namespace Arknights
{
    public static partial class Dispacher
    {
        private static ConcurrentQueue<IMessage> waitingSendMsgs = new();
        private static ConcurrentQueue<IMessage> waitingDistributeMsgs = new();

        public static Dictionary<ProtoIdx, MessageParser> parsers = new()
        {
            { ProtoIdx.LogicUpdate, LogicUpdate.Parser },
            { ProtoIdx.KeepAlive, KeepAlive.Parser },
            { ProtoIdx.create_room_s2c, create_room_s2c.Parser },
            { ProtoIdx.join_room_s2c, join_room_s2c.Parser },
        };


        public static void Distribute()
        {
            while (GetWaitingDistributeMsg() is { } msg)
            {
                switch (msg)
                {
                    case LogicUpdate data:
                        Type localCommanderType = typeof(LocalCommander);
                        foreach (var rpc in data.Rpcs)
                        {
                            var command = rpc.Command;
                            var method = command.Method;
                            object[] parameters = command.Params.ToArray();
                            localCommanderType.GetMethod(method)?.Invoke(null, parameters);
                        }

                        EventManager.CallLogicUpdate();
                        break;
                    case create_room_s2c or join_room_s2c:
                        Request.Response(msg);
                        break;
                    case KeepAlive:
                        SendMsg(new KeepAlive
                        {
                            Data = 1,
                        });
                        break;
                    default:
                        Debug.LogError("收到错误的消息: " + msg.GetType() + msg);
                        break;
                }
            }
        }


        public static IMessage GetWaitingSendMsg()
        {
            waitingSendMsgs.TryDequeue(out var result);
            return result;
        }

        public static IMessage GetWaitingDistributeMsg()
        {
            waitingDistributeMsgs.TryDequeue(out var result);
            return result;
        }


        //唯一指定发消息出口，禁止其他发消息方式
        public static void SendMsg(IMessage msg)
        {
            waitingSendMsgs.Enqueue(msg);
        }

        public static void ReceiveMsg(NetworkStream stream)
        {
            // while (stream.DataAvailable)  //这个属性不可靠，微软的问题
            while (true)
            {
                var msg = Receive(stream);
                waitingDistributeMsgs.Enqueue(msg);
            }
        }


        [MenuItem("Tools/test")]
        public static void MyTest()
        {
            var type = Type.GetType("Arknights.LocalCommander");
            Debug.Log(type);
            // waitingDistributeMsgs.Enqueue(new LogicUpdate()
            // {
            //     Rpcs =
            //     {
            //         new RpcMsg()
            //         {
            //             From = 2,
            //             Command = new Command()
            //             {
            //                 Method = "下场",
            //                 Params = { "1", "2" },
            //             }
            //         }
            //     }
            // });

            // var p = new string[2];
            // p[0] = "1";
            // p[1] = "2";
            // var a = new RpcMsg()
            // {
            //     From = 0,
            //     Command = new Command()
            //     {
            //         Method = "test",
            //         Params = { p },
            //     }
            // };
            //
            // var z = a.ToByteArray();
            // var b = RpcMsg.Parser.ParseFrom(z);
            // Debug.Log(b.Command.Params);


            // var s = Google.Protobuf.WellKnownTypes.Any.Pack("1");


            // CancellationToken token = new CancellationToken();
            // var source = new UniTaskCompletionSource();
            //
            // async UniTaskVoid Test1()
            // {
            //     Debug.Log("test1");
            // }
            //
            // async UniTaskVoid Test2()
            // {
            //     await UniTask.Delay(3000);
            //     source.TrySetCanceled(token);
            // }
            //
            //

            // create_room_c2s msg1 = new create_room_c2s()
            // {
            //     Name = "测试房间",
            // };
            //
            // waittingSendQueue.Enqueue(msg1);
            //
            // while (GetWaittingMsg() is { } msg)
            // {
            //    Debug.Log("msg: " + msg);
            // }
            // Debug.Log("end");
        }
    }
}