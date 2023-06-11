using System;
using System.Collections;
using System.Collections.Generic;
using Arknights;
using FairyGUI;
using UnityEngine;

public class UIMain : MonoBehaviour
{
   [SerializeField]
   private UIPanel Background;
   [SerializeField]
   private UIPanel Menu1;
   [SerializeField]
   private UIPanel Menu2;
   
   private GComponent ui_bg;
   private GComponent ui_menu1;
   private GComponent ui_menu2;

   private void Start()
   {
      ui_bg = Background.ui;
      ui_menu1 = Menu1.ui;
      ui_menu2 = Menu2.ui;

      GButton terminal = ui_menu1.GetChild("Terminal").asButton;
      terminal.onClick.Add(() =>
      {
         //todo: 进入Team界面选人
         Parking.StartBattle(Main.Instance.characterPrefabs);
      });
   }
   
}
