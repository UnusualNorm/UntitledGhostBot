using MelonLoader;
using System;
using UnityEngine;

namespace Untitled_Ghost_Mod
{
    internal class UGMUtils
    {
        public static T FindComponentWithName<T>(string name) where T : UnityEngine.Object
        {
            var components = UnityEngine.Object.FindObjectsOfType<T>(true);
            return Array.Find(components, component => component.name == name);
        }
    }
}
