public interface IMediatorGeneral
{
    void ShowStore();
    bool TerminoDeElegir { get; }
    void HideStore();
    void ConfiguraElSegundoPlayer();
    bool ElegidoSiCrearUnise();
    void MuestraLasOpcionesParaElJugador();
    bool EligioCrear();
    string GetNombreDeSala();
    void OcultarOpcionesAlJugador();
    void MostrarUnPanelDeCarga();
    void OcultarPanelDeCarga();
    void ResetLaParteDeUnirseCrearSala();
}