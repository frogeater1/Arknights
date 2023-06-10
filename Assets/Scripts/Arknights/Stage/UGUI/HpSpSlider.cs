using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arknights.UGUI
{
    public class HpSpSlider : MonoBehaviour
    {
        public Character character;
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void Refresh()
        {
            if (character.当前部署类型 == 部署类型.地面)
            {
                Follow();
            }

            slider.value = character.curHp / character.maxHp;
        }

        public void Follow()
        {
            Vector2 pos = Game.Instance.CameraManager.mainCamera.WorldToScreenPoint(character.transform.position);
            transform.position = pos + new Vector2(0, -16);
        }

        public void Init(Character character)
        {
            this.character = character;
            slider = GetComponent<Slider>();
            Follow();
            Refresh();
        }
    }
}