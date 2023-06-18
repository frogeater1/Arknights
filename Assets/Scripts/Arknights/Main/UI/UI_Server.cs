﻿using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Arknights
{
    public partial class UI_Server
    {
        partial void Init()
        {
            m_tip.visible = false;
            m_create.onClick.Set(() => OnCreate().Forget());
            m_join.onClick.Set(() => OnJoin().Forget());
            m_cancel.onClick.Set(OnCancel);
        }


        private async UniTaskVoid OnCreate()
        {
            if (m_roomname.text == "")
            {
                m_tip.visible = true;
                return;
            }

            m_tip.visible = true;

            m_state.selectedPage = RoomState.create_waiting.ToString();

            var msg = await Request.CreateRoom(m_roomname.text);
            var res_code = (ResCode)msg.ResCode;
            switch (res_code)
            {
                case ResCode.Success:
                    OnCreateRoomSuccess();
                    var msg1 = await Request.WaitJoinRoom();
                    if ((ResCode)msg1.ResCode != ResCode.Success)
                        throw new NotImplementedException("这里不该收到非成功的消息");
                    OnJoinRoomSuccess(msg1.Player);
                    break;
                case ResCode.DuplicateName:
                    OnCreateRoomFail("房间名重复");
                    Socket.Disconnect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async UniTaskVoid OnJoin()
        {
            m_state.selectedPage = RoomState.guest_waiting.ToString();

            var msg = await Request.JoinRoom(m_roomname.text);
            var res_code = (ResCode)msg.ResCode;
            switch (res_code)
            {
                case ResCode.Success:
                    OnJoinRoomSuccess(msg.Player);
                    break;
                case ResCode.CantFindRoom:
                    OnJoinRoomFail("找不到该房间");
                    Socket.Disconnect();
                    break;
                case ResCode.RoomIsFull:
                    OnJoinRoomFail("房间已满");
                    Socket.Disconnect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnCancel(EventContext context)
        {
            Enum.TryParse<RoomState>(m_state.selectedPage, out var state);
            switch (state)
            {
                case RoomState.create_waiting:
                    Request.CancelCreateRoom();
                    Socket.Disconnect();
                    break;
                case RoomState.guest_waiting:
                    Request.CancelJoinRoom();
                    Socket.Disconnect();
                    break;
                case RoomState.host_waiting:
                    Socket.Disconnect();
                    break;
            }

            m_state.selectedPage = RoomState.start.ToString();
            m_tip.visible = false;
        }

        public void OnCreateRoomSuccess()
        {
            m_state.selectedPage = RoomState.host_waiting.ToString();
        }

        public void OnCreateRoomFail(string tip)
        {
            m_state.selectedPage = RoomState.start.ToString();
            m_tip.text = tip;
            m_tip.visible = true;
        }

        public void OnJoinRoomSuccess(OnlineGame.Player playerData)
        {
            var player = new Player
            {
                team =  Team.Red,
                selectCardIdxs = playerData.Cards.ToList(),
            };
            
            m_state.selectedPage = RoomState.start.ToString();
            m_tip.visible = false;
            Main.Instance.ui_online_window.Hide();
            
            Parking.StartBattle(Main.Instance.me, player);
        }

        public void OnJoinRoomFail(string tip)
        {
            m_state.selectedPage = RoomState.start.ToString();
            m_tip.text = tip;
            m_tip.visible = true;
        }
    }
}