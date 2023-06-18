using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Google.Protobuf;
using OnlineGame;
using UnityEngine;

namespace Arknights
{
    public static class Request
    {
        private static Dictionary<ProtoIdx, UniTaskCompletionSource<ResCode>> tasks = new()
        {
            { ProtoIdx.create_room_s2c, new UniTaskCompletionSource<ResCode>() },
            { ProtoIdx.join_room_s2c, new UniTaskCompletionSource<ResCode>() },
        };

        public static async UniTask<ResCode> CreateRoom(string roomName)
        {
            Socket.Connect();

            Dispacher.SendMsg(new create_room_c2s
            {
                Name = roomName,
            });
            var source = tasks[ProtoIdx.create_room_s2c];
            return await source.Task;
        }

        public static async UniTask<ResCode> JoinRoom(string roomName)
        {
            Socket.Connect();

            Dispacher.SendMsg(new join_room_c2s
            {
                Name = roomName,
            });
            var source = tasks[ProtoIdx.join_room_s2c];
            return await source.Task;
        }

        public static async UniTask<ResCode> WaitJoinRoom()
        {
            Debug.Log("等待加入房间");
            var source = tasks[ProtoIdx.join_room_s2c];
            return await source.Task;
        }

        public static void Response(IMessage msg)
        {
            tasks.TryGetValue(Dispacher.GetProtoIdx(msg), out var source);
            if (source == null)
            {
                throw new Exception("收到未知消息");
            }

            switch (msg)
            {
                case create_room_s2c data:
                    source.TrySetResult((ResCode)data.ResCode);
                    break;
                case join_room_s2c data:
                    source.TrySetResult((ResCode)data.ResCode);
                    break;
            }
        }

        public static void CancelCreateRoom()
        {
            tasks[ProtoIdx.create_room_s2c].TrySetCanceled();
        }

        public static void CancelJoinRoom()
        {
            tasks[ProtoIdx.join_room_s2c].TrySetCanceled();
        }

        // public static async UniTaskVoid ExitRoom(string roomName)
        // {
        //     Dispacher.SendMsg(new exit_room_c2s
        //     {
        //         Name = roomName
        //     });
        //     var source = tasks[ProtoIdx.join_room_s2c];
        //     return await source.Task;
        // }
    }
}