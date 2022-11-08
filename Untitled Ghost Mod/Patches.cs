using HarmonyLib;
using Discord;
using MelonLoader;
using System;
using System.Diagnostics;
using System.Reflection;
using TMPro;
using UnityEngine.Windows.Speech;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Untitled_Ghost_Mod
{
    [HarmonyPatch(typeof(Discordlolplswork), "Start")]
    static class DiscordActivityPatch
    {
        private static void Postfix(Discordlolplswork __instance)
        {
            Melon<UGM>.Logger.Msg("Patching Discord Activity data...");
            FieldInfo type = typeof(Discordlolplswork).GetField("discord", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var discord = (Discord.Discord)type.GetValue(__instance);
            if (Process.GetProcessesByName("Discord").Length != 0)
            {
                discord.GetActivityManager().UpdateActivity(new Activity
                {
                    Name = "Untitled Ghost GMGMGMGMGMGMod",
                    Details = "Hunting a god-forsaken poopity-scoop willy-wolly peepee-poopoo unoriginal-ly epic-ness high-tax spooky-wooky cummy-wummy dommy-mommy hobby-lobby dumb ghost!",
                    State = "Download: https://dubscr.itch.io/ugg or, for better experience: https://github.com/UnusualNorm/UntitledGhostMod/releases",
                    Assets = new ActivityAssets
                    {
                        LargeImage = "logo",
                        LargeText = "Boo-hoo my wittle ghost game got modded!",
                    },
                    Timestamps = new Discord.ActivityTimestamps
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
    static class UserNameChangePatch
    {
        private static void Prefix(ConnectToServer __instance)
        {
            if (!PlayerPrefs.HasKey("Nickname")) return;
            Melon<UGM>.Logger.Msg("Patching username change blocker...");
            if (PlayerPrefs.GetString("Nickname", "") == __instance.usernameInput.text) return;

            PlayerPrefs.DeleteKey("Nickname");
            Melon<UGM>.Logger.Msg("Patched username change blocker!");
        }
    }

    [HarmonyPatch(typeof(BookUI), "Start")]
    static class CorrectGhostChoicePatch
    {
        private static void Postfix(BookUI __instance)
        {
            Melon<UGM>.Logger.Msg("Changing ghost choice to correct ghost...");
            var eai = GameObject.FindWithTag("Ghost").GetComponent<EnemyAI>();
            var ghostI = __instance.guessDropdown.options.FindIndex(type => type.text.ToLower() == eai.currghosttype.ToLower());
            __instance.guessDropdown.value = ghostI;
            Melon<UGM>.Logger.Msg("Changed ghost choice to correct ghost!");
        }
    }

    [HarmonyPatch(typeof(LuigiBoard), "Start")]
    static class ExtraLuigiBoardPatch
    {
        private static void Postfix(LuigiBoard __instance)
        {
            Melon<UGM>.Logger.Msg("Patching extra Luigi Board lines...");
            FieldInfo textType = typeof(Discordlolplswork).GetField("discord", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var text = (TMP_Text)textType.GetValue(__instance);
            FieldInfo ASType = typeof(Discordlolplswork).GetField("AS", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var AS = (AudioSource)textType.GetValue(__instance);

            var eai = GameObject.FindWithTag("Ghost").GetComponent<EnemyAI>();
            var kr = new KeywordRecognizer(new string[] { "Who is joe" });
            kr.OnPhraseRecognized += (PhraseRecognizedEventArgs speech) =>
            {
                Melon<UGM>.Logger.Msg($"Recognized extra Luigi Board line: {speech.text}");
                text.text = "What the fuck did you just fucking say about me, you little bitch? I'll have you know I graduated top of my class in the Navy Seals, and I've been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I'm the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You're fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that's just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little \"clever\" comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn't, you didn't, and now you're paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You're fucking dead, kiddo.";
                AS.clip = (AudioClip)Resources.Load("Ghost/moan" + Random.Range(1, 5).ToString());
                AS.Play();
                eai.hunting = true;
                return;
            };
            Melon<UGM>.Logger.Msg("Patched extra Luigi Board lines!");
        }
    }
}
