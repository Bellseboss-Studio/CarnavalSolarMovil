using ServiceLocatorPath;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class AtacarYCurarAliadoMasCercanoAlImpacto : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            //instanciar proyectil
            var aliadoCercano = _interaccionComponent.GetAliadoMasCercano(_personaje);
            if (aliadoCercano != null)
            {
                aliadoCercano.health += 120;
            }
            //Debug.Log(target.health);
        }
    }
}