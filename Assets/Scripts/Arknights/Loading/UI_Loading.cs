using System;
using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace Arknights.Loading
{
    public class UILoading : MonoBehaviour
    {
        private GComponent ui;
        [NonSerialized]
        public GGroup stage;
        private GGraph curtain;
        
        private void Start()
        {
            ui = GetComponent<UIPanel>().ui;
            curtain = ui.GetChild("curtain").asGraph;
            stage = ui.GetChild("stage").asGroup;
            stage.visible = false;
        }

        public void  Fade()
        {
            curtain.TweenFade(1, 0.5f).OnComplete(() =>
            {
                stage.visible = true;
                curtain.TweenFade(0, 0.2f);
            });
        }
    
    }
}