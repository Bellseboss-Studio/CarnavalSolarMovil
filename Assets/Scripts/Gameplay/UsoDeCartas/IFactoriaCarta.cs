using Gameplay.NewGameStates;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public interface IFactoriaCarta
    {
        void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje, GameObject canvasPrincipal);

        CartaTemplate Create(string id, GameObject posicion, int posicionEnBaraja);
        void CrearPrimerasCartas();
        void DestruirLasCartas();
        void CrearCartasEnHuecos();
        CartaTemplate CreateEnemigo(string carta, GameObject gameObject);
        void CrearHeroe(Vector3 point);
    }
}