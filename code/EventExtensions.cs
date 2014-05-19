using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System
{
    [DebuggerStepThrough]
    public static class EventExtensions
    {

        public static void RaiseEvent<T>(this EventHandler<T> handler, object sender, T arguments) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, arguments);
            }
        }

        public static void RaiseEvent(this PropertyChangedEventHandler handler, object sender, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void RaiseEvent(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }
    }
}
