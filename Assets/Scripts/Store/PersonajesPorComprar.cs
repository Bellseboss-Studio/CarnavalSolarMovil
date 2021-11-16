using System;
using UnityEngine.UI;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;

public class PersonajesPorComprar : MonoBehaviour
{
    [SerializeField] private List<ContainerDeSeleccionDePeronsaje> containers;
    [SerializeField] private GameObject botonDeFinalizar;
    [SerializeField] private Button terminar;

    private bool terminoDeElegir;

    private void Start()
    {
        botonDeFinalizar.SetActive(false);
    }

    public void FullPlaces(List<Personaje> pjs, ContainerDeSeleccionDePeronsaje.OnSelectingPj acction)
    {
        var index = 0;
        //Debug.Log($"cantidad es {pjs.Count}");
        foreach (var pj in pjs)
        {
            containers[index].Fulled(pj);
            containers[index].EsSeleccionadoElPersonaje = null;
            containers[index].EsSeleccionadoElPersonaje += acction;
            index++;
        }
    }

    public void DeshabilitarBotones()
    {
        foreach (var container in containers)
        {
            container.DesHabilitarBoton();
        }
        botonDeFinalizar.SetActive(true);
        terminar.onClick.AddListener(() =>
        {
            terminoDeElegir = true;
        });
    }

    public void Restart(ContainerDeSeleccionDePeronsaje.OnSelectingPj acction)
    {
        terminoDeElegir = false;
        foreach (var container in containers)
        {
            container.EsSeleccionadoElPersonaje -= acction;
        }
    }
    public bool TerminoDeElegir => terminoDeElegir;
    
    
    
    public void ReinicarMenu()
    {
        botonDeFinalizar.SetActive(false);
        foreach (var container in containers)
        {
            container.HabilitarBoton();
        }
    }
    
}
