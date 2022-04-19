using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace NewMultiplayer
{
    public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
        
        [SerializeField] private Button playOnlineButton;
        
        
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            playOnlineButton.onClick.AddListener(ConectarAlLobby);
        }

        private void ConectarAlLobby()
        {
            PhotonNetwork.JoinLobby(customLobby);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("En Linea");
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            Debug.Log($"Conectado al lobby ${PhotonNetwork.CurrentLobby.Name}");
        }
    }
}