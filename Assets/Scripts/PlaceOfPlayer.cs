using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            
            if (point.TryGetComponent<ControladorDeBatallaParaPersonajes>(out var controlador))
            {
                pjview.Configurate(personajesJugablesElegidos[index], controlador);
            }
            index++;
            personajes.Add(pjview);
        }
    }

    
    
    public bool HayAlguienDePie()
    {
        var EstanTodosVivos = false;
        foreach (var personaje in personajes.Where(personaje => personaje.EstaVivo()))
        {
            EstanTodosVivos = true;
        }

        return EstanTodosVivos;
    }

    public void DestroyPlayableCharacters()
    {
        var index = 0;
        foreach (var personaje in personajes)
        {
            Destroy(personaje.gameObject);
        }
        Configure();
    }

    public void DesConfigurePjView()
    {
        for (int i = 0; i < personajes.Count; i++)
        {
            if (points[i].TryGetComponent<ControladorDeBatallaParaPersonajes>(out var controlador))
            {
                personajes[i].DesConfigure(controlador);
            }
        }
    }

    public void DeshabilitaElDrag()
    {
        
    }
}