
using UnityEngine;

namespace ServiceLocatorPath
{
    public interface IInstanciadoDeCosasConfiguradas
    {
        void InstanciaSinCarta(string carta, Vector3 point, bool personajeEnemigo);
    }
}