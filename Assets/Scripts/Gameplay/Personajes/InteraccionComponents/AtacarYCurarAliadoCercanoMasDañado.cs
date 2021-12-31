using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class AtacarYCurarAliadoCercanoMasDañado : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            var aliadosCercanos = _interaccionComponent.GetAliadosCercanos(_personaje);
            var aliadoCercanoConMenosVida = _interaccionComponent.GetAliadoConMenosVida(aliadosCercanos);
            aliadoCercanoConMenosVida.health += 80;
            aliadoCercanoConMenosVida.ActualizarBarraDeVida();
            //Debug.Log(target.health);
        }
    }
}