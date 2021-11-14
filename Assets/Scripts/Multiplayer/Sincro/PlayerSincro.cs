using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using ServiceLocatorPath;
using UnityEngine;

public class PlayerSincro : MonoBehaviourPun, IPunObservable
{
   [SerializeField] private PhotonView pv;
   public OnLoadMyPj OnLoadMyOwnPj;
   public bool _EsOtroPlayer;
   public string unoN;
   public string dosN;
   public string tresN;
   public string campoParSincronizar;

   public delegate void OnLoadMyPj(string uno, string dos, string tres);

   private void Start()
   {
      DontDestroyOnLoad(gameObject);
   }

   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   {
      if (stream.IsWriting)
      {
         stream.SendNext("Hola a todos");
      }
      else
      { 
         if (!pv.IsMine)
         {
            Debug.Log($"recivido {pv.IsMine}"); 
            Debug.Log((string)stream.ReceiveNext());  
         }
      }
   }

   [PunRPC]
   public void Saludar()
   {
      Debug.Log($"Hola a todos {pv.IsMine}");
   }

   [PunRPC]
   public void PublicandoPersonajesElejidos(string uno, string dos, string tres)
   {
      if (!pv.IsMine)
      {
         Debug.Log($"recibo {uno} {dos} {tres}");
         unoN = uno;
         dosN = dos;
         tresN = tres;
         _EsOtroPlayer = true;
      }
      else
      {
         OnLoadMyOwnPj?.Invoke(uno, dos, tres);
      }
   }

   public void Configuralo()
   {
      pv.RPC("Saludar",RpcTarget.AllBuffered);
   }

   public bool IsMine()
   {
      return pv.IsMine;
   }

   public void ConfigurarPersonajes(List<Personaje> personajes)
   {
      pv.RPC("PublicandoPersonajesElejidos",RpcTarget.AllBuffered, personajes[0].nombre, personajes[1].nombre, personajes[2].nombre);
   }

   private void Update()
   {
      campoParSincronizar = $"{Time.realtimeSinceStartup}";
   }
}
