namespace StatesOfEnemies
{
    public interface IMediatorDeEspera
    {
        void SincronizaJugadores();
        bool EstanLosJugadoresSincronizados();
        float ColocarTemporalizador();
        void BuscarNuevosPlayers();
        void CompartirInformacion();
        bool TenemosTodosLosDatosDeLosPersonajes();
        void EnviaInformacionQueModifiqueElOtroPlayer();
    }
}