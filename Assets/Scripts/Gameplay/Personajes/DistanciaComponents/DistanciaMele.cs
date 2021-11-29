namespace Gameplay.Personajes.DistanciaComponents
{
    public class DistanciaMele : DistanciaComponent
    {
        public override float GetDistanciaDeAtaque()
        {
            return distanciaDeAtaque;
        }

        public override void SetDistanciaDeAtaque(float distancia)
        {
            distanciaDeAtaque = distancia;
        }
    }
}