using UnityEngine;
using System.Diagnostics;

namespace Ksy.Utility
{
    public static class DebugX
    {
        [Conditional("DEBUG_MODE")]
        public static void Log(string text)
        {
            UnityEngine.Debug.Log(text);
        }
        [Conditional("DEBUG_MODE")]
        public static void Log(string text, Color color)
        {
            string hex = ColorUtility.ToHtmlStringRGBA(color);
            UnityEngine.Debug.Log($"<color=#{hex}>{text}</color>");
        }
        [Conditional("DEBUG_MODE")]
        public static void Log(string text, Color color, bool isBold)
        {
            string hex = ColorUtility.ToHtmlStringRGBA(color);

            if(isBold)
                UnityEngine.Debug.Log($"<color=#{hex}><b>{text}</b></color>");
            else if(!isBold)
                UnityEngine.Debug.Log($"<color=#{hex}>{text}</color>");
        }
        [Conditional("DEBUG_MODE")]
        public static void Log(string text, Color color, bool isBold, float size)
        {
            string hex = ColorUtility.ToHtmlStringRGBA(color);

            if(isBold)
                UnityEngine.Debug.Log($"<color=#{hex}><b><size={size}>{text}</size></b></color>");
            else if(!isBold)
                UnityEngine.Debug.Log($"<color=#{hex}><size={size}>{text}</size></color>");
        }
        [Conditional("DEBUG_MODE")]
        public static void LogWarning(string text)
        {
            UnityEngine.Debug.LogWarning(text);
        }
        [Conditional("DEBUG_MODE")]
        public static void LogError(string text)
        {
            UnityEngine.Debug.LogError(text);
        }
        [Conditional("DEBUG_MODE")]
        public static void Assert(bool condition, string message)
        {
            UnityEngine.Debug.Assert(condition, message);
        }
        [Conditional("DEBUG_MODE")]
        public static void Assert(bool condition, string message, bool isBlock)
        {
            if(!isBlock && !condition)
                Log(message, Color.red);
            else if(!condition)
                UnityEngine.Debug.Assert(condition,message);
        }
    }
}
