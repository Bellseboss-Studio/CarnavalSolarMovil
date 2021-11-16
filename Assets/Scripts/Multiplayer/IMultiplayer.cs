public interface IMultiplayer
{
    bool EstaListo();
    void CrearSala(string nombreSala);
    void UnirseSala(string nombreSala);
    bool TerminoDeProcesarLaSala();
    bool FalloAlgo();
    void ResetFlags();
    string CantidadDePersonasEnSala();

    PlayerSincro CrearPersonaje(PlayerSincro.OnLoadMyPj ownPj);
}