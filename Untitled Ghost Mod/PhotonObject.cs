using MelonLoader;
using Photon.Pun;

namespace Untitled_Ghost_Mod
{
    internal class UGMPhotonObject : MonoBehaviourPunCallbacks
    {
        public int maxPlayers;

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            // TODO: Change this to a slider
            Melon<UGM>.Logger.Msg($"Changing max players: {maxPlayers}");
            PhotonNetwork.CurrentRoom.MaxPlayers = (byte)maxPlayers;
        }
    }
}
