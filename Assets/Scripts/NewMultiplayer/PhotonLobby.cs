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
            playOnlineButton.interactable = true;
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            Debug.Log($"Conectado al lobby ${PhotonNetwork.CurrentLobby.Name}");
        }


        public void JoinRandomOrCreateRoom()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Entraste a la sala");
            if (PhotonNetwork.PlayerList.Length >= 2)
            {
                PhotonNetwork.LoadLevel("MPNewGameStates");
            }
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            Debug.Log("Creaste la sala");
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            if (PhotonNetwork.PlayerList.Length >= 2)
            {
                PhotonNetwork.LoadLevel("MPNewGameStates");
            }
        }
    }
}