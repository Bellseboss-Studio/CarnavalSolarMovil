using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;

public class Player : MonoBehaviour
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
        Debug.Log($"Personaje {pj.GetNombre()} agregado");
    }

    public bool IsDead()
    {
        return false;
    }

    public void Configurarlo()
    {
        place.FulledCharacters(personajesJugablesElegidos);
    }

    public bool IsFull()
    {
        return personajesJugablesElegidos.Count > 2;
    }
}