using HarmonyLib;
using Discord;
using MelonLoader;
using System;
using System.Diagnostics;
using TMPro;
using UnityEngine.Windows.Speech;
using UnityEngine;
using Random = UnityEngine.Random;
using static Untitled_Ghost_Mod.UGMUtils;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Untitled_Ghost_Mod
{
    [HarmonyPatch(typeof(Discordlolplswork), "Start")]
    static class DiscordActivityPatch
    {
        private static void Postfix(Discordlolplswork __instance)
        {
            Melon<UGM>.Logger.Msg("Patching Discord Activity data...");
            var discord = GetPrivateValue<Discord.Discord, Discordlolplswork>("discord", __instance);

            if (Process.GetProcessesByName("Discord").Length != 0)
            {
                discord.GetActivityManager().UpdateActivity(new Activity
                {
                    Name = "Untitled Ghost GMGMGMGMGMGMod",
                    Details = "Hunting a god-forsaken poopity-scoop willy-wolly peepee-poopoo epic-ness high-tax spooky-wooky dommy-mommy hobby-lobby dumb ghost!",
                    State = "Download: https://dubscr.itch.io/ugg or, for a better experience: https://github.com/UnusualNorm/UntitledGhostMod",
                    Assets = new ActivityAssets
                    {
                        LargeImage = "logo",
                        LargeText = "Boo-hoo my wittle ghost game got modded!",
                    },
                    Timestamps = new ActivityTimestamps
                    {
                        Start = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                    }
                }, delegate (Result res)
                {
                    Melon<UGM>.Logger.Msg("Patched Discord Activity data!");
                    return;
                });
            }
        }
    }

    [HarmonyPatch(typeof(ConnectToServer), "OnClickConnect")]
    static class UsernameChangePatch
    {
        private static bool hasAttempted = false;

        private static void Prefix(ConnectToServer __instance)
        {
            if (!PlayerPrefs.HasKey("Nickname")) return;
            Melon<UGM>.Logger.Msg("Patching username change blocker...");
            if (PlayerPrefs.GetString("Nickname", "") == __instance.usernameInput.text) return;

            if (!hasAttempted)
            {
                __instance.buttonText.text = "Changing username... Are you sure?";
                hasAttempted = true;
                return;
            }

            PlayerPrefs.DeleteKey("Nickname");
            hasAttempted = false;
            Melon<UGM>.Logger.Msg("Patched username change blocker!");
        }
    }

    [HarmonyPatch(typeof(BookUI), "Start")]
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
    }

    [HarmonyPatch(typeof(LuigiBoard), "Start")]
    static class ExtraLuigiBoardPatch
    {
        private static void Postfix(LuigiBoard __instance)
        {
            Melon<UGM>.Logger.Msg("Patching extra Luigi Board lines...");
            var text = GetPrivateValue<TMP_Text, LuigiBoard>("text", __instance);
            var AS = GetPrivateValue<AudioSource, LuigiBoard>("AS", __instance);

            var eai = GameObject.FindWithTag("Ghost").GetComponent<EnemyAI>();
            var kr = new KeywordRecognizer(new string[] { "Who is joe" });
            kr.OnPhraseRecognized += (PhraseRecognizedEventArgs speech) =>
            {
                Melon<UGM>.Logger.Msg($"Recognized extra Luigi Board line: {speech.text}");
                text.text = "What the fuck did you just fucking say about me, you little bitch? I'll have you know I graduated top of my class in the Navy Seals, and I've been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I'm the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You're fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that's just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little \"clever\" comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn't, you didn't, and now you're paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You're fucking dead, kiddo.";
                eai.hunting = true;
                AS.clip = (AudioClip)Resources.Load("Ghost/moan" + Random.Range(1, 5).ToString());
                AS.Play();
                return;
            };
        }
    }

    [HarmonyPatch(typeof(Tips), "Start")]
    static class ExtraTipsPatch
    {
        private static void Postfix(Tips __instance)
        {
            Melon<UGM>.Logger.Msg("Patching extra tips...");
            string[] myTips = {
                "Give me my life back.",
                "Honestly, it's probably... [redacted]",
                "What do you call it when a ghost gets out of bounds? A bug.",
                "Hot diggity dang batman!",
                "I once sat on my keyboard.",
                "Woweewoweeowwweeowwweewwwwooowwwweeee",
                "Hummina hummina awooga!",
                "The devs said they're adding a sex mod!",
                "STOP POSTING ABOUT AMONG US! I'M TIRED OF SEEING IT. MY FRIENDS ON TIKTOK SEND MEMES, ON DISCORD IT'S FUCKING MEMES. I WAS IN A SERVER, RIGHT, AND ALL OF THE CHANNELS ARE JUST AMONG US STUFF. I SHOVED MY CHAMPION UNDERWEAR TO MY GIRLFRIEND AND THE LOGO I FLIPPED IT AND I SAID \"HEY BABE, WHEN THE UNDERWEAR SUS HAHA DING DING DING DING DING DIGN DING, DIDIDING\". I FUCKING LOOKED AT A TRASHCAN I SAID \"THAT'S A BIT SUSSY\". I LOOKED AT MY PENIS, I THINK OF THE ASTRONAUT'S HELMET AND I GO \"PENIS? MORE LIKE PENSUS!\" AAAAAAAAAAAAAAAAA",
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

            __instance.tips = myTips;
        }
    }

    [HarmonyPatch(typeof(MainOptions), "Start")]
    static class FreePointsButtonPatch
    {
        private static void OnFreePointsButtonClicked()
        {
            Application.OpenURL("https://www.youtube.com/watch?v=v47zSNZcyPQ");
        }

        private static void Postfix(MainOptions __instance)
        {
            Melon<UGM>.Logger.Msg("Patching free points button...");
            var buttons = __instance.optionsMenu.GetComponentsInChildren<Button>(true);
            var button = Array.Find(buttons, potButton => potButton.name == "Free points");
            if (!button)
            {
                Melon<UGM>.Logger.Warning("Could not find free points button...");
                return;
            }

            button.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);
            button.onClick.AddListener(OnFreePointsButtonClicked);
        }
    }

    [HarmonyPatch(typeof(MainOptions), "Start")]
    static class DifficultySliderPatch
    {

        private static void Postfix(MainOptions __instance)
        {
            Melon<UGM>.Logger.Msg("Patching difficulty slider...");
            // Well, I wish I could optimize this... I can't :(
            var sliders = __instance.GetComponentsInChildren<Slider>(true);
            var slider = Array.Find(sliders, potButton => potButton.name == "Difficulty");
            if (!slider)
            {
                Melon<UGM>.Logger.Warning("Could not difficulty slider...");
                return;
            }

            var maxDifficulty = Melon<UGM>.Instance.maxDifficulty;
            slider.maxValue = maxDifficulty.Value;
        }
    }

    [HarmonyPatch(typeof(Movement), "Awake")]
    static class CameraPatch
    {
        private static void Postfix(Movement __instance)
        {
            Melon<UGM>.Logger.Msg("Patching camera...");
            var cameraSizeMultiplier = Melon<UGM>.Instance.cameraSizeMultiplier;
            var cam = GetPrivateValue<Camera, Movement>("cam", __instance);
            cam.orthographicSize *= cameraSizeMultiplier.Value;
        }
    }

    [HarmonyPatch(typeof(Movement), "Update")]
    static class SmoothCameraPatch
    {
        private static Vector3 prevCam;

        private static void Postfix(Movement __instance)
        {
            // TODO: Optomize more, as this is called every frame...
            if (Melon<UGM>.Instance.smoothCamEnabled.Value)
            {
                var cam = GetPrivateValue<Camera, Movement>("cam", __instance);
                var bump = Melon<UGM>.Instance.smoothCamBump.Value;
                var speed = Melon<UGM>.Instance.smoothCamSpeed.Value;

                var prog = Mathf.Abs(Mathf.Clamp(Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical"), -1, 1));
                var target = __instance.transform.position + (__instance.transform.up * bump * prog);
                // Woopsie, don't wanna fix it
                target.z = -10;
                cam.transform.position = Vector3.Lerp(prevCam, target, Time.deltaTime * speed);
                prevCam = cam.transform.position;
            }
        }
    }

    [HarmonyPatch(typeof(Flashlight), "Update")]
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
    }
}
