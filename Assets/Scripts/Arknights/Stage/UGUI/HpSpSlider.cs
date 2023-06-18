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

        public void Update()
        {
            if (character.当前部署类型 == 部署类型.地面)
            {
                Follow();
            }

            slider.value = (float)character.curHp / character.maxHp;
        }

        public void Follow()
        {
            var offset = character.当前部署类型 == 部署类型.高台 ? new Vector2(0, -28) : new Vector2(0, -20);

            Vector2 pos = Game.Instance.CameraManager.mainCamera.WorldToScreenPoint(character.transform.position);
            transform.position = pos + offset;
        }

        public void Init(Character character)
        {
            this.character = character;
            slider = GetComponent<Slider>();
            Follow();
        }
    }
}