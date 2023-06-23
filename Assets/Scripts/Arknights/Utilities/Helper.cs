using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Arknights
{
    public static class Helper
    {
        public static float CalculateValue(float origin, ValueType type, float value)
        {
            return type switch
            {
                ValueType.Fixed => origin + value,
                ValueType.Percent => origin * value,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        #region 打印

        public static void PrintGreen(params object[] objs)
        {
            var s = "";
            foreach (object t in objs)
            {
                s += t.ToString();
            }

            Debug.Log("<color=#00ff00ff>\t" + s + "\t</color>");
        }

        public static void PrintPurple(params object[] objs)
        {
            var s = "";
            foreach (object t in objs)
            {
                s += t.ToString();
            }

            Debug.Log("<color=#ff00ffff>\t" + s + "\t</color>");
        }

        public static void PrintBlue(params object[] objs)
        {
            var s = "";
            foreach (object t in objs)
            {
                s += t.ToString();
            }

            Debug.Log("<color=#0000ffff>\t" + s + "\t</color>");
        }

        public static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        #endregion
    }
}