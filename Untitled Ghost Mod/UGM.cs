using MelonLoader;
using UnityEngine;
using Photon.Pun;

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
                LoggerInstance.Msg($"Overwriting Photon Game Version: {multiplayerMockGameVersion.Value}");
                PhotonNetwork.GameVersion = multiplayerMockGameVersion.Value;
            }
        }

        public override void OnLateInitializeMelon()
        {
            if (multiplayerMockGameVersion.Value != "")
            {
                LoggerInstance.Msg($"Double writing Photon Game Version just in case: {multiplayerMockGameVersion.Value}");
                PhotonNetwork.GameVersion = multiplayerMockGameVersion.Value;
            }
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
}
