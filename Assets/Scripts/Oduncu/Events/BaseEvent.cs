using System;

namespace Oduncu.Events
{
    public class BaseEvent<T> where T : EventArgs
    {   
        public delegate void EventAction(object sender, T e);
        private static event EventAction OnEvent;
        
        public static void Subscribe(EventAction method)
        {
            OnEvent += method;
        }
        
        public static void Unsubscribe(EventAction method)
        {
            OnEvent -= method;
        }
        
        public static void Invoke(object sender, T e)
        {
            OnEvent.Invoke(sender, e);
        }
    }
}