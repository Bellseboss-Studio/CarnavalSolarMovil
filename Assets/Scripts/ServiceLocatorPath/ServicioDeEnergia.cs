using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioDeEnergia : MonoBehaviour, IServicioDeEnergia
    {
        private int _energiaPlayer1;
        public void Init()
        {
            _energiaPlayer1 = 10;
        }

        public bool TieneEnergiaSuficiente(int costoDeEnergia)
        {
            var energiaSuficiente = costoDeEnergia <= _energiaPlayer1;
            if (energiaSuficiente)
            {
                _energiaPlayer1 -= costoDeEnergia;
            }

            return energiaSuficiente;
        }
    }
}