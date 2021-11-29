using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class ComportamientoDePersonajesFacade: MonoBehaviour
{
    private List<Personaje> _personajes;
    private PersonajeBuilder _personajeBuilder;
    [SerializeField] private Personaje _personaje;
    [SerializeField] private DistanciaComponent _distanciaComponent;
    [SerializeField] private RutaComponent _rutaComponent;
    [SerializeField] private TargetComponent _targetComponent;
    [SerializeField] private InteraccionComponent _interaccionComponent;
    [SerializeField] private bool EsEnemigoElPersonaje;
    
    
    private void Awake()
    {
        _personajes = new List<Personaje>();
        _personajeBuilder = new PersonajeBuilder();
        
    }
    
    void Start()
    {
        _personajeBuilder.WithPersonaje(_personaje);
        _personajeBuilder.WithTargetComponent(_targetComponent);
        _personajeBuilder.WithDistanciaComponent(_distanciaComponent);
        _personajeBuilder.WithInteraccionComponent(_interaccionComponent);
        _personajeBuilder.WithRutaComponent(_rutaComponent);
        _personajeBuilder.WithVelocidadDeInteraccion(1);
        var personaje = _personajeBuilder.Build();
        personaje.transform.parent = transform;
        personaje.enemigo = EsEnemigoElPersonaje;
        _personajes.Add(personaje);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            foreach (var variablePersonaje in _personajes)
            {
                variablePersonaje.GetInteractionComponent().Interactuar(variablePersonaje.GetTargetComponent().GetTargets()[0]);
            }
        }
    }
}
