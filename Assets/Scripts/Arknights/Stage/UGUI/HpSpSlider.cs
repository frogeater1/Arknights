using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arknights.UGUI
{
    public class HpSpSlider : MonoBehaviour
    {
        public Unit unit;
        private Slider slider;
        private Image imageFill;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            imageFill = slider.fillRect.GetComponent<Image>();
        }

        public void Update()
        {
            if (unit.当前部署类型 == 部署类型.地面)
            {
                Follow();
            }

            slider.value = (float)unit.curHp / unit.maxHp;
        }

        public void Follow()
        {
            var offset = unit.当前部署类型 == 部署类型.高台 ? new Vector2(0, -28) : new Vector2(0, -20);

            Vector2 pos = Game.Instance.CameraManager.mainCamera.WorldToScreenPoint(unit.transform.position);
            transform.position = pos + offset;
        }

        public void Init(Unit unit)
        {
            this.unit = unit;
            if (unit.player.team == Team.Neutral)
            {
                imageFill.color = Color.cyan;
            }
            else if (unit.player == Game.Instance.me)
            {
                imageFill.color = Color.green;
            }
            else
            {
                imageFill.color = Color.red;
            }

            Follow();
        }
    }
}