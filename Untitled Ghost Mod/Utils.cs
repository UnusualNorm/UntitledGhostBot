using UnityEngine;
using UnityEngine.SceneManagement;

namespace Untitled_Ghost_Mod
{
    internal class UGMUtils
    {
        private static GameObject OverlyComplexFindNest(GameObject child, string name)
        {
            var childCount = child.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var childChild = child.transform.GetChild(i).gameObject;
                if (childChild.name == name) return childChild;
                var foundChild = OverlyComplexFindNest(child, name);
                if (!foundChild) continue;
                return foundChild;
            }

            return null;
        }

        public static GameObject OverlyComplexFind(string name)
        {
            var scene = SceneManager.GetActiveScene();
            var children = scene.GetRootGameObjects();
            foreach (var child in children)
            {
                if (child.name == name) return child;
                var foundChild = OverlyComplexFindNest(child, name);
                if (!foundChild) continue;
                return foundChild;
            }

            return null;
        }
    }
}
