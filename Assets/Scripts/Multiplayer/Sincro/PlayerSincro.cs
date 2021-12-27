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
   public OnGuardarInformacionDelOtro onGuardarInformacionDelOtro;
   public bool _EsOtroPlayer;
   public string unoN;
   public string dosN;
   public string tresN;
   public string informacion;
   private string _danioDelOtroPlayer;

   public bool HayData()
   {
      //Debug.Log($"Datos a entregar soy yo? {pv.IsMine} ({_danioDelOtroPlayer})");
      return !string.IsNullOrEmpty(_danioDelOtroPlayer);
   }
   public string GetDanioDelOtroPlayer()
   {
      var aux = _danioDelOtroPlayer;
      _danioDelOtroPlayer = null;
      //Debug.Log($"entregando ({aux}) soy yo? {pv.IsMine}");
      return aux;
   }
   public delegate void OnLoadMyPj(string uno, string dos, string tres);

   public delegate void OnGuardarInformacionDelOtro(string jsonFormat);

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
            //Debug.Log($"recivido {pv.IsMine}"); 
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
   public void PublicandoPersonajesElejidos(string uno, string dos, string tres, string playerInJson)
   {
      if (!pv.IsMine)
      {
         Debug.Log($"recibo {uno} {dos} {tres}");
         unoN = uno;
         dosN = dos;
         tresN = tres;
         _EsOtroPlayer = true;
         //Debug.Log($">>>>>>recibo jsonFormat {playerInJson}");
         informacion = playerInJson;
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

   public void ConfigurarPersonajes(List<Personaje> personajes, string player)
   {
      pv.RPC("PublicandoPersonajesElejidos",RpcTarget.AllBuffered, personajes[0].nombre, personajes[1].nombre, personajes[2].nombre, player);
   }

   [PunRPC]
   public void CompartirInformacionDeTodoLoMio(string jsonFormat)
   {
      if (!pv.IsMine)
      {
         Debug.Log($"recibo jsonFormat");
         informacion = jsonFormat;
      }
   }

   [PunRPC]
   public void DanandoPersonaje(string jsonDeDatosDeDanio)
   {
      if (!pv.IsMine)
      {
         Debug.Log($"<<<<Viene danio de otro lado {jsonDeDatosDeDanio}");
         _danioDelOtroPlayer = jsonDeDatosDeDanio;
         onGuardarInformacionDelOtro?.Invoke(jsonDeDatosDeDanio);
      }  
   }
   public void CompartirInformacion(string jsonFormat)
   {
      pv.RPC("CompartirInformacionDeTodoLoMio", RpcTarget.AllBuffered, jsonFormat);
   }

   public void DanarOponente(string stringDeDanio)
   {
      Debug.Log($"Datos enviados {stringDeDanio}");
      pv.RPC("DanandoPersonaje", RpcTarget.AllBuffered, stringDeDanio);
   }
}