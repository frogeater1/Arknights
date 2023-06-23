using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using OnlineGame;
using UnityEngine;

namespace Arknights
{
    public class Commander : MonoBehaviour
    {
        public static void Enter(Character character)
        {
            Debug.Log("Enter");
            var rpcMsg = new RpcMsg
            {
                From = character.player.playerId,
                Command = Any.Pack(new Command_Enter
                {
                    CardIdx = character.cardListIdx,
                    LogicPosX = character.logicPos.x,
                    LogicPosY = character.logicPos.y,
                    Direction = (Direction)character.attackDir,
                })
            };
#if OUTLINE_TEST
            Dispacher.rpcMsgs.Enqueue(rpcMsg);
#else
            Dispacher.SendMsg(rpcMsg);
#endif
        }

        public static void Exit(Character character)
        {
            Debug.Log("Exit");
            var rpcMsg = new RpcMsg
            {
                From = character.player.playerId,
                Command = Any.Pack(new Command_Exit
                {
                    CardIdx = character.cardListIdx,
                })
            };
#if OUTLINE_TEST
            Dispacher.rpcMsgs.Enqueue(rpcMsg);
#else
            Dispacher.SendMsg(rpcMsg);
#endif
        }

        public static void Skill(Character character)
        {
            Debug.Log("Skill");
            var rpcMsg = new RpcMsg
            {
                From = character.player.playerId,
                Command = Any.Pack(new Command_Skill
                {
                    CardIdx = character.cardListIdx,
                })
            };
#if OUTLINE_TEST
            Dispacher.rpcMsgs.Enqueue(rpcMsg);
#else
            Dispacher.SendMsg(rpcMsg);
#endif
        }
    }
}