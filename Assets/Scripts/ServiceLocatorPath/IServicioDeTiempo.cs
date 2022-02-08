namespace ServiceLocatorPath
{
    public interface IServicioDeTiempo
    {
        bool EstaPausadoElJuego();
        void ComienzaAContarElTiempo(int queTiempoEstoyContando);
        void DejaDeContarElTiempo();
        bool EstanJugando();
        bool SeEstaColocandoElHeroe();
        void AniadirCantidadDeEnergiaAlSiguienteTurno(int entergiaASumar);
        void DescongelarPersonajes();
    }
}