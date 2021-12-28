using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public interface IHeroeInstancie
    {
        void InstanciateHero(FactoriaPersonaje factoriaPersonaje, Vector3 point);
    }
}