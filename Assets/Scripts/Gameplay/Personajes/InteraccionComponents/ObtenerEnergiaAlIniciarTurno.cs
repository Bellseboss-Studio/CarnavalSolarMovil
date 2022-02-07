using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class ObtenerEnergiaAlIniciarTurno : InteraccionBehaviour
    {
        public override void InteraccionAlIniciarTurno (Personaje origen)
        {
            if(_personaje.enemigo) return;
            ServiceLocator.Instance.GetService<IServicioDeEnergia>().AddQuantityOfEnergyInTheNextTurn(3);
        }
    }
}