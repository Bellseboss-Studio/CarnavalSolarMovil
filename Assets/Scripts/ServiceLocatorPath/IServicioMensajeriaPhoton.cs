using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public interface IServicioMensajeriaPhoton
    {
        bool VerificarSerMasterClient();

        void SincronizarInicioDePartida();
        void CrearHeroe(bool isMasterClient, Vector3 point);
        void SetHeroeEnemigo(SelectorBaraja getBarajaSeleccionadaId);
    }
}