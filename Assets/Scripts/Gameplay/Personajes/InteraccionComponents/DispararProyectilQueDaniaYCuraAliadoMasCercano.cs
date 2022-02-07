using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DispararProyectilQueDaniaYCuraAliadoMasCercano : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            //Disparamos el proyectil
            ServiceLocator.Instance.GetService<IInstanciadoDeCosasConfiguradas>().InstanciaSinCarta("ProyectilDeCuranderoLuchador",_personaje.GetCharacterPosition(), _personaje.enemigo);
        }
    }
}