namespace Gameplay.Personajes.InteraccionComponents
{
    public class ObtenerInmunidadAlIniciarTurno : InteraccionBehaviour
    {
        public override void InteraccionAlIniciarTurno (Personaje origen)
        {
            _interaccionComponent.ObtenerInmunidadPorSegundos(origen, 4);
        }
    }
}