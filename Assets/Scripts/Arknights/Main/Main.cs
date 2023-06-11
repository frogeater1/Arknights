using System;
using UnityEngine;

namespace Arknights
{
    public class Main : Singleton<Main>
    {
        public Character[] characterPrefabs;
        
        
        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
        }
    }
}