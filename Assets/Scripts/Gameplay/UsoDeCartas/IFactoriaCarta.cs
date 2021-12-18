using Gameplay.NewGameStates;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public interface IFactoriaCarta
    {
        void Configurate(IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas);

        CartaTemplate Create(string id, Vector3 posicion);
        void CrearPrimerasCartas();
    }
}