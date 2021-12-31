using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.NewGameStates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ServiceLocatorPath
{
    public class ServicioDeBaraja: IBarajaDelPlayer
    {
        private Stack<string> _baraja;
        private Stack<string> _heroes;

        public ServicioDeBaraja()
        {
            _baraja = new Stack<string>();
            _heroes = new Stack<string>();
        }

        public Stack<string> GetBaraja()
        {
            return new Stack<string>(_baraja.ToArray());
        }

        public void AddCarta(string cartaTemplateId)
        {
            _baraja.Push(cartaTemplateId);
        }

        public void AddHeroe(string cartaTemplateId)
        {
            _heroes.Push(cartaTemplateId);
        }

        public string GetCartaRandom()
        {
            var numberRandom = Random.Range(0, _baraja.Count);
            var i = 0;
            foreach (var b in _baraja)
            {
                if (i == numberRandom)
                {
                    return b;
                }
                i++;
            }
            throw new Exception("esto no debe de pasar");
        }

        public string GetHeroeRandom()
        {
            var numberRandom = Random.Range(0, _heroes.Count);
            var i = 0;
            foreach (var b in _heroes)
            {
                if (i == numberRandom)
                {
                    return b;
                }
                i++;
            }
            Debug.Log(_heroes.Count);
            throw new Exception("esto no debe de pasar");
        }
    }
}