using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSincro : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private int numeroParaSincronizar;

    private void Update()
    {
        if (photonView.IsMine)
        {
            //Debug.Log($"Soy Yo {numeroParaSincronizar}");
            //_debug?.Log($"Soy Yo {numeroParaSincronizar}");
        }
        else
        {
            //Debug.Log($"Soy otro {numeroParaSincronizar}");
            //_debug?.Log($"Soy otro {numeroParaSincronizar}");
        }
    }

    public void Configure()
    {
        photonView.ObservedComponents.Add(this);
        if (photonView.IsMine)
        {
            //photonView.RPC("CrearPersonajes", RpcTarget.AllBuffered, );
        }
    }

    [PunRPC]
    public void prueba_callback(float prueba)
    {
        Debug.Log($"LLamada con {prueba}");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext("Hola a todos");
        }
        else
        {
            Debug.Log((string)stream.ReceiveNext());
        }
    }
}