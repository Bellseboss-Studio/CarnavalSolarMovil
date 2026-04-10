using TMPro;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioDeEnergia : MonoBehaviour, IServicioDeEnergia
    {
        [SerializeField] private TextMeshProUGUI textoEnergia;
        [SerializeField] private int energiaPorTurno;
        private int _energiaPlayer1;
        private int _energiaPlayer2;
        public void Init()
        {
            _energiaPlayer1 = 5;
            _energiaPlayer2 = 5;
            //energiaPorTurno = 3;
            ActualizarTextoDeEnergia();
            //textoEnergia.text = "8";
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
        
        public bool TieneEnergiaSuficienteP2(int costoDeEnergia)
        {
            var energiaSuficiente = costoDeEnergia <= _energiaPlayer2;
            if (energiaSuficiente)
            {
                _energiaPlayer2 -= costoDeEnergia;
            }
            return energiaSuficiente;
        }

        public void AddEnergy()
        {
            _energiaPlayer1 += energiaPorTurno;
            energiaPorTurno++;
            ActualizarTextoDeEnergia();
        }
        
        public void AddEnergyP2()
        {
            _energiaPlayer2 += energiaPorTurno;
            //ActualizarTextoDeEnergia();
            Debug.Log($"_energiaPlayer2 {_energiaPlayer2}");
        }

        public void AddQuantityOfEnergyInTheNextTurn(int entergiaASumar)
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>()
                .AniadirCantidadDeEnergiaAlSiguienteTurno(entergiaASumar);
            _energiaPlayer1 += entergiaASumar;
            ActualizarTextoDeEnergia();
        }
        
        public void AddQuantityOfEnergyInTheNextTurnP2(int entergiaASumar)
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>()
                .AniadirCantidadDeEnergiaAlSiguienteTurno(entergiaASumar);
            _energiaPlayer2 += entergiaASumar;
            ActualizarTextoDeEnergia();
        }

        private void ActualizarTextoDeEnergia()
        {
            textoEnergia.text = $"{_energiaPlayer1}";
        }
        
    }
}