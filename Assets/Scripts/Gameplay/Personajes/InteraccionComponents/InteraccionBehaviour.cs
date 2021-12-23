using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public abstract class InteraccionBehaviour
    {
        
        protected Personaje _personaje;
        protected IInteraccionComponent _interaccionComponent;

        public virtual void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            //Debug.Log(target.health);
        }

        public virtual void AplicarInteraccion(Personaje origen)
        {
            //behaviour.AplicarInteraccion(origen);
            //Debug.Log(_personaje.name + origen.name);
            origen.GetInteractionComponent().EjecucionDeInteraccion(_personaje);
            if (_personaje.health <= 0)
            {
                _personaje.Muerte();
            }
        }


        public virtual void Interactuar(List<Personaje> target)
        {
            _personaje.GolpearTarget();
            target[0].GetInteractionComponent().AplicarInteraccion(_personaje);
        }

        public void Configurate(Personaje personaje, IInteraccionComponent interaccionComponent)
        {
            _personaje = personaje;
            _interaccionComponent = interaccionComponent;
        }

        public void AplicarDanio(Personaje target, float danioARealizar)
        {
            if (target.escudo > 0)
            {
                if (target.escudo > danioARealizar)
                {
                    target.escudo -= danioARealizar;
                }
                else
                {
                    var danioRestante = (target.escudo - danioARealizar)*-1;
                    target.escudo = 0;
                    target.health -= danioRestante;
                }
            }
            else
            {
                target.health -= danioARealizar;
            }
        }
    }
}