using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DispararProyectilesQueDanian : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            //Disparamos el proyectil
            foreach (var personaje in _personaje.GetTargetComponent().GetTargets())
            {
                ServiceLocator.Instance.GetService<IInstanciadoDeCosasConfiguradas>().InstanciaSinCartaConTarget(
                    "ProyectilDeDisparadorAsesino", _personaje.GetCharacterPosition(), _personaje.enemigo, personaje);
            }
        }
    }
}