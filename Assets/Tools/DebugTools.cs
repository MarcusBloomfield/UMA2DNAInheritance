using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace marcus
{
    public static class DebugTools 
    {
        public static void Log(string debug, Object context, bool enableDebug)
        {
            if (enableDebug) Debug.Log(debug, context);
        }
        public static void Log(string debug, bool enableDebug)
        {
            if (enableDebug) Debug.Log(debug);
        }
    }
}
