using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.TargetComponents
{
    public class BuscarEnemigoMasCercano : TargetBehaviour
    {
        public override List<Personaje> GetTargets()
        {
            return _targets;
        }

        public override void BuscaLosTargets()
        {
            _targets = new List<Personaje>();
            Personaje _personajeMasCercano = null;
            List<Personaje> _personajesList = new List<Personaje>();
            var personajes = _targetComponent.GetPersonajes();
            foreach (var personaje in personajes)
            {
                if (personaje.enemigo != _personaje.enemigo && personaje.isTargeteable && !personaje.esInmune && !personaje.EsUnaBala)
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
                    distance = Vector3.Distance(_personaje.transform.position, personajeList.transform.position);
                }
            }
            if (_personajeMasCercano != null && !_targets.Contains(_personajeMasCercano) && _personajeMasCercano.isTargeteable && !_personajeMasCercano.esInmune && !_personajeMasCercano.EsUnaBala) _targets.Add(_personajeMasCercano);
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