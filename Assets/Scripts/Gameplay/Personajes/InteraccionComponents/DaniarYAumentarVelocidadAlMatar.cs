namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarYAumentarVelocidadAlMatar : InteraccionBehaviour
    {
        public bool YaAumenteMiVelocidad { get; set; }

        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            if (target.health <= 0)
            {
                _interaccionComponent.AumentarVelocidadMovimientoPorCincoSegundos(_personaje, this);
            }
        }
    }
}