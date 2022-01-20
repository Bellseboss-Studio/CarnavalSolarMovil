using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarPersonajeCurarAliadoMasCercanoAndAutoDestruirse : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            //instanciar proyectil
            var aliadoCercano = _interaccionComponent.GetAliadoMasCercano(_personaje);
            if (aliadoCercano != null)
            {
                aliadoCercano.health += 200;
                aliadoCercano.ActualizarBarraDeVida();
            }
            _personaje.gameObject.SetActive(false);
            _personaje.Muerte();
            Object.Destroy(_personaje.gameObject,2);
        }
    }
}