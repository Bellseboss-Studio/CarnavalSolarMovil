using System;
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

        public void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas)
        {
            _colocacionCartas = colocacionCartas;
            _cartasConfiguracion = cartasConfiguracion;
            _canvasDeLasCartas = canvasDeLasCartas;
        }

        public CartaTemplate Create(string id, Vector3 posicion)
        {
            var cartaTemplate = _cartasConfiguracion.GetCartaTemplate(id);
            var cartaInstancia = Instantiate(cartaTemplate, _canvasDeLasCartas.transform);
            cartaInstancia.transform.position = posicion;
            return cartaInstancia;
        }

        public void CrearPrimerasCartas()
        {
            var posiciones = _colocacionCartas.GetPosicionesDeCartas();
            for (int i = 0; i < _colocacionCartas.GetNumeroDePosiciones(); i++)
            {
                Create("Llorona", posiciones[i].transform.position);
            }
        }
    }
}