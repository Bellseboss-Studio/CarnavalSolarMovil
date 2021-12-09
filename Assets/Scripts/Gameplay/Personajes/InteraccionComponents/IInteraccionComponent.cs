using System.Collections.Generic;

namespace Gameplay
{
    public interface IInteraccionComponent
    {
        public List<Personaje> GetAliadosCercanos(Personaje origen);
        float ObtenerDistancia(Personaje personaje, Personaje target);
        Personaje GetAliadoConMenosVida(List<Personaje> aliadosCercanos);
        void ReducirVelocidadDeMovimientoYAtaqueAlObjetivo(Personaje target, float i, float tiempoDeEspera);

        public void AplicarDanioProgresivamente(Personaje origen, Personaje target, float danioARealizar, int repeticiones,
            float tiempoDeEspera);
    }
}