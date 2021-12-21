using Gameplay.NewGameStates;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public interface IFactoriaCarta
    {
        void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje, GameObject canvasPrincipal);

        CartaTemplate Create(string id, GameObject posicion);
        void CrearPrimerasCartas();
        void DestruirLasCartas();
    }
}