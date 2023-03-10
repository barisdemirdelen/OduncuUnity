using System;
using UnityEngine;

namespace Oduncu.Events
{
    public class TreeKilled : BaseEvent<TreeKilled.Args>
    {
        public class Args : EventArgs
        {
            public readonly GameObject gameObject;
            
            public Args(GameObject gameObject)
            {
                this.gameObject = gameObject;

            }
        }
    }
}