using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Gameplay.UsoDeCartas;
using UnityEngine;

public class ConsumidorDeFactoriaDePersonajes : MonoBehaviour
{
    [SerializeField] private FactoriaPersonaje factoria;
    [SerializeField] private AnimationClip anims;

    public void CrearPersonaje(string cual)
    {
        var personaje = factoria.CreatePersonaje(Vector3.one,new EstadististicasYHabilidadesDePersonaje(cual,TargetComponentEnum.BuscarEnemigoMasCercano,InteraccionComponentEnum.DaniarPersonaje,RutaComponentEnum.RutaMasCorta,3,1,1,1,1,1,anims,anims,anims,anims));
        
    }
}
