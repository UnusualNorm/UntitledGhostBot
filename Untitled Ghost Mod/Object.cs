using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Untitled_Ghost_Mod
{
    internal class UGMObject : MonoBehaviour
    {
        public float maxDifficulty;

        public void OverwriteDifficultySlider(bool multiplayer)
        {
            Melon<UGM>.Logger.Msg("Overwriting difficulty slider...");
            /*var difficultyObject = GameObject.Find("Difficulty");
            var difficultySlider = difficultyObject.GetComponent<Slider>();
            difficultySlider.maxValue = maxDifficulty;*/
            // Screw you, Unity
            var slider = UGMUtils.OverlyComplexFind("Difficulty")?.GetComponent<Slider>();
            if (!slider)
            {
                Melon<UGM>.Logger.Warning("Could not find difficulty slider...");
                return;
            }

            slider.maxValue = maxDifficulty;
            Melon<UGM>.Logger.Msg("Overwrote difficulty slider!");
        }

        public void OverwriteTips()
        {
            Melon<UGM>.Logger.Msg("Overwriting tips tips...");
            string[] myTips = { "Give me my life back.", "Honestly, it's probably a [redacted]", "What do you call it when a ghost gets out of bounds? A bug." };
            var tips = UGMUtils.OverlyComplexFind("Tips")?.GetComponent<Tips>();
            if (!tips)
            {
                Melon<UGM>.Logger.Warning("Could not find tips tips...");
                return;
            }

            tips.tips = myTips;
            Melon<UGM>.Logger.Msg("Overwrote tips tips!");
        }

        public void Start()
        {
            var scene = SceneManager.GetActiveScene();
            Melon<UGM>.Logger.Msg($"UGM object has loaded: {scene.buildIndex} {scene.name}");
            switch (scene.buildIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        OverwriteDifficultySlider(false);
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        OverwriteDifficultySlider(true);
                        OverwriteTips();
                        break;
                    }
            }
        }
    }
}
