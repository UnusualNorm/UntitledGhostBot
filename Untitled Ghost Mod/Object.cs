using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

namespace Untitled_Ghost_Mod
{
    internal class UGMObject : MonoBehaviour
    {
        public float maxDifficulty;

        private void OverwriteDifficultySlider(bool multiplayer)
        {
            Melon<UGM>.Logger.Msg("Overwriting difficulty slider...");
            /*var difficultyObject = GameObject.Find("Difficulty");
            var difficultySlider = difficultyObject.GetComponent<Slider>();
            difficultySlider.maxValue = maxDifficulty;*/
            // Screw you, Unity
            var slider = UGMUtils.FindComponentWithName<Slider>("Difficulty");
            if (!slider)
            {
                Melon<UGM>.Logger.Warning("Could not find difficulty slider...");
                return;
            }

            slider.maxValue = maxDifficulty;
            Melon<UGM>.Logger.Msg("Overwrote difficulty slider!");
        }

        private void OverwriteTips()
        {
            Melon<UGM>.Logger.Msg("Overwriting tips tips...");
            string[] myTips = {
                "Give me my life back.",
                "Honestly, it's probably... [redacted]",
                "What do you call it when a ghost gets out of bounds? A bug.",
                "Hot diggity dang batman!",
                "I once sat on my keyboard.",
                "Woweewoweeowwweeowwweewwwwooowwwweeee",
                "Hummina hummina awooga!",
                "The devs said they're adding a sex mod!",
                "STOP POSTING ABOUT AMONG US! I'M TIRED OF SEEING IT. MY FRIENDS ON TIKTOK SEND MEMES, ON DISCORD IT'S [redacted] MEMES. I WAS IN A SERVER, RIGHT, AND ALL OF THE CHANNELS ARE JUST AMONG US STUFF. I SHOVED MY CHAMPION UNDERWEAR TO MY GIRLFRIEND AND THE LOGO I FLIPPED IT AND I SAID \"HEY BABE, WHEN THE UNDERWEAR SUS HAHA DING DING DING DING DING DIGN DING, DIDIDING\". I [redacted] LOOKED AT A TRASHCAN I SAID \"THAT'S A BIT SUSSY\". I LOOKED AT MY [redacted], I THINK OF THE ASTRONAUT'S HELMET AND I GO \"[redacted]? MORE LIKE [slightly-less redacted]!\" AAAAAAAAAAAAAAAAA",
                "Remember to restock on toilet paper!",
                "Have I shown my age yet? Take a wild guess...",
                "Hey baby, you come here often?",
                "I think these tips have become a reddit copypasta...",
                "*God has joined the chat*",
                "My friend stuck his 6th finger in a pencil sharpener once.",
                "How to GameDev? Where is google? Google, Why isn't Google working? Is Google down?",
                "UwU what's this?",
                "Breaking News: Local man takes a dump in a toaster, says it let's off a nice fragrance.",
                "Listen, between you and me; I think today's gonna be one of the days of your life.",
                "Anime mode coming soon!",
                "Master chief, mind telling me what you're doing on that wall?",
                "Fun Fact: I browse r/copypasta whenever I come up with tips!",
                "Gimme gimme chicken tendies.",
                "Sugar is the sustainance of life. Give me more please.",
                "You see, this is why I don't like video games, it appeals to the male fantasy.",
                "I AM, Iron Man!",
                "Is it pronounced Gif, or Gif?",
                "We don't talk about Paul."
            };
            var tips = UGMUtils.FindComponentWithName<Tips>("Tips");
            if (!tips)
            {
                Melon<UGM>.Logger.Warning("Could not find tips tips...");
                return;
            }

            tips.tips = myTips;
            Melon<UGM>.Logger.Msg("Overwrote tips tips!");
        }

        private void OnFreePointsButtonClicked()
        {
            Application.OpenURL("https://www.youtube.com/watch?v=v47zSNZcyPQ");
        }

        private void OverwriteFreePointsButton()
        {
            Melon<UGM>.Logger.Msg("Overwriting free points button...");
            var button = UGMUtils.FindComponentWithName<Button>("Free points");
            if (!button)
            {
                Melon<UGM>.Logger.Warning("Could not find free points button...");
                return;
            }

            button.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);
            button.onClick.AddListener(OnFreePointsButtonClicked);
            Melon<UGM>.Logger.Msg("Overwrote free points button!");
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
                        OverwriteFreePointsButton();
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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Melon<UGM>.Logger.Msg("Toggling ULTRA-FLASHLIGHT...");
                var children = SceneManager.GetActiveScene().GetRootGameObjects();
                var shadow = Array.Find(children, child => child.name == "Shadow");
                if (!shadow)
                {
                    Melon<UGM>.Logger.Warning("Could not shadows...");
                    return;
                }

                shadow.SetActive(!shadow.activeSelf);
                Melon<UGM>.Logger.Msg("Toggled ULTRA-FLASHLIGHT!");
            }
        }
    }
}
