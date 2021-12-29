using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DispararProyectilQueCuraYDania : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            //Disparamos el proyectil
            ServiceLocator.Instance.GetService<IInstanciadoDeCosasConfiguradas>().InstanciaSinCarta("proyectilDanioEnemigoMasCercano",_personaje.GetCharacterPosition());
        }
    }
}