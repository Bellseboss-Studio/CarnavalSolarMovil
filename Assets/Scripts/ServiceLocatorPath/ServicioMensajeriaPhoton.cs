using System;
using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using Photon.Pun;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioMensajeriaPhoton :MonoBehaviourPunCallbacks, IServicioMensajeriaPhoton
    {
        [SerializeField] private MediadorDeEstadosDelJuego gameManager;
        public bool VerificarSerMasterClient()
        {
            return PhotonNetwork.IsMasterClient;
        }

        public void SincronizarInicioDePartida()
        {
            photonView.RPC("SincronizarJugadores", RpcTarget.All);
        }

        public void CrearHeroe(bool isMasterClient, Vector3 point)
        {
            photonView.RPC("CrearHeroeRPC", RpcTarget.All, isMasterClient, point);
        }

        public void SetHeroeEnemigo(SelectorBaraja getBarajaSeleccionadaId)
        {
            Debug.Log(getBarajaSeleccionadaId);
            photonView.RPC("SetHeroeEnemigoRPC", RpcTarget.Others, getBarajaSeleccionadaId.IDHeroe);
        }

        public void CrearPersonaje(Vector3 hitPoint, string id)
        {
            photonView.RPC("CrearPersonajeRPC", RpcTarget.All, hitPoint, id, gameManager.IsMasterClient());
        }

        [PunRPC]
        private void SincronizarJugadores()
        {
            gameManager.SincronizarJugador();
            gameManager.SetIsMasterClient(PhotonNetwork.IsMasterClient);
        }

        [PunRPC]
        private void CrearHeroeRPC(bool isMasterClient, Vector3 point)
        {
            if (gameManager.IsMasterClient() == isMasterClient)
            {
                gameManager.FactoriaCarta.CrearHeroe(point);
            }
            else
            {
                gameManager.FactoriaCarta.CrearHeroeEnemigo(new Vector3(point.x * -1, point.y, point.z * -1));
                gameManager.SeColocoElHeroeEnemigo = true;
            }
        }

        [PunRPC]
        private void SetHeroeEnemigoRPC(string idHeroe)
        {
            ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().SetBarajaEnemigaSeleccionadaId(idHeroe);
        }

        [PunRPC]
        private void CrearPersonajeRPC(Vector3 hitPoint, string id, bool isMasterClient)
        {
            if (isMasterClient == gameManager.IsMasterClient())
            {
                ServiceLocator.Instance.GetService<IEnemyInstantiate>().InstanciateCharacter(id, hitPoint, false);
            }
            else
            {
                hitPoint = new Vector3(hitPoint.x * -1, hitPoint.y, hitPoint.z * -1);
                ServiceLocator.Instance.GetService<IEnemyInstantiate>().InstanciateCharacter(id, hitPoint, true);
            }
        }
        
    }
}