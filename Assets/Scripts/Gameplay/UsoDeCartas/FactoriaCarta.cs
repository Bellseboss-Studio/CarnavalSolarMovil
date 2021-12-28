using System;
using System.Collections.Generic;
using Gameplay.NewGameStates;
using ServiceLocatorPath;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.UsoDeCartas
{
    public class FactoriaCarta : MonoBehaviour, IFactoriaCarta
    {
        private IColocacionCartas _colocacionCartas;
        private CartasConfiguracion _cartasConfiguracion;
        private GameObject _canvasDeLasCartas, _canvasPrincipal;
        private FactoriaPersonaje _factoriaPersonaje;
        private Stack<CartaTemplate> cartasInstanciadas;

        public void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje, GameObject canvasPrincipal)
        {
            cartasInstanciadas = new Stack<CartaTemplate>();
            _colocacionCartas = colocacionCartas;
            _cartasConfiguracion = cartasConfiguracion;
            _canvasDeLasCartas = canvasDeLasCartas;
            _factoriaPersonaje = factoriaPersonaje;
            _canvasPrincipal = canvasPrincipal;
        }

        public CartaTemplate Create(string id, GameObject posicion, int posicionEnBaraja)
        {
            var cartaTemplate = _cartasConfiguracion.GetCartaTemplate(id);
            var cartaInstancia = Instantiate(cartaTemplate, _canvasDeLasCartas.transform);
            cartaInstancia.PosicionEnBaraja = posicionEnBaraja;
            _colocacionCartas.HayCartaEnPosicion(posicionEnBaraja);
            cartaInstancia.transform.position = posicion.transform.position;
            var dragDeLaCarta = cartaInstancia.GetComponent<DragComponent>();
            dragDeLaCarta.Configure(posicion.GetComponent<RectTransform>(), _canvasDeLasCartas.GetComponent<RectTransform>());
            cartaInstancia.Configurate(_factoriaPersonaje);
            return cartaInstancia;
        }

        public CartaTemplate CreateEnemigo(string id, GameObject posicion)
        {
            var cartaTemplate = _cartasConfiguracion.GetCartaTemplate(id);
            var cartaInstancia = Instantiate(cartaTemplate, _canvasDeLasCartas.transform);
            cartaInstancia.transform.position = posicion.transform.position;
            var dragDeLaCarta = cartaInstancia.GetComponent<DragComponent>();
            dragDeLaCarta.Configure(posicion.GetComponent<RectTransform>(), _canvasDeLasCartas.GetComponent<RectTransform>());
            cartaInstancia.Configurate(_factoriaPersonaje);
            return cartaInstancia;
        }

        public void CrearHeroe(Vector3 point)
        {
            var heroe = _colocacionCartas.GetHeroe();
            ServiceLocator.Instance.GetService<IHeroeInstancie>().InstanciateHero(_factoriaPersonaje, point, heroe);
            ServiceLocator.Instance.GetService<IEnemyInstantiate>().InstanciateHeroEnemy(_factoriaPersonaje);
            
        }

        public void CrearPrimerasCartas()
        {
            while (_colocacionCartas.PuedoSacarOtraCarta())
            {
                Create(_colocacionCartas.GetNextCartaId(), _colocacionCartas.GetSiguientePosicionDeCarta(), _colocacionCartas.GetPosicionDeUltimaCartaInstanciada());
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

        public void CrearCartasEnHuecos()
        {
            var posicionesSinCartas = _colocacionCartas.ObtenerPocisionesSinCartas();
            foreach (var posicionSinCarta in posicionesSinCartas)
            {
                Debug.Log($"colocandoCartaEnPosicion {posicionSinCarta}");
                if (_colocacionCartas.PuedoSacarOtraCarta())
                {
                    Create(_colocacionCartas.GetNextCartaId(), _colocacionCartas.GetPosicionDeCarta(posicionSinCarta),posicionSinCarta);   
                }
            }
        }
    }
}