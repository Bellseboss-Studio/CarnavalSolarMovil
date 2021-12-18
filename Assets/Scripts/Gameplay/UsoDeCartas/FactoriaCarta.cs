using System;
using System.Collections.Generic;
using Gameplay.NewGameStates;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.UsoDeCartas
{
    public class FactoriaCarta : MonoBehaviour, IFactoriaCarta
    {
        private IColocacionCartas _colocacionCartas;
        private CartasConfiguracion _cartasConfiguracion;
        private GameObject _canvasDeLasCartas;
        private FactoriaPersonaje _factoriaPersonaje;
        private Stack<CartaTemplate> cartasInstanciadas;

        public void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje)
        {
            cartasInstanciadas = new Stack<CartaTemplate>();
            _colocacionCartas = colocacionCartas;
            _cartasConfiguracion = cartasConfiguracion;
            _canvasDeLasCartas = canvasDeLasCartas;
            _factoriaPersonaje = factoriaPersonaje;
        }

        public CartaTemplate Create(string id, GameObject posicion)
        {
            var cartaTemplate = _cartasConfiguracion.GetCartaTemplate(id);
            var cartaInstancia = Instantiate(cartaTemplate, _canvasDeLasCartas.transform);
            cartaInstancia.transform.position = posicion.transform.position;
            var dragDeLaCarta = cartaInstancia.GetComponent<DragComponent>();
            dragDeLaCarta.Configure(posicion.GetComponent<RectTransform>());
            cartaInstancia.Configurate(_factoriaPersonaje);
            return cartaInstancia;
        }

        public void CrearPrimerasCartas()
        {
            var posiciones = _colocacionCartas.GetPosicionesDeCartas();
            for (int i = 0; i < _colocacionCartas.GetNumeroDePosiciones(); i++)
            {
                cartasInstanciadas.Push(Create("Bellseboss", posiciones[i]));
            }
        }

        public void DestruirLasCartas()
        {
            var aux = cartasInstanciadas;
            while (cartasInstanciadas.Count>0)
            {
                var carta = cartasInstanciadas.Pop();
                Destroy(carta.gameObject);
            }

            cartasInstanciadas = new Stack<CartaTemplate>();
        }
    }
}