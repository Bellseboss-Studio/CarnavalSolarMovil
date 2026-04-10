using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.RutaComponents
{
    public class MovimientoBala : RutaBehaviour
    {
        public override void DesplazarHaciaElTarget(GameObject personaje)
        {
            _sinApply.sePuedeMover = true;
        }

        public override void SetTargetsToNavMesh(List<Personaje> targets)
        {
            _sinApply.sePuedeMover = true;
            _sinApply.Configure(targets[0].gameObject);
        }

        public override void DejarDeDesplazar()
        {

            _sinApply.sePuedeMover = false;
        }
    }
}