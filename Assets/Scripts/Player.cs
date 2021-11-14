using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using ServiceLocatorPath;
using UnityEngine;

public class Player : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private PlaceOfPlayer place;

    private List<Personaje> personajesJugablesElegidos;
    // Start is called before the first frame update
    void Start()
    {
        personajesJugablesElegidos = new List<Personaje>();
    }

    public void AddPj(Personaje pj)
    {
        if (personajesJugablesElegidos.Count > 2)
        {
            throw new Exception("Ya no puede llevar mas personajes");
        }
        personajesJugablesElegidos.Add(pj);
    }

    public bool IsDead()
    {
        return !place.HayAlguienDePie();
    }

    public void Configurarlo()
    {
        place.FulledCharacters(personajesJugablesElegidos);
    }

    public bool IsFull()
    {
        return personajesJugablesElegidos.Count > 2;
    }

    public void Restart()
    {
        Start();
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