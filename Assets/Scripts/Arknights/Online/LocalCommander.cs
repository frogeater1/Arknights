﻿using OnlineGame;
using UnityEngine;

namespace Arknights
{
    public static class LocalCommander
    {
        public static void Enter(int rpcFrom, Command_Enter command)
        {
            Character character = Game.Instance.CharacterManager.characters[rpcFrom - 1][command.CardIdx];
            character.FixedPos(command.LogicPosX, command.LogicPosY);
            character.ChangeDirection((方向)command.Direction);
            character.Enter();
        }

        public static void Exit(int rpcFrom, Command_Exit command)
        {
            Character character = Game.Instance.CharacterManager.characters[rpcFrom - 1][command.CardIdx];
            character.Exit();
        }

        public static void Skill(int rpcFrom, Command_Skill command)
        {
            Character character = Game.Instance.CharacterManager.characters[rpcFrom - 1][command.CardIdx];
            character.Skill();
        }
    }
}