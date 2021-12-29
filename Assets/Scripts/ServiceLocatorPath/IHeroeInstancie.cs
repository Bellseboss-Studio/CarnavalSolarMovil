using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public interface IHeroeInstancie
    {
        void InstanciateHero(IFactoriaPersonajes factoriaPersonaje, Vector3 point, string cualHeroe);
        void InstanciateHero(IFactoriaPersonajes factoriaPersonaje, Vector3 point, string cualHeroe, bool enemigo = false);
    }
}