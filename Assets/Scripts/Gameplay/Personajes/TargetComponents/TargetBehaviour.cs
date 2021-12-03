using System;
using System.Collections;
using System.Collections.Generic;

namespace Gameplay.Personajes.TargetComponents
{
    public abstract class TargetBehaviour
    {
        protected Personaje _personaje;
        protected ITargetComponent _targetComponent;
        protected List<Personaje> _targets;
        protected List<Personaje> targetedBy;

        
        protected TargetBehaviour()
        {
            targetedBy = new List<Personaje>();
            _targets = new List<Personaje>();
        }

        public abstract List<Personaje> GetTargets();
        
        public void DejarDeSerTargeteado(Personaje personajeOrigen)
        {
            foreach (var personaje in targetedBy)
            {
                if (personaje != null && personajeOrigen != null) personaje.GetTargetComponent().RemoveTarget(personajeOrigen);
            }
        }

        public void HeDejadoDeTargetear()
        {
            foreach (var target in _targets)
            {
                target.GetTargetComponent().RemoveTargetedBy(_personaje);
            }
        }

        public List<Personaje> GetTargetedBy()
        {
            return targetedBy;
        }

        public abstract void BuscaLosTargets();

        public void AddTargetedBy(Personaje personaje)
        {
            targetedBy.Add(personaje);
        }

        public void RemoveTargetedBy(Personaje personaje)
        {
            targetedBy.Remove(personaje);
        }

        public void Configurate(ITargetComponent targetComponent, Personaje personaje)
        {
            _targetComponent = targetComponent;
            _personaje = personaje;
        }

        public void GetTargetsList()
        {
            throw new NotImplementedException();
        }

        public void RemoveTarget(Personaje personaje)
        {
            _targets.Remove(personaje);
        }
    }
}