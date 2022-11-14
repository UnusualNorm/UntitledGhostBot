using MelonLoader;
using Photon.Pun;

namespace Untitled_Ghost_Mod
{
    internal class UGMPhotonObject : MonoBehaviourPunCallbacks
    {
        public UGM ugm;
        public MelonPreferences_Entry<byte> maxPlayers;

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            // TODO: Change this to a slider
            Melon<UGM>.Logger.Msg($"Changing max players: {maxPlayers}");
            PhotonNetwork.CurrentRoom.MaxPlayers = maxPlayers.Value;
        }
    }
}
