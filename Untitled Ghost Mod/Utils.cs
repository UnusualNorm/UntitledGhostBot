using System;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace Untitled_Ghost_Mod
{
    internal class UGMUtils
    {
        public static Value GetPrivateValue<Value, Instance>(string name, Instance __instance)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var type = typeof(Instance).GetField(name, flags);
            return (Value)type.GetValue(__instance);
        }

        public static T FindComponentWithName<T>(string name) where T : UnityEngine.Object
        {
            var components = UnityEngine.Object.FindObjectsOfType<T>(true);
            return Array.Find(components, component => component.name == name);
        }
    }
}
