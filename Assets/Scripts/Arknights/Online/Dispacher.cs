using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
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

#if OUTLINE_TEST
        public static Queue<RpcMsg> rpcMsgs = new();
        public static Queue<LogicUpdate> logicUpdateMsgs = new();
#endif

        public static Dictionary<ProtoIdx, MessageParser> parsers = new()
        {
            { ProtoIdx.LogicUpdate, LogicUpdate.Parser },
            { ProtoIdx.KeepAlive, KeepAlive.Parser },
            { ProtoIdx.create_room_s2c, create_room_s2c.Parser },
            { ProtoIdx.join_room_s2c, join_room_s2c.Parser },
        };


        // public static void MainDistribute()
        // {
        //     while (GetWaitingDistributeMsg() is { } msg)
        //     {
        //         Debug.Log(msg);
        //         switch (msg)
        //         {
        //             case LogicUpdate data:
        //                 
        //                 break;
        //             case create_room_s2c or join_room_s2c:
        //                 Request.Response(msg);
        //                 break;
        //             case KeepAlive:
        //                 SendMsg(new KeepAlive
        //                 {
        //                     Data = 1,
        //                 });
        //                 break;
        //             default:
        //                 Debug.LogError("收到错误的消息: " + msg.GetType() + msg);
        //                 break;
        //         }
        //     }
        // }


        //todo:  应该分开两种，一种是大厅，一种是战斗。 不应该在
        public static void Distribute()
        {
            while (GetWaitingDistributeMsg() is { } msg)
            {
                Debug.Log(msg);
                switch (msg)
                {
                    case LogicUpdate data:
                        Type localCommanderType = typeof(LocalCommander);
                        foreach (var rpc in data.Rpcs)
                        {
                            if (rpc.Command.Is(Command_Enter.Descriptor))
                            {
                                var command = rpc.Command.Unpack<Command_Enter>();
                                LocalCommander.Enter(rpc.From, command);
                            }
                            else if (rpc.Command.Is(Command_Exit.Descriptor))
                            {
                                var command = rpc.Command.Unpack<Command_Exit>();
                            }
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
#if OUTLINE_TEST
            logicUpdateMsgs.TryDequeue(out var result);
            return result;
#else
            lock (waitingDistributeMsgs)
            {
                waitingDistributeMsgs.TryDequeue(out var result);
                return result;
            }
#endif
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
                lock (waitingDistributeMsgs)
                {
                    waitingDistributeMsgs.Enqueue(msg);
                    Debug.Log(waitingDistributeMsgs.TryPeek(out var result) ? result.ToString() : "null");
                }
            }
        }


        [MenuItem("Tools/test")]
        public static void MyTest()
        {
            Socket.Connect();
        }

        [MenuItem("Tools/test1")]
        public static void MyTest1()
        {
            Socket.Disconnect();
        }
    }
}