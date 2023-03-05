using System;
using UnityEngine;

namespace Oduncu.Events
{
    public class BossSpawned : BaseEvent<BossSpawned.Args>
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