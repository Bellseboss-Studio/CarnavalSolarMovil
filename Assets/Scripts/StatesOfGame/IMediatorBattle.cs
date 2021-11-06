namespace StatesOfEnemies
{
    public interface IMediatorBattle
    {
        bool OncePlayersIsDead();
        void ConfigurePlayers();
        void MuestraLaUiDeBatalla();
    }
}