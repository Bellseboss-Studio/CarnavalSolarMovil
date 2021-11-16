using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
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
    }

    public bool IsDead()
    {
        return !place.HayAlguienDePie();
    }

    public void Configurarlo()
    {
        place.FulledCharacters(personajesJugablesElegidos);
    }
    public void Configurarlo(PjView.OnApplyDamage ataqueNormal, PjView.OnApplyDamage ataqueEspecial)
    {
        place.FulledCharacters(personajesJugablesElegidos, ataqueNormal, ataqueEspecial);
    }

    public bool IsFull()
    {
        return personajesJugablesElegidos.Count > 2;
    }

    public void Restart()
    {
        Start();
    }

    public List<Personaje> GetPersonajes()
    {
        return personajesJugablesElegidos;
    }

    public void QuienHizoDanio(string vieneDe, string tipoDeDanio)
    {
        foreach (var view in place.GetViews().Where(view => view.PJ.nombre == vieneDe))
        {
            view.HacerAnimacionDeAtaque(tipoDeDanio);
        }
    }

    public void HayDanio(string quienRecivioElDanio, float danioAntesDeDescuentos)
    {
        foreach (var view in place.GetViews().Where(view => view.PJ.nombre == quienRecivioElDanio))
        {
            view.AplicaDanoDe(danioAntesDeDescuentos);
        }
    }
}