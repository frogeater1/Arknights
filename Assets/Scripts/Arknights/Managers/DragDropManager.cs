using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FairyGUI;
using UnityEngine;

namespace Arknights
{
     /// <summary>
     /// Helper for drag and drop.
     /// 这是一个提供特殊拖放功能的功能类。与GObject.draggable不同，拖动开始后，他使用一个替代的图标作为拖动对象。
     /// 当玩家释放鼠标/手指，目标组件会发出一个onDrop事件。
     /// </summary>
     public class DragDropManager
     {
         private GLoader _agent;
         private object _sourceData;
         private GObject _source;

         private static DragDropManager _inst;

         public static DragDropManager inst
         {
             get
             {
                 if (_inst == null)
                     _inst = new DragDropManager();
                 return _inst;
             }
         }

         public DragDropManager()
         {
             _agent = (GLoader)UIObjectFactory.NewObject(ObjectType.Loader);
             _agent.gameObjectName = "DragDropAgent";
             _agent.SetHome(Game.Instance.ui_battle);
             _agent.touchable = false; //important
             _agent.draggable = true;
             _agent.SetSize(0, 0);
             _agent.SetPivot(0.5f, 0.5f, true);
             _agent.align = AlignType.Center;
             _agent.verticalAlign = VertAlignType.Bottom;
             _agent.sortingOrder = 1;
             // _agent.onDragEnd.Add(__dragEnd);
             // _agent.onDragMove.Add(__dragMove);
         }

         /// <summary>
         /// Loader object for real dragging.
         /// 用于实际拖动的Loader对象。你可以根据实际情况设置loader的大小，对齐等。
         /// </summary>
         public GLoader dragAgent
         {
             get { return _agent; }
         }

         /// <summary>
         /// Is dragging?
         /// 返回当前是否正在拖动。
         /// </summary>
         public bool dragging
         {
             get { return _agent.parent != null; }
         }

         /// <summary>
         /// Start dragging.
         /// 开始拖动。
         /// </summary>
         /// <param name="source">Source object. This is the object which initiated the dragging.</param>
         /// <param name="icon">Icon to be used as the dragging sign.</param>
         /// <param name="sourceData">Custom data. You can get it in the onDrop event data.</param>
         /// <param name="touchPointID">Copy the touchId from InputEvent to here, if has one.</param>
         public void StartDrag(GObject source, string icon, object sourceData, int touchPointID = -1)
         {
             if (_agent.parent != null)
                 return;

             _sourceData = sourceData;
             _source = source;
             _agent.url = icon;
             GRoot.inst.AddChild(_agent);
             _agent.xy = GRoot.inst.GlobalToLocal(Stage.inst.GetTouchPosition(touchPointID));
             _agent.StartDrag(touchPointID);
         }

         /// <summary>
         /// Cancel dragging.
         /// 取消拖动。
         /// </summary>
         public void Cancel()
         {
             if (_agent.parent != null)
             {
                 _agent.StopDrag();
                 GRoot.inst.RemoveChild(_agent);
                 _sourceData = null;
             }
         }

         private void __dragMove(EventContext evt)
         {
             // evt.PreventDefault();
             // if (_source is UI_Button_角色卡)
             // {
             //     var button = (UI_Button_角色卡)_source;
             //     var ray = Game.Instance.CameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
             //     Physics.Raycast(ray, out var hit);
             //     Vector3 pos = Game.Instance.CameraManager.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.distance));
             //     //修正生成位置为格子正中间
             //     var can_set = false;
             //     if (pos.y < 10) //用这个判断是否打到可部署范围内，因为非可部署范围没有collider,会返回相机附近的位置。
             //     {
             //         var character = button.character;
             //         var setType = character.loadData.部署类型;
             //         var fixed_x = Mathf.FloorToInt(pos.x);
             //         var fixed_z = Mathf.FloorToInt(pos.z);
             //         var pos_fixed = new Vector3(fixed_x + 0.5f, 0, fixed_z + 0.2f);
             //         var grid_type = Map.Instance.GetGridType(fixed_x, fixed_z);
             //         if ((setType == 部署类型.地面 && grid_type == GridType.站人地面)
             //             || (setType == 部署类型.高台 && grid_type == GridType.站人高台)
             //             || (setType == 部署类型.Both && (grid_type == GridType.站人地面 || grid_type == GridType.站人高台)))
             //         {
             //             dragAgent.visible = true;
             //             character.transform.position = pos_fixed;
             //             can_set = true;
             //             Map.Instance.ShowAttackRange(character);
             //         }
             //     }
             //     ;
             //     if (!can_set)
             //     {
             //         dragAgent.visible = true;
             //         button.character.transform.position = new Vector3(1000, 0, 0);
             //         Map.Instance.HideAttackRange();
             //     }
             // }
         }

         private void __dragEnd(EventContext evt)
         {
             // if (_agent.parent == null) //cancelled
             //     return;
             // GRoot.inst.RemoveChild(_agent);
             //
             // object sourceData = _sourceData;
             // GObject source = _source;
             // _sourceData = null;
             // _source = null;
             //
             // if (source is UI_Button_角色卡)
             // {
             //     Game.Instance.ui_directionSelect.Show(((UI_Button_角色卡)source).character);
             // }
         }
     }
}