using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;

public class ControladorDeUI : MonoBehaviour
{
    [SerializeField] private Animator storeAnimations, loading;
    [SerializeField] private PersonajesPorComprar personajesPorComprar;
    private Player _player1;

    public void Configure(Player player1)
    {
        _player1 = player1;
    }

    public void ShowStore()
    {
        storeAnimations.SetTrigger("show");
    }
    
    public void HideStore()
    {
        storeAnimations.SetTrigger("hide");
    }

    public void ShowLoading()
    {
        loading.SetTrigger("show");
    }

    public void HideLoading()
    {
        loading.SetTrigger("hide");
    }

    public void LoadPersonajes()
    {
        personajesPorComprar.FullPlaces(ServiceLocator.Instance.GetService<IPlayFabCustom>().GetPjs(), CuandoEsSeleccionadoElPersonaje);
    }

    private void CuandoEsSeleccionadoElPersonaje(Personaje pj)
    {
        try
        {
            _player1.AddPj(pj);
            if (_player1.IsFull())
            {
                throw new Exception("Estas lleno");
            }
        }
        catch (Exception)
        {
            personajesPorComprar.DeshabilitarBotones();
        }
    }

    public bool GetTerminoDeElegir()
    {
        return personajesPorComprar.TerminoDeElegir;
    }

    public void Restart()
    {
        personajesPorComprar.Restart();
    }
}
