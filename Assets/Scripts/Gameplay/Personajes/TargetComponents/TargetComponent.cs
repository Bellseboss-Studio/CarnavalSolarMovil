using System.Collections.Generic;
using Gameplay.Personajes.Scripts;
using UnityEngine;

namespace Gameplay.Personajes.TargetComponents
{
    public class TargetComponent : MonoBehaviour, ITargetComponent
    {
        private TargetBehaviour _behaviour;


        public void Configuracion(TargetBehaviour behaviour, Personaje personaje)
        {
            _behaviour = behaviour;
            _behaviour.Configurate(this, personaje);
        } 
        
        public List<Personaje> GetTargets()
        {
            return _behaviour.GetTargets();
        }


        public List<Personaje> GetTargetTargetedBy()
        {
            return _behaviour.GetTargetedBy();
        }

        public void AddTargetedBy(Personaje personaje)
        {
            _behaviour.AddTargetedBy(personaje);
        }
        
        public void BuscaTusTargets()
        {
            _behaviour.BuscaLosTargets();
        }

        public void DejarDeSerTargeteado(Personaje personaje)
        {
            _behaviour.DejarDeSerTargeteado(personaje);
        }

        public void HeDejadoDeTargetear()
        {
            _behaviour.HeDejadoDeTargetear();
        }

        public Personaje[] GetPersonajes()
        {
            return GameObject.FindObjectsOfType<Personaje>();
        }

        public void RemoveTargetedBy(Personaje personaje)
        {
            _behaviour.RemoveTargetedBy(personaje);
        }
        
        public void RemoveTarget(Personaje personaje)
        {
            _behaviour.RemoveTarget(personaje);
        }

        public void SetTarget(Personaje target)
        {
            _behaviour.AddTarget(target);
        }
    }
}