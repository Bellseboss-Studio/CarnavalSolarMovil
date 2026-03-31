using System;
using UnityEngine;

namespace V2.Unidad
{
    public class UnitStatsController : MonoBehaviour
    {
        [SerializeField] private UnitStatsData statsSoInstance;

        [Header("Configuración de stats")] [SerializeField]
        private UnitStatsSO statsSo;

        private void Start()
        {
            Configure();
        }

        private void Configure()
        {
            statsSoInstance = new UnitStatsData(statsSo);
        }

        public UnitStatsData GetStat()
        {
            return statsSoInstance;
        }
    }
}