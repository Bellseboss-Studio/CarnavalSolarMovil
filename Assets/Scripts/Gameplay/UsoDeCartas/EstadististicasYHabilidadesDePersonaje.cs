namespace Gameplay.UsoDeCartas
{
    public class EstadististicasYHabilidadesDePersonaje
    {
        public string idModelo3D;
        public string idTargetComponent;
        public string idInteraccionComponent;
        public string idRutaComponent;
        public EstadisticasCarta EstadisticasCarta;

        public EstadististicasYHabilidadesDePersonaje(string idModelo3D, string idTargetComponent, string idInteraccionComponent, string idRutaComponent, float distanciaDeInteraccion, float health, float velocidadDeInteraccion, float velocidadDeMovimiento, float damage, float escudo)
        {
            this.idModelo3D = idModelo3D;
            this.idTargetComponent = idTargetComponent;
            this.idInteraccionComponent = idInteraccionComponent;
            this.idRutaComponent = idRutaComponent;
            EstadisticasCarta = new EstadisticasCarta(distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento, damage, escudo);
        }
    }
}