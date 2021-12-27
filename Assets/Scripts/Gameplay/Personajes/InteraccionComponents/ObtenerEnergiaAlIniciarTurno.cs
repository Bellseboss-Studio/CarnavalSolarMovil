using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class ObtenerEnergiaAlIniciarTurno : InteraccionBehaviour
    {
        public override void InteraccionAlIniciarTurno (Personaje origen)
        {
            ServiceLocator.Instance.GetService<IServicioDeEnergia>().AddQuantityOfEnergy(3);
        }
    }
}