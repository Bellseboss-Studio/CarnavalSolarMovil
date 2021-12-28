using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class RebotarEnTresEnemigos : InteraccionBehaviour
    {
        public override void Interactuar(List<Personaje> target)
        {
            _personaje.GolpearTarget();
        }
    }
}
/*public class BalaConReboteEn3Enemigos : MonoBehaviour
{
    private Personaje _personaje;
    private int _rebotes;
    private int _personajesAtacados = 0;
    private Personaje _target;
    private List<Personaje> _listaDePersonajesAtacados;
    private bool _esEnemigaLaBala;
    [SerializeField] private SinApply _sinApply;
    private float _velocidad;

    public void Configure(int rebotes, bool esAliadaLaBala,
        Personaje primerTarget, float velocity, Personaje personaje)
    {
        _listaDePersonajesAtacados = new List<Personaje>();
        _rebotes = rebotes;
        _esEnemigaLaBala = esAliadaLaBala;
        _target = primerTarget;
        _velocidad = velocity;
        _sinApply.Configure(primerTarget.gameObject, velocity);
        _listaDePersonajesAtacados.Add(primerTarget);
        _personaje = personaje;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) > .5) return;
        _target.GetInteractionComponent().AplicarInteraccion(_personaje);
        _personajesAtacados++;
        if (_personajesAtacados <3)
        {
            CambiarDeTarget();
        }
    }

    private void CambiarDeTarget()
    {
        var personajes = _personaje.GetTargetComponent().GetPersonajes();
        var nuevoTarget = GetNuevoTarget(personajes);
        if (nuevoTarget == null)
        {
            Destroy(gameObject);
            return;
        }
        _sinApply.Configure(nuevoTarget.gameObject, _velocidad);
        _listaDePersonajesAtacados.Add(nuevoTarget);
    }

    Personaje GetNuevoTarget(Personaje[] personajes)
    {
        foreach (var personaje in personajes)
        {
            if (!_listaDePersonajesAtacados.Contains(personaje) && personaje.enemigo != _esEnemigaLaBala)
            {
                return personaje;
            }
        }
        return null;
    }
}*/