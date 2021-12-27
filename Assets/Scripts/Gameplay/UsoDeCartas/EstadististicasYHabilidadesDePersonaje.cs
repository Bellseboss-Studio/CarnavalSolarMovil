using UnityEngine;
namespace Gameplay.UsoDeCartas
{
    public class EstadististicasYHabilidadesDePersonaje
    {
        public string idModelo3D;
        public TargetComponentEnum idTargetComponent;
        public InteraccionComponentEnum idInteraccionComponent;
        public RutaComponentEnum idRutaComponent;
        public EstadisticasCarta EstadisticasCarta;

        public EstadististicasYHabilidadesDePersonaje(string idModelo3D, TargetComponentEnum idTargetComponent, InteraccionComponentEnum idInteraccionComponent,
            RutaComponentEnum idRutaComponent, float distanciaDeInteraccion, float health, float velocidadDeInteraccion, float velocidadDeMovimiento, float damage, float escudo, AnimationClip caminar,
            AnimationClip golpear, AnimationClip morir, AnimationClip idle)
        {
            this.idModelo3D = idModelo3D;
            this.idTargetComponent = idTargetComponent;
            this.idInteraccionComponent = idInteraccionComponent;
            this.idRutaComponent = idRutaComponent;
            EstadisticasCarta = new EstadisticasCarta(distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento, damage, escudo, caminar, golpear, morir, idle);
        }
    }
}