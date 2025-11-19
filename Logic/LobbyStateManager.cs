using UnityEngine;
using Photon.Pun;

namespace Logic
{
    public static class LobbyStateManager
    {
        public static Color GetLobbyGlow()
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.CustomProperties != null && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("gameMode"))
            {
                string gm = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().ToLower();
                if (gm.Contains("infection")) return Color.red * 0.5f;
                if (gm.Contains("casual")) return Color.green * 0.5f;
                if (gm.Contains("private")) return Color.blue * 0.5f;
            }
            return new Color(0.3f, 0f, 0.6f);
        }
    }
}