using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.TargetComponents
{
    public class BuscarEnemigosDondeRebotar : TargetBehaviour
    {
        private int _personajesAtacados = 0;
        private List<Personaje> _listaDePersonajesAtacados;
        public override List<Personaje> GetTargets()
        {
            return _targets;
        }

        public override void BuscaLosTargets()
        {
            if (_personajesAtacados >= 3)
            {
                _personaje.Muerte();
            }
            Personaje _personajeMasCercano = null;
            List<Personaje> _personajesList = new List<Personaje>();
            var personajes = _targetComponent.GetPersonajes();
            
            var nuevoTarget = GetNuevoTarget(personajes);
            if (nuevoTarget == null)
            {
                //Destroy(gameObject);
                return;
            }
            _personaje.GetRutaComponent()._rutaBehaviour.ConfigureSinApply(nuevoTarget.gameObject);
            _listaDePersonajesAtacados.Add(nuevoTarget);
            
            Personaje GetNuevoTarget(Personaje[] personajes)
            {
                foreach (var personaje in personajes)
                {
                    if (!_listaDePersonajesAtacados.Contains(personaje) && personaje.enemigo != _personaje.enemigo && personaje.isTargeteable && !personaje.esInmune)
                    {
                        _personajesList.Add(personaje);
                        _personajesAtacados++;
                        return personaje;
                    }
                }
                return null;
            }
            
            float distance = 100;
            foreach (var personajeList in _personajesList)
            {
                if (Vector3.Distance(_personaje.transform.position, personajeList.transform.position) < distance)
                {
                    _personajeMasCercano = personajeList;
                    distance = Vector3.Distance(_personaje.transform.position, personajeList.transform.position);
                }
            }
            if (_personajeMasCercano != null && !_targets.Contains(_personajeMasCercano) && _personajeMasCercano.isTargeteable && !_personajeMasCercano.esInmune) _targets.Add(_personajeMasCercano);
            if (_personajeMasCercano != null)
            {
                if (!_personajeMasCercano.GetTargetComponent().GetTargetTargetedBy().Contains(_personaje))
                {
                    //Debug.Log("se añadio a la lista");
                    _personajeMasCercano.GetTargetComponent().AddTargetedBy(_personaje);
                }
                //Debug.Log(_personajeMasCercano.GetTargetComponent().GetTargetTargetedBy().Contains(_personaje));
            }
        }
    }
}