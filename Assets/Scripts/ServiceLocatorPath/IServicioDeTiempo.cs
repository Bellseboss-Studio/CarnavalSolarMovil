namespace ServiceLocatorPath
{
    public interface IServicioDeTiempo
    {
        bool EstaPausadoElJuego();
        void ComienzaAContarElTiempo();
        void DejaDeContarElTiempo();
        bool EstanJugando();
    }
}