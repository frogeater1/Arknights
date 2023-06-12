using System;
using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_Loading
    {
        partial void Init()
        {
            Main.Instance.ui_loading = this;
            m_stage.visible = false;
        }

        public void Fade()
        {
            m_curtain.TweenFade(1, 0.5f).OnComplete(() =>
            {
                m_stage.visible = true;
                m_curtain.TweenFade(0, 0.2f);
            });
        }
    }
}