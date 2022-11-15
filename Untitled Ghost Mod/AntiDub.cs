using MelonLoader;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using HarmonyLib;

namespace Untitled_Ghost_Mod
{
    /*[HarmonyPatch(typeof(BookUI), "Start")]
    static class CorrectGhostChoicePatch
    {
        private static void Postfix(BookUI __instance)
        {
            Melon<UGM>.Logger.Msg("Patching correct ghost choice...");
            var eai = GameObject.FindWithTag("Ghost").GetComponent<EnemyAI>();
            // No idea if I NEED to use .ToLower(), but it doesn't hurt to be safe...
            var ghostI = __instance.guessDropdown.options.FindIndex(type => type.text.ToLower() == eai.currghosttype.ToLower());
            __instance.guessDropdown.value = ghostI;
        }
    }*/

    /*[HarmonyPatch(typeof(Flashlight), "Update")]
    static class UltraFlashlightPatch
    {
        private static void Postfix()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Melon<UGM>.Logger.Msg("Toggling ULTRA-FLASHLIGHT...");
                var children = SceneManager.GetActiveScene().GetRootGameObjects();
                var shadow = Array.Find(children, child => child.name == "Shadow");
                if (!shadow)
                {
                    Melon<UGM>.Logger.Warning("Could not find shadow...");
                    return;
                }

                shadow.SetActive(!shadow.activeSelf);
            }
        }
    }*/
}
