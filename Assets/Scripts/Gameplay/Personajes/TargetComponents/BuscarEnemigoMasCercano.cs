using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BuscarEnemigoMasCercano : TargetComponent
    {
        public override List<Personaje> GetTargets()
        {
            Personaje _transformMasCercano = null;
            List<Personaje> transforms = new List<Personaje>();
            List<Personaje> transformsCercanos = new List<Personaje>();
            var personajes = GameObject.FindObjectsOfType<Personaje>();
            foreach (var personaje in personajes)
            {
                if (personaje.enemigo)
                {
                    transforms.Add(personaje);
                }
            }

            float distance = 100;
            foreach (var _transform in transforms)
            {
                if (Vector3.Distance(_personaje.transform.position, _transform.transform.position) < distance)
                {
                    _transformMasCercano = _transform;
                }
            }
            transformsCercanos.Add(_transformMasCercano);
            return transformsCercanos;
        }
    }
}