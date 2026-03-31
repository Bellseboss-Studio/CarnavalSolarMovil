using System.Linq;
using UnityEngine;

namespace V2.Unidad
{
    /// <summary>
    /// Implementación simple de IEnemyTargetFinder que simplemente retorna el target actual de la unidad.
    /// </summary>
    public class TargetEnemyFinder : IEnemyTargetFinder
    {
        public Transform BuscarEnemigoCercano(UnitMediator unidad)
        {
            var enemigo = Object
                .FindObjectsByType<UnitMediator>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID)
                .FirstOrDefault(u => u != unidad && u.HealthController.GetCurrentHp() > 0);
            return enemigo?.transform;
        }
    }
}