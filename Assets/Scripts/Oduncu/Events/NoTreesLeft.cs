using System;

namespace Oduncu.Events
{
    public class NoTreesLeft : BaseEvent<NoTreesLeft.Args>
    {

        public class Args : EventArgs
        {

        }
    }
}