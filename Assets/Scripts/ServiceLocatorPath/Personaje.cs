using System;
using Rest;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class Personaje
    {
        public Personaje(float ataque, float defensa, float velocidad, string imagen, string nombre, string type, float vida, float cooldown)
        {
            this.ataque = ataque;
            this.defensa = defensa;
            this.velocidad = velocidad;
            this.imagen = imagen;
            this.nombre = nombre;
            this.type = type;
            this.vida = vida;
            this.cooldown = cooldown;
        }

        public Personaje()
        {
        }

        public float ataque, defensa, velocidad, vida, cooldown;

        public string imagen, nombre, type;
        /*
         {"ataque":"1","defensa":"1","velocidad":"1","imagen":"url","type":"llorona","nombre":"llorona"}
         */
        public string GetSprite()
        {
            return imagen;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public float GetAtaque()
        {
            return ataque;
        }

        public float GetDefensa()
        {
            return defensa;
        }

        public float GetVelocidad()
        {
            return velocidad;
        }

        public float GetVida()
        {
            return vida;
        }
    }
}