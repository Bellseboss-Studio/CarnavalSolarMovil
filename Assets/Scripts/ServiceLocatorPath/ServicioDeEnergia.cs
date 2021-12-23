using TMPro;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioDeEnergia : MonoBehaviour, IServicioDeEnergia
    {
        [SerializeField] private TextMeshProUGUI textoEnergia;
        [SerializeField] private int energiaASumar;
        private int _energiaPlayer1;
        public void Init()
        {
            //_energiaPlayer1 = 0;
            ActualizarTextoDeEnergia();
        }

        public bool TieneEnergiaSuficiente(int costoDeEnergia)
        {
            var energiaSuficiente = costoDeEnergia <= _energiaPlayer1;
            if (energiaSuficiente)
            {
                _energiaPlayer1 -= costoDeEnergia;
            }
            ActualizarTextoDeEnergia();
            return energiaSuficiente;
        }

        public void AddEnergy()
        {
            _energiaPlayer1 += energiaASumar;
            ActualizarTextoDeEnergia();
        }

        private void ActualizarTextoDeEnergia()
        {
            textoEnergia.text = $"{_energiaPlayer1}";
        }
        
    }
}