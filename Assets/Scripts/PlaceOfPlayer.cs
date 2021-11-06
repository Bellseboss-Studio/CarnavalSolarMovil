using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlaceOfPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private List<PjView> personajes;
    [SerializeField] private PjView pjPrefab;

    public void Configure()
    {
        personajes = new List<PjView>();
    }

    public void FulledCharacters(List<Personaje> personajesJugablesElegidos)
    {
        var index = 0;
        foreach (var point in points)
        {
            //Aqui cambiar por una factoria
            var pjview = Instantiate(pjPrefab);
            pjview.transform.position = point.transform.position;
            pjview.Configurate(personajesJugablesElegidos[index]);
            personajes.Add(pjview);
        }
    }
}