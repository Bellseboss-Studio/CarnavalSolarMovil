
using UnityEngine;

namespace ServiceLocatorPath
{
    public interface IInstanciadoDeCosasConfiguradas
    {
        void InstanciaSinCarta(string carta, Vector3 point, bool personajeEnemigo);
        void InstanciaSinCartaConTarget(string carta, Vector3 point, bool personajeEnemigo, Gameplay.Personaje target);
    }
}