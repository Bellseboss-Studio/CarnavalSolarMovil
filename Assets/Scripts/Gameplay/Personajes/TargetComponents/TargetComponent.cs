using System;
using System.Collections.Generic;
using Gameplay.Personajes.Scripts;
using UnityEngine;

namespace Gameplay.Personajes.TargetComponents
{
    public abstract class TargetComponent : MonoBehaviour
    {
        //private TargetBehaviour _behaviour;
        public List<Personaje> TargetedBy;
        protected List<Personaje> _targets;
        protected Personaje _personaje;

        public void Configuracion(Personaje personaje)
        {
            _personaje = personaje;
            _targets = new List<Personaje>();
        } 
        
        public abstract List<Personaje> GetTargets();

        public void DejarDeSerTargeteado(Personaje personajeOrigen)
        {
            foreach (var personaje in TargetedBy)
            {
                if (personaje != null && personajeOrigen != null) personaje.GetTargetComponent()._targets.Remove(personajeOrigen);
            }
        }

        public void HeDejadoDeTargetear(Personaje personaje)
        {
            foreach (var target in _targets)
            {
                target.GetTargetComponent().TargetedBy.Remove(personaje);
            }
        }

        public List<Personaje> TargetsGet()
        {
            return _targets;
        }

        public Personaje GetPersonaje()
        {
            return _personaje;
        }
        
    }
}