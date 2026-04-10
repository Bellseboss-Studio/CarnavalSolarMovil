using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarTresTargetsMasCercanos : InteraccionBehaviour
    {
        public override void Interactuar(List<Personaje> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].GetInteractionComponent().AplicarInteraccion(_personaje);
            }
        }
        
    }
}