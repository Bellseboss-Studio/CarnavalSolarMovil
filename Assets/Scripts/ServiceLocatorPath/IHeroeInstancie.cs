using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public interface IHeroeInstancie
    {
        void InstanciateHero(FactoriaPersonaje factoriaPersonaje, Vector3 point, string cualHeroe);
        void InstanciateHero(FactoriaPersonaje factoriaPersonaje, Vector3 point, string cualHeroe, bool enemigo = false);
    }
}