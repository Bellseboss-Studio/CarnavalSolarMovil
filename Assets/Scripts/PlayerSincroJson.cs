using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSincroJson
{
  public string playerId;
  public List<PersonajeJson> personajesJson;
    public PlayerSincroJson(Player player)
    {
      playerId = SystemInfo.deviceUniqueIdentifier;
      personajesJson = new List<PersonajeJson>()
      {
        new PersonajeJson()
        {
          nombre = player.GetPersonajes()[0].nombre,
          vida = player.GetPersonajes()[0].vida
        },
        new PersonajeJson()
        {
          nombre = player.GetPersonajes()[1].nombre,
          vida = player.GetPersonajes()[1].vida
        },
        new PersonajeJson()
        {
          nombre = player.GetPersonajes()[2].nombre,
          vida = player.GetPersonajes()[2].vida
        }
      };
    }

    public PlayerSincroJson()
    {
    }
}

/*
{"playerId":"una id unica",
  "personajes":[
    {
      "nombre": "nombre",
      "vida":100
    },
    {
      "nombre": "nombre2",
      "vida":100
    },
    {
      "nombre": "nombre3",
      "vida":100
    }
  ]
}
*/