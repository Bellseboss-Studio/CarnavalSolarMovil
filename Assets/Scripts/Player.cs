using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private List<Personaje> personajesJugablesElegidos;
    [SerializeField] private PlaceOfPlayer place;

    private IMediatorCooldown _mediatorCooldown;

    // Start is called before the first frame update
    void Start()
    {
        personajesJugablesElegidos = new List<Personaje>();
    }

    public void AddPj(Personaje pj)
    {
        Debug.Log(personajesJugablesElegidos.Count);
        if (personajesJugablesElegidos.Count > 2)
        {
            Debug.Log(personajesJugablesElegidos.Count);
            Debug.Log((personajesJugablesElegidos[1]));
            Debug.Log((personajesJugablesElegidos[2]));
            Debug.Log((personajesJugablesElegidos[3]));
            throw new Exception("Ya no puede llevar mas personajes");
        }
        personajesJugablesElegidos.Add(pj);
    }

    public bool IsDead()
    {
        return !place.HayAlguienDePie();
    }

    public void Configurarlo(IMediatorCooldown mediatorCooldown)
    {
        _mediatorCooldown = mediatorCooldown;
        place.FulledCharacters(personajesJugablesElegidos);
        _mediatorCooldown.ConfiguraCooldownsPorPersonaje(personajesJugablesElegidos);
    }

    public bool IsFull()
    {
        Debug.Log(personajesJugablesElegidos.Count);
        return personajesJugablesElegidos.Count > 2;
    }

    public void Restart()
    {
        Start();
    }

    public void Destruyelo()
    {
        place.DestroyPlayableCharacters();
    }

    public void ClearPersonajesJugablesElejidos()
    {
        Debug.Log(personajesJugablesElegidos.Count);
        personajesJugablesElegidos = new List<Personaje>();
        Debug.Log(personajesJugablesElegidos.Count);
    }
    
}