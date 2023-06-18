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
        private static Dictionary<ProtoIdx, UniTaskCompletionSource<IMessage>> tasks = new()
        {
            { ProtoIdx.create_room_s2c, new UniTaskCompletionSource<IMessage>() },
            { ProtoIdx.join_room_s2c, new UniTaskCompletionSource<IMessage>() },
        };

        public static async UniTask<create_room_s2c> CreateRoom(string roomName)
        {
            Socket.Connect();

            Dispacher.SendMsg(new create_room_c2s
            {
                Name = roomName,
                Player = new OnlineGame.Player
                {
                    Name = Main.Instance.me.name,
                    Cards = { Main.Instance.me.selectCardIdxs }
                }
            });
            var source = tasks[ProtoIdx.create_room_s2c];
            return (create_room_s2c)await source.Task;
        }

        public static async UniTask<join_room_s2c> JoinRoom(string roomName)
        {
            Socket.Connect();

            Dispacher.SendMsg(new join_room_c2s
            {
                Name = roomName,
                Player = new OnlineGame.Player
                {
                    Name = Main.Instance.me.name,
                    Cards = { Main.Instance.me.selectCardIdxs }
                }
            });
            var source = tasks[ProtoIdx.join_room_s2c];
            return (join_room_s2c)await source.Task;
        }

        public static async UniTask<join_room_s2c> WaitJoinRoom()
        {
            Debug.Log("等待加入房间");
            var source = tasks[ProtoIdx.join_room_s2c];
            return (join_room_s2c)await source.Task;
        }

        public static void Response(IMessage msg)
        {
            if (!tasks.TryGetValue(Dispacher.GetProtoIdx(msg), out var source))
            {
                throw new Exception("收到未知消息");
            }

            source.TrySetResult(msg);
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