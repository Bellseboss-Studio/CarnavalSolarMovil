using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class TargetComponent : MonoBehaviour
    {
        //private TargetBehaviour _behaviour;
        private List<Personaje> _targets;
        protected Personaje _personaje;

        public void Configuracion(Personaje personaje)
        {
            _personaje = personaje;
        } 
        
        public abstract List<Personaje> GetTargets();
    }
}