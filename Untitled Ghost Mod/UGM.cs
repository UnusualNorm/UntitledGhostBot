﻿using MelonLoader;
using UnityEngine;

namespace Untitled_Ghost_Mod
{
    public class UGM : MelonMod
    {
        private MelonPreferences_Category ugmCategory;

        public MelonPreferences_Entry<bool> smoothCamEnabled;
        public MelonPreferences_Entry<float> smoothCamSpeed;
        public MelonPreferences_Entry<float> smoothCamBump;

        public MelonPreferences_Entry<float> cameraSizeMultiplier;
        public MelonPreferences_Entry<float> maxDifficulty;
        public MelonPreferences_Entry<byte> maxPlayers;

        private UGMPhotonObject ugmPhoton;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("UGM is up and rootin' tootin'!");

            LoggerInstance.Msg("Creating Melon configuration...");
            ugmCategory = MelonPreferences.CreateCategory("UGM");
            smoothCamEnabled = ugmCategory.CreateEntry<bool>("SmoothCam", true);
            smoothCamSpeed = ugmCategory.CreateEntry<float>("SmoothCameraSpeed", 6.5f);
            smoothCamBump = ugmCategory.CreateEntry<float>("SmoothCameraBump", .75f);

            cameraSizeMultiplier = ugmCategory.CreateEntry<float>("CameraSizeMultiplier", 1.5f);
            maxDifficulty = ugmCategory.CreateEntry<float>("MaxDifficulty", 10);
            maxPlayers = ugmCategory.CreateEntry<byte>("MultiplayerMaxPlayers", 255);

            Load();
        }

        public override void OnLateInitializeMelon()
        {
            Object.DontDestroyOnLoad(SpawnUGMPhoton());
            Save();
        }

        public void Load()
        {
            ugmCategory.LoadFromFile();
        }

        public void Save()
        {
            ugmCategory.SaveToFile();
        }

        public GameObject SpawnUGMPhoton()
        {
            LoggerInstance.Msg("Spawning UGM Photon object...");
            var obj = new GameObject("UGMPhotonObject");
            ugmPhoton = obj.AddComponent<UGMPhotonObject>();
            ugmPhoton.ugm = this;
            ugmPhoton.maxPlayers = maxPlayers;
            return obj;
        }
    }
}
