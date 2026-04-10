using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerControllerComponent : MonoBehaviourPunCallbacks
{
    [SerializeField] private DebuControler debuControler;
    private GameObject player01;
    private GameObject player02;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
}
