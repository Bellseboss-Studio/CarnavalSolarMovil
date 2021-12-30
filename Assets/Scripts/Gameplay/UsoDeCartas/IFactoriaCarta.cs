using Gameplay.NewGameStates;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public interface IFactoriaCarta
    {
        void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje, GameObject canvasPrincipal, Transform transformDondePosicionar);

        CartaTemplate Create(string id, GameObject posicion, int posicionEnBaraja, Transform transformParameter);
        void CrearPrimerasCartas();
        void DestruirLasCartas();
        void CrearCartasEnHuecos();
        CartaTemplate CreateEnemigo(string carta, GameObject gameObject);
        void CrearHeroe(Vector3 point);
    }
}