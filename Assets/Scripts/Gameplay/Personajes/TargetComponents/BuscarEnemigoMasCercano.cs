using System.Collections.Generic;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;

namespace Gameplay
{
    public class BuscarEnemigoMasCercano : TargetComponent
    {
        public override List<Personaje> GetTargets()
        {
            Personaje _personajeMasCercano = null;
            List<Personaje> _personajesList = new List<Personaje>();
            var personajes = GameObject.FindObjectsOfType<Personaje>();
            foreach (var personaje in personajes)
            {
                if (personaje.enemigo != _personaje.enemigo && personaje.isTargeteable)
                {
                    _personajesList.Add(personaje);
                }
            }

            float distance = 100;
            foreach (var personajeList in _personajesList)
            {
                if (Vector3.Distance(_personaje.transform.position, personajeList.transform.position) < distance)
                {
                    _personajeMasCercano = personajeList;
                }
            }
            if (!_targets.Contains(_personajeMasCercano) && _personajeMasCercano != null && _personajeMasCercano.isTargeteable) _targets.Add(_personajeMasCercano);
            if (_personajeMasCercano != null)
            {
                if (!_personajeMasCercano.GetTargetComponent().TargetedBy.Contains(_personaje))
                {
                    Debug.Log("se añadio a la lista");
                    _personajeMasCercano.GetTargetComponent().TargetedBy.Add(_personaje);
                }
                Debug.Log(_personajeMasCercano.GetTargetComponent().TargetedBy.Contains(_personaje));
                return _targets;
            }

            return new List<Personaje>();
        }
    }
}