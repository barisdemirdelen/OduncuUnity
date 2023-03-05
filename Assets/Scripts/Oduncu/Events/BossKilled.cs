using System;
using UnityEngine;

namespace Oduncu.Events
{
    public class BossKilled : BaseEvent<BossKilled.Args>
    {
        public class Args : EventArgs
        {
            public GameObject GameObject;
            
            public Args(GameObject gameObject)
            {
                GameObject = gameObject;

            }
        }
    }
}