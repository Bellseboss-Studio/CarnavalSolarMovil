using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarPersonajeCurarAliadoCercanoMasDaniadoAndAutoDestruirse : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            var aliadosCercanos = _interaccionComponent.GetAliadosCercanos(_personaje);
            var aliadoCercanoConMenosVida = _interaccionComponent.GetAliadoConMenosVida(aliadosCercanos);
            aliadoCercanoConMenosVida.health += 100;
            aliadoCercanoConMenosVida.ActualizarBarraDeVida();
            _personaje.gameObject.SetActive(false);
            _personaje.Muerte();
            Object.Destroy(_personaje.gameObject,2);
        }
    }
}