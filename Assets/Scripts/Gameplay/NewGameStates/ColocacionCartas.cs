using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class ColocacionCartas : MonoBehaviour, IColocacionCartas
    {
        [SerializeField] private List<GameObject> posicionesDeCartas;

        public List<GameObject> GetPosicionesDeCartas()
        {
            return posicionesDeCartas;
        }

        public int GetNumeroDePosiciones()
        {
            return posicionesDeCartas.Count;
        }
    }
}