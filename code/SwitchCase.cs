using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{
    [DebuggerStepThrough]
    public class SwitchCase : Dictionary<string, Action>
    {
        public void Eval(string key)
        {
            if (ContainsKey(key))
                this[key]();
            else
                this["default"]();
        }
    }

}
