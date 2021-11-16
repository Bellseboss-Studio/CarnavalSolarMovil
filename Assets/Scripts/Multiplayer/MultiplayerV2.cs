using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiplayerV2 : MonoBehaviourPunCallbacks, IMultiplayer
{
    private bool _estaListo;
    private bool _terminoDeProcesar;
    private bool _falloAlgo;
    private List<RoomInfo> _roomList;
    private DebuControler debuControler;
    private GameObject _prefab;

    public delegate void OnJoinPlayer();

    void Start()
    {
        Configure();
    }

    public void Configure()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Conectado");
        _estaListo = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log($"Desconectado");
        _estaListo = false;
    }

    public bool EstaListo()
    {
        return _estaListo;
    }

    public void CrearSala(string nombreSala)
    {
        PhotonNetwork.CreateRoom(nombreSala, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public void UnirseSala(string nombreSala)
    {
        if (nombreSala == null)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.JoinRoom(nombreSala);
        }
    }

    public bool TerminoDeProcesarLaSala()
    {
        return _terminoDeProcesar;
    }

    public bool FalloAlgo()
    {
        return _falloAlgo;
    }

    public void ResetFlags()
    {
        _falloAlgo = false;
        _terminoDeProcesar = false;
    }

    public string CantidadDePersonasEnSala()
    {
        return $"{PhotonNetwork.CurrentRoom.PlayerCount}";
    }

    public PlayerSincro CrearPersonaje(PlayerSincro.OnLoadMyPj ownPj)
    {
        var player01 = PhotonNetwork.Instantiate("PlayerBellseboss", Vector3.zero, Quaternion.identity, 0);
        if (player01.TryGetComponent<PlayerSincro>(out var sincro))
        {
            sincro.OnLoadMyOwnPj += ownPj;
            sincro.Configuralo();
            return sincro;
        }

        throw new Exception("no se pudo crear el pj");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Debemos notificar al jugador que alguien se unio a su partida");
        _terminoDeProcesar = true;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("Debemos notificar al jugador que alguien se salio de su partida");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Debemos notificar al jugador que se creo la sala");
        _terminoDeProcesar = true;
    }
    

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Debemos notificar al jugador que ocurrio un error al crear la sala");
        _terminoDeProcesar = true;
        _falloAlgo = true;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Debemos notificar al jugador que ocurrio un error al unirse a la sala");
        _terminoDeProcesar = true;
        _falloAlgo = true;
    }

    public void SetPrefabForInstantiante(GameObject prefab)
    {
        _prefab = prefab;
    }
}