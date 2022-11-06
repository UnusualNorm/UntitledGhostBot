using System;
using Photon.Realtime;
using Photon.Pun;
using MelonLoader;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Untitled_Ghost_Mod
{
    public class UGM : MelonMod
    {
        private MelonPreferences_Category UGMC;
        private MelonPreferences_Entry<float> generalMaxDifficulty;
        private MelonPreferences_Entry<string> multiplayerMockGameVersion;
        private MelonPreferences_Entry<int> multiplayerMaxPlayers;

        private UGMObject ugmObject;
        private UGMPhotonObject ugmPhotonObject;

        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();
            LoggerInstance.Msg("UGM is up and rootin' tootin'!");

            LoggerInstance.Msg("Creating Melon configuration...");
            UGMC = MelonPreferences.CreateCategory("Multiplayer");

            // Single/Multi player Generic
            generalMaxDifficulty = UGMC.CreateEntry<float>("MaxDifficulty", 10);

            // Multiplayer Specific
            multiplayerMockGameVersion = UGMC.CreateEntry<string>("MultiplayerMockGameversion", "");
            multiplayerMaxPlayers = UGMC.CreateEntry<int>("MultiplayerMaxPlayers", 255);

            if (multiplayerMockGameVersion.Value != "")
            {
                LoggerInstance.Msg("Overwriting Photon Game Version:", multiplayerMockGameVersion.Value);
                PhotonNetwork.GameVersion = multiplayerMockGameVersion.Value;
            }
        }

        public override void OnLateInitializeMelon()
        {
            LoggerInstance.Msg("Double writing Photon Game Version just in case:", multiplayerMockGameVersion.Value);
            PhotonNetwork.GameVersion = multiplayerMockGameVersion.Value;
        }

        public void SpawnUGMObject()
        {
            LoggerInstance.Msg("Spawning UGM object...");
            var obj = new GameObject("UGMPhotonObject");
            ugmPhotonObject = obj.AddComponent<UGMPhotonObject>();
            ugmPhotonObject.maxPlayers = multiplayerMaxPlayers.Value;
        }

        public void SpawnUGMPhotonObject()
        {
            LoggerInstance.Msg("Spawning UGM Photon object...");
            var obj = new GameObject("UGMObject");
            ugmObject = obj.AddComponent<UGMObject>();
            ugmObject.maxDifficulty = generalMaxDifficulty.Value;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg($"Scene was loaded: {buildIndex} {sceneName}");
            SpawnUGMObject();
            SpawnUGMPhotonObject();
        }
    }

    public class UGMPhotonObject : MonoBehaviourPunCallbacks
    {
        public int maxPlayers;

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            // TODO: Change this to a slider
            Melon<UGM>.Logger.Msg($"Changing max players: ");
            PhotonNetwork.CurrentRoom.MaxPlayers = (byte)maxPlayers;
        }
    }

    public class UGMObject : MonoBehaviour
    {
        public float maxDifficulty;

        public void OverwriteDifficultySlider(bool multiplayer)
        {
            Melon<UGM>.Logger.Msg("Overwriting difficulty slider...");
            /*var difficultyObject = GameObject.Find("Difficulty");
            var difficultySlider = difficultyObject.GetComponent<Slider>();
            difficultySlider.maxValue = maxDifficulty;*/
            // Screw you, Unity
            var scene = SceneManager.GetActiveScene();
            var children = scene.GetRootGameObjects();
            var canvas = Array.Find(children, child => child.name == "Canvas");
            var slider = canvas.transform.GetChild((multiplayer ? 2 : 7)).GetChild(2).GetComponent<Slider>();
            slider.maxValue = maxDifficulty;
        }

        public void OverwriteTips()
        {
            Melon<UGM>.Logger.Msg("Overwriting tips tips...");
            string[] myTips = { "Give me my life back.", "Honestly, it's probably a [redacted]", "What do you call it when a ghost gets out of bounds? A bug." };
            var scene = SceneManager.GetActiveScene();
            var children = scene.GetRootGameObjects();
            var canvas = Array.Find(children, child => child.name == "Canvas");
            var tips = canvas.transform.GetChild(1).GetChild(4).GetComponent<Tips>();
            tips.tips = myTips;
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
