using UnityEngine;

namespace Arknights
{
    public  static class Helper
    {
        public static Vector3 Fgui2Screen(Vector2 v)
        {
            return new Vector3(v.x, Screen.height - v.y, 0);
        }
    }
}