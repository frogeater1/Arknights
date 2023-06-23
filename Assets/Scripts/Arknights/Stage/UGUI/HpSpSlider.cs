using UnityEngine;
using UnityEngine.UI;

namespace Arknights.UGUI
{
    public abstract class HpSpSlider : MonoBehaviour
    {
        public Unit unit;
        protected Slider slider;
        protected Image imageFill;

        public abstract void Init(Unit unit);
    }
}