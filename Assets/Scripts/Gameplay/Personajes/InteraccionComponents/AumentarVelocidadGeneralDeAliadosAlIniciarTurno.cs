using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class AumentarVelocidadGeneralDeAliadosAlIniciarTurno : InteraccionBehaviour
    {
        public override void InteraccionAlIniciarTurno (Personaje origen)
        {
            var aliadosCercanos = _interaccionComponent.GetAliadosCercanos(_personaje);
            foreach (var aliadoCercano in aliadosCercanos)
            {
                _interaccionComponent.AumentarVelocidadMovimientoPersonajePorSegundos(aliadoCercano, 40, 5);
                _interaccionComponent.DisminuirVelocidadInteraciconPersonajePorSegundos(aliadoCercano, 40, 5);
            }
        }
    }
}