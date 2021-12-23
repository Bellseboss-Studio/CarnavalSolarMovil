using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class ColocacionCartas : MonoBehaviour, IColocacionCartas
    {
        [SerializeField] private List<GameObject> posicionesDeCartas;
        private Stack<string> barajaDeCartasId;
        private Stack<string> cartasEnMano;

        public void Configurate()
        {
            barajaDeCartasId = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetBaraja();
        }
        
        private void Start()
        {
            barajaDeCartasId = new Stack<string>();
            cartasEnMano = new Stack<string>();
        }

        public List<GameObject> GetPosicionesDeCartas()
        {
            return posicionesDeCartas;
        }

        public int GetNumeroDePosiciones()
        {
            return posicionesDeCartas.Count;
        }

        public bool PuedoSacarOtraCarta()
        {
            return cartasEnMano.Count < posicionesDeCartas.Count && barajaDeCartasId.Count > 0;
        }

        public GameObject GetPosicionDeCarta()
        {
            return posicionesDeCartas[cartasEnMano.Count-1];
        }

        public string GetNextCartaId()
        {
            return barajaDeCartasId.Pop();
        }
    }
}