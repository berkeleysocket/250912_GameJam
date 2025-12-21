using UnityEngine;

namespace _Scripts.Core.Utility
{
    public static class Logging
    {
        public static void Log(object msg)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log(msg);
#endif
        }

        public static void LogWarning(object msg)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogWarning(msg);
#endif
        }

        public static void LogError(object msg)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError(msg);
#endif
        }
    }
}