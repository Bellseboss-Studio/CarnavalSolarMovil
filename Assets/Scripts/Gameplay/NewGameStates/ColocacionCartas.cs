using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
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
            barajaDeCartasId = new Stack<string>();
            cartasEnMano = new Stack<string>();
            barajaDeCartasId = ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().GetBarajaElejida();
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
            return posicionesDeCartas[cartasEnMano.Count - 1];
        }

        public string GetNextCartaId()
        {
            var carta = barajaDeCartasId.Pop();
            cartasEnMano.Push(carta);
            return carta;

        }
    }
}