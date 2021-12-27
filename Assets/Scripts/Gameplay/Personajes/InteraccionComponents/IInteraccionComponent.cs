using System.Collections.Generic;
using Gameplay.Personajes.InteraccionComponents;

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

        void AumentarVelocidadMovimientoPorCincoSegundos(Personaje personaje, DaniarYAumentarVelocidadAlMatar interaccionComponent);
        void AumentarVelocidadMovimientoPersonajePorSegundos(Personaje aliadoCercano, float porcentaje, float tiempo);
        void DisminuirVelocidadInteraciconPersonajePorSegundos(Personaje personaje, float porcentaje, float tiempo);
        void AumentarArmaduraPorSegundos(Personaje origen, float porcentajeAAumentar, float tiempo);
        void ObtenerInmunidadPorSegundos(Personaje origen, float tiempo);
    }
}