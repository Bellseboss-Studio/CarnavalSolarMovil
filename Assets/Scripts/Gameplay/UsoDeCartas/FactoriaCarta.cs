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
        [SerializeField] private DropComponent all, medioCampo, touchDown;
        private IColocacionCartas _colocacionCartas;
        private CartasConfiguracion _cartasConfiguracion;
        private GameObject _canvasDeLasCartas, _canvasPrincipal;
        private FactoriaPersonaje _factoriaPersonaje;
        private Stack<CartaTemplate> cartasInstanciadas;
        private Transform _transformDondePosicionar;

        public void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje, GameObject canvasPrincipal, Transform transformDondePosicionar)
        {
            _transformDondePosicionar = transformDondePosicionar;
            cartasInstanciadas = new Stack<CartaTemplate>();
            _colocacionCartas = colocacionCartas;
            _cartasConfiguracion = cartasConfiguracion;
            _canvasDeLasCartas = canvasDeLasCartas;
            _factoriaPersonaje = factoriaPersonaje;
            _canvasPrincipal = canvasPrincipal;
        }

        public CartaTemplate Create(string id, GameObject posicion, int posicionEnBaraja, Transform transformParameter)
        {
            Debug.Log("crea aliado");
            var cartaTemplate = _cartasConfiguracion.GetCartaTemplate(id);
            var cartaInstancia = Instantiate(cartaTemplate, _canvasDeLasCartas.transform);
            cartaInstancia.PosicionEnBaraja = posicionEnBaraja;
            _colocacionCartas.HayCartaEnPosicion(posicionEnBaraja);
            cartaInstancia.transform.position = posicion.transform.position;
            var dragDeLaCarta = cartaInstancia.GetComponent<DragComponent>();
            dragDeLaCarta.Configure(posicion.GetComponent<RectTransform>(), _canvasDeLasCartas.GetComponent<RectTransform>(), transformParameter);
            cartaInstancia.Configurate(_factoriaPersonaje, ReferenciarDrpoComponentALaCarta(cartaInstancia));
            return cartaInstancia;
        }

        private DropComponent ReferenciarDrpoComponentALaCarta(CartaTemplate cartaInstancia)
        {
            return cartaInstancia.Zona switch
            {
                ZonaDeDropeo.TOUCHDOWN => touchDown,
                ZonaDeDropeo.MEDIOCAMPO => medioCampo,
                ZonaDeDropeo.TODOCAMPO => all,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public CartaTemplate CreateEnemigo(string id, GameObject posicion)
        {
            Debug.Log("crea enemigo");
            var cartaTemplate = _cartasConfiguracion.GetCartaTemplate(id);
            var cartaInstancia = Instantiate(cartaTemplate, _canvasDeLasCartas.transform);
            cartaInstancia.transform.position = posicion.transform.position;
            var dragDeLaCarta = cartaInstancia.GetComponent<DragComponent>();
            dragDeLaCarta.Configure(posicion.GetComponent<RectTransform>(), _canvasDeLasCartas.GetComponent<RectTransform>(), new RectTransform());
            cartaInstancia.Configurate(_factoriaPersonaje, null);
            return cartaInstancia;
        }

        public void CrearHeroe(Vector3 point)
        {
            var heroe = _colocacionCartas.GetHeroe();
            ServiceLocator.Instance.GetService<IHeroeInstancie>().InstanciateHero(_factoriaPersonaje, point, heroe);

        }

        public void CrearPrimerasCartas()
        {
            while (_colocacionCartas.PuedoSacarOtraCarta())
            {
                Create(_colocacionCartas.GetNextCartaId(), _colocacionCartas.GetSiguientePosicionDeCarta(), _colocacionCartas.GetPosicionDeUltimaCartaInstanciada(), _transformDondePosicionar);
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

        public void CrearCarta()
        {
            var posicionesSinCartas = _colocacionCartas.ObtenerPocisionesSinCartas();
            if (posicionesSinCartas.Count == 0) return;
            Debug.Log($"colocandoCartaEnPosicion {posicionesSinCartas[0]}");
            Create(_colocacionCartas.GetNextCartaId(), _colocacionCartas.GetPosicionDeCarta(posicionesSinCartas[0]),posicionesSinCartas[0], _transformDondePosicionar);
        }
    }
}