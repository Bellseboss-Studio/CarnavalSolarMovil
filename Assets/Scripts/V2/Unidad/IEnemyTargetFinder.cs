using UnityEngine;

namespace V2.Unidad
{
    /// <summary>
    /// Interfaz para la lógica de búsqueda de enemigos.
    /// </summary>
    public interface IEnemyTargetFinder
    {
        /// <summary>
        /// Devuelve el enemigo más cercano a la unidad dada, o null si no hay ninguno.
        /// </summary>
        Transform BuscarEnemigoCercano(UnitMediator unidad);
    }
}

