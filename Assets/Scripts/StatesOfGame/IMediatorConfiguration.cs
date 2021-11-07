namespace StatesOfEnemies
{
    public interface IMediatorConfiguration
    {
        void ShowLoad();
        void HideLoad();
        void MostrarElLetreroDeGanarOPerder();
        bool EligioLoQueQuiereHacer();
        bool QuiereHacerOtraBatalla();
        void ReiniciaTodosLosEstados();
        void OcultarElLetreroDeGanarOPerder();
    }
}